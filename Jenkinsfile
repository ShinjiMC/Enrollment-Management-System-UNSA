pipeline {
    agent any

    environment {
        SONAR_SCANNER_BIN = "/opt/sonar-scanner/bin"
        PATH = "${SONAR_SCANNER_BIN}:${env.PATH}"
        PROJECT_DIR = "/home/neodev/projects/Enrollment-Management-System-UNSA"
        USER_MCSV_DIR = "${PROJECT_DIR}/users-microservice"
        JMETER_RESULTS_DIR = "${PROJECT_DIR}/performanceTests"
    }

    stages {
        stage("Automatic Build") {
            steps {
                script {
                    dir(BACKEND_DIR) {
                        sh "dotnet --version"
                        // sh "mvn --version"
                        // sh "mvn clean install" //con tests // funciona
                        // sh "mvn clean install -DskipTests" // funciona
                        // sh "gradle clean build -x test" // funciona
                    }
                }
            }
        }
        stage("SonarCloud Static Analysis") {
            steps {
                cho "Code Static Analysis with SonarScanner..."
                script {
                    dir(PROJECT_DIR) {
                        sh "sonar-scanner --version"
                        sh "sonar-scanner" // Funciona
                    }
                }
            }
        }
        stage("Unit Testing") {
            steps {
                echo "Unit tests with NUnit + Moq ..."
                script {
                    dir(BACKEND_DIR) {
                        // sh "dotnet test"
                        // sh "gradle test"
                        // sh "mvn test" // funciona
                    }
                }
            }
        }
        stage("API Testing") {
            steps {
                echo "API tests with Postman ..."
                script {
                    dir(BACKEND_DIR) {
                        sh "postman --version"
                    }
                }
            }
        }
        stage("Performance Testing"){
            steps {
                echo "Performance tests with JMeter ..."
                script {
                    dir(JMETER_RESULTS_DIR) {
                        sh "jmeter --version"  
                        sh "jmeter -n -t ./Login_PerformanceTest.jmx -l 1.csv -e -o ${PROJECT_DIR}/reports/performance_testing_report"   
                    }
                }
            }
        }
        stage("Security Testing"){
            steps {
                script {
                    echo "Security tests with OWASP ZAP and Dependency-Check ..."
                    dir(PROJECT_DIR) {
                        sh "zap.sh -version"
                        sh "zap.sh -port 7000 -quickurl http://localhost:3000 -quickout ${PROJECT_DIR}/reports/security_testing__zaproxy_report.html -quickprogress" // con interfaz
                        // sh "zap.sh -daemon -port 7000 -quickurl http://localhost:3000 -quickout ${PROJECT_DIR}/reports/security_testing__zaproxy_report.html -quickprogress" // sin interfaz
                        sh "dependency-check.sh --version"  
                        sh "dependency-check.sh --scan ./backend --format HTML --out ./reports/security_testing__dependency_check_report.html --disableAssembly"
                        sh "dependency-check.sh --scan ./client --format HTML --out ./reports/security_testing__dependency_check_frontend_report.html --disableAssembly --disableYarnAudit --exclude 'node_modules/**'"
                    }
                }
            }
        }
        stage("Docker Image"){
            steps {
                script {
                    dir(PROJECT_DIR) {
                        sh "docker --version"
                        sh "docker compose build" // construye los contenedores // funciona
                        sh "docker compose up -d" // ejecuta los contenedores
                        sleep(time: 2, unit: 'MINUTES') // espera 1 minuto
                        sh "docker compose down" // detiene los contenedores
                        //sh "docker compose down --volumes --rmi all" // elimina contenedores
                    }
                }
            }
        }
    }
}