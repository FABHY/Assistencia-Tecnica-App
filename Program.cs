using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

// O namespace do seu projeto
namespace AssistenciaTecnicaApp
{
    // ---------------- CLASSE DE MODELO: CLIENTE ----------------
    public class Cliente
    {
        public int Id { get; set; }
        
        public required string NomeCompleto { get; set; }
        public required string EnderecoCompleto { get; set; }
        public required string Telefone { get; set; }
        public required string Email { get; set; }
        public required string Cidade { get; set; }
        public required string Cep { get; set; }
        
        public List<Servico> Servicos { get; set; } = new List<Servico>();
    }

    // ---------------- CLASSE DE MODELO: SERVICO ----------------
    public class Servico
    {
        public int Id { get; set; }
        public int ClienteId { get; set; } // Chave Estrangeira
        public required string DescricaoProblema { get; set; }
        public bool Concluido { get; set; }
        
        public Cliente? Cliente { get; set; }
    }

    // ---------------- CLASSE DE CONTEXTO DO BANCO (EF CORE) ----------------
    public class AppDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Servico> Servicos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // ATENÇÃO: SUBSTITUA as informações do banco de dados (Server, Port, Database, Uid, Pwd)
            string connectionString = "Server=localhost;Port=3306;Database=assistenciatecnica_db;Uid=root;Pwd=Fabio@4040;";
            
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }

    // ---------------- PONTO DE ENTRADA DO PROGRAMA ----------------
    public class Program
    {
        private static AppDbContext _context = null!; 

        public static void Main(string[] args)
        {
            Console.WriteLine("Bem-vindo ao Sistema de Assistência Técnica (MySQL/EF Core)!");
            
            _context = new AppDbContext();
            _context.Database.EnsureCreated(); 

            while (true)
            {
                ExibirMenu();
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                string opcao = Console.ReadLine();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

                switch (opcao)
                {
                    case "1":
                        AdicionarServico();
                        break;
                    case "2":
                        ListarServicos();
                        break;
                    case "3":
                        MarcarComoConcluido();
                        break;
                    case "4":
                        RemoverServico();
                        break;
                    case "5":
                        EditarServico();
                        break;
                    case "6":
                        CadastrarCliente();
                        break;
                    case "7":
                        ListarClientes();
                        break;
                    case "8":
                        EditarCliente();
                        break;
                    case "9":
                        RemoverCliente();
                        break;
                    case "10":
                        return; // Sair do programa
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }

                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        // --- MÉTODOS DE INTERFACE E MENU ---
        private static void ExibirMenu()
        {
            Console.WriteLine("\n--- Menu Principal ---");
            Console.WriteLine("1. Adicionar novo serviço");
            Console.WriteLine("2. Listar todos os serviços");
            Console.WriteLine("3. Marcar serviço como concluído");
            Console.WriteLine("4. Remover serviço");
            Console.WriteLine("5. Editar serviço");
            
            Console.WriteLine("--- Gestão de Clientes ---");
            Console.WriteLine("6. Cadastrar novo cliente");
            Console.WriteLine("7. Listar todos os clientes");
            Console.WriteLine("8. Editar cliente"); // Novo
            Console.WriteLine("9. Remover cliente"); // Novo
            
            Console.WriteLine("10. Sair"); // Sair (mudou para 10)
            Console.Write("Escolha uma opção: ");
        }

        // --- MÉTODOS DE CLIENTES ---
        private static void CadastrarCliente()
        {
            Console.Clear();
            Console.WriteLine("--- Cadastrar Novo Cliente ---");
            Console.Write("Nome Completo: ");
            string nomeCompleto = Console.ReadLine() ?? "";
            Console.Write("Endereço Completo: ");
            string enderecoCompleto = Console.ReadLine() ?? "";
            Console.Write("Telefone: ");
            string telefone = Console.ReadLine() ?? "";
            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";
            Console.Write("Cidade: ");
            string cidade = Console.ReadLine() ?? "";
            Console.Write("CEP: ");
            string cep = Console.ReadLine() ?? "";

            var novoCliente = new Cliente
            {
                NomeCompleto = nomeCompleto,
                EnderecoCompleto = enderecoCompleto,
                Telefone = telefone,
                Email = email,
                Cidade = cidade,
                Cep = cep
            };

            _context.Clientes.Add(novoCliente);
            _context.SaveChanges(); // Salva no MySQL

            Console.WriteLine($"\nCliente cadastrado com sucesso! ID: {novoCliente.Id}");
        }

        private static void ListarClientes()
        {
            Console.Clear();
            Console.WriteLine("--- Lista de Clientes Cadastrados ---");
            
            var clientes = _context.Clientes.ToList();

            if (clientes.Count == 0)
            {
                Console.WriteLine("Nenhum cliente registrado.");
            }
            else
            {
                foreach (var c in clientes)
                {
                    Console.WriteLine($"ID: {c.Id} | Nome: {c.NomeCompleto} | Telefone: {c.Telefone}");
                }
            }
        }
        
        // --- NOVO MÉTODO: EDITAR CLIENTE (Opção 8) ---
        private static void EditarCliente()
        {
            Console.Clear();
            Console.WriteLine("--- Editar Cliente ---");
            Console.Write("Digite o ID do cliente que deseja editar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var clienteParaEditar = _context.Clientes.FirstOrDefault(c => c.Id == id);
                
                if (clienteParaEditar != null)
                {
                    Console.WriteLine($"\nEditando cliente ID {id}. Deixe em branco para manter o valor atual.");
                    
                    // Edição do Nome Completo
                    Console.Write($"Nome atual ({clienteParaEditar.NomeCompleto}): ");
                    string novoNome = Console.ReadLine() ?? "";
                    if (!string.IsNullOrWhiteSpace(novoNome))
                    {
                        clienteParaEditar.NomeCompleto = novoNome;
                    }
                    
                    // Sua tarefa é completar a edição dos campos restantes AQUI:
                    // Exemplo: Telefone
                    Console.Write($"Telefone atual ({clienteParaEditar.Telefone}): ");
                    string novoTelefone = Console.ReadLine() ?? "";
                    if (!string.IsNullOrWhiteSpace(novoTelefone))
                    {
                        clienteParaEditar.Telefone = novoTelefone;
                    }
                    
                    // Adicione lógica para Endereco, Email, Cidade, CEP...

                    _context.SaveChanges();
                    Console.WriteLine("\nCliente atualizado com sucesso!");
                }
                else
                {
                    Console.WriteLine("Cliente não encontrado.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
        }

        // --- NOVO MÉTODO: REMOVER CLIENTE (Opção 9) ---
        private static void RemoverCliente()
        {
            Console.Clear();
            Console.WriteLine("--- Remover Cliente ---");
            
            // A lógica de remoção virá aqui (Será similar a RemoverServico)
            Console.WriteLine("Esta função ainda precisa ser implementada.");
        }


        // --- MÉTODOS DE SERVIÇOS ---
        private static void AdicionarServico()
        {
            Console.Clear();
            Console.WriteLine("--- Adicionar Novo Serviço ---");
            
            Console.Write("Digite o ID do Cliente: ");
            if (!int.TryParse(Console.ReadLine(), out int clienteId))
            {
                Console.WriteLine("\nID de cliente inválido. Tente novamente.");
                return;
            }
            
            var cliente = _context.Clientes.FirstOrDefault(c => c.Id == clienteId);
            if (cliente == null)
            {
                Console.WriteLine($"\nCliente com ID {clienteId} não encontrado. Cadastre-o primeiro.");
                return;
            }

            Console.WriteLine($"Cliente selecionado: {cliente.NomeCompleto}");
            Console.Write("Descrição do problema: ");
            string descricao = Console.ReadLine() ?? "";

            var novoServico = new Servico
            {
                ClienteId = clienteId,
                DescricaoProblema = descricao,
                Concluido = false
            };

            _context.Servicos.Add(novoServico);
            _context.SaveChanges(); 

            Console.WriteLine($"\nServiço adicionado com sucesso! ID: {novoServico.Id}");
        }

        private static void ListarServicos()
        {
            Console.Clear();
            Console.WriteLine("--- Lista de Serviços ---");
            
            var servicos = _context.Servicos
                                   .Include(s => s.Cliente)
                                   .ToList();

            if (servicos.Count == 0)
            {
                Console.WriteLine("Nenhum serviço registrado.");
            }
            else
            {
                foreach (var s in servicos)
                {
                    string status = s.Concluido ? "Concluído" : "Pendente";
                    string nomeCliente = s.Cliente?.NomeCompleto ?? "Cliente (ID: " + s.ClienteId + ") Não Encontrado";
                    
                    Console.WriteLine($"ID: {s.Id} | Cliente: {nomeCliente} | Problema: {s.DescricaoProblema} | Status: {status}");
                }
            }
        }

        private static void MarcarComoConcluido()
        {
            Console.Clear();
            Console.WriteLine("--- Marcar Serviço como Concluido ---");
            Console.Write("Digite o ID do serviço: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var servico = _context.Servicos.FirstOrDefault(s => s.Id == id); 

                if (servico != null)
                {
                    servico.Concluido = true;
                    _context.SaveChanges(); 
                    Console.WriteLine($"Serviço ID {id} marcado como concluído com sucesso!");
                }
                else
                {
                    Console.WriteLine("Serviço não encontrado.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
        }

        private static void RemoverServico()
        {
            Console.Clear();
            Console.WriteLine("--- Remover Serviço ---");
            Console.Write("Digite o ID do serviço que deseja remover: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var servicoParaRemover = _context.Servicos.FirstOrDefault(s => s.Id == id);
                
                if (servicoParaRemover != null)
                {
                    _context.Servicos.Remove(servicoParaRemover);
                    _context.SaveChanges(); 
                    Console.WriteLine($"Serviço ID {id} removido com sucesso!");
                }
                else
                {
                    Console.WriteLine("Serviço não encontrado.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
        }

        private static void EditarServico()
        {
            Console.Clear();
            Console.WriteLine("--- Editar Serviço ---");
            Console.Write("Digite o ID do serviço que deseja editar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var servicoParaEditar = _context.Servicos.FirstOrDefault(s => s.Id == id);
                
                if (servicoParaEditar != null)
                {
                    Console.WriteLine($"Editando serviço ID {id}:");
                    Console.WriteLine("Deixe em branco para manter o valor atual.");
                    
                    Console.Write($"Cliente ID atual ({servicoParaEditar.ClienteId}): ");
                    string novoClienteIdStr = Console.ReadLine() ?? "";
                    if (!string.IsNullOrWhiteSpace(novoClienteIdStr) && int.TryParse(novoClienteIdStr, out int novoClienteId))
                    {
                        var clienteExiste = _context.Clientes.FirstOrDefault(c => c.Id == novoClienteId);
                        if(clienteExiste != null)
                        {
                            servicoParaEditar.ClienteId = novoClienteId;
                        }
                        else
                        {
                            Console.WriteLine("Aviso: Novo ID de cliente não encontrado. Cliente não alterado.");
                        }
                    }

                    Console.Write($"Descrição atual ({servicoParaEditar.DescricaoProblema}): ");
                    string novaDescricao = Console.ReadLine() ?? "";
                    if (!string.IsNullOrWhiteSpace(novaDescricao))
                    {
                        servicoParaEditar.DescricaoProblema = novaDescricao;
                    }

                    _context.SaveChanges(); 
                    Console.WriteLine("\nServiço atualizado com sucesso!");
                }
                else
                {
                    Console.WriteLine("Serviço não encontrado.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
        }
    }
}