# StatlerWaldorfCorp.TeamService
Companies with geographically distributed team members often have difficult time keeping track of their members: their locations, contact info, project assignment. 

This team service solves this problem. This allows clients to manage their team lists as well as team members and their details.

### Technologies
- C#
- .NET 6
- Moq testing framework
- Docker
- GitHub Actions CI/CD pipeline

### Run Application

#### - Via Docker Image
1. Run the latest service docker image from Docker Hub;

    `$ docker run -p 8081:80 -d pkerbynn/statlerwaldorfcorp.teamservice:[TAG]`
2. Access url `http://localhost:8081/teams`

### Run Tests
#### - Run 
1. `$ dotnet restore`
2. `$ dotnet build`
3. `$ dotnet test`
