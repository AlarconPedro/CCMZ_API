using CCMN_API;
using CCMN_API.Models;
using CCMN_API.Models.Painel.Checkin;
using CCMZ_API.Models.Painel.QuartoPessoa;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CCMZ_API.Services.QuartoPessoa;

public class CheckinService : ICheckinService
{
    private readonly CCMNContext _context;

    public CheckinService(CCMNContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<QuartosCheckinEvento>> GetQuartoCheckin (int codigoEvento)
    {
        var retorno = await _context.TbEventoQuartos.Where(eq => eq.EveCodigo == codigoEvento)
            .Select(eq => new QuartosCheckinEvento
            {
                BloNome = eq.QuaCodigoNavigation.BloCodigoNavigation.BloNome,
                QuartosCheckin = _context.TbEventoQuartos.Where(qe => qe.EveCodigo == codigoEvento && qe.QuaCodigoNavigation.BloCodigo == eq.QuaCodigoNavigation.BloCodigo)
                                .Join(_context.TbQuartos, eq => eq.QuaCodigo, q => q.QuaCodigo, (eq, q) => new { eq, q })
                                .Select(qc => new QuartoPessoas
                                {
                                    BloCodigo = qc.q.BloCodigo,
                                    QuaCodigo = qc.q.QuaCodigo,
                                    QuaNome = qc.q.QuaNome,
                                    BloNome = qc.q.BloCodigoNavigation.BloNome,
                                    Vagas = qc.q.QuaQtdcamas - _context.TbQuartoPessoas.Join(_context.TbPessoas, qc => qc.PesCodigo, p => p.PesCodigo, (qc, p) => new { qc, p })
                                                                            .Join(_context.TbEventoPessoas, x => x.qc.PesCodigo, ep => ep.PesCodigo, (x, ep) => new { x, ep })
                                                                            .Where(x => x.x.qc.QuaCodigo == qc.q.QuaCodigo && x.ep.EveCodigo == codigoEvento).Count(),
                                    PessoasQuarto = _context.TbQuartoPessoas.Join(_context.TbPessoas, qp => qp.PesCodigo, p => p.PesCodigo, (qp, p) => new { qp, p })
                                                                            .Join(_context.TbEventoPessoas, x => x.qp.PesCodigo, ep => ep.PesCodigo, (x, ep) => new { x, ep })
                                                                            .Where(x => x.x.qp.QuaCodigo == qc.q.QuaCodigo && x.ep.EveCodigo == codigoEvento)
                                                                            .Select(x => new PessoaCheckin
                                                                            {
                                                                                PesCodigo = x.x.qp.PesCodigo,
                                                                                PesChave = x.x.qp.PesChave,
                                                                                QuaCodigo = x.x.qp.QuaCodigo,
                                                                                PesCheckin = x.x.qp.PesCheckin,
                                                                                PesNaovem = x.x.qp.PesNaovem,
                                                                                QupCodigo = x.x.qp.QupCodigo,
                                                                                PesNome = x.x.p.PesNome,
                                                                            }).ToList()
                                }).ToList(),

                /*QuartosCheckin = _context.TbQuartoPessoas.Join(_context.TbPessoas, qp => qp.PesCodigo, p => p.PesCodigo, (qp, p) => new { qp, p })
                                .Join(_context.TbEventoPessoas, x => x.qp.PesCodigo, ep => ep.PesCodigo, (x, ep) => new { x, ep })
                                .Where(x => x.x.qp.QuaCodigo == eq.q.QuaCodigo && x.ep.EveCodigo == codigoEvento)
                                .Select(x => new QuartoPessoas
                                {
                                    BloCodigo = eq.q.BloCodigo,
                                    QuaCodigo = eq.q.QuaCodigo,
                                    QuaNome = eq.q.QuaNome,
                                    BloNome = eq.q.BloCodigoNavigation.BloNome,
                                    Vagas = eq.q.QuaQtdcamas - _context.TbQuartoPessoas.Join(_context.TbPessoas, qp => qp.PesCodigo, p => p.PesCodigo, (qp, p) => new { qp, p })
                                                            .Join(_context.TbEventoPessoas, x => x.qp.PesCodigo, ep => ep.PesCodigo, (x, ep) => new { x, ep })
                                                            .Where(x => x.x.qp.QuaCodigo == eq.q.QuaCodigo && x.ep.EveCodigo == codigoEvento).Count(),
                                    PessoasQuarto = _context.TbQuartoPessoas.Join(_context.TbPessoas, qp => qp.PesCodigo, p => p.PesCodigo, (qp, p) => new { qp, p })
                                                            .Join(_context.TbEventoPessoas, x => x.qp.PesCodigo, ep => ep.PesCodigo, (x, ep) => new { x, ep })
                                                            .Where(x => x.x.qp.QuaCodigo == eq.q.QuaCodigo && x.ep.EveCodigo == codigoEvento)
                                                            .Select(x => new PessoaCheckin
                                                            {
                                                                PesCodigo = x.x.qp.PesCodigo,
                                                                PesChave = x.x.qp.PesChave,
                                                                QuaCodigo = x.x.qp.QuaCodigo,
                                                                PesCheckin = x.x.qp.PesCheckin,
                                                                PesNaovem = x.x.qp.PesNaovem,
                                                                QupCodigo = x.x.qp.QupCodigo,
                                                                PesNome = x.x.p.PesNome,
                                                            }).ToList()
                                }).ToList(),*/
            }).ToListAsync();
        return retorno.DistinctBy(d => d.BloNome);
        //return retorno.DistinctBy(d => d.BloNome);
        /* return await _context.TbEventoQuartos.Where(eq => eq.EveCodigo == codigoEvento)
             .Join(_context.TbQuartos, eq => eq.QuaCodigo, q => q.QuaCodigo, DistinctBy(d => d.BloNome)(eq, q) => new { eq, q })
             .Where(eq => eq.q.BloCodigo == codigoBloco)
             .Select(eq => new QuartoPessoas
             {
                 BloCodigo = eq.q.BloCodigo,
                 QuaCodigo = eq.q.QuaCodigo,
                 QuaNome = eq.q.QuaNome,
                 BloNome = eq.q.BloCodigoNavigation.BloNome,
                 Vagas = eq.q.QuaQtdcamas - _context.TbQuartoPessoas.Join(_context.TbPessoas, qp => qp.PesCodigo, p => p.PesCodigo, (qp, p) => new { qp, p })
                                                         .Join(_context.TbEventoPessoas, x => x.qp.PesCodigo, ep => ep.PesCodigo, (x, ep) => new { x, ep })
                                                         .Where(x => x.x.qp.QuaCodigo == eq.q.QuaCodigo && x.ep.EveCodigo == codigoEvento).Count(),
                 PessoasQuarto = _context.TbQuartoPessoas.Join(_context.TbPessoas, qp => qp.PesCodigo, p => p.PesCodigo, (qp, p) => new { qp, p })
                                                         .Join(_context.TbEventoPessoas, x => x.qp.PesCodigo, ep => ep.PesCodigo, (x, ep) => new { x, ep })
                                                         .Where(x => x.x.qp.QuaCodigo == eq.q.QuaCodigo && x.ep.EveCodigo == codigoEvento)
                                                         .Select(x => new PessoaCheckin
                                                         {
                                                             PesCodigo = x.x.qp.PesCodigo,
                                                             PesChave = x.x.qp.PesChave,
                                                             QuaCodigo = x.x.qp.QuaCodigo,
                                                             PesCheckin = x.x.qp.PesCheckin,
                                                             PesNaovem = x.x.qp.PesNaovem,
                                                             QupCodigo = x.x.qp.QupCodigo,
                                                             PesNome = x.x.p.PesNome,
                                                         }).ToList()
                 Vagas = _context.TbQuartos.Where(x => x.QuaCodigo == eq.q.QuaCodigo && x.TbEventoQuartos
                                           .Where(eq => eq.EveCodigo == codigoEvento).FirstOrDefault() != null)
                                           .Select(x => x.QuaQtdcamas - x.TbQuartoPessoas.Count).FirstOrDefault(),
                 PessoasQuarto = _context.TbQuartoPessoas
          .Join(_context.TbEventoQuartos, qp => qp.QuaCodigo, eq => eq.QuaCodigo, (qp, eq) => new { qp, eq })
          .Where(x => x.qp.QuaCodigo == eq.q.QuaCodigo && x.eq.EveCodigo == codigoEvento)
          .Select(x => new PessoaCheckin
          {
              PesCodigo = x.qp.PesCodigo,
              PesChave = x.qp.PesChave,
              QuaCodigo = x.qp.QuaCodigo,
              PesCheckin = x.qp.PesCheckin,
              QupCodigo = x.qp.QupCodigo,
              PesNome = x.qp.PesCodigoNavigation.PesNome,
          }).ToList()
             }).ToListAsync();*/
    }

    public async Task<IEnumerable<QuartoPessoas>> GetQuartoPessoasBusca(int codigoEvento, string busca)
    {
        return await _context.TbEventoQuartos.Where(eq => eq.EveCodigo == codigoEvento)
            .Join(_context.TbQuartos, eq => eq.QuaCodigo, q => q.QuaCodigo, (eq, q) => new { eq, q })
            .Where(eq =>  eq.q.TbQuartoPessoas.Any(x => x.PesCodigoNavigation.PesNome.Contains(busca)))
            .Select(eq => new QuartoPessoas
            {
                BloCodigo = eq.q.BloCodigo,
                QuaCodigo = eq.q.QuaCodigo,
                QuaNome = eq.q.QuaNome,
                BloNome = eq.q.BloCodigoNavigation.BloNome,
                Vagas = eq.q.QuaQtdcamas - _context.TbQuartoPessoas.Join(_context.TbPessoas, qp => qp.PesCodigo, p => p.PesCodigo, (qp, p) => new { qp, p })
                                                        .Join(_context.TbEventoPessoas, x => x.qp.PesCodigo, ep => ep.PesCodigo, (x, ep) => new { x, ep })
                                                        .Where(x => x.x.qp.QuaCodigo == eq.q.QuaCodigo && x.ep.EveCodigo == codigoEvento).Count(),
                PessoasQuarto = _context.TbQuartoPessoas.Join(_context.TbPessoas, qp => qp.PesCodigo, p => p.PesCodigo, (qp, p) => new { qp, p })
                                                        .Join(_context.TbEventoPessoas, x => x.qp.PesCodigo, ep => ep.PesCodigo, (x, ep) => new { x, ep })
                                                        .Where(x => x.x.qp.QuaCodigo == eq.q.QuaCodigo && x.ep.EveCodigo == codigoEvento)
                                                        .Select(x => new PessoaCheckin
                                                        {
                                                            PesCodigo = x.x.qp.PesCodigo,
                                                            PesChave = x.x.qp.PesChave,
                                                            QuaCodigo = x.x.qp.QuaCodigo,
                                                            PesCheckin = x.x.qp.PesCheckin,
                                                            PesNaovem = x.x.qp.PesNaovem,
                                                            QupCodigo = x.x.qp.QupCodigo,
                                                            PesNome = x.x.p.PesNome,
                                                        }).ToList()
            }).ToListAsync();
    }

    public async Task UpdateQuartoPessoa(TbQuartoPessoa quartoPessoa)
    {
        if (quartoPessoa.PesChave)
        {
            _context.TbQuartoPessoas.Update(quartoPessoa);
            await _context.SaveChangesAsync();
        } else
        {
            var statusQuarto = await _context.TbQuartoPessoas.Where(qp => qp.QuaCodigo == quartoPessoa.QuaCodigo).Select(x => x.PesChave).ToListAsync();
            if (!statusQuarto.Contains(true))
            {
                quartoPessoa.PesChave = true;
                _context.TbQuartoPessoas.Update(quartoPessoa);
                await _context.SaveChangesAsync();
            } else
            {
                _context.TbQuartoPessoas.Update(quartoPessoa);
                await _context.SaveChangesAsync();
            }

        }
        // var retorno = await _context.TbQuartoPessoas.Where(x => x.PesCodigo == quartoPessoa.PesCodigo && x.QuaCodigo == quartoPessoa.QuaCodigo).FirstOrDefaultAsync();
        /*_context.Entry(quartoPessoa).State = EntityState.Modified;*/
       
    }
}
