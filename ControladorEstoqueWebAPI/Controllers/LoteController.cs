using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Application.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class LoteController
    {
        private readonly ILoteService _service;

        public LoteController(ILoteService service)
        {
            _service = service;
        }

        [HttpPost("AdicionarLote", Name = "Adicionar lote")]
        public IActionResult AdicionarLote([FromBody] LoteRequest request)
        {
            try
            {
                var lote = _service.AdicionarLote(request);
                return new CustomObjectResult()
                {
                    Object = lote,
                    Message = "Lote criado com sucesso!",
                    StatusCode = (int)HttpStatusCode.Created
                };
            }
            catch (Exception ex)
            {
                return new CustomObjectResult()
                {
                    Message = $"Não foi possível criar um lote: {ex.Message}",
                    StatusCode = (int)HttpStatusCode.Created
                };
            }
        }

        [HttpGet("ListarTodosOsLotes", Name = "Listar todos os lotes")]
        public IActionResult ListarTodosOsLotes()
        {
            try
            {
                var lotes = _service.ListarTodosOsLotes();
                return new CustomObjectResult()
                {
                    Object = lotes,
                    Message = "Todos os lotes registrados.",
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new CustomObjectResult()
                {
                    Message = $"Não foi possível listar todos os lotes: {ex.Message}",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }

        [HttpGet("ListarLotesPorProduto", Name = "Listar todos os lotes de um produto")]
        public IActionResult ListarLotesPorProduto([FromQuery] int idProduto)
        {
            try
            {
                var lotes = _service.ListarLotesPorProduto(idProduto);
                return new CustomObjectResult()
                {
                    Object = lotes,
                    Message = $"Todos os lotes do produto de ID {idProduto}.",
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new CustomObjectResult()
                {
                    Message = $"Não foi possível listar todos os lotes do produto {idProduto}: {ex.Message}",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }

        [HttpGet("ListarLotesVencidos", Name = "Listar todos os lotes vencidos")]
        public IActionResult ListarLotesVencidos()
        {
            try
            {
                var lotes = _service.ListarLotesVencidos();
                return new CustomObjectResult()
                {
                    Object = lotes,
                    Message = "Todos os lotes vencidos.",
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new CustomObjectResult()
                {
                    Message = $"Não foi possível listar todos os lotes vencidos do sistema: {ex.Message}",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }

        [HttpGet("ListarLotePorID", Name = "Listar lote por ID")]
        public IActionResult ListarLotePorID([FromQuery] int id)
        {
            try
            {
                var lote = _service.ListarLotePorID(id);
                return new CustomObjectResult()
                {
                    Object = lote,
                    Message = $"Lote de id {id} retornado com sucesso.",
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new CustomObjectResult()
                {
                    Message = $"Não foi possível encontrar um lote com ID {id}: {ex.Message}",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }

        [HttpGet("ListarLotePorCodigo", Name = "Listar lote por código")]
        public IActionResult ListarLotePorCodigo([FromQuery] string codigo)
        {
            try
            {
                var lote = _service.ListarLotePorCodigo(codigo);
                return new CustomObjectResult()
                {
                    Object = lote,
                    Message = $"Lote de código {codigo} retornado com sucesso.",
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new CustomObjectResult()
                {
                    Message = $"Não foi possível encontrar um lote com código {codigo}: {ex.Message}",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }

        [HttpPut("AtualizarLote", Name = "Atualizar lote")]
        public IActionResult AtualizarLote([FromBody] LoteRequest request, [FromQuery] int id)
        {
            try
            {
                var lote = _service.AtualizarLote(request, id);
                return new CustomObjectResult()
                {
                    Object = lote,
                    Message = $"Alterações salvas com sucesso!",
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new CustomObjectResult()
                {
                    Message = $"Não foi possível atualizar o lote: {ex.Message}",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }

        [HttpDelete("RemoverLote", Name = "Remover lote")]
        public IActionResult RemoverLote([FromQuery] int id)
        {
            try
            {
                var lote = _service.RemoverLote(id);
                return new CustomObjectResult()
                {
                    Object = lote,
                    Message = $"Lote removido com sucesso!",
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new CustomObjectResult()
                {
                    Message = $"Não foi possível remover o lote: {ex.Message}",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }

        [HttpDelete("RemoverLotesVencidos", Name = "Remover lotes vencidos")]
        public IActionResult RemoverLotesVencidos()
        {
            try
            {
                var lotes = _service.RemoverLotesVencidos();
                return new CustomObjectResult()
                {
                    Object = lotes,
                    Message = $"Lotes vencidos removidos com sucesso!",
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new CustomObjectResult()
                {
                    Message = $"Não foi possível remover os lotes vencidos: {ex.Message}",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }
    }
}
