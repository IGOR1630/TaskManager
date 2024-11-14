using Microsoft.AspNetCore.Mvc;
using TaskMangerWeb.Models;
using Core.Service;
using Core;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace TaskManagerWeb.Controllers
{
    public class TarefaController : Controller
    {
        private readonly ITarefaService _tarefaService;
        private readonly IMapper _mapper;
        private readonly TaskManagerContext _context;

        // Injeção de dependência do serviço de tarefa
        public TarefaController(ITarefaService tarefaService, IMapper mapper, TaskManagerContext context)
        {
            _tarefaService = tarefaService;
            _mapper = mapper;
            _context = context;
        }


        // GET: TarefaController
        public ActionResult Index()
        {
            var tarefas = _tarefaService.ObterTodasTarefas();
            var tarefaModels = _mapper.Map<IEnumerable<TarefaModel>>(tarefas); // Mapeia para o modelo da view
            return View(tarefaModels); // Use tarefaModels aqui
        }


        // GET: TarefaController/Details/5
        public ActionResult Details()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangeOrder(int id, int direction)
        {
            var task = _context.Tarefas.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            // Calculando a nova ordem
            int newOrder = (task.OrdemApresentacao ?? 0) + direction;

            // Verifica se a tarefa que será movida existe na nova posição
            var otherTask = _context.Tarefas.FirstOrDefault(t => t.OrdemApresentacao == newOrder);
            if (otherTask != null)
            {
                // Troca a ordem das duas tarefas
                otherTask.OrdemApresentacao = task.OrdemApresentacao;
                task.OrdemApresentacao = newOrder;

                // Salvar as alterações
                _context.SaveChanges();
            }

            return Ok();
        }



        // GET: TarefaController/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TarefaModel model)
        {
            if (!ModelState.IsValid)
            {
                // Log para depuração
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine("Erro no ModelState: " + error.ErrorMessage); // Pode ser um log de erro real
                }
                return View(model); // Caso o modelo seja inválido, ele retorna a view com os erros
            }

            if (_tarefaService.VerificarNomeTarefaExistente(model.Nome))
            {
                ModelState.AddModelError("Nome", "Uma tarefa com esse nome já existe.");
                return View(model); // Retorna a view com o erro
            }

            var tarefa = _mapper.Map<Tarefa>(model); // Mapear o modelo para o modelo de domínio
            _tarefaService.InserirTarefa(tarefa);

            return RedirectToAction(nameof(Index)); // Redireciona para a lista de tarefas
        }
        // GET: TarefaController/Edit/5
        public ActionResult Edit(int id)
        {
            var tarefa = _tarefaService.ObterTarefaPorId(id);
            if (tarefa == null)
            {
                return NotFound();
            }

            var model = new TarefaModel
            {
                ID = tarefa.Id,
                Nome = tarefa.Nome,
                Custo = tarefa.Custo,
                DataLimite = tarefa.DataLimite,
                OrdemApresentacao = tarefa.OrdemApresentacao ?? 0 // Handle nullable int
            };

            return View(model);
        }

        // POST: TarefaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TarefaModel model)
        {
            if (ModelState.IsValid)
            {
                if (_tarefaService.VerificarNomeTarefaExistente(model.Nome))
                {
                    ModelState.AddModelError("Nome", "Uma tarefa com esse nome já existe.");
                    return View(model);
                }

                var tarefa = new Tarefa
                {
                    Id = id,
                    Nome = model.Nome,
                    Custo = model.Custo,
                    DataLimite = model.DataLimite,
                    OrdemApresentacao = model.OrdemApresentacao
                };

                _tarefaService.EditarTarefa(tarefa);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: TarefaController/Delete/5
        public ActionResult Delete(int id)
        {
            var tarefa = _tarefaService.ObterTarefaPorId(id);
            if (tarefa == null)
            {
                return NotFound();
            }

            // Mapear `tarefa` para `TarefaModel` se necessário
            var tarefaModel = new TaskMangerWeb.Models.TarefaModel
            {
                ID = tarefa.Id,
                Nome = tarefa.Nome,
                Custo = tarefa.Custo,
                DataLimite = tarefa.DataLimite,
                OrdemApresentacao = tarefa.OrdemApresentacao ?? 0
            };

            return View(tarefaModel);
        }
        // POST: TarefaController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var tarefa = _tarefaService.ObterTarefaPorId(id);
            if (tarefa == null)
            {
                return NotFound();
            }

            _tarefaService.RemoverTarefa(id);
            return RedirectToAction(nameof(Index));
        }
    }
}