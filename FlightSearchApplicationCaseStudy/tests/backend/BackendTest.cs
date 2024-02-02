using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;
using FlightSearchApplicationCaseStudy.entities;

/**
 * @author Humeyra Koseoglu
 * @since 1.02.2024
 */

namespace FlightSearchApplicationCaseStudy.tests.backend
{
    public class BackendTests
    {
        private RestClient _client;
        private readonly string _apiUrl = "https://flights-api.buraky.workers.dev/";

        // Initialize the RestClient before each test
        [SetUp]
        public void Setup()
        {
            _client = new RestClient();
        }

        /*  Check the HTTP status code of the API response
            GET requests should return status code 200  */
        [Test]
        public void CheckStatusCode()
        {
            // Creating a GET request to the API
            RestRequest request = new(_apiUrl, Method.Get);

            // Executing the request and getting the response
            RestResponse response = _client.Execute(request);

            // Asserting that the status code is equal to 200 status code
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        /*  Test method to check the content of the API response
            The response from GET requests must be in the Object[string->Array[Flight]] structure   */
        [Test]
        public void CheckResponseContent()
        {
            RestRequest request = new(_apiUrl, Method.Get);
            RestResponse response = _client.Execute(request);

            /*
             Console.WriteLine(response.Content);
            */

            // Parsing the JSON content of the response
            var json = JObject.Parse(response.Content!);
            // Deserializing the JSON into an array of Flight objects
            var flightResponse = JsonConvert.DeserializeObject<Flight[]>(json["data"]!.ToString());
            // Asserting that the response structure is not null
            Assert.IsNotNull(flightResponse, "Response does not contain the expected structure.");

            // Looping through each flight and checking specific conditions
            for (var i = 0; i < flightResponse.Length; i++)
            {
                var flight = flightResponse[i];

                Assert.Greater(flight.Id, 0, "Flight Id should greater than 0.");   // Asserting that the Flight Id is greater than 0
                Assert.IsNotEmpty(flight.From, "Flight From should empty.");    // Asserting that the From field is not empty
                Assert.IsNotEmpty(flight.To, "Flight To should not empty.");    // Asserting that the To field is not empty
                Assert.IsNotEmpty(flight.Date, "Flight Date should not empty.");    // Asserting that the Date field is not empty
            }
        }

        /*  Test method to check the Content-Type header of the API response
            The response from GET requests must have a "Content-Type" header and its value must be "application/json"   */
        [Test]
        public void CheckHeader()
        {
            RestRequest request = new(_apiUrl, Method.Get);
            RestResponse response = _client.Execute(request);

            /*            
            Console.WriteLine("Response Headers:");
            foreach (var header in response.Headers)
            {
                Console.WriteLine($"{header.Name}: {header.Value}");
            }
          */

            // Checking if the Content-Type header is present and has the value "application/json"
            if (response.Headers!.Any(header => header.Name!.Equals("Content-Type", StringComparison.OrdinalIgnoreCase)
                                           && header.Value!.ToString()!.Equals("application/json", StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Baþlýk kontrolü baþarýlý. Content-Type: application/json");
            }
            else
            {
                Console.WriteLine("Baþlýk kontrolü baþarýsýz. Content-Type: " + response.Headers!.FirstOrDefault(h => h.Name!.Equals("Content-Type"))?.Value);
            }
        }

        [TearDown]
        public void TearDown()
        {
            // Cleanup resources if needed
        }
    }



}