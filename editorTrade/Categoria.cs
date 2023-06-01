// See https://aka.ms/new-console-template for more information
public class Categoria
{
    public Categoria()
    {
    }

    public Categoria(int id, string titulo)
    {
        Id = id;
        Titulo = titulo;
        CategoriaItem = new List<CategoriaItem>();

    }

    public int Id { get; set; }
    public string Titulo { get; set; } = null!;
    public List<CategoriaItem> CategoriaItem { get; set; }
}