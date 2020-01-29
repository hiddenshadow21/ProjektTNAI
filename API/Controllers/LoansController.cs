using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using API.Models.InputModels.Loan;
using API.Models.OutputModels;
using AutoMapper;
using Model;
using Model.Entities;
using Repository.Abstract;

namespace API.Controllers
{
    public class LoansController : ApiController
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;

        public LoansController(ILoanRepository loanRepository, IMapper mapper)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Funkcja do pobierania informacji o wszystkich trwających wypożyczeniach
        /// </summary>
        /// <returns></returns>
        // GET: api/Loans
        public async Task<IHttpActionResult> GetLoans()
        {
            var loans = await _loanRepository.GetAllAsync();
            return Ok(_mapper.Map<List<LoanOutputModel>>(loans));
        }

        /// <summary>
        /// Funkcja do pobierania informacji o trwającym wypożyczeniu o podanym ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Loans/5
        public async Task<IHttpActionResult> GetLoan(int id)
        {
            Loan loan = await _loanRepository.GetByIdAsync(id);
            if (loan == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<LoanOutputModel>(loan));
        }

        /// <summary>
        /// Funkcja do edycji trwającego wypożyczenia o podanym ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        // PUT: api/Loans/5
        public async Task<IHttpActionResult> PutLoan(int id, LoanInputModel inputModel)
        {
            if (inputModel == null)
                return BadRequest(message: "empty");

            if (id != inputModel.ID)
            {
                return BadRequest();
            }

            var loan = await _loanRepository.GetByIdAsync(id);
            if (loan == null)
                return NotFound();

            loan.LoanStart = DateTime.Now;
            if (inputModel.BookID != default(int))
                loan.BookID = inputModel.BookID;
            if (inputModel.BorrowerID != default(int))
                loan.BorrowerID = inputModel.BorrowerID;

            var result = await _loanRepository.SaveAsync(loan);
            if (!result)
                return InternalServerError();
            return Ok();
        }

        /// <summary>
        /// Funkcja do dodawania nowego wypożyczenia
        /// </summary>
        /// <param name="loan"></param>
        /// <returns></returns>
        // POST: api/Loans
        public async Task<IHttpActionResult> PostLoan(LoanInputModel loan)
        {
            if (loan == null)
                return BadRequest(message: "empty");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            loan.LoanStart = DateTime.Now;

            var result = await _loanRepository.SaveAsync(_mapper.Map<Loan>(loan));
            if (!result)
                return InternalServerError();
            return Ok();
        }

        /// <summary>
        /// Funkcja do usuwania trwającego wypożyczenia o podanym ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Loans/5
        public async Task<IHttpActionResult> DeleteLoan(int id)
        {
            Loan loan = await _loanRepository.GetByIdAsync(id);
            if (loan == null)
            {
                return NotFound();
            }

            var result = await _loanRepository.DeleteAsync(loan);
            if (!result)
                return InternalServerError();
            return Ok(loan);
        }
    }
}