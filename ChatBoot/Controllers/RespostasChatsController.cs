using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChatBoot.Models;

namespace ChatBoot.Controllers
{
    public class RespostasChatsController : Controller
    {
        private readonly Contexto _context;

        public RespostasChatsController(Contexto context)
        {
            _context = context;
        }

        // GET: RespostasChats
        public async Task<IActionResult> Index()
        {
            return View(await _context.RespostasCaht.ToListAsync());
        }

        // GET: RespostasChats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var respostasChat = await _context.RespostasCaht
                .FirstOrDefaultAsync(m => m.id == id);
            if (respostasChat == null)
            {
                return NotFound();
            }

            return View(respostasChat);
        }

        // GET: RespostasChats/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RespostasChats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Resposta,Mensagem")] RespostasChat respostasChat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(respostasChat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(respostasChat);
        }

        // GET: RespostasChats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var respostasChat = await _context.RespostasCaht.FindAsync(id);
            if (respostasChat == null)
            {
                return NotFound();
            }
            return View(respostasChat);
        }

        // POST: RespostasChats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Resposta,Mensagem")] RespostasChat respostasChat)
        {
            if (id != respostasChat.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(respostasChat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RespostasChatExists(respostasChat.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(respostasChat);
        }

        // GET: RespostasChats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var respostasChat = await _context.RespostasCaht
                .FirstOrDefaultAsync(m => m.id == id);
            if (respostasChat == null)
            {
                return NotFound();
            }

            return View(respostasChat);
        }

        // POST: RespostasChats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var respostasChat = await _context.RespostasCaht.FindAsync(id);
            _context.RespostasCaht.Remove(respostasChat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RespostasChatExists(int id)
        {
            return _context.RespostasCaht.Any(e => e.id == id);
        }


        [HttpPost("api/Chat")]
        public async Task<JsonResult> Chat(RequestApi request)
        {
            var chatResposta = await _context.RespostasCaht.Where(m => m.Mensagem.ToUpper().Contains(request.mensagem.ToUpper())).FirstOrDefaultAsync();
            
            if(chatResposta != null)
            {
                var resposta = new ResponseApi { resposta = chatResposta.Resposta };
                return Json(resposta);
            }
            else
            {
                var reposta = new ResponseApi { resposta = "Não compreendi sua pergunta. Por gentileza reformule a pergunta. " };
                return Json(reposta);
            }
        
        }

    }
}
