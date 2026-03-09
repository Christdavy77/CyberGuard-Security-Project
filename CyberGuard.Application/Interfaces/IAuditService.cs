using CyberGuard.Domain;

namespace CyberGuard.Application.Interfaces;

public interface IAuditService
{
    Task LogActionAsync(string action, string details);
    Task<IEnumerable<AuditLog>> GetLogsAsync();
}