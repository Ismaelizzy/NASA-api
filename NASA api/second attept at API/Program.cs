using NASAApi;
using System.Diagnostics;
using System;

using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{

    static async Task Main(string[] args)
    {

        Console.WriteLine("Welcome to Coordinate Downloader!");
        Console.WriteLine("The place with all the earth pictures you would ever want.\n");
        Console.WriteLine("You will be asked to input some coordinates.\n");
        Console.WriteLine("If you have the decimal value, you can input it now.\n");
        Console.WriteLine("If not, it's okay. Just input the degrees for latitude as a negative that will work on this side of the world\n");
        Console.WriteLine("Lastly, try to make the inputs as accurate as possible for the best results. Have fun!");

        Console.WriteLine("expamples\nJohnsoncity: 36.3134400,-82.3534700\nNew York: 40.71427,-74.00597\nColorado Springs: 38.8338800,-104.8213600\n ");

        string apiKey = "uihMuiMNDYxhjM0hbMxUxQCFadewhyC00494E2u3";
        double latitude = 0;
        double longitude = 0;
        string date = "2019-02-01";
        double dim = 0.20;
        double cloudScore = 0;
        bool validInput = false;

        while (!validInput)
        {
            Console.Write("Please enter latitude: ");
            string latitudeInput = Console.ReadLine();
            if (double.TryParse(latitudeInput, out latitude))
            {
                validInput = true;
            }
            else
            {
                Console.WriteLine("Invalid latitude format. Please enter a valid number.");
            }
        }

        validInput = false;

        while (!validInput)
        {
            Console.Write("Please enter longitude: ");
            string longitudeInput = Console.ReadLine();
            if (double.TryParse(longitudeInput, out longitude))
            {
                validInput = true;
            }
            else
            {
                Console.WriteLine("Invalid longitude format. Please enter a valid number.");
            }
        }
    
        string apiUrl = $"https://api.nasa.gov/planetary/earth/imagery?lon={longitude}&lat={latitude}&date={date}&dim={dim}&api_key={apiKey}";



        using (HttpClient client2 = new HttpClient())
        {
            try
            {
                // Send the GET request to the API
                HttpResponseMessage response = await client2.GetAsync(apiUrl);

                // Check if the request was successful (status code 200)
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("What would like to save your picture as?");
                    var location = Console.ReadLine();
                    // Read the response content as a stream
                    using (var stream = await response.Content.ReadAsStreamAsync())

                    using (var fileStream = File.Create($"{location}.jpg")) // Save the image to a file
                    {
                        // Copy the image stream to the file stream
                        await stream.CopyToAsync(fileStream);
                    }

                    Console.WriteLine("Image downloaded successfully.");
                    

                    Process.Start("explorer.exe", $"{location}.jpg");
                }
                else
                {

                    Console.WriteLine($"Failed to get image");
                    
                }
            }
            catch (HttpRequestException e)
            {
                ErrorLogger.LogError(e);
                Console.WriteLine(e.Message);
            }
        }



    }
}

