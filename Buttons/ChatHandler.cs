using System.Dynamic;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebSocketManager;

namespace Prac4
{
    public class ChatHandler : WebSocketHandler
    {
        private readonly ChatManager _chatManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ChatHandler(WebSocketConnectionManager webSocketConnectionManager,ChatManager chatManager,IHttpContextAccessor httpContextAccessor) : base(webSocketConnectionManager)
        {
            _chatManager = chatManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task SendMessage(string message)
        {
            dynamic dynamicMessage = new ExpandoObject();
            dynamicMessage.UserId = _httpContextAccessor.HttpContext.Session.GetString("name");
            if (dynamicMessage.UserId != null)
            {
                dynamicMessage.Message = message;
                _chatManager.Messages.Add(dynamicMessage);
                await InvokeClientMethodToAllAsync("pingMessage", dynamicMessage.UserId, message);
            }
        }
    }
}