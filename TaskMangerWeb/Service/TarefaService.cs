using Core;
using Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal class TarefaService : ITarefaService
    {
        public void EditarTarefa(Tarefa tarefa)
        {
            throw new NotImplementedException();
        }

        public int InserirTarefa(Tarefa tarefa)
        {
            throw new NotImplementedException();
        }

        public Tarefa ObterTarefaPorId(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tarefa> ObterTodasTarefas()
        {
            throw new NotImplementedException();
        }

        public void RemoverTarefa(int id)
        {
            throw new NotImplementedException();
        }

        public void ReordenarTarefas(int idTarefa, int novaOrdem)
        {
            throw new NotImplementedException();
        }

        public bool VerificarNomeTarefaExistente(string nome)
        {
            throw new NotImplementedException();
        }
    }
}
