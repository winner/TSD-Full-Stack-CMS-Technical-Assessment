# Getting Started

Prerequisites: .NET 10 SDK, Node.js (for Sass compilation).

## 1. Install front-end dependencies

From the **WWW project root**:

```bash
cd TSDTechAssessment2026.WWW
npm install
```

## 2. Compile Sass

Run once to generate CSS:

```bash
npm run sass
```

Or watch for changes during development:

```bash
npm run sass:watch
```

Compiled CSS is output to `wwwroot/css/main.css`.

## 3. Start the API

Open a **separate terminal** at the repository root and run:

```bash
cd TSDTechAssessment2026.API
dotnet run --launch-profile https
```

The API will start on **https://localhost:7294** (HTTP: http://localhost:5266).

You can verify it works by visiting `https://localhost:7294/products` in a browser.

### API endpoints

| Method | URL | Description |
|--------|-----|-------------|
| GET | `/products` | Returns all products |
| GET | `/products?category=Electronics` | Returns products filtered by category |

## 4. Start the Umbraco CMS site

Open a **separate terminal** at the repository root and run:

```bash
cd TSDTechAssessment2026.WWW
dotnet run --launch-profile "Umbraco.Web.UI"
```

The CMS will start on **https://localhost:44321** (HTTP: http://localhost:13894).

**Important:** The API must be running before the CMS site loads, so the product data can be fetched.

### First run

On first startup, Umbraco performs an unattended install and creates a SQLite database. A Composer then automatically seeds the CMS with:

- A **Product Catalogue** element type (for use in Block Lists / Block Grids)
- A **Home** document type with a template
- A published **Home** content page at the root

### Umbraco backoffice

Access the backoffice at **https://localhost:44321/umbraco**

Credentials:

- Email: `admin@example.com`
- Password: `1234567890`

## 5. View the site

Open **https://localhost:44321/** in your browser.

You should see a product catalogue page with:

- A responsive grid of product cards with images, names, prices, ratings, and categories
- Products marked as **Unavailable** (red badge, greyed out) when `active` is `false`
- Products marked as **Out of Stock** (orange badge) when all variants have zero stock

## Running with Docker

Prerequisites: Docker and Docker Compose.

From the **repository root**, run:

```bash
docker compose up --build
```

This builds and starts both services:

| Service | URL | Description |
|---------|-----|-------------|
| API | http://localhost:5266/products | Product data API |
| WWW | http://localhost:13894 | Umbraco CMS site |

The Sass compilation, .NET restore, and publish all happen inside the Docker build stages automatically. On first startup the Umbraco unattended install and CMS seeding run just like the local setup.

To stop the containers:

```bash
docker compose down
```

To stop and remove the persisted SQLite database volume:

```bash
docker compose down -v
```

## Using Rider

If you are using Rider, you can run both the API and CMS simultaneously using the **"Run All"** compound build target defined in `.run/Run All.run.xml`.

## Project structure

```
TSDTechAssessment2026.API/          .NET 10 Web API
  data/products.json                Stubbed product data (30 products)
  Models/Product.cs                 API models
  Services/ProductService.cs        Reads and caches products.json
  Program.cs                        Minimal API endpoint + CORS

TSDTechAssessment2026.WWW/          Umbraco 17 CMS
  Models/Product.cs                 Client-side product models
  Services/ProductApiClient.cs      HttpClient wrapper for the API
  ViewComponents/                   ProductCatalogueViewComponent
  Views/Home.cshtml                 Home page template
  Views/Shared/Components/          ViewComponent Razor views
  Views/Partials/blockgrid/         Block Grid partial views
  Views/Partials/blocklist/         Block List partial views
  Composing/                        CMS seeder (auto-creates content types)
  wwwroot/scss/                     Sass source files
  wwwroot/css/                      Compiled CSS output
  wwwroot/media/images/             Product stock imagery
```
