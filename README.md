# DocumentManagement

Small document management system with the following components:

  - ASP.NET REST Web API that has endpoints for uploading/downloading documents.
  - Database (SQL Server) to store metadata about documents. (metadata could
include document ID, upload date, last accessed date, user who uploaded it, file
size, etc …)
  - Document storage solution, this could be the file system for now but could be
changed later.
  - Simple web interface to show a user’s uploaded documents, sorted by date
accessed in descending order. This could be a standard ASP.NET MVC or a SinglePage Application (SPA).
  - Web API and web interface should be secured and should check for identity.
  - Only administrators can upload documents.
  - For simplify user management, read a custom HTTP header “Username” if its passed
then assume user is authenticated and the username is the one passed. Also, if the
request contains the HTTP header “Admin” with value of “1” that means it’s an
administrator.
  - Every HTTP request for the API logged to a log file.
