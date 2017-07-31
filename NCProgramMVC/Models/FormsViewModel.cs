using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NCProgramMVC.Models
{
    public class InfoViewModels
    {
        [Display(Name = "Nome")]
        [Required]
        public string Nome { get; set; }
        [Display(Name = "Cognome")]
        [Required]
        public string Cognome { get; set; }
        [Required]
        [Display(Name = "Posta elettronica")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Tel./Cell.")]
        public string Tel { get; set; }
        [Display(Name = "Messaggio")]
        public string Messaggio { get; set; }
    }
    public class InfoSerViewModels
    {
        public string Servizio { get; set; }
        [Display(Name = "Nome")]
        [Required]
        public string Nome { get; set; }
        [Display(Name = "Cognome")]
        [Required]
        public string Cognome { get; set; }
        [Required]
        [Display(Name = "Posta elettronica")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Tel./Cell.")]
        public string Tel { get; set; }
        [Display(Name = "Messaggio")]
        public string Messaggio { get; set; }
    }
}