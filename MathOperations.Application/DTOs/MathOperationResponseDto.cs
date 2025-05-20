namespace Erp_Repo.DTOs;

public class MathOperationResponseDto
{
    public int Id { get; set; }
    public double Number1 { get; set; }
    public double Number2 { get; set; }
    public double Result { get; set; }
    public string OperationType { get; set; }
    public DateTime CreatedAt { get; set; }
}
