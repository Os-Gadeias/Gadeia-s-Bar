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

        mesa.OcuparMesa(OcuparAMesa: true);

        repositorioMesa.Editar(mesa.Id, mesa);

        return Result.Ok();
    }

    public List<ListarContaDto> SelecionarTodos()
    {
        return repositorioConta.SelecionarTodos().Select(c => new ListarContaDto(
            c.Id, c.NomeCliente, c.Garcom.Nome, c.Mesa.Id, c.Mesa.NumeroMesa,
            c.DataDeAbertura.ToShortDateString(), c.DataDeFechamento?.ToShortDateString(), c.StatusConta, c.ValorFinal
        )).ToList();
    }
    public List<SelectListItem> CarregarMesas(Guid? idMesaIgnorado = null)
    {
        return repositorioMesa.SelecionarTodos()
            .Where(m =>
                m.statusMesa == StatusMesa.Livre ||
                m.Id == idMesaIgnorado
            )
            .Select(m => new SelectListItem(
                m.NumeroMesa.ToString(),
                m.Id.ToString()
            ))
            .ToList();
    }
    public List<SelectListItem> CarregarGarcons()
    {
        return repositorioGarcom.SelecionarTodos()
            .Select(m => new SelectListItem(m.Nome.ToString(), m.Id.ToString()))
            .ToList();
    }

    public Result<ListarContaDto> SelecionarPorId(Guid id)
    {
        Conta? conta = repositorioConta.SelecionarPorId(id);

        if (conta is null)
            return Result.Fail("Conta não encontrada!");

        return new ListarContaDto(
            conta.Id,
            conta.NomeCliente,
            conta.Garcom.Nome,
            conta.Mesa.Id,
            conta.Mesa.NumeroMesa,
            conta.DataDeAbertura.ToShortDateString(),
            conta.DataDeFechamento?.ToShortDateString(),
            conta.StatusConta,
            conta.ValorFinal);
    }

    public Result Excluir(ExcluirContaDto dto)
    {
        Conta? conta = repositorioConta.SelecionarPorId(dto.Id);

        if (conta is null)
            return Result.Fail("Conta não encontrada!");

        Mesa? mesa = repositorioMesa.SelecionarPorId(dto.IdMesa);

        if (mesa is null)
            return Result.Fail("Mesa não encontrada!");

        repositorioConta.Excluir(dto.Id);

        mesa.OcuparMesa(OcuparAMesa: false);

        repositorioMesa.Editar(mesa.Id, mesa);

        return Result.Ok();
    }

    public Result Editar(EditarContaDto dto)
    {
        Conta? conta = repositorioConta.SelecionarPorId(dto.Id);

        if (conta is null)
            return Result.Fail("Conta não encontrada!");

        Mesa? mesa = repositorioMesa.SelecionarPorId(new Guid(dto.Mesa));

        if (mesa is null)
            return Result.Fail("Mesa não encontrada!");

        Garcom? garcom = repositorioGarcom.SelecionarPorId(new Guid(dto.Garcom));

        if (garcom is null)
            return Result.Fail("Garçom não encontrado!");

        Conta contaAtualizada = new(dto.NomeCliente, garcom, mesa);

        Result resultadoValidacao = ValidarEntidade(contaAtualizada);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioConta.Editar(conta.Id, contaAtualizada);

        return Result.Ok();
    }
}
