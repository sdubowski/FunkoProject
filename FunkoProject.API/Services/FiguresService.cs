using FunkoProject.Entities;
using FunkoProject.Exceptions;
using static FunkoProject.Entities.FiguresDbConntext;

namespace FunkoProject.Services
{
    public class FiguresService
    {
        public interface IFiguresService
        {
            public Figure GetById(string id);
            public void AddNewFigure(Figure figure);
            public void DeleteFigure(string Id);
            public void EditFigure(Figure figure);
        }

        public class FiguresServices : IFiguresService 
        {
            private readonly FigurestDbContext _context;
            public FiguresServices(FigurestDbContext context)
            {
                _context = context;
            }

            public Figure GetById(string id)
            {
                var idAsInt = Int32.Parse(id);
                var figure = GetFigureById(idAsInt);

                if (figure == null)
                {
                    throw new NotFoundException("Figure not found");
                }

                return figure;
            }

            public void AddNewFigure(Figure figure)
            {
                _context.Figures.Add(figure);
                _context.SaveChanges();
            }

            public void EditFigure(Figure figure)
            {
                var actualFigure = _context.Figures.FirstOrDefault(f => f.Id == figure.Id);
                if (actualFigure == null)
                {
                    throw new NotFoundException("Figure not found");
                }
                else
                {
                    actualFigure.Title = figure.Title;
                    actualFigure.Handle = figure.Handle;
                    actualFigure.Series = figure.Series;
                    _context.SaveChanges();
                }
            }

            public void DeleteFigure(string Id)
            {
                var figure = _context.Figures.FirstOrDefault(f => f.Equals(Id));
                if (figure == null)
                {
                    throw new NotFoundException("Figure not found");
                }
                else
                {
                    _context.Figures.Remove(figure);
                }
                _context.SaveChanges();
            }

            private Figure GetFigureById(int id)
            {
                var figure = _context.Figures.FirstOrDefault(f => f.Id == id);
                return figure;
            }
        }

    }
}
