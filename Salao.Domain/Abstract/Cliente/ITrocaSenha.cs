
namespace Salao.Domain.Abstract.Cliente
{
    public interface ITrocaSenha
    {
        void TrocarSenha(int idUsuario, string senhaAnterior, string novaSenha, bool enviarEmail = true);
        void RedefinirSenha(int idUsuario);
    }
}
