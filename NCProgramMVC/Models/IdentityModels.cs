using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace NCProgramMVC.Models
{
    // È possibile aggiungere dati di profilo dell'utente specificando altre proprietà della classe ApplicationUser. Per ulteriori informazioni, visitare http://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        [Display(Name = "Cognome")]
        public string Cognome { get; set; }
        [Display(Name = "Indirizzo")]
        public string Indirizzo { get; set; }
        [Display(Name = "Città")]
        public string Città { get; set; }
        [Display(Name = "CAP")]
        public string CAP { get; set; }
        [Display(Name = "Telefono")]
        public string Telefono { get; set; }
        [Display(Name = "Professione")]
        public string Professione { get; set; }
        [Display(Name = "Organizzazione di appartenenza")]
        public string Organizzazione { get; set; }
        [Display(Name = "Informativa privacy")]
        public bool Privacy { get; set; }
        [Display(Name = "Bloccato")]
        public bool Bloccato { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenere presente che il valore di authenticationType deve corrispondere a quello definito in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Aggiungere qui i reclami utente personalizzati
            return userIdentity;
        }
    }

    public class Slide
    {
        [Key]
        public int Slide_Id { get; set; }
        [Display(Name = "Titolo")]
        public string Titolo { get; set; }
        [Display(Name = "Sottotitolo")]
        public string Sottotitolo { get; set; }
        [Display(Name = "Descrizione")]
        public string Descrizione { get; set; }
        [Display(Name = "Immagine sfondo")]
        public string Sfondo { get; set; }
        [Display(Name = "Posizione")]
        public int Posizione { get; set; }
        [Display(Name ="Pubblica")]
        public bool Pubblica { get; set; }
    }

    public class Documenti
    {
        [Key]
        public int Documenti_Id { get; set; }
        [Display(Name = "Titolo")]
        public string Titolo { get; set; }
        [Display(Name = "Descrizione")]
        public string Descrizione { get; set; }
        [Display(Name = "Nome file")]
        public string Nomefile { get; set; }
        [Display(Name = "Riservato")]
        public bool Riservato { get; set; }
    }

    public class Servizi
    {
        [Key]
        public int Servizio_Id { get; set; }
        [Display(Name ="Categoria servizio")]
        public string Servizio { get; set; }
        [Display(Name ="Descrizione")]
        public string Descrizione { get; set; }

        public virtual ICollection<ServiziDett> Servizis { get; set; }

    }

    public class ServiziDett
    {
        [Key]
        public int ServizoDett_Id { get; set; }
        public int Servizio_Id { get; set; }
        public virtual Servizi Servizio { get; set; }
        [Display(Name ="Nome del servizio")]
        public string ServizioDett { get; set; }
        [Display(Name ="Descrizione")]
        public string Descrizione { get; set; }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Slide> Slides { get; set; }
        public DbSet<Documenti> Documentis { get; set; }
        public DbSet<Servizi> Servizis { get; set; }
        public DbSet<ServiziDett> ServiziDetts { get; set; }
    }

}