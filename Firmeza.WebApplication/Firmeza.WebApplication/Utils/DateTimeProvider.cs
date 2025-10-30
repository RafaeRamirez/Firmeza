using Firmeza.WebApplication.Interfaces;
namespace Firmeza.WebApplication.Utils;
public sealed class DateTimeProvider : IDateTimeProvider { public DateTime UtcNow => DateTime.UtcNow; }