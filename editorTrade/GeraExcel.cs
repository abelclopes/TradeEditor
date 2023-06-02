
public class GeraExcel
{

    private List<string>? _tradeConfig = new List<string>();
    private List<string>? _newTradeConfig = new List<string>();
    public GeraExcel(List<string> tradeConfig)
    {
        _tradeConfig = tradeConfig;
    }

    public void GerarCSV(List<Trader> traders)
    {
        var tradename = "";
        var itemLine = "".Split("");
        var tradeLine = 0;

        using (var file = File.CreateText("./../.././../../TraderConfig4.csv"))
        {
            var catname = "";
            var i = 0;
            var totalLinha = _tradeConfig.Count();
            foreach (var line in _tradeConfig)
            {
                var lineIsTrade = line.Contains("<Trader>");
                var lineIsCategoria = line.Contains("<Category>");

                if (lineIsTrade)
                {
                    tradename = line.Replace("\t", "").Replace("<Trader>", "").ToString();
                    // Console.WriteLine(tradename.Trim());
                }

                if (lineIsCategoria)
                {
                    catname = line.Replace("\t", "").Replace("<Category>", "").ToString();
                    if (traders != null)
                    {
                        var tradeatual = traders?.FirstOrDefault(x => x.TradeTitulo == tradename);
                        tradeatual?.AddCategoria(new Categoria(i, catname));
                        i++;
                        //Console.WriteLine($"TRADE NAME: {tradename.Trim()}");
                        //Console.WriteLine($"CATEGORI NAME: {catname.Trim()}");
                    }
                }
               
                if (!lineIsTrade && !lineIsCategoria)
                {
                    var Item = line.Replace("\t", "").Split(",");
                    var newItemCat = new CategoriaItem()
                    {
                        Item = Item[0].Trim().ToString(),
                        Quantidade = Item[1].Trim().ToString(),
                        ValorPlayerCompra = Item[2].Trim().ToString(),
                        ValorPlayerVenda = Item[3].Trim().ToString(),
                    };

                    if (traders != null && !string.IsNullOrEmpty(tradename))
                    {
                        if (i == 4662)
                        {
                            break;
                        }

                        //                        Console.WriteLine($"CATEGORI NAME: {catname.Trim()}");
                        var tradeatual = traders?.FirstOrDefault(x => x.TradeTitulo == tradename);
                        var catAtual = tradeatual?.Categoria.FirstOrDefault(x => x.Titulo == catname);
                        var saveLine = $"{tradeatual?.TradeTitulo};{catAtual?.Titulo};{newItemCat.Item};{newItemCat.Quantidade};{newItemCat.ValorPlayerCompra};{newItemCat.ValorPlayerVenda};";
                        file.WriteLine(saveLine);
                        Console.WriteLine($"READ :{saveLine}");
                        i++;

                    }

                }


            }
        }
        Console.WriteLine($"READ COMPLET");
    }
}