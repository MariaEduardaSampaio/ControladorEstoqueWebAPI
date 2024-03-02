using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        [JsonIgnore]
        public List<Lote> Lotes { get; set; }
    }
}
