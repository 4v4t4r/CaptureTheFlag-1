using CaptureTheFlag.Models;

namespace CaptureTheFlag.Services
{
    public interface IGameModelService
    {
        void Create(string token, Game game);
    }
}
