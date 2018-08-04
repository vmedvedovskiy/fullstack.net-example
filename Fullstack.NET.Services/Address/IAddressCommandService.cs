using System.Threading.Tasks;

namespace Fullstack.NET.Services.Address
{
    public interface IAddressCommandService
    {
        Task CreateAddress(NewAddressCommand newAddress);
    }
}
