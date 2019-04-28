# mock-signalr

A simple implementation of a SignalR server for testing and development purposes.

Currently only supports a single hub and stimulating server push.

Not tested in Linux or Mac yet!

## Prerequisites

The SignalR server requires .NET core to be installed. https://dotnet.microsoft.com/download

## Getting started

Run the command:

`mock-signalR -origins "http://127.0.0.1:8080, http://127.0.0.1:8000" -hub "chatHub" -port 5005`

The server can be started uisng the `mock-signar` command. 

1. You must specify a CORS origin with `-origins`, for example `-origins http://127.0.0.1:8080`. Multiple origins can be specified by separating them with a comma, but the origins argument must be contained in quotes, for example: `-origins "http://127.0.0.1:80, http://127.0.0.1:8080"`.

2. You must specify the hub to mock with the `-hub` command. For example: `-hub chatHub`.

3. A port can be specified with the `-port` command, for example `-port 4000`. If not specified it will default to 5000.

4. You can stimulate the server to send a response by sending a POST command to `http://localhost:5000/api/send/{myMethod}` (note: use the sepcified port if configured). The body must be a JSON payload.

## Development

You can start the sample/testing site by running `npm run build-site` and `npm run start-site`.

## License

Licensed under MIT.