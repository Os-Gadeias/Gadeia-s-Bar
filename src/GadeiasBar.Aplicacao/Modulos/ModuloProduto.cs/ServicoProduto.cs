using FluentResults;
using GadeiasBar.Aplicacao.Compartilhado;
using GadeiasBar.Dominio.Modulos.ModuloProduto.cs;

namespace GadeiasBar.Aplicacao.Modulos.ModuloProduto.cs;

public class ServicoProduto(IRepositorioProduto repositorioProduto) : ServicoBase<Produto>
{
    public Result Cadastrar(CadastrarProdutoDto dto)
    {
        Produto produto = new(dto.Nome, dto.TipoProduto, dto.Valor);

        Result resultadoValidacao = ValidarEntidade(produto);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        if (ExisteProduto_ComMesmoNome(dto.Nome))
            return Falha(nameof(dto.Nome), $"O nome: {dto.Nome} já está sendo utilizado!");

        repositorioProduto.Cadastrar(produto);

        return Result.Ok();
    }

    public Result Excluir(ExcluirProdutoDto dto)
    {
        Produto? produto = repositorioProduto.SelecionarPorId(dto.Id);

        if (produto is null)
            return Result.Fail("Produto não encontrado");

        repositorioProduto.Excluir(dto.Id);

        return Result.Ok();
    }
    public Result Editar(EditarProdutoDto dto)
    {
        Produto? produto = repositorioProduto.SelecionarPorId(dto.Id);

        if (produto is null)
            return Falha(nameof(dto.Nome), "Produto não encontrado.");

        Produto produtoAtualizado = new(dto.Nome, dto.TipoProduto, dto.Valor);

        Result resultadoValidacao = ValidarEntidade(produtoAtualizado);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        if (ExisteProduto_ComMesmoNome(dto.Nome, dto.Id))
            return Falha(nameof(dto.Nome), "Já existe um produto com este nome.");

        repositorioProduto.Editar(produto.Id, produtoAtualizado);

        return Result.Ok();
    }
    public Result<ListarProdutoDto> SelecionarPorId(Guid id)
    {
        Produto? produto = repositorioProduto.SelecionarPorId(id);

        if (produto is null)
            return Result.Fail("Produto não encontrado.");

        return new ListarProdutoDto(produto.Id, produto.Nome, produto.TipoProduto, produto.Valor);
    }
    public List<ListarProdutoDto> SelecionarTodos()
    {
        return repositorioProduto.SelecionarTodos().Select(p =>
        new ListarProdutoDto(
            p.Id, p.Nome,
            p.TipoProduto, p.Valor
            )).ToList();
    }
    private bool ExisteProduto_ComMesmoNome(string nome, Guid? idIgnorado = null)
    {
        return repositorioProduto.SelecionarTodos().Any(p => p.Nome == nome && p.Id != idIgnorado);
    }
}
