namespace CyberGuard.Domain;

public class AuditLog
{
    // Cosmos DB utilise souvent des string pour les ID
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    public string User { get; set; } = "System"; // Qui a fait l'action
    
    public string Action { get; set; } = string.Empty; // ex: "Scan réseau lancé"
    
    public string Details { get; set; } = string.Empty; // Détails techniques
    
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}