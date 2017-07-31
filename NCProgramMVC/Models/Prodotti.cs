using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NCProgramMVC.Models
{
    public class Prodotti
    {
    }
    public class Tdm
    {
        [Key]
        public int Tdm_Id { get; set; }
        [Display(Name = "Gruppo prodotti")]
        public string Prodotto { get; set; }
        [Display(Name = "Descrizione")]
        public string Descrizione { get; set; }

        public virtual ICollection<TdmDett> Tdms { get; set; }

    }

    public class TdmDett
    {
        [Key]
        public int TdmDett_Id { get; set; }
        public int Tdm_Id { get; set; }
        public virtual Tdm Prodotto { get; set; }
        [Display(Name = "Nome del prodotto")]
        public string ProdottoDett { get; set; }
        [Display(Name = "Descrizione")]
        public string Descrizione { get; set; }

    }


    public class Cimco
    {
        [Key]
        public int Cimco_Id { get; set; }
        [Display(Name = "Gruppo prodotti")]
        public string Prodotto { get; set; }
        [Display(Name = "Descrizione")]
        public string Descrizione { get; set; }

        public virtual ICollection<CimcoDett> Cimcos { get; set; }

    }

    public class CimcoDett
    {
        [Key]
        public int CimcoDett_Id { get; set; }
        public int Cimco_Id { get; set; }
        public virtual Cimco Prodotto { get; set; }
        [Display(Name = "Nome del prodotto")]
        public string ProdottoDett { get; set; }
        [Display(Name = "Descrizione")]
        public string Descrizione { get; set; }
    }

    public class Mazacam
    {
        [Key]
        public int Mazacam_Id { get; set; }
        [Display(Name = "Gruppo prodotti")]
        public string Prodotto { get; set; }
        [Display(Name = "Descrizione")]
        public string Descrizione { get; set; }

        public virtual ICollection<MazacamDett> Mazacams { get; set; }

    }

    public class MazacamDett
    {
        [Key]
        public int MazacamDett_Id { get; set; }
        public int Mazacam_Id { get; set; }
        public virtual Mazacam Prodotto { get; set; }
        [Display(Name = "Nome del prodotto")]
        public string ProdottoDett { get; set; }
        [Display(Name = "Descrizione")]
        public string Descrizione { get; set; }
    }

}