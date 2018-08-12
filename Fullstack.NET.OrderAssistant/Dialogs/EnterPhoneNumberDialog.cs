using System;
using System.Threading.Tasks;
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

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            
            await context.PostAsync($"Hello. Please enter your phone number, so Ican identify you.");

            context.Wait(MessageReceivedAsync);
        }
    }
}