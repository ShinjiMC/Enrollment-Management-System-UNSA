#!/bin/bash

# Directorio de trabajo
WORKDIR="$(pwd)"

# Directorios y archivos
TEST_PLANS_DIR="./TestPlans"
TEST_PLAN="PerformanceTestPlan.jmx"
RESULTS_CSV="1.csv"
REPORT_DIR="${WORKDIR}/TestReports/PerformanceTestReport"

# Crear el directorio de TestPlans si no existe
if [ ! -d "$TEST_PLANS_DIR" ]; then
    mkdir -p "$TEST_PLANS_DIR"
fi

# Moverse al directorio de TestPlans
cd "$TEST_PLANS_DIR" || { echo "Error: No se pudo cambiar al directorio $TEST_PLANS_DIR."; exit 1; }

# Verificar si el archivo de plan de prueba existe
if [ ! -f "$TEST_PLAN" ]; then
    echo "Error: El archivo $TEST_PLAN no existe o no se puede abrir."
    exit 1
fi

# Crear el directorio de informes si no existe
if [ ! -d "$REPORT_DIR" ]; then
    mkdir -p "$REPORT_DIR"
fi

# Ejecutar JMeter en modo no gráfico
jmeter -n -t "$TEST_PLAN" -l "${RESULTS_CSV}" -e -o "${REPORT_DIR}"

# Verificar si el comando JMeter tuvo éxito
if [ $? -eq 0 ]; then
    echo "JMeter ejecutado exitosamente. Los informes se han generado en $REPORT_DIR"
else
    echo "Error: La ejecución de JMeter falló."
    exit 1
fi


# # Remove the CSV file
rm 1.csv

# # Remove the log file
rm jmeter.log

# jmeter -n -t ./PerformanceTestPlan.jmx -l 1.csv -e -o /home/neodev/Documents/projects/Enrollment-Management-System-UNSA/payments-microservice/TestReports/PerformanceTestReport

# Run the JMeter test
# jmeter -n -t ./PerformanceTestPlan.jmx -l 1.csv -e -o ./TestReports/PerformanceTestReport

