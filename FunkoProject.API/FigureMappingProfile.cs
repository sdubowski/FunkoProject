using AutoMapper;
using FunkoApi.Entities;
using FunkoApi.Models;

namespace FunkoApi
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
