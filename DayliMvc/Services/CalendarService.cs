using DayliMvc.Models;
using System.Net.Http;
using System.Text.Json;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Threading;

#nullable enable

namespace DayliMvc.Services;
public class CalendarService
{
    static private string RootPath = @"I:\Programming\dayliCredentials\";
    static string[] Scopes = [Google.Apis.Calendar.v3.CalendarService.Scope.CalendarReadonly];
    static string applicationName = "Google Calendar API for dayli project";
    public static async Task<CalendarDataFront?> GetCalendarDailyEvents()
    {
        try
        {
            // Authentication
            UserCredential credential;
            using (var stream =
                    new FileStream(RootPath + "credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = RootPath + "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credetial file saved at: " + credPath);
            }

            // Create calendar service
            var service = new Google.Apis.Calendar.v3.CalendarService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName
            });

            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMinDateTimeOffset = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // List current events
            Events events = request.Execute();
            Console.WriteLine("Upcoming events:");

            if (events == null || events.Items.Count == 0)
            {
                Console.WriteLine("NO events");
            }

            foreach (var eventItem in events.Items)
            {
                string when = eventItem.Start.DateTimeDateTimeOffset.ToString();
                if (String.IsNullOrEmpty(when))
                {
                    when = eventItem.Start.Date;    
                }
                Console.WriteLine("{0} ({1})", eventItem.Summary, when);
            }
            // For testing
            HttpClient httpClient = new HttpClient();
            try
            {
                HttpResponseMessage pointDataResponse = await httpClient.GetAsync($"https://jsonplaceholder.typicode.com/todos/1");
                pointDataResponse.EnsureSuccessStatusCode();
                string pointData = await pointDataResponse.Content.ReadAsStringAsync();
                var calendarData = JsonSerializer.Deserialize<CalendarDataFront>(pointData);
                return calendarData;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e}");
                return null;
            }

        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
}