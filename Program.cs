using System;
using System.IO;
using listaligada;
using arquivos;
using excecoes;

namespace search
{
    class Program
    {
        static void Main(string[] args)
        {
			bool flag = true;									// Valor booleano que indica se a busca por um arquivo será feita em subdiretórios
			string diretorio = null;							// Indica o diretório alvo da busca
			string arquivo = null;								// Arquivo a ser procurado
			ListaLigada<string> listaDeArquivos = null;    	// Cria uma lista para todos os arquivos obtidos num diretório e subdiretórios
			ListaLigada<string> arquivosEncontrados = null;	// Cria uma lista para os arquivos que forem encontrados na busca
			
			// Apresentação
            Console.WriteLine("Search versão 1.2.0");
			Console.WriteLine("Uso: search [opçao] [local] [arquivo]");
			Console.WriteLine("Opçao: \n" +
								"  -ns		Diz ao programa para procurar arquivo somente no diretorio corrente nao em seus subdiretorios.");
				
            Console.WriteLine();
			
			// Classificação dos argumentos de linha de comando
			switch(args.Length)
			{
				case 1 : 
					diretorio = Directory.GetCurrentDirectory();				// Define diretório raiz de busca
					arquivo = args[0];											// Arquivo procurado
					Console.WriteLine("Diretorio: {0}", diretorio);
					Console.WriteLine("Arquivo procurado: {0}", arquivo);
				break;
				case 2 :
					if(args[0] == "-ns")
					{
						flag = false;											// Flag false indica que a busca não sera feita em subdiretórios
						diretorio = Directory.GetCurrentDirectory();			// Define diretório raiz de busca
						arquivo = args[1];										// Arquivo procurado
						Console.WriteLine("Diretorio: {0}", diretorio);
						Console.WriteLine("Arquivo procurado: {0}", arquivo);
						Console.WriteLine("* Sem busca em subdiretorios...");
					}
					else
					{
						diretorio = args[0];									// Define diretório inicial de busca
						arquivo = args[1];										// Arquivo procurado
						Console.WriteLine("Diretorio: {0}", diretorio);
						Console.WriteLine("Arquivo procurado: {0}", arquivo);
					}
				break;
				case 3 :
					if(args[0] == "-ns")
					{
						flag = false;
						diretorio = args[1];									// Define diretório inicial de busca
						arquivo = args[2];										// Arquivo procurado
						Console.WriteLine("Diretorio: {0}", diretorio);
						Console.WriteLine("Arquivo procurado: {0}", arquivo);
						Console.WriteLine("* Sem busca em subdiretorios...");
					}
					else
					{
						Console.WriteLine("Opçao invalida!");
						Environment.Exit(0);
					}
				break;
				default :
					Console.WriteLine("Numero de argumentos invalido!");
					Environment.Exit(0);
				break;
			}
			
			// Processo de busca
			try{
				Console.WriteLine("Carregando arquivos...");
				listaDeArquivos = Arquivos.ObtemArquivos(diretorio, flag);				// Carrega todos os arquivos no diretório para uma lista
				
				Console.WriteLine("Procurando...");
				arquivosEncontrados = Arquivos.BuscaArquivos(arquivo, listaDeArquivos);	// Procura pelo arquivo na lista de arquivos
				Console.WriteLine();
				
				if(Arquivos.AcessoNegado)
					Console.WriteLine("Alguns arquivos tiveram seu acesso negado e não puderam ser verificados.");
				
				Console.WriteLine();
				for (int i = 0; i < arquivosEncontrados.Tamanho(); i++)					// Laço que percorre a lista de arquivos encontrados
				{
					Console.WriteLine(arquivosEncontrados.ObtemItemEm(i));            	// Exibe todos os caminhos (path) onde o arquivo foi encontrado
				}
				
				Console.WriteLine();
				Console.WriteLine("Total de arquivos encontrados: {0}", arquivosEncontrados.Tamanho());
				Console.WriteLine("Total de arquivos verificados: {0}", listaDeArquivos.Tamanho());
			}
			catch(ExcecaoDeDiretorio e)
			{
				Console.WriteLine();
				Console.WriteLine(e.Mensagem);
			}
			catch(ExcecaoDeArquivo e)
			{
				Console.WriteLine();
				Console.WriteLine(e.Mensagem);
			}
			catch(ExcecaoDeAcesso e)
			{
				Console.WriteLine();
				Console.WriteLine(e.Mensagem);
			}
        }
    }
}
