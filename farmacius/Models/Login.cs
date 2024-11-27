﻿using System.ComponentModel.DataAnnotations;

namespace farmacius.Models
{
    public class Login
    {
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")][EmailAddress(ErrorMessage = "Correo electrónico no válido.")] 
        public string Email { get; set; }
        [Required(ErrorMessage = "La contraseña es obligatoria.")][DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Recordarme")] public bool RememberMe { get; set; }
    }
}
