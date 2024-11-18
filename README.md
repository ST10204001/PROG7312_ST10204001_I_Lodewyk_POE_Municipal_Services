# South African Municipality Application

## Overview
The South African Municipality Application is a WPF-based desktop application designed to allow users to report various community issues, view local events, and track the status of service requests. The application integrates tree algorithms, heaps, and graphs to efficiently manage service requests and their relationships. The Service Request Status feature, which is central to the application, uses advanced data structures like heaps, graphs, and trees (e.g., Minimum Spanning Tree (MST), Binary Search Tree (BST), Red-Black Tree (RBT)) to optimize the process of managing and filtering requests.

## Getting Started

### Prerequisites
Before running the project, ensure the following tools and dependencies are installed:

- Git: To clone the repository. Download Git here
- Visual Studio 2019 (or later): Download Visual Studio 2019 here
- WPF .NET Framework 4.8 or later
- Newtonsoft.Json NuGet Package: Latest version for handling JSON parsing.
- service_requests.json file: Should be stored within the Resources folder.

Cloning the Project
- Open Visual Studio.
- Navigate to File > Start Window. If it doesn’t appear automatically, go to - File > Open > Start Window.
- Select Clone a repository in Visual Studio.
- Copy the repository URL from GitHub:
https://github.com/ST10204001/PROG7312_ST10204001_I_Lodewyk_POE_Municipal_Services
- Paste the URL into the Repository Location field.
- Choose a directory to store the project and click Clone.
- After cloning, the project will open automatically in Visual Studio

### Installation

1. **Unzip Project Folder**: Extract Project in preferred location.

2. **Open the Project**: Open the solution file (`PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.sln`) in Visual Studio.

3. **Build the Project**: Build the project by selecting `Build > Build Solution` in the Visual Studio menu.

4. **Run the Application**: Start the application by pressing `F5` or selecting `Debug > Start Debugging` in Visual Studio.

## Usage

### Running the Application
1. In Visual Studio, go to the top menu and select Build > Build Solution.
2. Once the solution is built successfully, click the Start button or press F5 to launch the application.

### Part 1 - Reporting an Issue:
1. Open the application.
2. Choose "Reporting an Issue" option from the menu screen.
3. Fill in the **Location** field with the issue's location.
4. Select the **Category** from the dropdown menu.
5. Provide a detailed **Description** of the issue in the `RichTextBox`.
6. Click the **Attach Media** button to upload images or documents related to the issue.
7. Click **Submit** to finalise the report.

#### Reporting an Issue Features 
- **Location Input**: Users can specify the location of the reported issue.
- **Category Selection**: A dropdown menu allows users to select the category of the issue (e.g., sanitation, roads, utilities).
- **Description Box**: A `RichTextBox` is available for users to provide a detailed description of the issue.
- **Media Attachment**: Users can attach images or documents related to the reported issue using an integrated file dialog.
- **Submission**: A "Submit" button finalizes the report and displays a summary of the submitted information.
- **Navigation**: Users can navigate between different sections of the app, including returning to the main menu.
- **Progress Bar and percentage**: Increments every time user fills in inputs.

### Part 2 - Events Page:
1. Open the application.
2. Choose "Events" from the menu screen to view community events.
3. Users can see a list of upcoming events related to municipal activities.
4. Clicking on an event provides more detailed information, including the event's location, date, and description.

#### Events Page Features
- Event List: Displays a list of upcoming municipal events for user awareness.
- Event Details: Users can click on an event to view detailed information, including location, date, and description.
- Search Functionality: Filter events by category or date.
- Recent Searches: View past search queries.
- Recommendations: Get event recommendations based on user preferences.
- Clear Filters: Reset all filter and search settings

### POE - Status Service Request Page:
- Service Request Graphs: Visualize the relationships between service requests using a graph.
- Minimum Spanning Tree (MST): Calculate the most efficient way to address service requests, displaying their dependencies.
- Service Request Filtering: Filter requests by priority (using a Heap) or status.
- Search by Request description: Search for requests by their description using a Adelson-Velsky-Landis Tree (AVLTree).
- Priority queue: Sorts requests from highest to lowest priority.

### Data Structures
#### Graph and MST Features:
- Type: Graph (Adjacency List)
- Purpose: Models service requests and their dependencies, where nodes represent service requests and edges represent relationships between them.
- Efficiency: Efficient traversal of related requests with a time complexity of O(n) for finding dependencies.
- Example: Service requests that are interdependent.

#### Minimum Spanning Tree (MST)
- Type: Tree (MST)
- Purpose: Computes the most efficient way to connect all service requests, reducing the number of dependencies that need to be processed.
- Efficiency: The MST is computed using algorithms like Prim's or Kruskal's with a time complexity of O(E log V), where E is the number of edges and V is the number of vertices.
- Example: Display only the most critical or cost-effective relationships between service requests.

#### Red-Black Tree (RBT)
- Type: Tree (Red-Black Tree)
- Purpose: Used to get issues submitted by the user.
- Efficiency: Supports O(log n) time complexity for insertion, deletion, and search operations.
- Example: .

### Navigating the App:
The main menu of the application provides easy access to the three main features:

- Report Issues (Part 1)
- Local Events (Part 2)
- Request Status (POE)

#### Other Navigation features:
- Use the **Back Arrow** button to return to the main menu.
- Use the **Submit** button to confirm and send the report.

## Code Structure
### Part 1- Report Issues
- `ReportIssuesView.xaml`: Defines the user interface of the issue reporting control.
- `ReportIssuesView.xaml.cs`: Contains the logic for handling user interactions, including form submission and media attachment.
- `MainWindow.xaml`: The main window that hosts the `ReportIssuesView`.
- `MainViewModel.cs`: Manages navigation and other main window functionalities.

### Part 2 - Local Events and Announcements


### POE- Service Requests
- `ServiceRequestViewModel.cs`: The ViewModel for handling service request data, including searching, filtering, and generating the MST.
- `ServiceRequest.cs`: Defines the service request data model.
- `Graph.cs`: Implements the graph structure, including the ServiceRequest nodes and the adjacency list. It also includes methods for adding requests and edges, as well as calculating the MST.
- `MinimumSpanningTree.cs`: Contains the logic for computing the MST using algorithms like Prim’s or Kruskal’s.
- `Edge.cs`: Represents an edge in the graph, connecting two ServiceRequest nodes and containing a weight for that connection.
- `RedBlackTree.cs`: A self-balancing tree structure used to sort service requests by submission date.

## Author/Developer
Imrah Lodewyk (ST10204001)
