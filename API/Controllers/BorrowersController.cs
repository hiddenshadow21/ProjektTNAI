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
using API.Models.OutputModels;
using AutoMapper;
using Model;
using Model.Entities;
using Repository.Abstract;

namespace API.Controllers
{
    public class BorrowersController : ApiController
    {
        private readonly IBorrowerRepository _borrowerRepository;
        private readonly IMapper _mapper;

        public BorrowersController(IBorrowerRepository borrowerRepository, IMapper mapper)
        {
            _borrowerRepository = borrowerRepository;
            _mapper = mapper;
        }

        // GET: api/Borrowers
        /// <summary>
        /// Pobranie informacji o wszystkich wypożyczających
        /// </summary>
        public async Task<IHttpActionResult> GetBorrowers()
        {
            var borrowers = await _borrowerRepository.GetAllAsync();
            return Ok(_mapper.Map<List<BorrowerOutputModel>>(borrowers));
        }

        // GET: api/Borrowers/5
        /// <summary>
        /// Pobranie informacji wypożyczającego o podanym ID
        /// </summary>
        public async Task<IHttpActionResult> GetBorrower(int id)
        {
            Borrower borrower = await _borrowerRepository.GetByIdAsync(id);
            if (borrower == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<BorrowerOutputModel>(borrower));
        }

        // PUT: api/Borrowers/5
        /// <summary>
        /// Edycja informacji wypożyczającego o podanym ID
        /// </summary>
        public async Task<IHttpActionResult> PutBorrower(int id, Borrower inputModel)
        {
            if (inputModel == null)
                return BadRequest(message: "empty model");

            if (id != inputModel.ID)
            {
                return BadRequest();
            }

            var borrower = await _borrowerRepository.GetByIdAsync(id);
            if (borrower == null)
                return NotFound();

            borrower.FirstName = inputModel.FirstName;
            borrower.LastName = inputModel.LastName;

            var result = await _borrowerRepository.SaveAsync(borrower);
            if (!result)
                return InternalServerError();
            return Ok();
        }

        // POST: api/Borrowers
        /// <summary>
        /// Dodanie nowego wypożyczającego
        /// </summary>
        public async Task<IHttpActionResult> PostBorrower(Borrower borrower)
        {
            if (borrower == null)
                return BadRequest(message: "empty");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _borrowerRepository.SaveAsync(borrower);
            if (!result)
                return InternalServerError();
            return Ok();
        }

        // DELETE: api/Borrowers/5
        /// <summary>
        /// Usuwanie wypożyczającego o podanym ID
        /// </summary>
        public async Task<IHttpActionResult> DeleteBorrower(int id)
        {
            Borrower borrower = await _borrowerRepository.GetByIdAsync(id);
            if (borrower == null)
            {
                return NotFound();
            }

            var result = await _borrowerRepository.DeleteAsync(borrower);
            if (!result)
                return InternalServerError();
            return Ok(borrower);
        }
    }
}