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
| USERS           | TRANSACTIONS | RECURRING\_TRANSACTIONS | ACCOUNTS                    | TARGETS                |
| --------------- | ------------ | ----------------------- | --------------------------- | ---------------------- |
| id              | id           | id                      | id                          | id                     |
| name            | amount       | name                    | user\_id                    | creation\_date         |
| dob             | date         | amount                  | name                        | expected\_end\_date    |
| email           | category     | recurring\_date         | type                        | actual\_end\_date      |
| password        | name         | category                | default                     | goal                   |
| currency        | store        | type                    | date\_offset\_balance       | date\_offset\_progress |
| {ACCOUNTS}      | goods        |                         | balance                     | goal\_met              |
| \[TARGET\_IDS\] |              |                         | color                       | amount                 |
|                 |              |                         | last\_updated               | name                   |
|                 |              |                         | \[TRANSACTION\_IDS\]        |                        |
|                 |              |                         | \[RECURRING\_TRANSACTIONS\] |                        |

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
Week beginning - 08/03/2021
```diff
+ Updated table structure
```
Week beginning - 15/03/2021
```diff
+ Added models according to db structure
+ Added enums according to models
```
