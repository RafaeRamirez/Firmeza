// Interfaces/IAiChatService.cs
namespace Firmeza.WebApplication.Interfaces;

public interface IAiChatService
{
    Task<string> GetAnswerAsync(string prompt, CancellationToken cancellationToken = default);
}
