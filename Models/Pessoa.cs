using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace primeira_projeto_aspnetmvc.Models
{
    [Table("pessoa", Schema = "public")]
    public class Pessoa {
        
        [Column("id", TypeName = "integer")]
        [Key]
        public int Id { get; set; }

        [Column("nome", TypeName = "varchar(100)")]
        public string Nome { get; set; }

        [Column("cpf", TypeName = "varchar(20)")]
        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        //[Column("endereco")]
        [Display(Name = "Endereço")]
        public int EnderecoId { get; set; }
        [Display(Name = "Endereço")]
        public Endereco Endereco { get; set; }
    }
}