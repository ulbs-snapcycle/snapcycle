﻿using System.ComponentModel.DataAnnotations;
using polyclinic_service.Users.Models;

namespace polyclinic_service.Users.DTOs;

public class UpdateUserRequest
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
    
    [Required]
    public string Phone { get; set; }
    
    [Required]
    public UserType Type { get; set; }
    
    public override string ToString()
    {
        String message = "";
        message += $"Id: {Id}\n";
        message += $"Name: {Name}\n";
        message += $"Email: {Email}\n";
        message += $"Password: {Password}\n";
        message += $"Phone: {Phone}\n";
        message += $"Type: {Type}\n";
        return message;
    }
}