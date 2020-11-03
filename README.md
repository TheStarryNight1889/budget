# LaBudget 
By Christie Molloy

## What is LaBudget?
LaBudget is an implemntaion of an API for managing finances and providing spending insights. Accompanied with the API is a front-end made with Vue.js.
All information in this README is subject to change. Check back for updates.

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
