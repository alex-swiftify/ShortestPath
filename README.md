
# ShortestPath

This project finds the shortest connection path between two actors in a movie dataset. It consists of three main parts:

1. **ShortestPath.Frontend**: An Angular frontend for selecting actors and viewing paths.
2. **ShortestPath.WebAPI**: A .NET Web API backend serving actors and paths.
3. **ShortestPath.ConsoleApp**: A console application for testing and debugging.

---

### Web API (Backend)

1. Navigate to `ShortestPath.WebAPI`:
   ```bash
   cd ShortestPath.WebAPI
   ```
2. Run the Web API:
   ```bash
   dotnet run
   ```
3. Access the API:
   - Swagger UI: [http://localhost:5255/swagger](http://localhost:5255/swagger)
   - API Base URL: [http://localhost:5255/api](http://localhost:5255/api)

---

### Angular Frontend

1. Navigate to `ShortestPath.Frontend`:
   ```bash
   cd ShortestPath.Frontend
   ```
2. Start the frontend:
   ```bash
   npm install
   ng serve
   ```
3. Access the frontend:
   - URL: [http://localhost:4200](http://localhost:4200)

The frontend interacts with the backend to fetch available actors and compute the shortest path.

---

### Console App

1. Navigate to `ShortestPath.ConsoleApp`:
   ```bash
   cd ShortestPath.ConsoleApp
   ```
2. Run the console app:
   ```bash
   dotnet run
   ```

## Notes

The project was built and tested in Rider on macOS.