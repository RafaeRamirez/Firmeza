namespace Firmeza.WebApplication.Interfaces;
public interface IAiChatService { Task<string> AskAsync(string userMessage, CancellationToken ct = default); }
