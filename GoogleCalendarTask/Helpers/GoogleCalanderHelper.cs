using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google_Calendar_Task.Models;
using GoogleCalendarTask.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GoogleCalendarTask.Helpers
{
    public class GoogleCalanderHelper
    {
        protected GoogleCalanderHelper()
        {

        }

        public static async Task<Event> CreateGoogleCalander(GoogleCalander request)
        {
            string[] Scopes = { "https://www.googleapis.com/auth/calendar" };
            string ApplicationName = "Google Calender API";
            UserCredential credential;
            using (var stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "Credintials", "GoogleCalander.json"), FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                     GoogleClientSecrets.Load(stream).Secrets,
                     Scopes,
                     "user",
                     CancellationToken.None,
                     new FileDataStore(credPath, true)).Result;
            }


            // define services

            var services = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,

            });

            // define request

            Event eventCalendar = new Event()
            {
                Summary = request.Summary,
                Location = request.Location,
                Start = new EventDateTime
                {
                    DateTime = request.Start,
                    TimeZone = "Africa/Cairo",

                },
                End = new EventDateTime
                {
                    DateTime = request.End,
                    TimeZone = "Africa/Cairo",
                },
                Description = request.Description
            };

            var eventRequest = services.Events.Insert(eventCalendar, "primary");
            var requestCreate = await eventRequest.ExecuteAsync();


            return requestCreate;
        }

        public static async Task<object> GetAll(PaginationRequest request)
        {
            string[] Scopes = { "https://www.googleapis.com/auth/calendar" };
            string ApplicationName = "Google Calender API";
            UserCredential credential;
            using (var stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "Credintials", "GoogleCalander.json"), FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                     GoogleClientSecrets.Load(stream).Secrets,
                     Scopes,
                     "user",
                     CancellationToken.None,
                     new FileDataStore(credPath, true)).Result;
            }


            // define services

            var services = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,

            });


            // Authenticate and initialize services as in the CreateGoogleCalander method

            var listRequest = services.Events.List("primary");
            listRequest.PageToken = request.PageToken;
            listRequest.MaxResults = request.Take;
            var eventDetails = await listRequest.ExecuteAsync();

            return eventDetails;
        }

        public static async Task<Event> GetGoogleCalanderEvent(string eventId)
        {

            string[] Scopes = { "https://www.googleapis.com/auth/calendar" };
            string ApplicationName = "Google Calender API";
            UserCredential credential;
            using (var stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "Credintials", "GoogleCalander.json"), FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                     GoogleClientSecrets.Load(stream).Secrets,
                     Scopes,
                     "user",
                     CancellationToken.None,
                     new FileDataStore(credPath, true)).Result;
            }


            // define services

            var services = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,

            });


            // Authenticate and initialize services as in the CreateGoogleCalander method

            var eventRequest = services.Events.Get("primary", eventId);
            var eventDetails = await eventRequest.ExecuteAsync();

            return eventDetails;
        }
        public static async Task DeleteGoogleCalanderEvent(string eventId)
        {

            string[] Scopes = { "https://www.googleapis.com/auth/calendar" };
            string ApplicationName = "Google Calender API";
            UserCredential credential;
            using (var stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "Credintials", "GoogleCalander.json"), FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                     GoogleClientSecrets.Load(stream).Secrets,
                     Scopes,
                     "user",
                     CancellationToken.None,
                     new FileDataStore(credPath, true)).Result;
            }


            // define services

            var services = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,

            });


            // Authenticate and initialize services as in the CreateGoogleCalander method

            var eventDeleteRequest = services.Events.Delete("primary", eventId);
            await eventDeleteRequest.ExecuteAsync();
        }
    }
}
