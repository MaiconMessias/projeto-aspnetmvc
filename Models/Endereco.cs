using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace primeira_projeto_aspnetmvc.Models
{
    [Table("endereco", Schema="public")]
    public class Endereco {

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("logradouro", TypeName = "varchar(256)")]
        [Display(Name = "Logradouro")]
        public string Logradouro { get; set; }

        [Column("numero", TypeName = "integer")]
        [Display(Name = "Número")]
        public int Numero { get; set; }

        [Column("cep", TypeName = "varchar(10)")]
        [Display(Name = "CEP")]
        public string Cep { get; set; }

        [Column("bairro", TypeName = "varchar(50)")]
        public string Bairro { get; set; }

        [Column("cidade", TypeName = "varchar(30)")]
        public string Cidade { get; set; }

        [Column("estado", TypeName = "varchar(20)")]
        public string Estado { get; set; }
    }
}