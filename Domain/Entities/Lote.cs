namespace Domain.Entities
{
    public class Lote
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public int Quantidade { get; set; }
        public DateOnly Fabricacao { get; set; }
        public DateOnly Validade { get; set; }
        public Produto Produto { get; set; }
        public int ProdutoId { get; set; }
    }
}