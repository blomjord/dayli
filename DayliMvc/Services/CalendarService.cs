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
using DayliMvc.Models.CalendarData;

#nullable enable

namespace DayliMvc.Services;
public class CalendarService
{
    static private string RootPath = @"I:\Programming\dayliCredentials\";
    static string[] Scopes = [Google.Apis.Calendar.v3.CalendarService.Scope.CalendarReadonly];
    static string applicationName = "Google Calendar API for dayli project";

    public static async Task<List<Event>> GetCalendarDailyEvents(int numberOfDays)
    {
        var shared = File.ReadAllText(@"wwwroot\calendarShared.txt");

        try
        {
            // Authentication
            UserCredential credential;
            using (var stream =
                    new FileStream(RootPath + "credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = RootPath + "token.json";
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true));
                Console.WriteLine("Credetial file saved at: " + credPath);
            }

            // Create calendar service
            var service = new Google.Apis.Calendar.v3.CalendarService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName
            });

            DateTimeOffset Today = DateTime.Today;
            DateTimeOffset MaxTimeLimit = DateTime.Today.AddDays(numberOfDays);

            // Needs to be two separate requests, combine afterwards
            EventsResource.ListRequest request_primary = service.Events.List("primary");
            request_primary.TimeMinDateTimeOffset = Today;
            request_primary.TimeMaxDateTimeOffset = MaxTimeLimit;
            request_primary.ShowDeleted = false;
            request_primary.SingleEvents = true;
            request_primary.MaxResults = 10;
            request_primary.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
            Events events_primary = request_primary.Execute();

            EventsResource.ListRequest request_shared = service.Events.List(shared);
            request_shared.TimeMinDateTimeOffset = Today;
            request_shared.TimeMaxDateTimeOffset = MaxTimeLimit;
            request_shared.ShowDeleted = false;
            request_shared.SingleEvents = true;
            request_shared.MaxResults = 10;
            request_shared.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
            Events events_shared = request_shared.Execute();

            var combined = new List<Event>();

            if (events_primary != null && events_primary.Items.Count != 0)
            {
                combined.AddRange(events_primary.Items);
            }

            if (events_shared != null && events_shared.Items.Count != 0)
            {
                combined.AddRange(events_shared.Items);
            }

            combined = combined
                .OrderBy(e =>
                    e.Start.DateTimeDateTimeOffset ??
                    (e.Start != null 
                        ? DateTimeOffset.Parse(e.Start.Date)
                        : DateTimeOffset.MaxValue)
                ).ToList();
            return combined;     
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine(e.Message);
            return new List<Event>();
        }
    }
}