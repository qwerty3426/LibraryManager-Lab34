namespace BankSystem.Application.Strategies;

public interface IFeeStrategy
{
    decimal CalculateFee(decimal amount);
}