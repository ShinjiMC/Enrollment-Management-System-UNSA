@echo off
setlocal

:: Directorio de trabajo
set "WORKDIR=%cd%"

:: Directorios y archivos
set "TEST_PLANS_DIR=%WORKDIR%\TestPlans"
set "TEST_PLAN=PerformanceTestPlan.jmx"
set "RESULTS_CSV=1.csv"
set "REPORT_DIR=%WORKDIR%\TestReports\PerformanceTestReport"

:: Crear el directorio de TestPlans si no existe
if not exist "%TEST_PLANS_DIR%" mkdir "%TEST_PLANS_DIR%"

:: Moverse al directorio de TestPlans
cd /d "%TEST_PLANS_DIR%"
if errorlevel 1 exit /b 1

:: Verificar si el archivo de plan de prueba existe
if not exist "%TEST_PLAN%" (
    echo Error: El archivo %TEST_PLAN% no existe o no se puede abrir.
    exit /b 1
)

:: Crear el directorio de informes si no existe
if not exist "%REPORT_DIR%" mkdir "%REPORT_DIR%"

:: Ejecutar JMeter en modo no gráfico
jmeter -n -t "%TEST_PLAN%" -l "%RESULTS_CSV%" -e -o "%REPORT_DIR%"

:: Verificar si el comando JMeter tuvo éxito
if %errorlevel% neq 0 (
    echo Error: La ejecución de JMeter falló.
    exit /b 1
)

:: Eliminar el archivo CSV
del "%RESULTS_CSV%"

:: Eliminar el archivo de log
del jmeter.log

:: Fin del script
endlocal