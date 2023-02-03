using System.ComponentModel.DataAnnotations.Schema;

namespace ExercicioSemana2.Dto
{
    public class FuncionarioRequest
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public decimal Salario { get; set; }

        [Column("permissaoId")]
        public int PermissaoId { get; set; }
    }
}
