// Projector
public class Projector : Device
{
    public int Lumens { get; set; }
    public int LampHoursUsed { get; set; }

    public Projector(string id, string name, int year, decimal price,
        int lumens, int lampHours, DeviceStatus status = DeviceStatus.Active)
        : base(id, name, year, price, status)
    { Lumens = lumens; LampHoursUsed = lampHours; }

    // Bao tri: 3% gia mua + 1.5tr neu bong >3000 gio.
    public override decimal CalculateAnnualMaintenanceCost()
    {
        decimal cost = PurchasePrice * 0.03m;
        if (LampHoursUsed > 3000) cost += 1_500_000m;
        return cost;
    }

    public override string ToString()
        => base.ToString() + $" | Sang: {Lumens} lm | Bong: {LampHoursUsed:N0} gio";
}
