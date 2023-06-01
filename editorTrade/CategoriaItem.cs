// See https://aka.ms/new-console-template for more information
public class CategoriaItem
{
    public CategoriaItem() { }
    public int Id { get; set; }
    public string Item { get; set; } = null!;
    public string Quantidade { get; set; } = null!;
    public string ValorPlayerCompra { get; set; } = null!;
    public string ValorPlayerVenda { get; set; } = null!;
}