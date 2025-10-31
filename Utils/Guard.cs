namespace Firmeza.WebApplication.Utils;

public class Guard
{
    public static void AgainstNull(object? v, string name)
    {
        if (v is null)
            throw new ArgumentNullException(name);
    }
    public static void AgainstNullOrWhiteSpace(string? v, string name)
    {
       { if (string.IsNullOrWhiteSpace(v)) throw new ArgumentException($"{name} is required", name); }
            
        
    }
    
}