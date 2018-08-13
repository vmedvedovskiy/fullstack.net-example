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
    public class FindUserDialog : IDialog<Option<User>>
    {
        private int attempts = 3;

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            if (message.AsMessageActivity() != null)
            {
                var apiClient = new StoreClient();

                var user = await apiClient.FindUser(message.Text);

                await user.Match(
                    async _ =>
                    {
                        await context.PostAsync($"Please, enter code we've sent to your phone number to confirm your identity.");

                        context.Done(Option.Some(_));
                    },
                    async () =>
                    {
                        --attempts;

                        if (attempts > 0)
                        {
                            await context.PostAsync(
                                $"Sorry, I cannot find you in our database. " +
                                $"Probably, you've entered number wrong. " +
                                $"Please, check your number for spelling errors.");

                            context.Wait(this.MessageReceivedAsync);
                        }
                        else
                        {
                            context.Done(Option.None<User>());
                        }
                    });
            }
            else
            {
                context.Wait(this.MessageReceivedAsync);
            }
        }
    }
}