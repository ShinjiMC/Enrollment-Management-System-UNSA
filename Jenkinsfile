pipeline {
    agent any

    environment {
        SONAR_SCANNER_BIN = "/opt/sonar-scanner-6.1.0.4477-linux-x64/bin"
        PATH = "${SONAR_SCANNER_BIN}:${env.PATH}"
        PROJECT_DIR = "/home/neodev/Documents/projects/Enrollment-Management-System-UNSA"

        AUTH_MCSV_DIR = "${PROJECT_DIR}/auth-microservice"
        USERS_MCSV_DIR = "${PROJECT_DIR}/users-microservice"
        COURSES_MCSV_DIR = "${PROJECT_DIR}/courses-microservice"
        ENROLLMENTS_MCSV_DIR = "${PROJECT_DIR}/enrollments-microservice"
        SCHOOLS_MCSV_DIR = "${PROJECT_DIR}/schools-microservice"
        NOTIFICATIONS_MCSV_DIR = "${PROJECT_DIR}/notifications-microservice"
        PAYMENTS_MCSV_DIR = "${PROJECT_DIR}/payments-microservice"
        
        API_TESTING_RESULTS = "${PROJECT_DIR}/reports/apiTests"
        PERFORMANCE_TESTING_RESULTS = "${PROJECT_DIR}/reports/performanceTests"
        SECURITY_TESTING_RESULTS = "${PROJECT_DIR}/reports/securityTests"
        UNIT_TESTING_RESULTS = "${PROJECT_DIR}/reports/unitTests"
    }

    stages {
        stage("Automatic Build") {
            parallel {
                stage ("Build Auth Microservice") {
                    steps {
                        dir(AUTH_MCSV_DIR) {
                            sh "dotnet --version"
                            // sh "dotnet build src/src.csproj"
                        }
                    }
                }
                stage("Build Users Microservice") {
                    steps {
                        dir(USERS_MCSV_DIR) {
                            sh "dotnet --version"
                            // sh "dotnet build src/src.csproj"
                        }
                    }
                }
                stage("Build Courses Microservice") {
                    steps {
                        dir(COURSES_MCSV_DIR) {
                            sh "dotnet --version"
                            // sh "dotnet build src/src.csproj"
                        }
                    }
                }
                stage("Build Enrollments Microservice") {
                    steps {
                        dir(ENROLLMENTS_MCSV_DIR) {
                            sh "pwd"
                            // sh "dotnet build src/src.csproj"
                        }
                    }
                }
                stage ("Build Schools Microservice") {
                    steps {
                        dir(SCHOOLS_MCSV_DIR) {
                            sh "dotnet --version"
                            // sh "dotnet build src/src.csproj"
                        }
                    }
                }
                stage("Build Notifications Microservice") {
                    steps {
                        dir(NOTIFICATIONS_MCSV_DIR) {
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
                            // sh "npm install"
                            // sh "npm run build"
                        }
                    }
                }
            }
        }

        stage("SonarCloud Static Analysis") {
            parallel {
                stage ("Auth Microservice") {
                    steps {
                        dir(AUTH_MCSV_DIR) {
                            sh "sonar-scanner --version"
                            // sh "sonar-scanner"
                        }
                    }
                }
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
                stage("Enrollments Microservice") {
                    steps {
                        dir(ENROLLMENTS_MCSV_DIR) {
                            sh "sonar-scanner --version"
                            // sh "sonar-scanner"
                        }
                    }
                }
                stage ("Schools Microservice") {
                    steps {
                        dir(SCHOOLS_MCSV_DIR) {
                            sh "sonar-scanner --version"
                            // sh "sonar-scanner"
                        }
                    }
                }
                stage("Notifications Microservice") {
                    steps {
                        dir(NOTIFICATIONS_MCSV_DIR) {
                            sh "sonar-scanner --version"
                            // sh "sonar-scanner"
                        }
                    }
                }
                stage("Payments Microservice") {
                    steps {
                        dir(PAYMENTS_MCSV_DIR) {
                            sh "sonar-scanner --version"
                            // sh "sonar-scanner"
                        }
                    }
                }
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
                stage ("Auth Microservice") {
                    steps {
                        script {
                            dir(AUTH_MCSV_DIR) {
                                sh "pwd"
                                // sh "newman --version"
                                // sh "newman run ./tests/EnrollmentManagementSystem.postman_collection.json -e ./tests/EnrollmentManagementSystem.postman_environment.json -r cli,html --reporter-html-export ${API_TESTING_RESULTS}/auth_microservice_api_tests_report.html"
                            }
                        }
                    }
                }
                stage("Users Microservice") {
                    steps {
                        script {
                            dir(USERS_MCSV_DIR) {
                                sh "pwd"
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
                                // sh "newman --version"
                                // sh "newman run ./tests/EnrollmentManagementSystem.postman_collection.json -e ./tests/EnrollmentManagementSystem.postman_environment.json -r cli,html --reporter-html-export ${API_TESTING_RESULTS}/courses_microservice_api_tests_report.html"
                            }
                        }
                    }
                }
                stage("Enrollments Microservice") {
                    steps {
                        script {
                            dir(ENROLLMENTS_MCSV_DIR) {
                                sh "pwd"
                                // sh "newman --version"
                                // sh "newman run ./tests/EnrollmentManagementSystem.postman_collection.json -e ./tests/EnrollmentManagementSystem.postman_environment.json -r cli,html --reporter-html-export ${API_TESTING_RESULTS}/matriculate_microservice_api_tests_report.html"
                            }
                        }
                    }
                }
                stage ("Schools Microservice") {
                    steps {
                        script {
                            dir(SCHOOLS_MCSV_DIR) {
                                sh "pwd"
                                // sh "newman --version"
                                // sh "newman run ./tests/EnrollmentManagementSystem.postman_collection.json -e ./tests/EnrollmentManagementSystem.postman_environment.json -r cli,html --reporter-html-export ${API_TESTING_RESULTS}/schools_microservice_api_tests_report.html"
                            }
                        }
                    }
                }
                stage("Notifications Microservice") {
                    steps {
                        script {
                            dir(NOTIFICATIONS_MCSV_DIR) {
                                sh "pwd"
                                // sh "newman --version"
                                // sh "newman run ./tests/EnrollmentManagementSystem.postman_collection.json -e ./tests/EnrollmentManagementSystem.postman_environment.json -r cli,html --reporter-html-export ${API_TESTING_RESULTS}/notifications_microservice_api_tests_report.html"
                            }
                        }
                    }
                }
                stage("Payments Microservice") {
                    steps {
                        script {
                            dir(PAYMENTS_MCSV_DIR) {
                                sh "pwd"
                                // sh "newman --version"
                                // sh "newman run ./tests/EnrollmentManagementSystem.postman_collection.json -e ./tests/EnrollmentManagementSystem.postman_environment.json -r cli,html --reporter-html-export ${API_TESTING_RESULTS}/payments_microservice_api_tests_report.html"
                            }
                        }
                    }
                }
            }
        }

        stage("Performance Testing") {
            parallel {
                stage ("Auth Microservice") {
                    steps {
                        script {
                            dir(AUTH_MCSV_DIR) {
                                sh "pwd"
                                sh "jmeter --version"
                                // sh "jmeter -n -t ./PerformanceTest.jmx -l ${PERFORMANCE_TESTING_RESULTS}/auth_microservice_performance_test_results.csv -e -o ${PERFORMANCE_TESTING_RESULTS}/auth_microservice_performance_test_report"
                            }
                        }
                    }
                }
                stage("Users Microservice") {
                    steps {
                        script {
                            dir(USERS_MCSV_DIR) {
                                sh "pwd"
                                sh "jmeter --version"
                                // sh "jmeter -n -t ./PerformanceTest.jmx -l ${PERFORMANCE_TESTING_RESULTS}/users_microservice_performance_test_results.csv -e -o ${PERFORMANCE_TESTING_RESULTS}/users_microservice_performance_test_report"
                            }
                        }
                    }
                }
                stage("Courses Microservice") {
                    steps {
                        script {
                            dir(COURSES_MCSV_DIR) {
                                sh "pwd"
                                sh "jmeter --version"
                                // sh "jmeter -n -t ./PerformanceTest.jmx -l ${PERFORMANCE_TESTING_RESULTS}/courses_microservice_performance_test_results.csv -e -o ${PERFORMANCE_TESTING_RESULTS}/courses_microservice_performance_test_report"
                            }
                        }
                    }
                }
                stage("Enrollments Microservice") {
                    steps {
                        script {
                            dir(ENROLLMENTS_MCSV_DIR) {
                                sh "pwd"
                                sh "jmeter --version"
                                // sh "jmeter -n -t ./PerformanceTest.jmx -l ${PERFORMANCE_TESTING_RESULTS}/matriculate_microservice_performance_test_results.csv -e -o ${PERFORMANCE_TESTING_RESULTS}/matriculate_microservice_performance_test_report"
                            }
                        }
                    }
                }
                stage ("Schools Microservice") {
                    steps {
                        script {
                            dir(SCHOOLS_MCSV_DIR) {
                                sh "pwd"
                                sh "jmeter --version"
                                // sh "jmeter -n -t ./PerformanceTest.jmx -l ${PERFORMANCE_TESTING_RESULTS}/schools_microservice_performance_test_results.csv -e -o ${PERFORMANCE_TESTING_RESULTS}/schools_microservice_performance_test_report"
                            }
                        }
                    }
                }
                stage("Notifications Microservice") {
                    steps {
                        script {
                            dir(NOTIFICATIONS_MCSV_DIR) {
                                sh "pwd"
                                sh "jmeter --version"
                                // sh "jmeter -n -t ./PerformanceTest.jmx -l ${PERFORMANCE_TESTING_RESULTS}/notifications_microservice_performance_test_results.csv -e -o ${PERFORMANCE_TESTING_RESULTS}/notifications_microservice_performance_test_report"
                            }
                        }
                    }
                }
                stage("Payments Microservice") {
                    steps {
                        script {
                            dir(PAYMENTS_MCSV_DIR) {
                                sh "pwd"
                                sh "jmeter --version"
                                // sh "jmeter -n -t ./PerformanceTest.jmx -l ${PERFORMANCE_TESTING_RESULTS}/payments_microservice_performance_test_results.csv -e -o ${PERFORMANCE_TESTING_RESULTS}/payments_microservice_performance_test_report"
                            }
                        }
                    }
                }
            }
        }

        stage("Security Testing") {
            parallel {
                stage ("Auth Microservice") {
                    steps {
                        script {
                            dir(AUTH_MCSV_DIR) {
                                sh "pwd"
                                // sh "zap.sh -version"
                                // sh "zap.sh -daemon -port 7000 -quickurl http://localhost:8080 -quickout ${SECURITY_TESTING_RESULTS}/auth_microservice_zaproxy_report.html -quickprogress"
                            }
                        }
                    }
                }
                stage("Users Microservice") {
                    steps {
                        script {
                            dir(USERS_MCSV_DIR) {
                                sh "pwd"
                                // sh "zap.sh -version"
                                // sh "zap.sh -daemon -port 7000 -quickurl http://localhost:8080 -quickout ${SECURITY_TESTING_RESULTS}/users_microservice_zaproxy_report.html -quickprogress"
                            }
                        }
                    }
                }
                stage("Courses Microservice") {
                    steps {
                        script {
                            dir(COURSES_MCSV_DIR) {
                                sh "pwd"
                                // sh "zap.sh -version"
                                // sh "zap.sh -daemon -port 7000 -quickurl http://localhost:8080 -quickout ${SECURITY_TESTING_RESULTS}/courses_microservice_zaproxy_report.html -quickprogress"
                            }
                        }
                    }
                }
                stage("Enrollments Microservice") {
                    steps {
                        script {
                            dir(ENROLLMENTS_MCSV_DIR) {
                                sh "pwd"
                                // sh "zap.sh -version"
                                // sh "zap.sh -daemon -port 7000 -quickurl http://localhost:8080 -quickout ${SECURITY_TESTING_RESULTS}/matriculate_microservice_zaproxy_report.html -quickprogress"
                            }
                        }
                    }
                }
                stage ("Schools Microservice") {
                    steps {
                        script {
                            dir(SCHOOLS_MCSV_DIR) {
                                sh "pwd"
                                // sh "zap.sh -version"
                                // sh "zap.sh -daemon -port 7000 -quickurl http://localhost:8080 -quickout ${SECURITY_TESTING_RESULTS}/schools_microservice_zaproxy_report.html -quickprogress"
                            }
                        }
                    }
                }
                stage("Notifications Microservice") {
                    steps {
                        script {
                            dir(NOTIFICATIONS_MCSV_DIR) {
                                sh "pwd"
                                // sh "zap.sh -version"
                                // sh "zap.sh -daemon -port 7000 -quickurl http://localhost:8080 -quickout ${SECURITY_TESTING_RESULTS}/notifications_microservice_zaproxy_report.html -quickprogress"
                            }
                        }
                    }
                }
                stage("Payments Microservice") {
                    steps {
                        script {
                            dir(PAYMENTS_MCSV_DIR) {
                                sh "pwd"
                                // sh "zap.sh -version"
                                // sh "zap.sh -daemon -port 7000 -quickurl http://localhost:8080 -quickout ${SECURITY_TESTING_RESULTS}/payments_microservice_zaproxy_report.html -quickprogress"
                            }
                        }
                    }
                }
            }
        }

        stage("Dockerization") {
            steps {
                script {
                    dir(PROJECT_DIR) {
                        sh "pwd"
                        sh "docker --version"
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
