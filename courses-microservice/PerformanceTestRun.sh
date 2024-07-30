#!/bin/bash

# Directorio de trabajo
WORKDIR="$(pwd)"

# Directorios y archivos
TEST_PLANS_DIR="./TestPlans"
TEST_PLAN="Test de Rendimiento Course Microservice.jmx"
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

# Eliminar el directorio de informes si existe
if [ -d "$REPORT_DIR" ]; then
    rm -rf "$REPORT_DIR"
fi

# Crear el directorio de informes nuevamente
mkdir -p "$REPORT_DIR"

# Ejecutar JMeter en modo no gráfico
jmeter -n -t "$TEST_PLAN" -l "${RESULTS_CSV}" -e -o "${REPORT_DIR}"

# Verificar si el comando JMeter tuvo éxito
if [ $? -eq 0 ]; then
    echo "JMeter ejecutado exitosamente. Los informes se han generado en $REPORT_DIR"
else
    echo "Error: La ejecución de JMeter falló."
    exit 1
fi

# Eliminar el archivo CSV
rm "${RESULTS_CSV}"

# Eliminar el archivo de log si existe
if [ -f "jmeter.log" ]; then
    rm "jmeter.log"
fi
