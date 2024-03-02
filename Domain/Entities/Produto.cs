namespace Domain.Entities
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<Lote> Lotes { get; set; }
    }
}
