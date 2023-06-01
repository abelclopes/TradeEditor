// See https://aka.ms/new-console-template for more information
public class TradeLoadResponse
{
    public List<Trader> traders {  get; set; }  
    public List<string>? tradeConfig { get; set; }

    public TradeLoadResponse(List<Trader> traders, List<string>? tradeConfig)
    {
        this.traders = traders;
        this.tradeConfig = tradeConfig;
    }
}