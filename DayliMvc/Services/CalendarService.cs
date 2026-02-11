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
        var shared = File.ReadAllText(@"wwwroot\calendarShared.txt");

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

            DateTimeOffset now = DateTime.Now;
            DateTimeOffset MaxTimeLimit = now.AddHours(24);

            // Private calendar
            EventsResource.ListRequest request_primary = service.Events.List("primary");
            request_primary.TimeMinDateTimeOffset = now;
            request_primary.TimeMaxDateTimeOffset = MaxTimeLimit;
            request_primary.ShowDeleted = false;
            request_primary.SingleEvents = true;
            request_primary.MaxResults = 10;
            request_primary.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // Secondary calendar (optional)
            EventsResource.ListRequest request_shared = service.Events.List(shared);
            request_shared.TimeMinDateTimeOffset = now;
            request_shared.TimeMaxDateTimeOffset = MaxTimeLimit;
            request_shared.ShowDeleted = false;
            request_shared.SingleEvents = true;
            request_shared.MaxResults = 10;
            request_shared.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // List current events
            Events events_primary = request_primary.Execute();
            Events events_shared = request_shared.Execute();

            var primaryNotNull = true;
            var sharedNotNull = true;

            if (events_primary == null || events_primary.Items.Count == 0)
            {
                Console.WriteLine("No events in private calendar");
                primaryNotNull = false;
            }
            if (events_shared == null || events_shared.Items.Count == 0)
            {
                Console.WriteLine("No events in shared calendar.");
                sharedNotNull = false;
            }

            if (primaryNotNull && sharedNotNull)
            {
                // TODO: Implement response join
            }
            else if (sharedNotNull)
            {
                foreach (var eventItem in events_shared.Items)
                {
                    string when = eventItem.Start.DateTimeDateTimeOffset.ToString();
                    if (string.IsNullOrEmpty(when))
                    {
                        when = eventItem.Start.Date;    
                    }
                    Console.WriteLine("{0} ({1})", eventItem.Summary, when);
                }                
            }
            else if (primaryNotNull)
            {
                foreach (var eventItem in events_primary.Items)
                {
                    string when = eventItem.Start.DateTimeDateTimeOffset.ToString();
                    if (string.IsNullOrEmpty(when))
                    {
                        when = eventItem.Start.Date;    
                    }
                    Console.WriteLine("{0} ({1})", eventItem.Summary, when);
                }
            }
            return null;
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
}