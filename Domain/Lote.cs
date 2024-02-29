namespace Domain
{
    public class Lote
    {
        public int Id { get; set; }
        public List<Produto> Produtos { get; set; }
        public string Codigo { get; set; }
    }
}