pipeline{
  
  agent any
  
    stages {
        stage('BuildAndTest') {
            steps {
                checkout scm
                bat 'powershell.exe -file ./build.ps1 -Configuration Debug -Target Default -StartupProject ./src/Kuando.Control
            }
        }
        stage('StaticAnalysis') {
            steps {
                bat 'SonarQube.Scanner.MSBuild.exe begin /k:"org.sonarqube:sonarqube-scanner-msbuild" /n:"MogProperties.Presentation.Web" /v:"1.0"'
                bat '"C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Community\\MSBuild\\15.0\\Bin\\MSBuild.exe" /t:Rebuild'
                bat 'SonarQube.Scanner.MSBuild.exe end'
            }
        }
    }
    post { 
        always {
            deleteDir()
        }
    }
}