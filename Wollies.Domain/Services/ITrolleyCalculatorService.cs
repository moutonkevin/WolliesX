using Wollies.Contracts;

namespace Wollies.Domain.Services
{
    public interface ITrolleyCalculatorService
    {
        decimal CalculateLowestTotal(Trolley trolley);
    }
}