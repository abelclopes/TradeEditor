// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography.X509Certificates;

public class Trader
{
    public Trader()
    {
    }

    public Trader(int id, string tradeTitulo)
    {
        Id = id;
        TradeTitulo = tradeTitulo;
        Categoria = new List<Categoria>();
    }

    public int Id { get; set; }
    public string TradeTitulo { get; set; } = null!;
    public List<Categoria> Categoria { get; set; } = null!;

    public void AddCategoria(Categoria categoria)
    {
        Categoria.Add(categoria);
    }
}