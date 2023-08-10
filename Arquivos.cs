using System;
using System.IO;
using listaligada;
using excecoes;

namespace arquivos
{
	// Classe que manipula arquivos no programa
	public static class Arquivos
    {
		private static bool acessonegado = false;
		
		public static bool AcessoNegado
		{
			get{	return acessonegado; }
			set{	acessonegado = value; }
		}
		
		/*
        // Método que recebe um diretório como argumento e busca todos os arquivos
        // contidos nesse diretório e em seus subdiretórios de maneira recursiva. Caso o argumento "subs" seja falso,
		// a busca não é feita em subdiretórios
        */
		public static ListaLigada<string> ObtemArquivos(string diretorio, bool subs)
		{
            ListaLigada<string> listaDeArquivos = new ListaLigada<string>();		// Cria lista
            try
            {
				if(!Directory.Exists(diretorio))									// Verifica se o diretório existe
				{
					throw new ExcecaoDeDiretorio();
				}
				if(subs == true)													// Se a procura em subdiretórios é verdadeira
				{
					string[] subDiretorios = Directory.GetDirectories(diretorio);  	// Obtém os subdiretórios
					string[] arquivos = Directory.GetFiles(diretorio);             	// Obtém os arquivos do diretório atual 
					
					// Percorrendo subdiretórios
					if (subDiretorios != null)
					{
						// Laço que percorre o vetor de subdiretórios e obtém os arquivos desses subdiretórios
						for (int i = 0; i < subDiretorios.Length; i++)
							listaDeArquivos.AdicionaLista(ObtemArquivos(subDiretorios[i], subs));	// Concatena com a lista obtida na chamada do método à lista atual

						listaDeArquivos.AdicionaLista(Lista.ConverteEmLista(arquivos));          	// Converte o vetor de arquivos numa lista e adiciona à lista já existente de arquivos
					}
					// Se não existem subdiretórios
					else
					{
						listaDeArquivos = Lista.ConverteEmLista(arquivos);         	// Converte o vetor de arquivos numa lista de arquivos
					}
				}
				// Se a busca em subdiretórios foi desabilitada
				else
				{
					string[] arquivos = Directory.GetFiles(diretorio);            	// Obtém os arquivos do diretório atual 
					listaDeArquivos = Lista.ConverteEmLista(arquivos);             	// Converte o vetor de arquivos numa lista de arquivos
				}
				return listaDeArquivos;                                   			// Retorna a lista
            }
            catch (UnauthorizedAccessException excGetDir)                          	// Exceção causada por erro na tentativa de acessar um diretório
            {
                AcessoNegado = true;
            }
            catch (IOException exc)                                                	// Exceção causada por erro de arquivo
            {
                throw new ExcecaoDeArquivo();
            }
            return listaDeArquivos;
        }
	
		// Método que recebe um arquivo a ser procurado e a lista de caminhos de arquivos
		public static ListaLigada<string> BuscaArquivos(string arquivoProcurado, ListaLigada<string> lista)
		{
			string caminhoDoArquivo;
			ListaLigada<string> arquivos = new ListaLigada<string>();
			for(int i = 0; i < lista.Tamanho(); i++)                   	// Laço que percorre toda a lista em busca da ocorrência do nome de arquivo procurado
			{
				caminhoDoArquivo = lista.ObtemItemEm(i);
				if (caminhoDoArquivo.Contains(arquivoProcurado))      	// Cada caminho que contém o arquivo procurado é inserido na lista
					arquivos.Adiciona(caminhoDoArquivo);
			}
			return arquivos;                                            // Retorna a lista com os caminhos que contém o arquivo ou uma lista vazia
		}
	}
}