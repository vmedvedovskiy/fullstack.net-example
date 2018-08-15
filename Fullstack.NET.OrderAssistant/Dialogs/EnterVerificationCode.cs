using System;
using System.Threading.Tasks;
using Fullstack.NET.StoreIntegration;
using Fullstack.NET.StoreIntegration.Contract;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Optional;

namespace Fullstack.NET.OrderAssistant.Dialogs
{
    [Serializable]
    public class EnterVerificationCode : IDialog<Option<User>>
    {
        private int attempts = 3;
        private readonly User user;

        public EnterVerificationCode(User user) 
            => this.user = user;

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(
            IDialogContext context, 
            IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            if (message.AsMessageActivity() == null)
            {
                context.Wait(this.MessageReceivedAsync);

                return;
            }

            var apiClient = new StoreClient();
            var user = await apiClient.SubmitVerificationCode(
                this.user.PhoneNumber, 
                message.Text);

            await user.Match(
                async _ =>
                {
                    await context.PostAsync(
                        $"Thanks, code is correct. Let's proceeed with order.");

                    await context.Forward(
                        new FindLatestOrder(this.user),
                        this.AfterOrderFound,
                        message);
                },
                async () =>
                {
                    --attempts;

                    if (attempts > 0)
                    {
                        await context.PostAsync($"Sorry, code is not correct.");

                        context.Wait(this.MessageReceivedAsync);
                    }
                    else
                    {
                        context.Done(Option.None<User>());
                    }
                });
        }

        private async Task AfterOrderFound(
            IDialogContext context,
            IAwaitable<Option<Order>> result) => context.Done(await result);
    }
}