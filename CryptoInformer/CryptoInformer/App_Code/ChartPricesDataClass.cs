/// <summary>
/// Summary description for ChartPricesDataClass
/// </summary>
public class ChartPricesDataClass
{
    public string time_period_start { get; set; }
    public string time_period_end { get; set; }
    public string time_open { get; set; }
    public string time_close { get; set; }
    public string price_open { get; set; }
    public string price_high { get; set; }
    public string price_low { get; set; }
    public string price_close { get; set; }
    public string volume_traded { get; set; }
    public string trades_count { get; set; }

    public ChartPricesDataClass()
    {

    }
}