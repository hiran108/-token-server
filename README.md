# -token-server
asp.net core  web api, that use to issue JWT token for several audiences.
This project was genarated using asp.net core 2 web api default template.
## V1
sample token genaration based on audince secret key.
taking username , password and audince as the parameters.
username and password shoud be test , whine the sudince name is "audience from database".
Lgics behind the token genaration is placed inside the token controller

## V2
put audince details into Database and add DB project to solution.
add nlog for login and swagger for api documentation

## V3
Add sample resouce server that consume the authantication server. 
