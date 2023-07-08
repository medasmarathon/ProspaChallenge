## Start project

Inside this folder, you can issue this command to start the project
```
    dotnet run --project .\ProspaChallenge\ProspaChallenge.csproj
```
then the API host will be available at `http:\\localhost:5203`

Or you want to specify the specific ports and/or using HTTPS, then use this command
```
    dotnet run --project .\ProspaChallenge\ProspaChallenge.csproj --urls "http://localhost:5111;https://localhost:7222"
```
The API host will be available for HTTP call at `http:\\localhost:5111` and HTTPS call at `https:\\localhost:7222`


## API

The Lead Assessment is made by sending a POST request to `{host}/api/lead/assessment`

Sample JSON
```json
{
  "firstName": "string",
  "lastName": "string",
  "emailAddress": "aa@gg.c",
  "phoneNumber": "0396173903",
  "businessNumber": "12345678901",
  "loanAmount": 110,
  "citizenshipStatus": "Permanent Resident",
  "timeTrading": 10,
  "countryCode": "AU",
  "industry": "Allowed Industry 1"
}
```
