using FluentResults;
using GadeiasBar.Aplicacao.Compartilhado;
using GadeiasBar.Dominio.Modulos.ModuloMesa;

namespace GadeiasBar.Aplicacao.Modulos.ModuloMesa;

public class ServicoMesa(IRepositorioMesa repositorioMesa) : ServicoBase<Mesa>
{
    public Result Cadastrar(CadastrarMesaDto dto)
    {
        Mesa mesa = new Mesa(dto.NumeroMesa, dto.QuantidadeLugares, dto.StatusMesa);

        Result resultValidacao = ValidarEntidade(mesa);

        if (resultValidacao.IsFailed)
            return resultValidacao;

        if (ExiateMesa_ComMesmoNumero(dto.NumeroMesa))
            return Falha(nameof(dto.NumeroMesa), $"O Numero da Mesa: {dto.NumeroMesa} Já esta sendo usado");

        repositorioMesa.Cadastrar(mesa);

        return Result.Ok();
    }

    public Result Excluir(ExcluirMesaDto dto)
    {
        Mesa? mesa = repositorioMesa.SelecionarPorId(dto.Id);

        if (mesa == null)
            return Falha(nameof(dto.NumeroMesa), "Mesa não emcontrada");

        repositorioMesa.Excluir(dto.Id);

        return Result.Ok();
    }

    public Result Editar(EditarMesaDto dto)
    {
        Mesa? mesa = repositorioMesa.SelecionarPorId(dto.Id);

        if (mesa == null)
            return Falha(nameof(dto.NumeroMesa), "Mesa não encontrada");

        Mesa mesaAtualizada = new Mesa(dto.NumeroMesa, dto.QuantidadeLugares, dto.StatusMesa);

        Result resultValidação = ValidarEntidade(mesaAtualizada);

        if (resultValidação.IsFailed)
            return resultValidação;

        if (ExiateMesa_ComMesmoNumero(dto.NumeroMesa, dto.Id))
            return Falha(nameof(dto.NumeroMesa), "Ja existe uma mesa com esse numero");

        repositorioMesa.Editar(dto.Id, mesaAtualizada);

        return Result.Ok();
    }

    public List<ListarMesaDto> SelecionarTodos()
    {
        return repositorioMesa.SelecionarTodos().Select(m =>
        new ListarMesaDto(
            m.Id, m.NumeroMesa,
            m.QuantidadeLugares,
            m.statusMesa
        )).ToList();
    }

    private bool ExiateMesa_ComMesmoNumero(int numeroMesa, Guid? idNull = null)
    {
        return repositorioMesa.SelecionarTodos()
        .Any(n => n.NumeroMesa == numeroMesa && n.Id != idNull);
    }
}
