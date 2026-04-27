namespace Steelbay.Domain.Orders;

public enum OrderAction
{
    Confirm,
    Cancel,
    StartProcurement,
    StartManufacturing,
    StartAssembly,
    Complete,
    UpdateSpecification
}
