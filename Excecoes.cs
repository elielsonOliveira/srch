using System;

namespace excecoes
{

    class ExcecaoDeArquivo : Exception
    {
        public string Mensagem;

        public ExcecaoDeArquivo()
        {
            Mensagem = "[Erro] Erro de arquivo ...";
        }
    } 
	
	class ExcecaoDeDiretorio : Exception
    {
        public string Mensagem;

        public ExcecaoDeDiretorio()
        {
            Mensagem = "O nome de diretório passado como argumento não existe ...";
        }
    }  

	class ExcecaoDeAcesso : Exception
    {
        public string Mensagem;

        public ExcecaoDeAcesso()
        {
            Mensagem = "Acesso não autorizado a um diretório.";
        }
    }  	
}
