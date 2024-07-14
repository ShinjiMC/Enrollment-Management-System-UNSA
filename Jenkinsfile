pipeline {
    agent any

    environment {
        SONAR_SCANNER_BIN = "/opt/sonar-scanner-6.1.0.4477-linux-x64/bin"
        PATH = "${SONAR_SCANNER_BIN}:${env.PATH}"
        PROJECT_DIR = "/home/neodev/Documents/projects/Enrollment-Management-System-UNSA"

        USERS_MCSV_DIR = "${PROJECT_DIR}/users-microservice"
        COURSES_MCSV_DIR = "${PROJECT_DIR}/courses-microservice"
        MATRICULATE_MCSV_DIR = "${PROJECT_DIR}/matriculate-microservice"
        PAYMENTS_MCSV_DIR = "${PROJECT_DIR}/payments-microservice"
        
        API_TESTING_RESULTS = "${PROJECT_DIR}/apiTests"
        PERFORMANCE_TESTING_RESULTS = "${PROJECT_DIR}/performanceTests"
        SECURITY_TESTING_RESULTS = "${PROJECT_DIR}/securityTests"
    }

    stages {
        stage("Automatic Build") {
            parallel {
                stage("Build Users Microservice") {
                    steps {
                        dir(USERS_MCSV_DIR) {
                            sh "dotnet build src/src.csproj"
                        }
                    }
                }
                stage("Build Courses Microservice") {
                    steps {
                        dir(COURSES_MCSV_DIR) {
                            sh "dotnet build src/src.csproj"
                        }
                    }
                }
                stage("Build Matriculate Microservice") {
                    steps {
                        dir(MATRICULATE_MCSV_DIR) {
                            sh "pwd"
                            // sh "dotnet build src/src.csproj"
                        }
                    }
                }
                stage("Build Payments Microservice") {
                    steps {
                        dir(PAYMENTS_MCSV_DIR) {
                            sh "pwd"
                            // sh "dotnet build src/src.csproj"
                        }
                    }
                }
                stage("Build Frontend") {
                    steps {
                        dir("${PROJECT_DIR}/client") {
                            sh "pwd"
                            sh "npm install"
                            // sh "npm run build"
                        }
                    }
                }
            }
        }

        stage("SonarCloud Static Analysis") {
            parallel {
                stage("Users Microservice") {
                    steps {
                        dir(USERS_MCSV_DIR) {
                            sh "sonar-scanner --version"
                            // sh "sonar-scanner"
                        }
                    }
                }
                stage("Courses Microservice") {
                    steps {
                        dir(COURSES_MCSV_DIR) {
                            sh "sonar-scanner --version"
                            // sh "sonar-scanner"
                        }
                    }
                }
                stage("Matriculate Microservice") {
                    steps {
                        dir(MATRICULATE_MCSV_DIR) {
                            sh "sonar-scanner --version"
                            // sh "sonar-scanner"
                        }
                    }
                }
                // stage("Payments Microservice") {
                //     steps {
                //         dir(PAYMENTS_MCSV_DIR) {
                //             sh "sonar-scanner --version"
                //             // sh "sonar-scanner"
                //         }
                //     }
                // }
                // stage("Frontend") {
                //     steps {
                //         dir("${PROJECT_DIR}/client") {
                //             sh "sonar-scanner --version"
                //             // sh "sonar-scanner"
                //         }
                //     }
                // }
            }
        }

        stage("Unit Testing") {
            steps {
                script {
                    dir(PROJECT_DIR) {
                        sh "dotnet --version"
                        // sh "dotnet test"
                        // sh "gradle test"
                        // sh "mvn test" // funciona
                    }
                }
            }
        }

        stage("API Testing") {
            parallel {
                stage("Users Microservice") {
                    steps {
                        script {
                            dir(USERS_MCSV_DIR) {
                                sh "pwd"
                                sh "postman --version"
                                // sh "newman --version"
                                // sh "newman run ./tests/EnrollmentManagementSystem.postman_collection.json -e ./tests/EnrollmentManagementSystem.postman_environment.json -r cli,html --reporter-html-export ${API_TESTING_RESULTS}/users_microservice_api_tests_report.html"
                            }
                        }
                    }
                }
                stage("Courses Microservice") {
                    steps {
                        script {
                            dir(COURSES_MCSV_DIR) {
                                sh "pwd"
                                sh "postman --version"
                                // sh "newman --version"
                                // sh "newman run ./tests/EnrollmentManagementSystem.postman_collection.json -e ./tests/EnrollmentManagementSystem.postman_environment.json -r cli,html --reporter-html-export ${API_TESTING_RESULTS}/courses_microservice_api_tests_report.html"
                            }
                        }
                    }
                }
            }
        }

        stage("Performance Testing"){
            steps {
                echo "Performance tests with JMeter ..."
                script {
                    dir(PERFORMANCE_TESTING_RESULTS) {
                        sh "pwd"
                        // sh "jmeter --version"  
                        // sh "jmeter -n -t ./Login_PerformanceTest.jmx -l 1.csv -e -o ${PROJECT_DIR}/reports/performance_testing_report"   
                    }
                }
            }
        }

        stage("Security Testing"){
            steps {
                script {
                    echo "Security tests with OWASP ZAP and Dependency-Check ..."
                    dir(PROJECT_DIR) {
                        sh "pwd"
                        // sh "zap.sh -version"
                        // sh "zap.sh -port 7000 -quickurl http://localhost:3000 -quickout ${PROJECT_DIR}/reports/security_testing__zaproxy_report.html -quickprogress" // con interfaz
                        // sh "zap.sh -daemon -port 7000 -quickurl http://localhost:3000 -quickout ${PROJECT_DIR}/reports/security_testing__zaproxy_report.html -quickprogress" // sin interfaz
                        // sh "dependency-check.sh --version"  
                        // sh "dependency-check.sh --scan ./backend --format HTML --out ./reports/security_testing__dependency_check_report.html --disableAssembly"
                        // sh "dependency-check.sh --scan ./client --format HTML --out ./reports/security_testing__dependency_check_frontend_report.html --disableAssembly --disableYarnAudit --exclude 'node_modules/**'"
                    }
                }
            }
        }
        stage("Dockerization"){
            steps {
                script {
                    dir(PROJECT_DIR) {
                        sh "pwd"
                        // sh "docker --version"
                        // sh "docker compose build" // construye los contenedores // funciona
                        // sh "docker compose up -d" // ejecuta los contenedores
                        // sleep(time: 2, unit: 'MINUTES') // espera 1 minuto
                        // sh "docker compose down" // detiene los contenedores
                        //sh "docker compose down --volumes --rmi all" // elimina contenedores
                    }
                }
            }
        }
    }
}