using CadastroClientes.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace CadastroCliente2.Models.Repository
{
    public class ClientesRepository
    {
        private readonly string dbPath;

        public ClientesRepository()
        {
            dbPath = Directory.GetCurrentDirectory() + "\\BancoDeDados\\bancodados.txt";
        }

        public Clientes? Consultar(string doc)
        {
            var arqStr = File.ReadAllText(dbPath);

            List<Clientes>? lista = JsonConvert.DeserializeObject<List<Clientes>>("[" + arqStr + "]");

            if (lista != null)
            {
                var cliente = lista.Where(t => t.Documento == doc).FirstOrDefault();

                if (cliente != null)
                {
                    return cliente;
                }
            }

            return null;
        }

        public void Salvar(Clientes clientes)
        {
            var clientesTexto = JsonConvert.SerializeObject(clientes) + "," + Environment.NewLine;
            File.AppendAllText(dbPath, clientesTexto);
        }

        public List<Clientes>? Listar()
        {
            var clientes = File.ReadAllText(dbPath);

            List<Clientes>? clientesLista = JsonConvert.DeserializeObject<List<Clientes>>("[" + clientes + "]");

            if (clientesLista != null)
            {
                return [.. clientesLista.OrderByDescending(t => t.Nome)];
            }

            return null;
        }

        public bool Deletar(string doc)
        {
            var listaClientes = Listar();

            if (listaClientes == null)
                return false;

            var item = listaClientes.Where(t => t.Documento == doc).FirstOrDefault();
            
            if (item != null)
            {
                listaClientes.Remove(item);

                File.WriteAllText(dbPath, string.Empty);

                foreach (var c in listaClientes)
                {
                    Salvar(c);
                }
                return true;
            }

            return false;
        }
    }
}
