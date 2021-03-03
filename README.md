# LaBudget 
By Christie Molloy

## What is LaBudget?
LaBudget is an implemntaion of an API for managing finances and providing spending insights. Accompanied with the API is a front-end made with Vue.js.
All information in this README is subject to change. Check back for updates.

## What does LaBudget achieve?
- Create savings targets and track progress.
- Track outgoing capital.
- Track inncoming capital.
- Provide unique financial insights (e.g. spending more than you should on entertainment, luxury goods etc...)
- Send reminders when charges will be added to your accounts.

***
### Database structure (mogodb)
| USERS    | TRANSACTIONS | TARGETS                |
|----------|--------------|------------------------|
| id       | id           | id                     |
| name     | user\_id     | user\_id               |
| dob      | amount       | creation\_date         |
| email    | date         | goal\_end\_date        |
| password | reoccuring   | end\_date              |
| currency | day/date     | type                   |
|          | frequency    | goal                   |
|          | category     | date\_offset\_progress |
|          |              | goal\_met              |
|          |              | amount                 |
|          |              | name                   |

***
### How it Works

Simple System Flow:

![Application Flow](https://github.com/TheStarryNight1889/budget/blob/main/md_images/Capture.PNG "sample post flow")

***
### Technologies Used

+ .NET 5.0
+ Vue.js
+ npm
+ Docker

***
### Progress
Week beginning - 02/11/2020
```diff
+ Added file structure to project.
+ Added Model,Repository & Service files to folders.
+ Added Interfaces for Repositories, Helpers & Services.
+ Added .gitignore
+ Added Controllers
- Removed sample Controller
+ Added database conection functionality
```

Week beginning - 23/11/2020
```diff
+ Added xd wireframes
+ Changed database connection. in line with .NET DOCS now.
```

Week beginning - 04/01/2021
```diff
+ Added orm features to User
+ Upgraded to .NET 5.0
+ Fully implemented all CRUD operations for User
```
Week beginning - 11/01/2021
```diff
+ Added Authentication
+ Added Authorization
+ Added Token system
+ Added Swagger
```
Week beginning - 18/01/2021
```diff
+ Made code truly asyncronous 
+ Added Integration Test project
+ Added Integration Tests
+ Began work on front end.
+ Created Vue.js boilerplate
+ Added navigation
+ Added Home page
+ Added Register page
+ Added Login page
```
Week beginning - 01/03/2021
```diff
+ Login functionality working on front end
+ Register functionality working on front end
+ Integrations tests added
```
