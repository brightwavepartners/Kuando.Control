pipeline{
  
    agent {
        node {
            label 'master'
            customWorkspace 'a'	
        }
    }

    stages {
        stage('BuildAndTest') {
            steps {
                checkout scm
                bat 'powershell.exe -file ./build.ps1 -Configuration Debug -Target Default -ScriptArgs --StartupProject=Kuando.Control'
            }
        }
    }

    post { 
        always {
            deleteDir()
        }
    }
}