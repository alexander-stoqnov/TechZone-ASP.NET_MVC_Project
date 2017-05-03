namespace TechZone.Web.Hubs
{
    using Microsoft.AspNet.SignalR;

    public class TechChat : Hub
    {
        public void ReceiveMessage(string name, string message)
        {
            Clients.All.receiveMessage(name, message);
        }
    }
}