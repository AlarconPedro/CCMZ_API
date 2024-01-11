using CCMZ_API.Models.Painel.QuartoPessoa;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CCMZ_API.Services.QuartoPessoa;

public class QuartoPessoaService : IQuartoPessoaService
{
    private readonly CCMZContext _context;

    public QuartoPessoaService(CCMZContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<QuartoPessoas>> GetQuartoPessoas(int codigoBloco)
    {
        return await _context.TbQuartoPessoas
            .Where(qp => qp.QuaCodigoNavigation.BloCodigo == codigoBloco && qp.QuaCodigoNavigation.TbEventoQuartos.Any()).Select(qp => new QuartoPessoas
            {
                BloCodigo = qp.QuaCodigoNavigation.BloCodigo,
                QuaCodigo = qp.QuaCodigo,
                QuaNome = qp.QuaCodigoNavigation.QuaNome,
                PessoasQuarto = _context.TbQuartoPessoas
                .Where(qp => qp.QuaCodigo == qp.QuaCodigo)
                .Select(x => new PessoaCheckin
                {
                    PesCodigo = x.PesCodigo,
                    PesChave = x.PesChave,
                    PesCheckin = x.PesCheckin,
                    QupCodigo = x.QupCodigo,
                    PesNome = x.PesCodigoNavigation.PesNome,
                })
            }).ToListAsync();
       /* return await _context.TbQuartoPessoas.Where(qp => qp.QuaCodigoNavigation.BloCodigo == codigoBloco)
            .Join(_context.TbEventoQuartos, qp => qp.QuaCodigo, eq => eq.QuaCodigo, (qp, eq) => new {qp, eq})
            .Where(x => x.eq.QuaCodigo == x.qp.QuaCodigo)
            .Select(x => new QuartoPessoas
            {
                BloCodigo = x.qp.QuaCodigoNavigation.BloCodigo,
                QuaCodigo = x.eq.QuaCodigo,
                QuaNome = x.qp.QuaCodigoNavigation.QuaNome,
                PessoasQuarto = _context.TbQuartoPessoas
                .Where(qp => qp.QuaCodigo == x.qp.QuaCodigo)
                .Select(x => new PessoaCheckin
                {
                   PesCodigo = x.PesCodigo,
                   PesChave = x.PesChave,
                   PesCheckin = x.PesCheckin,
                   QupCodigo = x.QupCodigo,
                   PesNome = x.PesCodigoNavigation.PesNome,
                })
            })
            .ToListAsync();*/
    }
}
