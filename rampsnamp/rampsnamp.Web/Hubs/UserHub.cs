using Microsoft.AspNet.SignalR;

namespace rampsnamp.Web.Hubs
{
    public class UserHub : Hub
    {
        public void Send(string firstname)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<UserHub>();
            context.Clients.All.addNewMessageToPage(firstname);    
        }
    }
}