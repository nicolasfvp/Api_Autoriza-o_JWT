using ExercicioSemana2.Models;

namespace ExercicioSemana2.Services
{
    public interface ITokenService
    {
        string GerarToken(Funcionario funcionario);
    }
}
