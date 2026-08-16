using System.Net.Mail;
using FluentResults;
using GadeiasBar.Aplicacao.Compartilhado;
using GadeiasBar.Dominio.Modulos.ModuloGarcom;

namespace GadeiasBar.Aplicacao.Modulos.ModuloGarcom;

public class ServicoGarcom(IRepositorioGarcom repositorioGarcom) : ServicoBase<Garcom>
{
    public Result Cadastrar(CadastrarGarcomDto dto)
    {
        Garcom garcom = new(dto.Nome);

        Result validarEntidade = ValidarEntidade(garcom);

        if (validarEntidade.IsFailed)
            return Falha(nameof(dto.Nome), validarEntidade.Errors.First().Message);

        if (ExisteGarcomComMesmoNome(dto.Nome))
            return Falha(nameof(dto.Nome), "Já existe um garçom com esse nome!");

        repositorioGarcom.Cadastrar(garcom);

        return Result.Ok();
    }

    public List<ListarGarcomDto> SelecionarTodos()
    {
        return repositorioGarcom.SelecionarTodos().Select(g =>
        new ListarGarcomDto(g.Id, g.Nome))
        .ToList();
    }
    private bool ExisteGarcomComMesmoNome(string nome, Guid? idIgnorado = null)
    {
        return repositorioGarcom.SelecionarTodos().Any(g => g.Nome == nome && g.Id != idIgnorado);
    }

}
