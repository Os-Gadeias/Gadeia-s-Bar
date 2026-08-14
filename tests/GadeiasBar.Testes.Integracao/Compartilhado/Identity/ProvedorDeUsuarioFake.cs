using GadeiasBar.Dominio.Compartilhado.Identity;

namespace GadeiasBar.Testes.Integracao.Compartilhado.Identity;

public sealed class ProvedorDeUsuarioFake(Guid userId) : IProvedorDeUsuario
{
    public Guid? Id => userId;

    public bool EstaAutenticado => true;
}
