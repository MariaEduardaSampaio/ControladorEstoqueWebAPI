using System.ComponentModel.DataAnnotations;

namespace Domain.Requests
{
    public class LoteRequest
    {
        public string? Codigo { get; set; }
        [Required]
        public string DataValidade { get; set; }
        [Required]
        public int UnidadesProdutos { get; set; }
        [Required]
        public int ProdutoId { get; set; }
    }
}
