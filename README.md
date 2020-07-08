# RemindMe - Backend

**Note taking app built with Angular frontend and .Net backend and SQL database for asset management**

- this WEB API handles HTTP requests like get, post, put and delete
- it listens to HTTPS calls and routes the calls to the correct methods to trigger the associated CRUD operations on a SQL database
- the methods return an HTTPResponse with error code and message in the header and the data in the body
- the controller class: NotesController is inherited from APIController class and the uri is specified in WebApiConfig file making the uri format: "api/{controller}/{id}" with id as an optional parameter
- the methods return an HTTPResponse with error code and message in the header and the data in the body
- ORM (Object-Relational Mapping) Entity Framework has been used to convert the data row from the SQL database into an entity object to create a virtual database that can be used within the programming language
- the SQL database is being hosted on an SQL server and the entity framework reflects the changes to it synchronously or asynchronously
- The API had the following resources:
	- GET URL
	- POST URL BODY
	- PUt URL BODY
	- DELETE URL BODY

