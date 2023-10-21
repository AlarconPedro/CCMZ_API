﻿using CCMZ_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CCMZ_API.Services.Eventos;

public class EventosService : IEventosService
{
    private readonly CCMZContext _context;

    public EventosService(CCMZContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TbEvento>> GetEventos()
    {
        return await _context.TbEventos.ToListAsync();
    }

    public async Task<TbEvento> GetEvento(int id)
    {
        return await _context.TbEventos.FirstOrDefaultAsync(e => e.EveCodigo == id);
    }

    public async Task PostEvento(TbEvento evento)
    {
        if (evento.EveCodigo == 0)
        {
            var lastEvento = await _context.TbEventos.FirstOrDefaultAsync();
            if (lastEvento != null)
            {
                evento.EveCodigo = await _context.TbEventos.MaxAsync(e => e.EveCodigo) + 1;
            } else
            {
                evento.EveCodigo = 1;
            }
        } else
        {
            await UpdateEvento(evento);
        }
        _context.TbEventos.Add(evento);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateEvento(TbEvento evento)
    {
        _context.Entry(evento).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteEvento(TbEvento evento)
    {
        _context.TbEventos.Remove(evento);
        await _context.SaveChangesAsync();
    }
}
