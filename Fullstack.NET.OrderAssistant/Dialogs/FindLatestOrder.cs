using System;
using System.Linq;
using System.Threading.Tasks;
using Fullstack.NET.StoreIntegration;
using Fullstack.NET.StoreIntegration.Contract;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Optional;

namespace Fullstack.NET.OrderAssistant.Dialogs
{
    [Serializable]
    public class FindLatestOrder : IDialog<Option<Order>>
    {
        private int attempts = 3;
        private readonly User user;

        public FindLatestOrder(User user) 
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
            var orders = await apiClient.GetUserOrders(this.user.Id);

            await orders
                .FlatMap(_ => _.FirstOrDefault().SomeNotNull())
                .Match(
                async _ =>
                {
                    await context.PostAsync($"Is it your order? Created at: {_.CreatedDate}, contains following products: ");

                    context.Done(Option.Some(_));
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
                        context.Done(Option.None<Order>());
                    }
                });
        }
    }
}