Software Requirements:

	Visual Studio 2019 v16.9

	Visual Studio Build Tools 2017 v15.9.30
	
	Visual Studio Code v1.55.2

	.NET 5.0

	Node.js v14.16.0

	Python v3.9

	Google Chrome vLatest

	MongoDB v4.4.1

How To Run API:
	1.) Ensure all Software Requirments (above) are met.
	2.) Ensure MongoDB is running.
	2.) Open api.sln (budget/api/api.sln) with Visual Studio
	3.) Build solution (F6)
	4.) Debug solution (F5)

How to Run API Test:
	1.) Open Visual Studio Code
	2.) Click 'File' -> 'Open Folder' -> open(budget/IntegrationTests/TEST_PREREQUISITES)
	3.) Run Python Script [path/to/python.exe "path/to/script/budget/IntegrationTests/TEST_PREREQUISITES/integration_tests_db_prerequisites.py"] 
	Example -> C:/Python39/python.exe "c:/Users/mollo/Documents/LIT/Final Year Project/budget/IntegrationTests/TEST_PREREQUISITES/integration_tests_db_prerequisites.py"
	4.) Once the Python Script has ran. Open Visual Studio.
	5.) If 'Test Explorer' panel is not visible, Click 'View' -> 'Test Explorer'. The Panel Should now be visible.
	6.) In the top left corner of the 'Test Explorer', click the double arrow icon.
	7.) If done correctly, all tests should pass.

Installing Vue.js:
	1.) Open Visual Studio Code
	2.) Click 'File' -> 'Open Folder' -> open(budget/frontend)
	3.) Click 'Terminal' -> 'Open new terminal'
	4.) in the terminal type "npm install -g @vue/cli". press enter
	5.) in the terminal type "vue --version". the version should be 4.5.11. press enter
	6.) in the terminal type "npm install". press enter

Running Frontend:
	1.) In the same Visual Studio Code instance that was used to install Vue.js
	2.) in the terminal type "npm run serve".
	3.) the front end should deploy on localhost:8080
	4.) if there is a warning present in ./src/main.js, ignore it. it wont affect anything.

Using Frontend and API at the same time:
	1.) Run API using steps above.
	2.) Run Frontend using steps above.
	3.) Open Chrome, type "localhost:8080"
	4.) The app should be running.

Additional Information:
	1.) To view database, Install Robo3T and connect to localhost:27017 - no authentication

	
	
