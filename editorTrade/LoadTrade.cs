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

    public async Task<TradeLoadResponse> LerTradeConfig(string path)
    {
        var traders = new List<Trader>();
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

            var tradersCabecalho = _tradeConfig.Where(x => x.Contains("<Trader>")).Select(x => x.Replace("\t", "").Replace("<Trader>", "").ToString());

            var tradename = "";
            var itemLine = "".Split("");
            var tradeLine = 0;
            foreach (var line in _tradeConfig)
            {
                var lineIsTrade = line.Contains("<Trader>");
                var lineIsCategoria = line.Contains("<Category>");

                if (lineIsTrade)
                {
                    tradename = line.Replace("\t", "").Replace("<Trader>", "").ToString();
                    traders.Add(new Trader(tradeLine, tradename.Trim()));
                    tradeLine++;

                    //Console.WriteLine(tradename.Trim());
                }


            }

        }
        catch (Exception e)
        {
            // Let the user know what went wrong.
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
        
        
        return await Task.FromResult(new TradeLoadResponse(traders, _tradeConfig));
    }
}