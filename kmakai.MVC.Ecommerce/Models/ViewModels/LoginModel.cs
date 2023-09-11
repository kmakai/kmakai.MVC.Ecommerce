﻿using System.ComponentModel.DataAnnotations;

namespace kmakai.MVC.Ecommerce.Models.ViewModels;

public class LoginModel
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    public string? ReturnUrl { get; set; }
}
