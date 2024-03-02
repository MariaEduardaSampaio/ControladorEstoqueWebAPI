using Azure.Core;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Application.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class ProdutoController : Controller
    {
        private readonly IProdutoService _service;

        public ProdutoController(IProdutoService service)
        {
            _service = service;
        }

        [HttpPost("AdicionarProduto", Name = "Adicionar produto")]
        public IActionResult AdicionarProduto([FromBody] ProdutoRequest request)
        {
            try
            {
                var produto = _service.AdicionarProduto(request);
                return new CustomObjectResult()
                {
                    Object = produto,
                    Message = "Produto criado com sucesso!",
                    StatusCode = (int)HttpStatusCode.Created
                };
            }
            catch (Exception ex)
            {
                return new CustomObjectResult()
                {
                    Message = $"Não foi possível adicionar um produto: {ex.Message}",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }

        [HttpGet("ListarTodosOsProdutos", Name = "Listar todos os produtos")]
        public IActionResult ListarTodosOsProdutos()
        {
            try
            {
                var produtos = _service.ListarTodosOsProdutos();
                return new CustomObjectResult()
                {
                    Object = produtos,
                    Message = "Todos os produtos registrados.",
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new CustomObjectResult()
                {
                    Message = $"Não foi possível listar todos os produtos: {ex.Message}",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }

        [HttpGet("ListarProdutosPorNome", Name = "Listar produtos por nome")]
        public IActionResult ListarProdutosPorNome([FromQuery] string nome)
        {
            try
            {
                var produtos = _service.ListarProdutosPorNome(nome);
                return new CustomObjectResult()
                {
                    Object = produtos,
                    Message = $"Todos os produtos de nome {nome}.",
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new CustomObjectResult()
                {
                    Message = $"Não foi possível listar os produtos com nome {nome}: {ex.Message}",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }

        [HttpGet("ListarProdutoPorID", Name = "Listar produto por ID")]
        public IActionResult ListarProdutoPorID([FromQuery] int id)
        {
            try
            {
                var produto = _service.ListarProdutoPorID(id);
                return new CustomObjectResult()
                {
                    Object = produto,
                    Message = $"Produto de id {id} retornado com sucesso.",
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new CustomObjectResult()
                {
                    Message = $"Não foi possível encontrar um produto com ID {id}: {ex.Message}",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }

        [HttpPut("AtualizarProduto", Name = "Atualizar produto")]
        public IActionResult AtualizarProduto([FromBody] ProdutoRequest request, [FromQuery] int id)
        {
            try
            {
                var produto = _service.AtualizarProduto(request, id);
                return new CustomObjectResult()
                {
                    Object = produto,
                    Message = $"Alterações salvas com sucesso!",
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new CustomObjectResult()
                {
                    Message = $"Não foi possível atualizar o produto: {ex.Message}",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }

        [HttpDelete("RemoverProduto", Name = "Remover produto")]
        public IActionResult RemoverProduto([FromQuery] int id)
        {
            try
            {
                var produto = _service.RemoverProduto(id);
                return new CustomObjectResult()
                {
                    Object = produto,
                    Message = $"Produto removido com sucesso!",
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new CustomObjectResult()
                {
                    Message = $"Não foi possível remover o produto: {ex.Message}",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }
    }
}
