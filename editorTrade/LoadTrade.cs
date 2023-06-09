// See https://aka.ms/new-console-template for more information
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

public class LoadTrade
{


    public List<string>? _tradeConfig = new List<string>();

    public LoadTrade()
    {
    }

    public async Task LerTradeConfig(string path)
    {
        var traders = new List<string>();
        try
        {
            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            using (StreamReader reader = new StreamReader(path))
            {

                var textInBetween = new List<string>();

                bool startTagFound = false;

                // Read and display lines from the file until the end of
                // the file is reached.
                var tradeTitulo = "";

                Console.WriteLine("inicia leitura da trade");

                while (!reader.EndOfStream)
                {
                    var writeNovalinha = false;
                    var line = reader.ReadLine();
                    _tradeConfig.Add(line);

                }
            }

            var count = 0;
            var tempTradeName = "";
            var tempCatName = "";
            var dblines = new List<string>();
            var str = new StringBuilder();
            str = str.Append(@"Trade; Categoria; Item Name; Quant; Valor; Valor");
            foreach (var line in _tradeConfig)
            {
                var lineIsTrade = line.Contains("<Trader>");
                var lineIsCategoria = line.Contains("<Category>");

                if (!line.Contains("<Trader>") && line.Contains("<Category>"))
                {
                    tempCatName = line.Replace("<Category>", "").Replace("\n", "").Replace("\t", "");
                    Console.WriteLine("TRADER: {}, Category : {}", tempTradeName, tempCatName);
                    continue;
                }

                if (line.Contains("<Trader>") && !line.Contains("<Category>"))
                {
                    tempTradeName = line.Replace("<Trader>", "").Replace("\n", "").Replace("\t", "");
                    tempCatName = "";
                    Console.WriteLine("TRADER: {}", tempTradeName);
                    continue;

                }
                if (!line.Contains("<Trader>") && !line.Contains("<Category>"))
                {
                    var ctitem = line.Split(",");
                    var listCtitem = "";
                    var ic = 0;
                    foreach(var item in ctitem)
                    {
                        var item2 = "";
                        ic += 1;
                        if (item.Replace("\n", "").Replace("\t", "") == "")
                            item2 = "*";
                        if (ic == 1)
                            listCtitem = $"{item.Replace("\n", "").Replace("\t", "")}";
                        if (ic > 1)
                            listCtitem = $"{listCtitem};{item.Replace("\n", "").Replace("\t", "")}";
                    }
                    var newLine = $"{count};{tempTradeName};{tempCatName};{listCtitem};";
                    str.Append(newLine);
                    Console.WriteLine(newLine);
                    continue;
                 }

            }
            Console.WriteLine(str);
        }
        catch (Exception e)
        {
            // Let the user know what went wrong.
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
        
        
        //return await Task.FromResult(new TradeLoadResponse(traders, _tradeConfig));
    }
}
