using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Requests
{
    public class ProdutoRequest
    {
        [Required]
        public string Nome { get; set; }
    }
}
