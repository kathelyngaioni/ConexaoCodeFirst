using ConexaoCodeFirst.Data;
using ConexaoCodeFirst.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConexaoCodeFirst.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HomeController : ControllerBase
{
    //variavel apenas de leitura
    private readonly AppDbContext _context;
    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<List<ProdutoModel>> getProdutos()
    {
        //pegar o que tem no banco de dados e transformar em uma lista
        //salvar essa lista em uma variavel
        //Produtos vem do DbSet
        var produtos = await _context.Produtos.ToListAsync();
        return produtos;
    }

    [HttpPost]
    public async Task<List<ProdutoModel>> criarProdutos(ProdutoModel produto)
    {
  
        if(produto != null)
        {
            _context.Add(produto);
            await _context.SaveChangesAsync();
            
        }
        var produtos = await _context.Produtos.ToListAsync();
        return produtos;

    }

    [HttpPut]
    public async Task<List<ProdutoModel>> editarProduto(ProdutoModel produto)
    {
        //procurar o produto no banco de dados
        ProdutoModel produtoModel = _context.Produtos.AsNoTracking().FirstOrDefault(x => x.Id == produto.Id);
        
        if(produtoModel != null)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();

        }
        var produtos = await _context.Produtos.ToListAsync();
        return produtos;
    }

    [HttpDelete]
    public async Task<List<ProdutoModel>> apagarProduto(int id)
    {
        ProdutoModel produtoModel = _context.Produtos.FirstOrDefault(x => x.Id == id);
        if( produtoModel != null)
        {
            _context.Produtos.Remove(produtoModel);
            await _context.SaveChangesAsync();
        }
        var produtos = await _context.Produtos.ToListAsync();
        return produtos;
    }
}
