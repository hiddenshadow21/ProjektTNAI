﻿using System;
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
    public class BorrowersController : Controller
    {
        private readonly BorrowerRepository _borrowerRepository;

        public BorrowersController()
        {
            _borrowerRepository = new BorrowerRepository();
        }

        // GET: Borrowers
        public async Task<ActionResult> Index()
        {
            return View(await _borrowerRepository.GetAllAsync());
        }

        // GET: Borrowers/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Borrower borrower = await _borrowerRepository.GetByIdAsync(id);
            if (borrower == null)
            {
                return HttpNotFound();
            }
            return View(borrower);
        }

        // GET: Borrowers/Create
        public ActionResult Create()
        {
            return View(new Borrower());
        }

        // POST: Borrowers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                var result = await _borrowerRepository.SaveAsync(borrower);
                if (!result)
                    return View(borrower);
                return RedirectToAction("Index");
            }

            return View(borrower);
        }

        // GET: Borrowers/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Borrower borrower = await _borrowerRepository.GetByIdAsync(id);
            if (borrower == null)
            {
                return HttpNotFound();
            }
            return View(borrower);
        }

        // POST: Borrowers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                var result = await _borrowerRepository.SaveAsync(borrower);
                if (!result)
                    return View(borrower);
                return RedirectToAction("Index");
            }
            return View(borrower);
        }

        // GET: Borrowers/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Borrower borrower = await _borrowerRepository.GetByIdAsync(id);
            if (borrower == null)
            {
                return HttpNotFound();
            }
            return View(borrower);
        }

        // POST: Borrowers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Borrower borrower = await _borrowerRepository.GetByIdAsync(id);
            if (borrower == null)
                return HttpNotFound();
            var result = await _borrowerRepository.DeleteAsync(borrower);
            if (!result)
                return View(borrower);
            return RedirectToAction("Index");
        }
    }
}
