# Finding Tracker Application

A web application for tracking and managing findings such as coins and artifacts. Users can search, filter, and manage findings, view detailed information, and access user profiles.

## Features

- **Finding Management**: 
  - Add, edit, and delete findings.
  - Categorize findings as coins or artifacts.
- **Advanced Search & Filters**:
  - Filter findings by name, username, or type (coin/artifact).
- **User Profiles**:
  - View user profiles with their associated findings.
  - Profile access via clean URLs (e.g., `/Profile/ProfileReview/Username`).
- **Map View**:
  - Visualize findings on a map.
- **Photo Support**:
  - Upload and display photos of findings.

## Technology Stack

- **Backend**: ASP.NET Core MVC
- **Frontend**: Razor Pages, Bootstrap
- **Database**: Entity Framework Core with SQL Server
- **Authentication**: ASP.NET Identity
- **Version Control**: Git
- **Map**: Leaflet.js
- **Email sending service**: Mimekit, Net.Mailkit
- **SMTP provider**: mailersend.com
