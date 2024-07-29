/*
   Licensed to the Apache Software Foundation (ASF) under one or more
   contributor license agreements.  See the NOTICE file distributed with
   this work for additional information regarding copyright ownership.
   The ASF licenses this file to You under the Apache License, Version 2.0
   (the "License"); you may not use this file except in compliance with
   the License.  You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/
var showControllersOnly = false;
var seriesFilter = "";
var filtersOnlySampleSeries = true;

/*
 * Add header in statistics table to group metrics by category
 * format
 *
 */
function summaryTableHeader(header) {
    var newRow = header.insertRow(-1);
    newRow.className = "tablesorter-no-sort";
    var cell = document.createElement('th');
    cell.setAttribute("data-sorter", false);
    cell.colSpan = 1;
    cell.innerHTML = "Requests";
    newRow.appendChild(cell);

    cell = document.createElement('th');
    cell.setAttribute("data-sorter", false);
    cell.colSpan = 3;
    cell.innerHTML = "Executions";
    newRow.appendChild(cell);

    cell = document.createElement('th');
    cell.setAttribute("data-sorter", false);
    cell.colSpan = 7;
    cell.innerHTML = "Response Times (ms)";
    newRow.appendChild(cell);

    cell = document.createElement('th');
    cell.setAttribute("data-sorter", false);
    cell.colSpan = 1;
    cell.innerHTML = "Throughput";
    newRow.appendChild(cell);

    cell = document.createElement('th');
    cell.setAttribute("data-sorter", false);
    cell.colSpan = 2;
    cell.innerHTML = "Network (KB/sec)";
    newRow.appendChild(cell);
}

/*
 * Populates the table identified by id parameter with the specified data and
 * format
 *
 */
function createTable(table, info, formatter, defaultSorts, seriesIndex, headerCreator) {
    var tableRef = table[0];

    // Create header and populate it with data.titles array
    var header = tableRef.createTHead();

    // Call callback is available
    if(headerCreator) {
        headerCreator(header);
    }

    var newRow = header.insertRow(-1);
    for (var index = 0; index < info.titles.length; index++) {
        var cell = document.createElement('th');
        cell.innerHTML = info.titles[index];
        newRow.appendChild(cell);
    }

    var tBody;

    // Create overall body if defined
    if(info.overall){
        tBody = document.createElement('tbody');
        tBody.className = "tablesorter-no-sort";
        tableRef.appendChild(tBody);
        var newRow = tBody.insertRow(-1);
        var data = info.overall.data;
        for(var index=0;index < data.length; index++){
            var cell = newRow.insertCell(-1);
            cell.innerHTML = formatter ? formatter(index, data[index]): data[index];
        }
    }

    // Create regular body
    tBody = document.createElement('tbody');
    tableRef.appendChild(tBody);

    var regexp;
    if(seriesFilter) {
        regexp = new RegExp(seriesFilter, 'i');
    }
    // Populate body with data.items array
    for(var index=0; index < info.items.length; index++){
        var item = info.items[index];
        if((!regexp || filtersOnlySampleSeries && !info.supportsControllersDiscrimination || regexp.test(item.data[seriesIndex]))
                &&
                (!showControllersOnly || !info.supportsControllersDiscrimination || item.isController)){
            if(item.data.length > 0) {
                var newRow = tBody.insertRow(-1);
                for(var col=0; col < item.data.length; col++){
                    var cell = newRow.insertCell(-1);
                    cell.innerHTML = formatter ? formatter(col, item.data[col]) : item.data[col];
                }
            }
        }
    }

    // Add support of columns sort
    table.tablesorter({sortList : defaultSorts});
}

$(document).ready(function() {

    // Customize table sorter default options
    $.extend( $.tablesorter.defaults, {
        theme: 'blue',
        cssInfoBlock: "tablesorter-no-sort",
        widthFixed: true,
        widgets: ['zebra']
    });

    var data = {"OkPercent": 100.0, "KoPercent": 0.0};
    var dataset = [
        {
            "label" : "FAIL",
            "data" : data.KoPercent,
            "color" : "#FF6347"
        },
        {
            "label" : "PASS",
            "data" : data.OkPercent,
            "color" : "#9ACD32"
        }];
    $.plot($("#flot-requests-summary"), dataset, {
        series : {
            pie : {
                show : true,
                radius : 1,
                label : {
                    show : true,
                    radius : 3 / 4,
                    formatter : function(label, series) {
                        return '<div style="font-size:8pt;text-align:center;padding:2px;color:white;">'
                            + label
                            + '<br/>'
                            + Math.round10(series.percent, -2)
                            + '%</div>';
                    },
                    background : {
                        opacity : 0.5,
                        color : '#000'
                    }
                }
            }
        },
        legend : {
            show : true
        }
    });

    // Creates APDEX table
    createTable($("#apdexTable"), {"supportsControllersDiscrimination": true, "overall": {"data": [0.07205882352941176, 500, 1500, "Total"], "isController": false}, "titles": ["Apdex", "T (Toleration threshold)", "F (Frustration threshold)", "Label"], "items": [{"data": [0.08, 500, 1500, "GetAllAdminWith50RequestsTest"], "isController": false}, {"data": [0.0, 500, 1500, "GetAdminWith50RequestsTest"], "isController": false}, {"data": [0.11, 500, 1500, "GetStudentControllerWith50RequestsTest"], "isController": false}, {"data": [0.0, 500, 1500, "GetStudentCourseControllerWith10RequestsTest"], "isController": false}, {"data": [0.0, 500, 1500, "GetExternalServiceStudentCourseWith50RequestsTest"], "isController": false}, {"data": [0.42, 500, 1500, "GetAllStudentControllerWith25RequestsTest"], "isController": false}, {"data": [0.0, 500, 1500, "GetAdminWith10RequestsTest"], "isController": false}, {"data": [0.0, 500, 1500, "GetStudentCourseWith50RequestsTest"], "isController": false}, {"data": [0.08, 500, 1500, "GetStudentControllerWith25RequestsTest"], "isController": false}, {"data": [0.0, 500, 1500, "GetStudentWith10RequestsTest"], "isController": false}, {"data": [0.0, 500, 1500, "GetStudentCourseWith25RequestsTest"], "isController": false}, {"data": [0.0, 500, 1500, "GetExternalServiceStudentCourseWith25RequestsTest"], "isController": false}, {"data": [0.39, 500, 1500, "GetAllStudentControllerWith50RequestsTest"], "isController": false}, {"data": [0.06, 500, 1500, "GetAllAdminWith25RequestsTest"], "isController": false}, {"data": [0.0, 500, 1500, "GetStudentWith25RequestsTest"], "isController": false}, {"data": [0.0, 500, 1500, "GetStudentCourseControllerWith25RequestsTest"], "isController": false}, {"data": [0.0, 500, 1500, "GetStudentCourseWith10RequestsTest"], "isController": false}, {"data": [0.05, 500, 1500, "GetAllAdminWith10RequestsTest"], "isController": false}, {"data": [0.0, 500, 1500, "GetStudentWith50RequestsTest"], "isController": false}, {"data": [0.45, 500, 1500, "GetAllStudentControllerWith10RequestsTest"], "isController": false}, {"data": [0.0, 500, 1500, "GetExternalServiceStudentCourseWith10RequestsTest"], "isController": false}, {"data": [0.1, 500, 1500, "GetStudentControllerWith10RequestsTest"], "isController": false}, {"data": [0.0, 500, 1500, "GetStudentCourseControllerWith50RequestsTest"], "isController": false}, {"data": [0.0, 500, 1500, "GetAdminWith25RequestsTest"], "isController": false}]}, function(index, item){
        switch(index){
            case 0:
                item = item.toFixed(3);
                break;
            case 1:
            case 2:
                item = formatDuration(item);
                break;
        }
        return item;
    }, [[0, 0]], 3);

    // Create statistics table
    createTable($("#statisticsTable"), {"supportsControllersDiscrimination": true, "overall": {"data": ["Total", 680, 0, 0.0, 2456.607352941174, 1306, 5396, 2177.0, 3891.999999999999, 4198.299999999999, 5329.5199999999995, 31.369654472482356, 15.566569333279514, 4.196916662822346], "isController": false}, "titles": ["Label", "#Samples", "FAIL", "Error %", "Average", "Min", "Max", "Median", "90th pct", "95th pct", "99th pct", "Transactions/s", "Received", "Sent"], "items": [{"data": ["GetAllAdminWith50RequestsTest", 50, 0, 0.0, 2031.38, 1421, 2713, 1983.0, 2396.8, 2710.35, 2713.0, 11.704119850187267, 13.304292485955056, 1.5544534176029963], "isController": false}, {"data": ["GetAdminWith50RequestsTest", 50, 0, 0.0, 4347.139999999999, 3705, 5396, 4096.0, 5315.7, 5366.65, 5396.0, 8.635578583765112, 2.3359914723661483, 1.1300464162348878], "isController": false}, {"data": ["GetStudentControllerWith50RequestsTest", 50, 0, 0.0, 1957.6000000000004, 1318, 2819, 1987.0, 2806.8, 2812.45, 2819.0, 9.203018590097553, 2.93885456929873, 1.1683519694459783], "isController": false}, {"data": ["GetStudentCourseControllerWith10RequestsTest", 10, 0, 0.0, 2292.7, 2148, 2770, 2157.0, 2768.3, 2770.0, 2770.0, 2.029632636492795, 0.6857938400649483, 0.2735247107773493], "isController": false}, {"data": ["GetExternalServiceStudentCourseWith50RequestsTest", 50, 0, 0.0, 2509.019999999999, 2115, 4111, 2283.5, 3105.9, 3561.1999999999957, 4111.0, 7.047216349541931, 1.7342758985200846, 1.0047788935870332], "isController": false}, {"data": ["GetAllStudentControllerWith25RequestsTest", 25, 0, 0.0, 1415.52, 1316, 1995, 1332.0, 1832.6, 1947.0, 1995.0, 6.245316012990258, 6.1416340088683485, 0.8050602672995253], "isController": false}, {"data": ["GetAdminWith10RequestsTest", 10, 0, 0.0, 4305.6, 3726, 5341, 4117.5, 5331.4, 5341.0, 5341.0, 1.7277125086385627, 0.4673597313407049, 0.22608737906012438], "isController": false}, {"data": ["GetStudentCourseWith50RequestsTest", 50, 0, 0.0, 2293.4000000000015, 2006, 3171, 2175.0, 2663.9, 3005.45, 3171.0, 8.12743823146944, 2.746185183680104, 1.14292100130039], "isController": false}, {"data": ["GetStudentControllerWith25RequestsTest", 25, 0, 0.0, 1970.9999999999995, 1323, 2818, 1985.0, 2514.000000000001, 2816.8, 2818.0, 4.906771344455349, 1.5669084273797842, 0.622929955839058], "isController": false}, {"data": ["GetStudentWith10RequestsTest", 10, 0, 0.0, 2776.5, 2177, 3128, 3008.0, 3127.9, 3128.0, 3128.0, 2.3180343069077423, 0.7855057661103384, 0.3078639313861845], "isController": false}, {"data": ["GetStudentCourseWith25RequestsTest", 25, 0, 0.0, 2352.3999999999996, 2002, 4010, 2181.0, 3003.8, 3709.399999999999, 4010.0, 4.820671037408408, 1.6288595497493252, 0.6779068646355573], "isController": false}, {"data": ["GetExternalServiceStudentCourseWith25RequestsTest", 25, 0, 0.0, 2527.12, 2113, 4103, 2277.0, 3106.4, 3804.1999999999994, 4103.0, 4.588839941262849, 1.129284829295154, 0.654268194750367], "isController": false}, {"data": ["GetAllStudentControllerWith50RequestsTest", 50, 0, 0.0, 1435.9199999999998, 1306, 1996, 1327.5, 1829.0, 1834.8, 1996.0, 11.57675387821255, 11.384561675156286, 1.4923159296133364], "isController": false}, {"data": ["GetAllAdminWith25RequestsTest", 25, 0, 0.0, 2062.1599999999994, 1424, 2709, 1985.0, 2418.2000000000003, 2632.2, 2709.0, 5.635707844905321, 6.406214776825969, 0.7484924481514879], "isController": false}, {"data": ["GetStudentWith25RequestsTest", 25, 0, 0.0, 2844.4799999999996, 2180, 3291, 2890.0, 3194.6000000000004, 3290.7, 3291.0, 4.8666536889234955, 1.6443966566089159, 0.6463524430601519], "isController": false}, {"data": ["GetStudentCourseControllerWith25RequestsTest", 25, 0, 0.0, 2240.32, 1649, 2985, 2163.0, 2754.4, 2916.0, 2985.0, 4.613397305775973, 1.55882369902196, 0.6217273712862152], "isController": false}, {"data": ["GetStudentCourseWith10RequestsTest", 10, 0, 0.0, 2319.7, 2162, 3006, 2173.0, 2971.7000000000003, 3006.0, 3006.0, 2.391200382592061, 0.8079641917742706, 0.3362625538020086], "isController": false}, {"data": ["GetAllAdminWith10RequestsTest", 10, 0, 0.0, 1914.6999999999998, 1425, 2134, 1981.5, 2119.3, 2134.0, 2134.0, 2.4330900243309004, 2.7657390510948905, 0.32314476885644766], "isController": false}, {"data": ["GetStudentWith50RequestsTest", 50, 0, 0.0, 2789.5599999999995, 2178, 4009, 2889.0, 3128.0, 3201.4499999999994, 4009.0, 7.9239302694136295, 2.67742175118859, 1.0523969889064977], "isController": false}, {"data": ["GetAllStudentControllerWith10RequestsTest", 10, 0, 0.0, 1407.8999999999999, 1316, 1995, 1331.5, 1942.1000000000001, 1995.0, 1995.0, 2.871912693854107, 2.8242344557725443, 0.37020749569213096], "isController": false}, {"data": ["GetExternalServiceStudentCourseWith10RequestsTest", 10, 0, 0.0, 2472.1, 2116, 3106, 2312.0, 3105.9, 3106.0, 3106.0, 1.9561815336463224, 0.4814040492957746, 0.278908695226917], "isController": false}, {"data": ["GetStudentControllerWith10RequestsTest", 10, 0, 0.0, 1995.6, 1324, 2813, 2046.0, 2747.1000000000004, 2813.0, 2813.0, 2.4425989252564726, 0.7800096177332682, 0.31009556668295063], "isController": false}, {"data": ["GetStudentCourseControllerWith50RequestsTest", 50, 0, 0.0, 2296.480000000001, 1655, 2981, 2313.0, 2753.9, 2765.0, 2981.0, 8.667013347200553, 2.9285025567689376, 1.1680154706188248], "isController": false}, {"data": ["GetAdminWith25RequestsTest", 25, 0, 0.0, 4291.800000000001, 3718, 5371, 4072.0, 5305.8, 5358.1, 5371.0, 4.3140638481449525, 1.1669879745470233, 0.5645356988783434], "isController": false}]}, function(index, item){
        switch(index){
            // Errors pct
            case 3:
                item = item.toFixed(2) + '%';
                break;
            // Mean
            case 4:
            // Mean
            case 7:
            // Median
            case 8:
            // Percentile 1
            case 9:
            // Percentile 2
            case 10:
            // Percentile 3
            case 11:
            // Throughput
            case 12:
            // Kbytes/s
            case 13:
            // Sent Kbytes/s
                item = item.toFixed(2);
                break;
        }
        return item;
    }, [[0, 0]], 0, summaryTableHeader);

    // Create error table
    createTable($("#errorsTable"), {"supportsControllersDiscrimination": false, "titles": ["Type of error", "Number of errors", "% in errors", "% in all samples"], "items": []}, function(index, item){
        switch(index){
            case 2:
            case 3:
                item = item.toFixed(2) + '%';
                break;
        }
        return item;
    }, [[1, 1]]);

        // Create top5 errors by sampler
    createTable($("#top5ErrorsBySamplerTable"), {"supportsControllersDiscrimination": false, "overall": {"data": ["Total", 680, 0, "", "", "", "", "", "", "", "", "", ""], "isController": false}, "titles": ["Sample", "#Samples", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors"], "items": [{"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}]}, function(index, item){
        return item;
    }, [[0, 0]], 0);

});
