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
    public class EnterVerificationCodeDialog : IDialog<Option<User>>
    {
        private int attempts = 3;
        private readonly User user;

        public EnterVerificationCodeDialog(User user) => this.user = user;

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
            var user = await apiClient.SubmitVerificationCode(message.Text, message.Text);

            await user.Match(
                async _ =>
                {
                    await context.PostAsync(
                        $"Thanks, code is correct");

                    context.Done(Option.Some(this.user));
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
    }
}