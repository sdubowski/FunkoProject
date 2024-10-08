﻿namespace FunkoProject.Web.Models.ViewModels;

public class UserViewModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
    public string ProfileImageUrl { get; set; }
    public int FigureCount { get; set; }
}