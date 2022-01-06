using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eAuditoria.Data.Repository.ModelEntity
{
    [Table("FILME")]
    public class Filme
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [MaxLength(100)]
        public string Tituto { get; set; }

        [Required]
        public int ClassificacaoIndicativa { get; set; }

        [Required]
        public short Lancamento { get; set; }

    }
}
