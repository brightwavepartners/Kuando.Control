pipeline{
  
  agent any
    
    node {
        customWorkspace 'a'	

        stages {
            stage('BuildAndTest') {
                node {


                    steps {
                        checkout scm
                        bat 'powershell.exe -file ./build.ps1 -Configuration Debug -Target Default -ScriptArgs --StartupProject=Kuando.Control'
                    }
                }
            }
        }
    }

    post { 
        always {
            deleteDir()
        }
    }
}