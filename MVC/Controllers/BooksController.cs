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
using Repository.Concrete;

namespace MVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookRepository _bookRepository;

        public BooksController()
        {
            _bookRepository = new BookRepository();
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
            return View(new Book());
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
                var result = await _bookRepository.SaveAsync(book);
                if (!result)
                    return View(book);
                return RedirectToAction("Index");
            }
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
                var result = await _bookRepository.SaveAsync(book);
                if (!result)
                    return View(book);
                return RedirectToAction("Index");
            }
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
            if (book == null)
                return HttpNotFound();
            var result = await _bookRepository.DeleteAsync(book);
            if (!result)
                return View(book);
            return RedirectToAction("Index");
        }
    }
}
