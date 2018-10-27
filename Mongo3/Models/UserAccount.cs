using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Mongo3.Models
{
    public class UserAccount
    {
        [Key]
        public int UserID { get; set; }
        [Required(ErrorMessage ="Se requiere el primer nombre")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Se requiere el Apellido")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Se requiere el email")]
        
        public string Email { get; set; }
        [Required(ErrorMessage = "Se requiere el nombre de usuario")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Se requiere una contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password",ErrorMessage ="Por favor confirme su contraseña")]
        [DataType(DataType.Password)] 
        public string ConfirmPassword { get; set; }
            
    }
}