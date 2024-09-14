using AutoMapper;
using FunkoProject.Data.Entities;
using FunkoProject.Models;

namespace FunkoProject
{
    public class FigureMappingProfile : Profile
    {
        public FigureMappingProfile()
        {
            CreateMap<Figure, FigureDto>();
            CreateMap<RegisterFigureDto, Figure>();
        }
    }
}
