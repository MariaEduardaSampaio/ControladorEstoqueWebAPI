namespace Domain
{
    public class Item
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public decimal Peso { get; set; }
        public DateOnly Fabricacao { get; set; }
        public DateOnly Validade { get; set; }
        public Produto Produto { get; set; }
        public int ProdutoId { get; set; }
    }
}
