using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBoot.Models
{
    [Table("RespostasChat")]
    public class RespostasChat
    {   
        [Column("Id")]
        [Display(Name ="Códigos")]
        public int id { get; set; }


        [Column("Resposta")]
        [Display(Name = "Respostas")]
        public String Resposta { get; set; }

        [Column("Mensagem")]
        [Display(Name = "Mensagem")]
        public String Mensagem { get; set; }
    }
}

