using System;
using System.ComponentModel.DataAnnotations;

namespace TaskMangerWeb.Models
{
    public class TarefaModel
    {
        [Key]
        public int ID { get; set; }  // Identificador da tarefa (chave primária)

        [Required(ErrorMessage = "O nome da tarefa é obrigatório.")]
        [Display(Name = "Nome da Tarefa")]
        [StringLength(100, ErrorMessage = "O nome da tarefa deve ter no máximo 100 caracteres.")]
        public required string Nome { get; set; }  // Nome da tarefa

        [Required(ErrorMessage = "O custo da tarefa é obrigatório.")]
        [Range(0, double.MaxValue, ErrorMessage = "O custo deve ser um valor positivo.")]
        [Display(Name = "Custo (R$)")]
        public decimal Custo { get; set; }  // Custo em R$

        [Required(ErrorMessage = "A data limite é obrigatória.")]
        [Display(Name = "Data Limite")]
        [DataType(DataType.Date)]
        public DateTime DataLimite { get; set; }  // Data limite da tarefa

        
        [Display(Name = "Ordem de Apresentação")]
        public int OrdemApresentacao { get; set; }  // Ordem de apresentação (para ordenação)
    }
}
