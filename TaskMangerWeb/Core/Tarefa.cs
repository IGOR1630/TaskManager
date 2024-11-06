using System;
using System.Collections.Generic;

namespace Core;

public partial class Tarefa
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public decimal Custo { get; set; }

    public DateTime DataLimite { get; set; }

    public int? OrdemApresentacao { get; set; }

    public string Tarefascol { get; set; } = null!;
}
