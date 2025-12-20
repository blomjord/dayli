import datetime
import os.path

from pathlib import Path

from google.auth.transport.requests import Request
from google.oauth2.credentials import Credentials
from google_auth_oauthlib.flow import InstalledAppFlow
from googleapiclient.discovery import build
from googleapiclient.errors import HttpError

# If modifying these scopes, delete the file token.json.
SCOPES = ["https://www.googleapis.com/auth/calendar.readonly"]
PATH_ROOT = Path(__file__).parent
PATH_TOKEN = PATH_ROOT / "token.json"
PATH_CREDS = PATH_ROOT / "credentials.json"

def main():

  creds = None
  # The file token.json stores the user's access and refresh tokens, and is
  # created automatically when the authorization flow completes for the first
  # time.
  if os.path.exists(PATH_TOKEN):
    creds = Credentials.from_authorized_user_file(PATH_TOKEN, SCOPES)
  # If there are no (valid) credentials available, let the user log in.
  if not creds or not creds.valid:
    if creds and creds.expired and creds.refresh_token:
      creds.refresh(Request())
    else:
      flow = InstalledAppFlow.from_client_secrets_file(
          PATH_CREDS, SCOPES
      )
      creds = flow.run_local_server(port=0)
    # Save the credentials for the next run
    with open(PATH_TOKEN, "w") as token:
      token.write(creds.to_json())

  try:
    service = build("calendar", "v3", credentials=creds)

    # Call the Calendar API
    now = datetime.datetime.now(tz=datetime.timezone.utc).isoformat()
    print("Getting the upcoming 10 events in primary calendar...")
    events_result_primary = (
        service.events()
        .list(
            calendarId="primary",
            timeMin=now,
            maxResults=10,
            singleEvents=True,
            orderBy="startTime",
        )
        .execute()
    )
    events = events_result_primary.get("items", [])

    if not events:
      print("No upcoming events found.")
      return

    # Prints the start and name of the next 10 events
    for event in events:
      start = event["start"].get("dateTime", event["start"].get("date"))
      try:
        print(start.split("+")[0].replace("T", " "), event["summary"]) # Format it more nicely
      except Exception as e:
        print(e)
    
    ## Secondary calendar, (add manually)
    print(".. and secondary")
    events_result_secondary = (
        service.events()
        .list(
            calendarId="1847ded94b89ae1d2223704cdaebaf68c98b63151a6a47b456272768d4b2aa91@group.calendar.google.com",
            timeMin=now,
            maxResults=10,
            singleEvents=True,
            orderBy="startTime",
        )
        .execute()
    )
    events = events_result_secondary.get("items", [])

    if not events:
      print("No upcoming events found.")
      return

    # Prints the start and name of the next 10 events
    for event in events:
      start = event["start"].get("dateTime", event["start"].get("date"))
      try:
        print(start.split("+")[0].replace("T", " "), event["summary"]) # Format it more nicely
      except Exception as e:
        print(e)

  except HttpError as error:
    print(f"An error occurred: {error}")


if __name__ == "__main__":
  main()