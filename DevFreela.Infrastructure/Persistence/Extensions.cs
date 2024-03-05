using DevFreela.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence
{
    public static class Extensions
    {
        // Método de extensão responsável para gerenciar a paginação das querys
        public static async Task<PaginationResult<T>> GetPaged<T>(
            this IQueryable<T> query,
            int page,
            int pageSize) where T : class
        {
            var result = new PaginationResult<T>();

            result.Page = page;
            result.PageSize = pageSize;
            result.ItemsCount = await query.CountAsync();

            // Calcula a quantidade de páginas
            var pageCount = (double)result.ItemsCount / pageSize;

            // Calcula o total de páginas arredondando para o valor inteiro de haver numero quebrado
            result.TotalPages = (int)Math.Ceiling(pageCount);

            // Calcula qual a quantidade a ser pulada de dados
            var skip = (page - 1) * pageSize;

            // Executa a query pelos calculos feitos acima
            result.Data = await query.Skip(skip).Take(pageSize).ToListAsync();

            return result;
        }
    }
}
