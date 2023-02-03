using System.ComponentModel.DataAnnotations.Schema;

namespace ExercicioSemana2.Models
{
    public class Permissao
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("nome")]
        public string Nome { get; set; }
    }
}
