namespace API.Entities
{
    public class Connection
    {
        public Connection(){ // Need the default constructor as this is an Entity [to prevent error]

        }

        public Connection(string connectionId, string username)
        {
            ConnectionId = connectionId;
            Username = username;
        }

        public string ConnectionId { get; set; } // By convention, EF will figure out the Id 
        public string Username { get; set; }
    }
}