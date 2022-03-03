using System;

namespace RoomManagement.Entity {

    public class Client : IEquatable<Client> {

        private Client(int id, string avatarId) {
            this.Id = id;
            this.Avatar = avatarId;
        }

        internal int Id { get; }
        internal string Avatar { get; }

        internal static Client CreateClient(int id, string avatarId) {
            return new Client(id, avatarId);
        }

        public override bool Equals(object obj) {
            return this.Equals(obj as Client);
        }

        public override int GetHashCode() {
            return 17 * 7 + HashCode.Combine(this.Id, this.Avatar);
        }

        public bool Equals(Client client) {
            return (client != null) && (this.Id == client.Id) && (this.Avatar == client.Avatar);
        }

    }

}