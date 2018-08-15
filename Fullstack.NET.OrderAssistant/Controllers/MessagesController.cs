using System;
using System.Threading.Tasks;
using System.Web.Http;
using Fullstack.NET.OrderAssistant.Dialogs;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Fullstack.NET.OrderAssistant
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        public async Task<IHttpActionResult> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                await SendTypingIndicator(activity);

                await Conversation.SendAsync(activity, () => new Welcome());
            }
            else
            {
                await HandleSystemMessage(activity);
            }

            return this.Ok();
        }

        private async Task<Activity> HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                if (message.From.Id != message.Recipient.Id)
                {
                    await Conversation.SendAsync(message, () => new Welcome());
                }
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
            }
            else if (message.Type == ActivityTypes.Typing)
            {
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }

        private static async Task SendTypingIndicator(Activity activity)
        {
            var typing = activity.CreateReply();

            typing.Type = ActivityTypes.Typing;

            using (var connector = new ConnectorClient(new Uri(activity.ServiceUrl)))
            {
                await connector.Conversations.ReplyToActivityAsync(typing);
            }
        }
    }
}