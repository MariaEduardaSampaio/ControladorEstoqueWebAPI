using System.ComponentModel.DataAnnotations;

namespace Domain.Requests
{
    public class LoteRequest
    {
        [Required]
        public string Codigo { get; set; }
        [Required]
        public DateOnly Validade { get; set; }
        [Required]
        public int UnidadesProdutos { get; set; }
        [Required]
        public int ProdutoId { get; set; }
    }
}
