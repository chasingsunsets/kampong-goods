using Microsoft.AspNetCore.SignalR;

namespace kampong_goods.Hubss
{
    public class ChatHub : Hub
    {
        // no restriction on the parameter
        public async Task SendMessage(MessageProcessingHandler message) =>
            await Clients.All.SendAsync("receiveMessage", message);
    }
}
