namespace Domain
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<Item> Itens { get; set; }
        public Lote Lote { get; set; } 
        public int LoteId { get; set; }
    }
}
