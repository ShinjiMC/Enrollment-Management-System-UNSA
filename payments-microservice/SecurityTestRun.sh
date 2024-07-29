#!/bin/bash

# Mostrar mensaje de inicio
echo "=== Security Testing Start ==="

# Create the directories to store the reports
mkdir -p zap-wrk
mkdir -p zap-reports

# Dar permisos de escritura a los directorios
chmod 777 zap-wrk
chmod 777 zap-reports

# Descargar la imagen de Docker de OWASP ZAP

# Ejecutar el comando y guardar la salida en SecurityTestReport.txt
docker run --network host -v $(pwd)/zap-wrk:/zap/wrk -v $(pwd)/zap-reports:/zap/reports -t zaproxy/zap-stable zap-baseline.py -t http://192.168.1.38:8007 -r SecurityTestReport.html > SecurityTestReport.txt

# Mostrar mensaje de fin
echo "=== Security Testing End ==="

# Añadir la salida del comando al archivo de reporte
cat SecurityTestReport.txt

# Move the report to the reports directory
mv zap-wrk/SecurityTestReport.html TestReports/SecurityTestReport.html
mv SecurityTestReport.txt TestReports/SecurityTestReport.txt

# Delete the directories
rm -rf zap-wrk
rm -rf zap-reports