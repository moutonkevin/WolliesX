using Wollies.Contracts;

namespace Wollies.Domain.Services
{
    public interface ITrolleyCalculator
    {
        decimal CalculateLowestTotal(Trolley trolley);
    }
}