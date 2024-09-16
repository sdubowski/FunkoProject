using AutoMapper;

namespace FunkoProject.Extensions;

public static class MappingExtensions
{
    public static class MapperAccessor
    {
        public static IMapper Mapper { get; private set; }

        public static void Configure(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
    
    public static TDestination ToModel<TSource, TDestination>(this TSource source)
    {
        return MapperAccessor.Mapper.Map<TDestination>(source);
    }
    
    public static List<TDestination> ToModelList<TSource, TDestination>(this IEnumerable<TSource> source)
    {
        return MapperAccessor.Mapper.Map<List<TDestination>>(source);
    }
    
    public static TDestination ToEntity<TSource, TDestination>(this TSource source)
    {
        return MapperAccessor.Mapper.Map<TDestination>(source);
    }
    
    public static List<TDestination> ToEntityList<TSource, TDestination>(this IEnumerable<TSource> source)
    {
        return MapperAccessor.Mapper.Map<List<TDestination>>(source);
    }
}