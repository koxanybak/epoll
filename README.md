# Startup

### This application requires .NET 5.0 and Docker to run.
### Configure database
Install EntityFramework Tools for migrations
```
dotnet tool install --global dotnet-ef
# My version was 5.0.3
```
Start the database
```
chmod +x startDb.sh

# Without persisting data over container restarts

./startDb.sh

# OR

# Persisting data into a directory (must be an absolute path)

./startDb.sh (unix_dirpath_here_without_parenthesis)

# Example

mkdir ~/epolldata
./startDb.sh /home/$USER/epolldata
```
Run the application
```
dotnet run
```
The application is accessible at http://localhost:5000
