using System;
using System.Threading.Tasks;
using Fullstack.NET.StoreIntegration.Contract;
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
            
            if(activity.Type == ActivityTypes.ConversationUpdate)
            {
                await context.PostAsync($"Hello. Please enter your phone number, so I can identify you.");

                context.Call(
                    new EnterPhoneNumberDialog(),
                    this.ResumeAfterNewOrderDialog);
            }
            else
            {
                await context.Forward(
                    new EnterPhoneNumberDialog(),
                    this.ResumeAfterNewOrderDialog,
                    activity);
            }

        }

        private async Task ResumeAfterNewOrderDialog(IDialogContext context, IAwaitable<User> result)
        {
            try
            {
                var resultFromNewOrder = await result;

                await context.PostAsync($"New order dialog just told me this: {resultFromNewOrder}");
            }
            catch(InvalidOperationException)
            {
                await context.PostAsync(
                    $"Sorry, I cannot find you in our database. " +
                    $"Probably, you've entered number wrong. " +
                    $"Please, check your number for spelling errors.");
            }

            context.Wait(this.MessageReceivedAsync);
        }
    }
}