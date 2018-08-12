using System;
using System.Threading.Tasks;
using Fullstack.NET.StoreIntegration;
using Fullstack.NET.StoreIntegration.Contract;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Fullstack.NET.OrderAssistant.Dialogs
{
    [Serializable]
    public class EnterPhoneNumberDialog : IDialog<User>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            if (message.Type == ActivityTypes.Message)
            {
                var apiClient = new StoreClient();

                (await apiClient.FindUser(message.Text))
                    .Match(
                        context.Done,
                        () => context.Fail(new InvalidOperationException("User was not found")));
            }
            else
            {
                context.Wait(this.MessageReceivedAsync);
            }
        }
    }
}