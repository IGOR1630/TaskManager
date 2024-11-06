using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface ITarefaService
    {
   
        IEnumerable<Tarefa> ObterTodasTarefas();

 
        Tarefa ObterTarefaPorId(int id);


        int InserirTarefa(Tarefa tarefa);

      
        void EditarTarefa(Tarefa tarefa);

        
        void RemoverTarefa(int id);

        
        bool VerificarNomeTarefaExistente(string nome);

      
        void ReordenarTarefas(int idTarefa, int novaOrdem);
    }
}
