using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eAuditoria.Data.Repository.ModelEntity
{
    [Table("LOCACAO")]
    public class Locacao
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Id_Cliente { get; set; }

        [Required]
        public int Id_Filme { get; set; }

        public DateTime DataLocacao { get; set; }

        public DateTime? DataDevolucao { get; set; }



        [ForeignKey("Id_Cliente")]
        public Cliente Cliente { get; set; }

        [ForeignKey("Id_Filme")]
        public Filme Filme { get; set; }

    }
}
