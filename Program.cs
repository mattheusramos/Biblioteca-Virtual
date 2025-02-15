using System;
using System.Collections.Generic;

class Livro
{
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public bool Disponivel { get; set; } = true;
    public string UsuarioEmprestimo { get; set; } = "Nenhum";

    public Livro(string titulo, string autor)
    {
        Titulo = titulo;
        Autor = autor;
    }
}

class Usuario
{
    public string Nome { get; set; }
    public List<Livro> LivrosEmprestados { get; set; } = new List<Livro>();

    public Usuario(string nome)
    {
        Nome = nome;
    }
}

class Biblioteca
{
    private List<Livro> livros = new List<Livro>();
    private List<Usuario> usuarios = new List<Usuario>();

    public Biblioteca()
    {
        livros.Add(new Livro("O Senhor dos Anéis", "J.R.R. Tolkien"));
        livros.Add(new Livro("1984", "George Orwell"));
        livros.Add(new Livro("Jogador n1", "Ernest Cline"));
        livros.Add(new Livro("Duna", "Frank Herbert"));
        livros.Add(new Livro("Harry Potter e a Pedra Filosofal", "J.K. Rowling"));
    }

    public void CadastrarUsuario()
    {
        Console.Write("Digite o nome do usuário: ");
        string nome = Console.ReadLine();
        usuarios.Add(new Usuario(nome));
        Console.WriteLine($"Usuário '{nome}' cadastrado com sucesso!");
    }

    public void AlugarLivro()
    {
        Console.Write("Digite seu nome de usuário: ");
        string nomeUsuario = Console.ReadLine();
        Usuario usuario = usuarios.Find(u => u.Nome == nomeUsuario);

        if (usuario == null)
        {
            Console.WriteLine("Usuário não encontrado!");
            return;
        }

        Console.WriteLine("\nLivros disponíveis para alugar:");
        for (int i = 0; i < livros.Count; i++)
        {
            if (livros[i].Disponivel)
            {
                Console.WriteLine($"{i + 1}. {livros[i].Titulo} ({livros[i].Autor})");
            }
        }

        Console.Write("Escolha o número do livro que deseja alugar: ");
        int escolha;
        if (int.TryParse(Console.ReadLine(), out escolha) && escolha > 0 && escolha <= livros.Count && livros[escolha - 1].Disponivel)
        {
            livros[escolha - 1].Disponivel = false;
            livros[escolha - 1].UsuarioEmprestimo = usuario.Nome;
            usuario.LivrosEmprestados.Add(livros[escolha - 1]);
            Console.WriteLine($"Livro '{livros[escolha - 1].Titulo}' alugado com sucesso para {usuario.Nome}!");
        }
        else
        {
            Console.WriteLine("Escolha inválida!");
        }
    }

    public void ListarLivros()
    {
        Console.WriteLine("\nLista de livros na biblioteca:");
        foreach (var livro in livros)
        {
            string status = livro.Disponivel ? "Disponível" : $"Emprestado para {livro.UsuarioEmprestimo}";
            Console.WriteLine($"- {livro.Titulo} ({livro.Autor}) - {status}");
        }
    }
}

class Program
{
    static void Main()
    {
        Biblioteca biblioteca = new Biblioteca();

        while (true)
        {
            Console.WriteLine("\nEscolha uma opção:");
            Console.WriteLine("1 - Cadastrar Usuário");
            Console.WriteLine("2 - Alugar Livro");
            Console.WriteLine("3 - Listar Livros");
            Console.WriteLine("4 - Sair");
            Console.Write("\nOpção: ");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    biblioteca.CadastrarUsuario();
                    break;
                case "2":
                    biblioteca.AlugarLivro();
                    break;
                case "3":
                    biblioteca.ListarLivros();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("\nOpção inválida, tente novamente.");
                    break;
            }
        }
    }
}