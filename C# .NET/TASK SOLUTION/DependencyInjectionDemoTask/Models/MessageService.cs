using DependencyInjectionDemoTask.Models.Services;

namespace DependencyInjectionDemoTask.Models
{
    public class MessageService : IMessageService
    {
        public MessageService()
        {

        }
        public string GetMessage()
        {
            return "Hello Everyone!";
        }
    }
}
