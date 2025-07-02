using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using CommuteGoApi.Models;
using System.Net.WebSockets;


/*
 
 Features to add
💡 Bonus: Expand It Later
	•	Add walking/public transit/bike options.
	•	Build in a rewards system for punctuality.
    Smart departure alerts: Not just traffic updates, but “Leave by 7:28 AM today to arrive at 8:00 AM”.
	•	School calendar integration: Days off, delayed openings, exam days, etc.
	•	Alternative route suggestions: If there’s an accident or heavy traffic.
	•	Weather-based timing adjustments: Rain or snow might require earlier departure.
	•	Push notifications: So you don’t forget to check manually
 */


namespace TrafiicTracksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrafficController : ControllerBase
    {

        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public TrafficController(IConfiguration config, IHttpClientFactory httpClient) {

            _config = config;
            _httpClient = httpClient.CreateClient();
        }

            [HttpGet("Smart-Departure")]
            public async Task<IActionResult> GetTraffic([FromQuery] string destination, [FromQuery] string origin, [FromQuery] string arrivalTime)
            {

            var api_key = _config["GoogleMapsApiKey"];

            var origin_encoded = Uri.EscapeDataString(origin);
            var destination_encoded = Uri.EscapeDataString(destination);

            var url = $"https://maps.googleapis.com/maps/api/distancematrix/json" +
                      $"?origins={origin_encoded}" +
                      $"&destinations={destination_encoded}" +
                      $"&units=imperial" +
                      $"&departure_time=now" +
                      $"&traffic_model=pessimistic" +
                      $"&key={api_key}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Failed To Get Data");
            }

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<Traffic>(json);

            
            int durationInSeconds = data.rows[0].elements[0].duration_in_traffic.value;


            if (!DateTime.TryParse(arrivalTime, out var parsedArrival))
                return BadRequest("Invalid arrival time. Use format HH:mm");


            var now = DateTime.Now;
            var arrival = new DateTime(now.Year, now.Month, now.Day, parsedArrival.Hour, parsedArrival.Minute, 0);

            
            var recommend_departure = arrival.AddSeconds(-durationInSeconds);


            return Ok(new
            {
                origin = data.origin_addresses.FirstOrDefault(),
                destination = data.destination_addresses.FirstOrDefault(),
                distance = data.rows[0].elements[0].distance.text,
                duration = data.rows[0].elements[0].duration.text,
                arrival_time = arrival.ToString("hh:mm tt"),
                leave_by = recommend_departure.ToString("hh:mm tt")
            });
        }



    }
}

