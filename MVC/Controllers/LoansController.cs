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
    public class LoansController : Controller
    {
        private readonly LoanRepository _loanRepository;

        public LoansController()
        {
            _loanRepository = new LoanRepository();
        }

        // GET: Loans
        public async Task<ActionResult> Index()
        {
            var loans = await _loanRepository.GetAllAsync();
            return View(loans);
        }

        // GET: Loans/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Loan loan = await _loanRepository.GetByIdAsync(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }

        // GET: Loans/Create
        public ActionResult Create()
        {
            return View(new Loan());
        }

        // POST: Loans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Loan loan)
        {
            if (ModelState.IsValid)
            {
                loan.LoanStart = DateTime.Now;
                BookRepository bookRepo = new BookRepository();
                var book = await bookRepo.GetByIdAsync(loan.BookID);
                if (book.Amount <= 0)
                    return HttpNotFound();
                book.Amount--;
                await bookRepo.SaveAsync(book);

                var result = await _loanRepository.SaveAsync(loan);
                if (!result)
                    return View(loan);
                return RedirectToAction("Index");
            }
            return View(loan);
        }

        // GET: Loans/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Loan loan = await _loanRepository.GetByIdAsync(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }

        // POST: Loans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,BookID,BorrowerID,LoanStart")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                loan.LoanStart = DateTime.Now;
                var result = await _loanRepository.SaveAsync(loan);
                if (!result)
                    return View(loan);
                return RedirectToAction("Index");
            }
            return View(loan);
        }

        // GET: Loans/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Loan loan = await _loanRepository.GetByIdAsync(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }

        // POST: Loans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Loan loan = await _loanRepository.GetByIdAsync(id);
            if (loan == null)
                return HttpNotFound();

            BookRepository bookRepo = new BookRepository();
            var book = await bookRepo.GetByIdAsync(loan.BookID);
            
            var result = await _loanRepository.DeleteAsync(loan);

            book.Amount++;
            await bookRepo.SaveAsync(book);
            if (!result)
                return View(loan);
            return RedirectToAction("Index");
        }
    }
}
