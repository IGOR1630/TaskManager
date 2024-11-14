using Core;
using Core.Service;

namespace Service
{
    public class TarefaService : ITarefaService
    {
        private readonly TaskManagerContext _context;

        public TarefaService(TaskManagerContext context)
        {
            _context = context;
        }

        public void EditarTarefa(Tarefa tarefa)
        {
            var tarefaExistente = _context.Tarefas.Find(tarefa.Id);
            if (tarefaExistente == null)
                throw new Exception("Tarefa não encontrada.");

            if (_context.Tarefas.Any(t => t.Nome == tarefa.Nome && t.Id != tarefa.Id))
                throw new Exception("Já existe uma tarefa com este nome.");

            tarefaExistente.Nome = tarefa.Nome;
            tarefaExistente.Custo = tarefa.Custo;
            tarefaExistente.DataLimite = tarefa.DataLimite;
            _context.SaveChanges();
        }

        public int InserirTarefa(Tarefa tarefa)
        {
            if (VerificarNomeTarefaExistente(tarefa.Nome))
                throw new Exception("Já existe uma tarefa com este nome.");

            tarefa.OrdemApresentacao = _context.Tarefas.Count() + 1;
            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();
            return tarefa.Id;
        }

        public Tarefa ObterTarefaPorId(int id)
        {
            return _context.Tarefas.Find(id) ?? throw new Exception("Tarefa não encontrada.");
        }

        public IEnumerable<Tarefa> ObterTodasTarefas()
        {
            return _context.Tarefas.OrderBy(t => t.OrdemApresentacao).ToList();
        }

        public void RemoverTarefa(int id)
        {
            var tarefa = ObterTarefaPorId(id);
            _context.Tarefas.Remove(tarefa);
            _context.SaveChanges();
        }

        public void ReordenarTarefas(int idTarefa, int novaOrdem)
        {
            var tarefa = ObterTarefaPorId(idTarefa);
            if (novaOrdem < 1 || novaOrdem > _context.Tarefas.Count())
                throw new ArgumentOutOfRangeException("A nova ordem está fora do intervalo.");

            var tarefas = _context.Tarefas.OrderBy(t => t.OrdemApresentacao).ToList();
            tarefas.Remove(tarefa);
            tarefas.Insert(novaOrdem - 1, tarefa);

            for (int i = 0; i < tarefas.Count; i++)
            {
                tarefas[i].OrdemApresentacao = i + 1;
                _context.Tarefas.Update(tarefas[i]);
            }

            _context.SaveChanges();
        }

        public bool VerificarNomeTarefaExistente(string nome)
        {
            return _context.Tarefas.Any(t => t.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
        }
    }
}
