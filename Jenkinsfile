node {

	stage ('Checkout'){
	
		checkout scm
	}
	
	stage ('build & test')
	{
		sh "docker -v"
		sh "cd Develop && docker build -t replaceapp:B${BUILD_NUMBER} -f dockerfile_replace_buil_test ."
	}
	stage ('publish result')
	{
		containerID = sh (
            script: "docker run -d replaceapp:B${BUILD_NUMBER}", 
			returnStdout: true
        ).trim()
        echo "Container-ID is ==> ${containerID}"
        sh "docker cp ${containerID}:/TestResults/test_results.xml test_results.xml"
		sh "echo `ls`"
        sh "docker stop ${containerID}"
        sh "docker rm ${containerID}"
		
		step([$class: 'MSTestPublisher', testResultsFile:"**/test_results.xml", failOnError: true, keepLongStdio: true])	
		
	}
}