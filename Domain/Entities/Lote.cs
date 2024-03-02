namespace Domain.Entities
{
    public class Lote
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public DateOnly Validade { get; set; }
        public int UnidadesProdutos { get; set; }
        public Produto Produto { get; set; }
        public int ProdutoId { get; set; }
    }
}