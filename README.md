
# Calendar
Calendar API made with ASP.NET C# framework.

## Docker

To run developer server enter project directory in shell and type in `docker-compose up`. API will be accesable under `https://localhost:51836`. You can browse API endpoints under `/swagger` endpoint.

## API endpoints

Access to protected views is managed via JWT Token.

### /api/User/register
#### post
Register new user.

request:
```
{
  "email": "string",
  "username": "string",
  "password": "string",
  "firstname": "string",
  "lastname": "string"
}
```
response:
```
{
  "email": "string",
  "username": "string",
  "password": "string",
  "firstname": "string",
  "lastname": "string"
}
```
### /api/User/login
#### post
Getting auth token.
request:
```
{
  "username": "string",
  "password": "string"
}
```
response:
```
{
  "username": "string",
  "token": "string"
}
```
### /api/User
Login required
#### get
Get user info. 
response:
```
{
  "username": "string",
  "email": "string",
  "firstName": "string",
  "lastName": "string"
}
```
#### put
Change user password and/or firstname, lastname
request:
```
{
  "firstName": "string",
  "lastName": "string",
  "newPassword": "string",
  "currentPassword": "string"
}
```
#### delete
Delete user account.

### /api/Tasks/{id}
#### get
Returns task with set id. If task is not set as shared login is required.

Response:
```
{
  "id": 0,
  "name": "string",
  "description": "string",
  "createdDate": "2023-04-27T08:14:28.443Z",
  "updatedDate": "2023-04-27T08:14:28.443Z",
  "dueOn": "2023-04-27T08:14:28.443Z",
  "shared": true,
  "userId": "string"
}
```
#### put
Edit task. Login required.
Request:
```
{
  "name": "string",
  "description": "string",
  "dueOn": "2023-04-27T08:15:24.512Z",
  "shared": true
}
```
Response:
```
{
  "id": 0,
  "name": "string",
  "description": "string",
  "createdDate": "2023-04-27T08:14:28.443Z",
  "updatedDate": "2023-04-27T08:14:28.443Z",
  "dueOn": "2023-04-27T08:14:28.443Z",
  "shared": true,
  "userId": "string"
}
```
#### delete
Delete task. Login required.

Success code: `204`

### /api/Tasks
#### post
Create new task. Login Required.
Request:
```
{
  "name": "string",
  "description": "string",
  "dueOn": "2023-04-27T08:18:03.576Z",
  "shared": true
}
```
Response:
```
{
  "name": "string",
  "description": "string",
  "dueOn": "2023-04-27T08:18:03.577Z",
  "shared": true
}
```
### api/Tasks/today
#### get
Returns tasks that are due today. Login required.
Response:
```
[
	{
	  "id": 0,
	  "name": "string",
	  "description": "string",
	  "createdDate": "2023-04-27T08:18:03.566Z",
	  "updatedDate": "2023-04-27T08:18:03.566Z",
	  "dueOn": "2023-04-27T08:18:03.566Z",
	  "shared": true,
	  "userId": "string"
	}
]
```
### api/Tasks/{year}/{month}/{day}
#### get
Returns tasks that are due on on given date. Login required.
Response:
```
[
	{
	  "id": 0,
	  "name": "string",
	  "description": "string",
	  "createdDate": "2023-04-27T08:18:03.566Z",
	  "updatedDate": "2023-04-27T08:18:03.566Z",
	  "dueOn": "2023-04-27T08:18:03.566Z",
	  "shared": true,
	  "userId": "string"
	}
]
```
