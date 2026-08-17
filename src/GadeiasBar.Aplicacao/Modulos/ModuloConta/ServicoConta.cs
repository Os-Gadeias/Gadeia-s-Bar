using FluentResults;
using GadeiasBar.Aplicacao.Compartilhado;
using GadeiasBar.Dominio.Modulos.ModuloConta;
using GadeiasBar.Dominio.Modulos.ModuloGarcom;
using GadeiasBar.Dominio.Modulos.ModuloMesa;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GadeiasBar.Aplicacao.Modulos.ModuloConta;

public class ServicoConta(
    IRepositorioConta repositorioConta,
    IRepositorioMesa repositorioMesa,
    IRepositorioGarcom repositorioGarcom) : ServicoBase<Conta>
{
    public Result Cadastrar(CadastrarContaDto dto)
    {
        Mesa? mesa = repositorioMesa.SelecionarPorId(new Guid(dto.Mesa));

        if (mesa is null)
            return Result.Fail("Mesa não encontrada!");

        Garcom? garcom = repositorioGarcom.SelecionarPorId(new Guid(dto.Garcom));

        if (garcom is null)
            return Result.Fail("Garçom não encontrado!");

        Conta conta = new(dto.NomeCliente, garcom, mesa);

        Result validarEntidade = ValidarEntidade(conta);

        if (validarEntidade.IsFailed)
            return validarEntidade;

        repositorioConta.Cadastrar(conta);

        return Result.Ok();
    }

    public List<ListarContaDto> SelecionarTodos()
    {
        return repositorioConta.SelecionarTodos().Select(c => new ListarContaDto(c.Id, c.NomeCliente, c.Garcom.Nome, c.Mesa.NumeroMesa
        , c.DataDeAbertura, c.DataDeFechamento, c.StatusConta, c.ValorFinal
        )).ToList();
    }
    public List<SelectListItem> CarregarMesas()
    {
        return repositorioMesa.SelecionarTodos().Select(m => new SelectListItem(m.NumeroMesa.ToString(), m.Id.ToString())).ToList();
    }
    public List<SelectListItem> CarregarGarcons()
    {
        return repositorioGarcom.SelecionarTodos().Select(m => new SelectListItem(m.Nome.ToString(), m.Id.ToString())).ToList();
    }


}
