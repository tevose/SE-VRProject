using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoomManagement.Entity;
using RoomManagement.Security;
using RoomManagement.IdGenerator;
using RoomManagement.HelperObjects;
using Grapevine;
using Newtonsoft.Json;

namespace RoomManagement {

    [RestResource]
    public static class Program {
        
        // Room ID to Room Obj mapping
        private static readonly Dictionary<string, Room> ROOMS = new ();
        
        // Client IP Address to Client Obj mapping
        private static readonly Dictionary<ClientAddress, Client> CLIENTS = new ();
        private static readonly PasswordManager PASSWORD_MANAGER = new ("");
        private static int _clientId = 1;

        public static void Main(string[] args) {
            
            // Initialises server
            using IRestServer server = RestServerBuilder.UseDefaults().Build();
            
            // Adds base routes to listen on
            server.Prefixes.Add("http://localhost:8080/");
            
            // Starts server
            server.Start();

            Console.WriteLine("Press enter to stop the server");
            Console.ReadLine();
            
            server.Stop();

        }

        [RestRoute("Get", "/test")]
        public static async Task TestRoute(IHttpContext context) {

            string[] clientEndPoint = context.Request.RemoteEndPoint.ToString().Split(":");
            string host = clientEndPoint[0];
            string port = clientEndPoint[1];
            Console.WriteLine("Client Host: " + host);
            Console.WriteLine("Client Port: " + port);
            await context.Response.SendResponseAsync();

        }

        // This route is called by the front end after the user has selected their avatar
        // When this route is called, the front end needs to send the ID of the avatar that was chosen as part of a
        // POST request
        // The backend handles the logic of creating the client
        // In response, the backend will send the front end either an OK status or a 406
        // The front end should only let the user proceed if the status code is OK
        [RestRoute("Get", "/createClient/{avatarId}")]
        public static async Task HandleCreateClient(IHttpContext context) {
            
            string avatarId = context.Request.PathParameters["avatarId"];
            Client client = CreateClient(_clientId, avatarId);
            
            string[] clientEndPoint = context.Request.RemoteEndPoint.ToString().Split(":");
            ClientAddress clientAddress = new (clientEndPoint[0], clientEndPoint[1]);

            if (!CLIENTS.ContainsKey(clientAddress)) {
                
                CLIENTS.Add(clientAddress, client);
                _clientId++;
                
                context.Response.StatusCode = HttpStatusCode.Ok;
                
            }

            else {
                context.Response.StatusCode = 406;
            }
            
            await context.Response.SendResponseAsync();

        }

        // This route is called by the front end when the client creates a room
        // When this route is called, the front end needs to send the ID of the client creating the room
        // as well as the ID of the room environment he/she has chosen (e.g. ID of cafe/forest etc) in a POST request
        // The backend handles the logic of creating the room
        // In response, the backend will send the front end a serialised (string) Json object containing all the info
        // necessary to actually create the room in the unity editor
        // The front end should only let the user proceed if the status code is OK
        [RestRoute("Get", "/createRoom/{roomEnvId}")]
        public static async Task HandleCreateRoom(IHttpContext context) {

            string[] clientEndPoint = context.Request.RemoteEndPoint.ToString().Split(":");
            ClientAddress clientAddress = new (clientEndPoint[0], clientEndPoint[1]);
            string roomEnvId = context.Request.PathParameters["roomEnvId"];

            Client client = GetClient(clientAddress);

            if (client != null) {
                
                Room room = CreateRoomForClient(client, roomEnvId);
                string serialisedRoom = JsonConvert.SerializeObject(room);
                
                context.Response.StatusCode = HttpStatusCode.Ok;
                await context.Response.SendResponseAsync(serialisedRoom);
                
            }

            else {

                context.Response.StatusCode = 406;

                await context.Response.SendResponseAsync();

            }

        }

        // This route is called by the front end when the client clicks join room after entering the room ID
        // When this route is called, the front end needs to send the ID of the client joining the room
        // as well as the inputted room ID
        // The backend handles the logic of validating whether the room ID is valid and whether the room exists
        // In response, the backend will send the front end a serialised (string) Json object containing all the info
        // necessary to actually join the room in the unity editor
        // The front end should only let the user proceed if the status code is OK
        [RestRoute("Get", "/joinRoom/{roomId}")]
        public static async Task HandleJoinRoom(IHttpContext context) {
            
            string[] clientEndPoint = context.Request.RemoteEndPoint.ToString().Split(":");
            ClientAddress clientAddress = new (clientEndPoint[0], clientEndPoint[1]);
            string roomId = context.Request.PathParameters["roomId"];

            Client client = GetClient(clientAddress);
            Room room = GetRoom(roomId);
            string serialisedRoom = JsonConvert.SerializeObject(room);

            if ((client != null) && (room != null) && JoinRoom(client, room)) {
                
                context.Response.StatusCode = HttpStatusCode.Ok;
                await context.Response.SendResponseAsync(serialisedRoom);
                
            }

            else {
                
                context.Response.StatusCode = 406;
                await context.Response.SendResponseAsync();
                
            }

        }

        // This route is called by the front end when the client clicks exit room
        // When this route is called, the front end needs to send the ID of the client exiting the room
        // The backend handles the logic of validating whether the client and room he/she wants to leave exists then
        // updates the backend to reflect the user leaving
        // In response, the backend will send the front end a status code
        // The front end should only let the user proceed to exit the room if the status code is OK
        [RestRoute("Get", "/exitRoom")]
        public static async Task HandleLeaveRoom(IHttpContext context) {
            
            string[] clientEndPoint = context.Request.RemoteEndPoint.ToString().Split(":");
            ClientAddress clientAddress = new (clientEndPoint[0], clientEndPoint[1]);
            Client client = GetClient(clientAddress);

            Room room;

            if (client != null) {
                room = GetClientRoom(client);
            }

            else {
                
                context.Response.StatusCode = 406;
                await context.Response.SendResponseAsync();

                return;
            }

            if ((room != null) && (RemoveFromRoom(client, room))) {

                if (room.Clients.Count < 1) {
                    DeleteRoom(room);
                }
                
                context.Response.StatusCode = HttpStatusCode.Ok;
                
            }

            else {
                
                context.Response.StatusCode = 406;
                
            }
            
            await context.Response.SendResponseAsync();

        }

        private static Room CreateRoomForClient(Client client, string roomEnvId) {

            string serverId = RoomIdGenerator.GenerateId(IdType.SERVER_ID);
            string roomId = RoomIdGenerator.GenerateId(IdType.ROOM_ID);
            
            Room room = Room.CreateRoom(serverId, roomEnvId);
            room.JoinRoom(client);
            
            PASSWORD_MANAGER.SetPassword(roomId);
            
            ROOMS.Add(PasswordManager.HashPassword(roomId), room);

            return room;

        }

        private static Client CreateClient(int id, string avatarId) {
            return Client.CreateClient(id, avatarId);
        }

        private static Client GetClient(ClientAddress clientAddress) {
            return CLIENTS.TryGetValue(clientAddress, out Client client) ? client : null;
        }

        private static Room GetRoom(string roomId) {
            
            if (!PASSWORD_MANAGER.ValidatePassword(roomId)) {
                return null;
            }

            return ROOMS.TryGetValue(PasswordManager.HashPassword(roomId), out Room room) ? room : null;
            
        }

        private static Room GetClientRoom(Client client) {
            return ROOMS.Values.ToList().FirstOrDefault(room => room.Clients.Contains(client));
        }
        
        private static bool JoinRoom(Client client, Room room) {
            return room.JoinRoom(client);
        }
        
        private static bool RemoveFromRoom(Client client, Room room) {
            return room.RemoveFromRoom(client);
        }

        private static void DeleteRoom(Room room) {

            KeyValuePair<string, Room> kvp = ROOMS.First(item => item.Value.Equals(room));

            ROOMS.Remove(kvp.Key);

        }

    }

}