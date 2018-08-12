using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Fullstack.NET.OrderAssistant.Dialogs
{
    [Serializable]
    public class WelcomeDialog : IDialog
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            
            await context.PostAsync($"Hello. Please enter your phone number, so I can identify you.");

            context.Call(
                new EnterPhoneNumberDialog(),
                this.ResumeAfterNewOrderDialog);

            context.Wait(MessageReceivedAsync);
        }

        private async Task ResumeAfterNewOrderDialog(IDialogContext context, IAwaitable<User> result)
        {
            var resultFromNewOrder = await result;

            await context.PostAsync($"New order dialog just told me this: {resultFromNewOrder}");

            // Again, wait for the next message from the user.
            context.Wait(this.MessageReceivedAsync);
        }
    }
}