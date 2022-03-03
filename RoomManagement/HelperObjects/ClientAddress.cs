using System;

namespace RoomManagement.HelperObjects {

    public class ClientAddress : IEquatable<ClientAddress> {

        internal string Host { get; }
        internal string Port { get; }

        public ClientAddress(string host, string port) {
            this.Host = host;
            this.Port = port;
        }

        public override bool Equals(object obj) {
            return this.Equals(obj as ClientAddress);
        }

        public override int GetHashCode() {
            return 17 * 7 + HashCode.Combine(this.Host, this.Port);
        }

        public bool Equals(ClientAddress clientAddress) {
            return (clientAddress != null) && (this.Host == clientAddress.Host) && (this.Port == clientAddress.Port);
        }

    }

}