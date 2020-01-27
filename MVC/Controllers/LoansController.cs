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
using Repository.Abstract;
using AutoMapper;
using MVC.Models.OutputModels;

namespace MVC.Controllers
{
    public class LoansController : Controller
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IBorrowerRepository _borrowerRepository;
        private readonly IMapper _mapper;

        public LoansController(ILoanRepository loanRepository, IBorrowerRepository borrowerRepository, IBookRepository bookRepository, IMapper mapper)
        {
            _loanRepository = loanRepository;
            _bookRepository = bookRepository;
            _borrowerRepository = borrowerRepository;
            _mapper = mapper;
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
            var books = Task.Run(() => _bookRepository.GetAllAsync()).Result;
            ViewBag.BookId = new SelectList(books, "ID", "Name");
            var borrowers = Task.Run(() => _borrowerRepository.GetAllAsync()).Result;
            ViewBag.BorrowerId = new SelectList(_mapper.Map<List<BorrowerOutputModel>>(borrowers), "ID", "Fullname");
            return View();
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
                var book = await _bookRepository.GetByIdAsync(loan.BookID);
                if (book.Amount <= 0)
                    return HttpNotFound();
                book.Amount--;
                await _bookRepository.SaveAsync(book);

                var result = await _loanRepository.SaveAsync(loan);
                if (!result)
                    return View(loan);
                return RedirectToAction("Index");
            }
            var books = await _bookRepository.GetAllAsync();
            ViewBag.BookId = new SelectList(books, "ID", "Name");
            var borrowers = await _borrowerRepository.GetAllAsync();
            ViewBag.BorrowerId = new SelectList(_mapper.Map<List<BorrowerOutputModel>>(borrowers), "ID", "FullName");
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
            var books = await _bookRepository.GetAllAsync();
            ViewBag.BookId = new SelectList(books, "ID", "Name");
            var borrowers = await _borrowerRepository.GetAllAsync();
            ViewBag.BorrowerId = new SelectList(_mapper.Map<List<BorrowerOutputModel>>(borrowers), "ID", "FullName");
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
            var books = await _bookRepository.GetAllAsync();
            ViewBag.BookId = new SelectList(books, "ID", "Name");
            var borrowers = await _borrowerRepository.GetAllAsync();
            ViewBag.BorrowerId = new SelectList(_mapper.Map<List<BorrowerOutputModel>>(borrowers), "ID", "FullName");
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

            var book = await _bookRepository.GetByIdAsync(loan.BookID);
            
            await _loanRepository.DeleteAsync(loan);

            book.Amount++;
            await _bookRepository.SaveAsync(book);
            return RedirectToAction("Index");
        }
    }
}
