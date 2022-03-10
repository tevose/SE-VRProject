using System;
using System.Collections.Generic;

namespace RoomManagement.Entity {

    public class Room : IEquatable<Room> {

        internal List<Client> Clients { get; }
        internal string ServerId { get; }
        
        internal string RoomId { get; }
        internal string EnvironmentId { get; }

        private Room(string serverId, string environmentId, string roomId) {
            this.ServerId = serverId;
            this.EnvironmentId = environmentId;
            this.RoomId = roomId;
            this.Clients = new List<Client>();
        }

        internal static Room CreateRoom(string serverId, string environmentId, string roomId) {
            return new Room(serverId, environmentId, roomId);
        }

        internal bool JoinRoom(Client client) {

            if ((this.Clients.Count > 1) || this.Clients.Contains(client)) {
                return false;
            }

            this.Clients.Add(client);

            return true;

        }

        internal bool RemoveFromRoom(Client client) {

            if (this.Clients.Contains(client)) {
                
                this.Clients.Remove(client);

                return true;
            }

            return false;

        }

        public override bool Equals(object obj) {
            return this.Equals(obj as Room);
        }

        public override int GetHashCode() {
            return 17 * 7 + HashCode.Combine(this.ServerId, this.EnvironmentId, this.RoomId);
        }

        public bool Equals(Room room) {
            return (room != null) && (this.ServerId == room.ServerId) && (this.EnvironmentId == room.EnvironmentId)
            && (this.RoomId == room.RoomId);
        }

    }

}