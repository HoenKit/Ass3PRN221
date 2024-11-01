using Microsoft.AspNetCore.SignalR;

namespace PRN221ExampleWeb.Hubs
{
    public class SignalRServer : Hub
    {
        public async Task NotifyBookCreated(int bookId)
        {
            // Broadcast the event to all connected clients
            await Clients.All.SendAsync("BookCreated", bookId);
        }
    }
}
