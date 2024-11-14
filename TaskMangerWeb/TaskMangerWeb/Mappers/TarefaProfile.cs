using AutoMapper;
using Core;
using TaskMangerWeb.Models;

namespace TaskManagerWeb.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tarefa, TarefaModel>();
            CreateMap<TarefaModel, Tarefa>();
        }
    }

}
