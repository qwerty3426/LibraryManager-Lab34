namespace BankSystem.Application.Strategies;

public class StandardFeeStrategy : IFeeStrategy
{
    public decimal CalculateFee(decimal amount)
    {
        decimal fee = amount * 0.01m;

        if (fee < 2)
        {
            fee = 2;
        }

        return fee;
    }
}