# 🚦 CommuteGo

**CommuteGo** is a smart traffic assistant that helps users determine the best time to leave based on real-time traffic data, school calendars, and weather.

---

## 🗂 Project Structure

```
CommuteGo-1/
├── frontend/         # Angular UI
├── CommuteGoAPI/     # ASP.NET Core Web API
```

---

## 📦 Features (In Progress)

- ✅ Smart departure alerts (e.g. "Leave by 7:28 AM to arrive at 8:00 AM")
- ✅ Real-time traffic data (Google Maps API)
- 🏫 School calendar integration
- 🌧 Weather-aware departure adjustments
- 🚗 Alternate route suggestions
- 🔔 Push notifications
- 🎯 Rewards system for punctuality

---

## 🚀 Getting Started

### 🔧 Backend (.NET 8 Web API)

1. Navigate to the API project:

```bash
cd CommuteGoAPI
```

2. Restore and run the API:

```bash
dotnet restore
dotnet build
dotnet run
```

3. Make sure you have a valid Google Maps API key in `appsettings.json`:

```json
{
  "GoogleMapsApiKey": "YOUR_GOOGLE_MAPS_API_KEY"
}
```

---

### 🌐 Frontend (Angular)

1. Navigate to the frontend project:

```bash
cd frontend
```

2. Install dependencies and start the dev server:

```bash
npm install
ng serve
```

3. Open your browser to:  
   [http://localhost:4200](http://localhost:4200)

---

## 📡 Example API Request

```
GET /api/Traffic/Smart-Departure?origin=123+Main+St&destination=UC+Davis&arrivalTime=08:00
```

**Response:** Estimated departure time, duration, and distance.

---

## 📋 To-Do

- [ ] Connect frontend to backend
- [ ] Responsive design
- [ ] School calendar logic
- [ ] Weather integration
- [ ] Push notification setup
- [ ] Rewards logic
- [ ] Unit & integration tests

---

## 🧠 Tech Stack

- [Angular](https://angular.io/) – Frontend framework  
- [.NET 8 Web API](https://learn.microsoft.com/en-us/aspnet/core) – Backend framework  
- [Google Maps Distance Matrix API](https://developers.google.com/maps/documentation/distance-matrix) – Traffic data

---

## 📌 Author

**Sudhakhar Katta**

---

## 🏁 License

MIT

---

> This project is actively being developed and will expand with new features like calendar sync, alternative routes, and departure tracking.
