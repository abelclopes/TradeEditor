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
    public LoadTrade(string path)
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
                int i = 1;
                while (!reader.EndOfStream)
                {
                    var writeNovalinha = false;
                    var line = reader.ReadLine();
                    if (String.IsNullOrEmpty(line))
                        continue;

                    startTagFound = line.StartsWith("<Trader>");
                    if (startTagFound && line.Replace("<Trader>", "").ToString() != tradeTitulo)
                    {
                        var tradeName = line.Replace("<Trader>", "").ToString();
                        tradeTitulo = tradeName.Trim();
                        traders.Add(new Trader(i, tradeTitulo));
                        i++;
                        continue;
                    }

                }
                Console.WriteLine($"Total de Trader Encontrada: {i}");

            }
            traders = lerCategorias(path, traders);

            var newTrades = new List<Trader>();
            foreach (var trader in traders)
            {
                var changegCategiria = new List<Categoria>();
                var newTrade2 = new Trader();
                foreach(var categoria in trader.Categoria)
                {
                    Console.WriteLine(categoria.Titulo);
                    changegCategiria.Add(lerItensCategoria(path, categoria, trader.Id));
                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine(JsonSerializer.Serialize(traders));
                }
                newTrades.Add(newTrade2);

            }

            //traders = lerItensCategoria(path, traders);


            Console.WriteLine(JsonSerializer.Serialize(newTrades));

            using (var file = File.CreateText("./../.././../../TraderConfig.json"))
            {
                file.WriteLine(JsonSerializer.Serialize(newTrades));
            }              

            using (var file = File.CreateText("./../.././../../TraderConfig.csv"))
            { }

        }
        catch(Exception e)
        {
            // Let the user know what went wrong.
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
    }

    private Categoria lerItensCategoria(string path, Categoria categoria, int tradeId)
    {
        using (StreamReader readerCategoria = new StreamReader(path))
        {
            var textInBetween = new List<string>();

            bool startTagFound = false;
            bool startTagTradeFound = false;

            var categoriaTitulo = "";

            Console.WriteLine("inicia leitura itens da Categora");

            var lineNumber = 0;
            var localLineNumber = 0;
            string[] linesplit = "".Split("");
            string[] lineItemsplit = "".Split("");
            var categoriaReadName = "";
            while (!readerCategoria.EndOfStream)
            {
                var writeNovalinha = false;
                var line = readerCategoria.ReadLine();
                if (String.IsNullOrEmpty(line))
                    continue;

                startTagFound = line.StartsWith("\t<Category>");
                startTagTradeFound = line.StartsWith("<Trader>");
                if (startTagTradeFound)
                {
                    linesplit = line.Split();
                    linesplit.Count();
                    var n = linesplit[linesplit.Length - 1].Replace("ID=", "");
                    localLineNumber = int.Parse(n) + 1;
                }
                var tempName = line.ToString();
                categoriaReadName = startTagFound && tradeId == localLineNumber ? line.Replace("\t<Category>", "").ToString() : categoriaReadName;

                if (!startTagTradeFound && !startTagFound && categoriaReadName == categoria.Titulo)
                {
                    var Item = line.Replace("\t", "").Replace(" ", "").ToString().Split(",");

                    var newItemCat = new CategoriaItem()
                    {
                        Item = Item[0].ToString(),
                        Quantidade = Item[1].ToString(),
                        ValorPlayerCompra = Item[2].ToString(),
                        ValorPlayerVenda = Item[3].ToString(),
                    };


                    Console.WriteLine($"Categoria: {categoria.Titulo}, Linha: {JsonSerializer.Serialize(newItemCat)}");

                    categoria.CategoriaItem.Add(newItemCat);
                    startTagFound = false;
                    continue;                 
                }
                continue;


            }
        }
        return categoria;
    }
    private List<Trader> lerCategorias(string path, List<Trader> traders)
    {
        using (StreamReader readerCategoria = new StreamReader(path))
        {
            var textInBetween = new List<string>();

            bool startTagFound = false;
            bool startTagTradeFound = false;

            var categoriaTitulo = "";

            Console.WriteLine("inicia leitura da Categora");

            var lineNumber = 0;
            var tradeId = 0;
            string[] linesplit = "".Split("");
            while (!readerCategoria.EndOfStream)
            {
                var writeNovalinha = false;
                var line = readerCategoria.ReadLine();
                if (String.IsNullOrEmpty(line))
                    continue;

                startTagFound = line.StartsWith("\t<Category>");
                startTagTradeFound = line.StartsWith("<Trader>");
                if (startTagTradeFound)
                {
                    linesplit = line.Split();
                    linesplit.Count();
                    var n = linesplit[linesplit.Length - 1].Replace("ID=", "");
                    tradeId = int.Parse(n) + 1;
                }
                var exists = traders.Any(x => x.Id == tradeId);
                var trade = traders.FirstOrDefault(x => x.Id == tradeId);
                var categoriaReadName = startTagFound && exists ? null : line.Replace("\t<Category>", "").ToString();


                if (startTagFound && !startTagTradeFound)
                {
                    var categoriaName = line.Replace("\t<Category>", "").ToString();
                    if (line.StartsWith("\t<Category>"))
                    {
                        categoriaTitulo = categoriaName;

                        var addTrade = traders.FirstOrDefault(x => x.Id == trade.Id);

                        addTrade.AddCategoria(new Categoria(lineNumber++, categoriaTitulo));

                        var tradeRemove = traders.FirstOrDefault(x => x.Id == trade.Id);

                        Console.WriteLine($"Trade: {trade.TradeTitulo}, Categoria: {line.Replace("<Category>", "")}");

                        traders.Remove(tradeRemove);
                        traders.Add(addTrade);

                        startTagFound = false;
                        continue;
                    }
                    lineNumber = 0;

                    startTagTradeFound = false;
                    continue;
                }
                continue;


            }
        }
        return traders;
    }
}