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
using Repository.Concrete;

namespace MVC.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly AuthorRepository _authorRepository;

        public AuthorsController()
        {
            _authorRepository = new AuthorRepository();
        }

        // GET: Authors
        public async Task<ActionResult> Index()
        {
            return View(await _authorRepository.GetAllAsync());
        }

        // GET: Authors/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Author author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // GET: Authors/Create
        public ActionResult Create()
        {
            return View(new Author());
        }

        // POST: Authors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Author author)
        {
            if (ModelState.IsValid)
            {
                var result = await _authorRepository.SaveAsync(author);
                if (!result)
                    return View(author);
                return RedirectToAction("Index");
            }

            return View(author);
        }

        // GET: Authors/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Author author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Author author)
        {
            if (ModelState.IsValid)
            {
                var result = await _authorRepository.SaveAsync(author);
                if (!result)
                    return View(author);
                return RedirectToAction("Index");
            }
            return View(author);
        }

        // GET: Authors/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Author author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
                return HttpNotFound();
            var result = await _authorRepository.DeleteAsync(author);
            if (!result)
                return View(author);
            return RedirectToAction("Index");
        }
    }
}
