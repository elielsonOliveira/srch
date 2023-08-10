using System;
using System.Collections.Generic;

namespace listaligada
{
    // Classe que representa um nó da lista
    public class NoListaLigada<Item> where Item : System.IComparable<Item>
    {
        public Item valor;											// Item
        public NoListaLigada<Item> proximo;                       	// Referência para o próximo nó

        // Construtor padrão do nó da lista
        public NoListaLigada()
        { }

        // Construtor de nó que permite argumentos de inicialização
        public NoListaLigada(Item valor, NoListaLigada<Item> proximo)
        {
            this.valor = valor;
            this.proximo = proximo;
        }
    }

    public class ListaLigada<Item> where Item : System.IComparable<Item>
    {
		public NoListaLigada<Item> head = null;                                   // Nó "Head" da lista
		
        // Método que adiciona um item à lista
        public void Adiciona(Item valor)
        {
			this.head = new NoListaLigada<Item>(valor, this.head);
        }

		// Método que adiciona um item em uma posição da lista
        public void AdicionaEm(int indice, Item valor)
        {
			NoListaLigada<Item> novo;
			NoListaLigada<Item> aux;
            NoListaLigada<Item> iterador = this.head;
            NoListaLigada<Item> anterior = this.head;

			if(indice < 0 || indice > this.Tamanho())
				throw new IndexOutOfRangeException();
			
			if(this.Tamanho() == 0 || indice == this.Tamanho())
			{
				this.Adiciona(valor);
				return ;
			}
			
			int i = 0;
			do
			{
				if (i == (this.Tamanho() - indice))
				{
					novo = new NoListaLigada<Item>(valor, iterador);
					anterior.proximo = novo;
					return ;
				}
				anterior = iterador;
				iterador = iterador.proximo;
				i++;
			}while (iterador != null);
			
			ListaLigada<Item> nova = new ListaLigada<Item>();
			for(int j = 0; j < this.Tamanho(); j++)
				nova.Adiciona(this.ObtemItemEm(j));
			this.Limpar();
			this.Adiciona(valor);
			for(int j = 0; j < nova.Tamanho(); j++)
				this.Adiciona(nova.ObtemItemEm(j));
        }
		
        // Método que adiciona otra lista a lista atual (concatena listas)
        public void AdicionaLista(ListaLigada<Item> lista)
        {
            NoListaLigada<Item> iterador = lista.head;
            
            while (iterador != null)                                        
            {
                this.Adiciona(iterador.valor);
                iterador = iterador.proximo;
            }
        }

        // Método que remove um item da lista
        public bool Remove(Item valor)
        {
            NoListaLigada<Item> iterador = this.head;
            NoListaLigada<Item> anterior = this.head;

            if (head.valor.CompareTo(valor) == 0)
            {
                this.head = this.head.proximo;
                return true;
            }
            while (iterador != null)
            {
                if (iterador.valor.CompareTo(valor) == 0)
                {
                    anterior.proximo = iterador.proximo;
                    return true;
                }
                anterior = iterador;
                iterador = iterador.proximo;
            }
            return false;
        }
		
		// Método que remove um item da lista
        public bool RemoveEm(int indice)
        {
            NoListaLigada<Item> iterador = this.head;
            NoListaLigada<Item> anterior = this.head;

			if(indice >= 0 && indice < this.Tamanho())
			{
				if(indice == this.Tamanho() - 1)
				{
					this.head = this.head.proximo;
					return true;
				}
				int i = 1;
				do
				{
					if (i == this.Tamanho() - indice)
					{
						anterior.proximo = iterador.proximo;
						return true;
					}
					anterior = iterador;
					iterador = iterador.proximo;
					i++;
				}while (true);
				return false;
			}
			throw new IndexOutOfRangeException();
        }
		
        // Método que evazia a lista
        public void Limpar()
        {
            this.head = null;

        }

        // Método que verifica se um item está na lista
        public bool Contem(Item valor)
        {
            NoListaLigada<Item> iterador = this.head;
            while (iterador != null)
            {
                if (iterador.valor.CompareTo(valor) == 0)
                {
                    return true;
                }
                iterador = iterador.proximo;
            }
            return false;
        }

        // Método que obtém o número de itens na lista
        public int Tamanho()
        {
            int i = 0;
            NoListaLigada<Item> iterador = this.head;
            while (iterador != null)
            {
                i++;
                iterador = iterador.proximo;
            }
            return i;
        }

        // Método que busca o índice de um item na lista
        public int IndiceDe(Item valor)
        {
            int indice = 0;
            NoListaLigada<Item> iterador = this.head;
            if (iterador != null)
            {
                while (iterador != null)
                {
                    indice++;
                    if (iterador.valor.CompareTo(valor) == 0)
                    {
                        return ++indice;
                    }
                    iterador = iterador.proximo;
                }
                return -1;
            }
            throw new NullReferenceException();
        }

        // Método que busca um item na lista cuja posição é dada por um índice
        public Item ObtemItemEm(int indice)
        {
			int i = this.Tamanho() - 1;
			NoListaLigada<Item> iterador = this.head;
			if (iterador != null)
            {
                if (indice > -1 & indice < this.Tamanho())
                {
                    while (i > indice)
                    {
                        iterador = iterador.proximo;
                        i--;
                    }
                }
                else
                    throw new IndexOutOfRangeException();
                return iterador.valor;
            }
            throw new NullReferenceException();
        }

    }

    // Classe Lista, ela contém um método estático de converção
    public class Lista
    {
        // Método que converte um vetor numa lista e retorna essa lista
        public static ListaLigada<Item> ConverteEmLista<Item>(Item[] vetor) where Item : System.IComparable<Item>
        {
			ListaLigada<Item> lista = null;
			if(vetor != null)
			{
				lista = new ListaLigada<Item>();
				for (int i = 0; i < vetor.Length; i++)
				{
					lista.Adiciona(vetor[i]);
				}
			}
            return lista;
        }
    }
}
