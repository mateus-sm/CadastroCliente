using CadastroCliente2.Models.Repository;
using CadastroClientes.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CadastroClientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        [HttpPost("Salvar")]
        public object? Salvar([FromBody] Clientes cadastro)
        {
            try
            {
                ClientesRepository clientes = new();
                clientes.Salvar(cadastro);
            } 
            catch (Exception ex)
            {
                return new { Erro = "Erro interno da API: " + ex.Message };
            }

            return null;
        }

        [HttpGet("Listar")]
        public object? Listar()
        {
            List<Clientes>? listaCli;

            try
            {
                ClientesRepository clientesRepo = new();
                listaCli = clientesRepo.Listar();
            }
            catch (Exception ex)
            {
                return new { Erro = "Erro interno da API: " + ex.Message };
            }

            return listaCli;
        }

        [HttpDelete("Deletar")]
        public object Deletar(string doc)
        {
            try
            {
                ClientesRepository clientes = new();
                bool retornoDelete = clientes.Deletar(doc);
                return retornoDelete;
            }
            catch (Exception ex)
            {
                return new { Erro = "Erro interno da API: " + ex.Message };
            }
        }

        [HttpGet("Get")]
        public object? Get([FromHeader] string doc)
        {
            try
            {
                ClientesRepository clientes = new();

                if (clientes == null)
                    return null;

                return clientes.Consultar(doc);
            } 
            catch (Exception ex)
            {
                return new { Erro = "Erro interno da API: " + ex.Message };
            }
        }

        //[HttpPost("Alterar")]
        //public object Alterar([FromBody] Clientes cliente)
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    return null;
        //}
    }
}
