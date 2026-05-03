using Steelbay.Domain.Orders.TechnicalSpecifications;




namespace Steelbay.Domain.Orders.OrderCalculator;

public static class OrderCalculator
{
    private static List<Func<TechnicalSpecification, double, double>> _calculators = new();

}
