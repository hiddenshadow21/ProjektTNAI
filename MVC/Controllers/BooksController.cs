using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model;
using Model.Entities;
using Repository.Abstract;
using AutoMapper;
using MVC.Models.OutputModels;

namespace MVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public BooksController(IBookRepository bookRepository, IAuthorRepository authorRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        // GET: Books
        public async Task<ActionResult> Index()
        {
            var books = await _bookRepository.GetAllAsync();
            return View(books);
        }

        // GET: Books/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Book book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            var authors = Task.Run(() => _authorRepository.GetAllAsync()).Result;
            ViewBag.Author = new SelectList( _mapper.Map<List<AuthorOutputModel>>(authors), "ID", "Fullname");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                await _bookRepository.SaveAsync(book);
                return RedirectToAction("Index");
            }
            var authors = await _authorRepository.GetAllAsync();
            ViewBag.Author = new SelectList(_mapper.Map<List<AuthorOutputModel>>(authors), "ID", "Fullname");
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Book book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            var authors = await _authorRepository.GetAllAsync();
            ViewBag.Author = new SelectList(_mapper.Map<List<AuthorOutputModel>>(authors), "ID", "Fullname");
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                await _bookRepository.SaveAsync(book);
                return RedirectToAction("Index");
            }
            var authors = await _authorRepository.GetAllAsync();
            ViewBag.Author = new SelectList(_mapper.Map<List<AuthorOutputModel>>(authors), "ID", "Fullname");
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Book book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Book book = await _bookRepository.GetByIdAsync(id);
            await _bookRepository.DeleteAsync(book);
            return RedirectToAction("Index");
        }
    }
}
