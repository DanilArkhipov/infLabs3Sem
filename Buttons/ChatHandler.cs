using System.Dynamic;
using System.Net.WebSockets;
using System.Threading.Tasks;
using WebSocketManager;

namespace Prac4
{
    public class ChatHandler : WebSocketHandler
    {
        private readonly ChatManager _chatManager;
        public ChatHandler(WebSocketConnectionManager webSocketConnectionManager,ChatManager chatManager) : base(webSocketConnectionManager)
        {
            _chatManager = chatManager;
        }
        public async Task SendMessage(string socketId, string message)
        {
            dynamic dynamicMessage = new ExpandoObject();
            dynamicMessage.UserId = socketId;
            dynamicMessage.Message = message;
            _chatManager.Messages.Add(dynamicMessage);
            await InvokeClientMethodToAllAsync("pingMessage", socketId, message);
        }
    }
}