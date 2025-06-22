using System.Runtime.InteropServices;
using FunkoProject.Common.Atributes;
using FunkoProject.Data;
using FunkoProject.Data.Entities;
using FunkoProject.Exceptions;
using FunkoProject.Models;

namespace FunkoProject.Services;

public interface IFiguresService
{
    public Figure GetById(string id);
    public void AddNewFigure(Figure figure);
    public void DeleteFigure(string Id);
    public void EditFigure(Figure figure);
    public List<UserFigure> GetAllUsersFigures(int userId);
    void AddNewUserFigure(RegisterFigureDto userFigure);
}

public class FiguresServices : IFiguresService
{
    private readonly AppDbContext _context;

    public FiguresServices(AppDbContext context)
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

    public List<UserFigure> GetAllUsersFigures(int userId)
    {
        var figures = _context.UserFigures.Where(f => f.UserId == userId).ToList();
        return figures;
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

    public void AddNewUserFigure(RegisterFigureDto userFigure)
    {
        var figure = new UserFigure
        {
            Name = userFigure.FigureName,
            Description = userFigure.Description,
            UserId = userFigure.UserId,
        };
        figure.UserId = 27;
        _context.UserFigures.Add(figure);
        _context.SaveChanges();
    }
}