
namespace Salao.Domain.Abstract.Admin
{
    public interface ITrocaSenha
    {
        void TrocarSenha(int idUsuario, string senhaAnterior, string novaSenha);
    }
}
