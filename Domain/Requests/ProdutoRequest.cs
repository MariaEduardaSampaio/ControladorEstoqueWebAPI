using System.ComponentModel.DataAnnotations;

namespace Domain.Requests
{
    public class ProdutoRequest
    {
        [Required]
        [MinLength(2)]
        public string Nome { get; set; }
    }
}
