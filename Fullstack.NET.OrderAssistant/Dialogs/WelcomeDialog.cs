using System;
using System.Threading.Tasks;
using Fullstack.NET.StoreIntegration.Contract;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Optional;

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
            
            if (activity.AsConversationUpdateActivity() != null)
            {
                await context.PostAsync($"Hello. Please enter your phone number, so I can identify you.");

                context.Call(
                    new FindUserDialog(),
                    this.ResumeAfterFindUser);
            }
            else
            {
                await context.Forward(
                    new FindUserDialog(),
                    this.ResumeAfterFindUser,
                    activity);
            }

        }

        private async Task ResumeAfterFindUser(IDialogContext context, IAwaitable<Option<User>> result)
        {
            var resultFromNewOrder = await result;

            await resultFromNewOrder.Match(
                _ => ProceedToDelivery(context),
                () => HandleFailure(context));
        }

        private static Task ProceedToDelivery(IDialogContext context) 
            => context.PostAsync("Let's proceed to delivery address.");

        private static async Task HandleFailure(IDialogContext context)
        {
            await context.SayAsync(
                "Sorry, I can't recognize you. " +
                "Please wait for a human a bit, he'll take over your order.");

            context.Done(new object());
        }
    }
}