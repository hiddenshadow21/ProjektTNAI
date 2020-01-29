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
    public class BooksController : ApiController
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BooksController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        // GET: api/Books
        /// <summary>
        /// Funkcja do pobierania informacji o wszystkich książkach
        /// </summary>
        /// <returns></returns>
        public async Task<IHttpActionResult> GetBooks()
        {
            var books = await _bookRepository.GetAllAsync();
            return Ok(_mapper.Map<List<BookOutputModel>>(books));
        }

        /// <summary>
        /// Funkcja do pobierania informacji o książce o podanym ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Books/5
        public async Task<IHttpActionResult> GetBook(int id)
        {
            Book book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<BookOutputModel>(book));
        }

        /// <summary>
        /// Funkcja do edycji informacji o książce o podanym ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        // PUT: api/Books/5
        public async Task<IHttpActionResult> PutBook(int id, Book inputModel)
        {

            if (inputModel == null)
                return BadRequest(message: "empty model");

            if (id != inputModel.ID)
            {
                return BadRequest();
            }

            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
                return NotFound();

            book.Name = inputModel.Name;

            var result = await _bookRepository.SaveAsync(book);
            if (!result)
                return InternalServerError();
            return Ok();
        }

        /// <summary>
        /// Funkcja do dodawania nowej książki
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        // POST: api/Books
        public async Task<IHttpActionResult> PostBook(Book book)
        {
            if (book == null)
                return BadRequest(message: "empty");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _bookRepository.SaveAsync(book);
            if (!result)
                return InternalServerError();
            return Ok();
        }

        /// <summary>
        /// Funkcja do usuwania książki o podanym ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Books/5
        public async Task<IHttpActionResult> DeleteBook(int id)
        {
            Book book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            var result = await _bookRepository.DeleteAsync(book);
            if (!result)
                return InternalServerError();
            return Ok(book);
        }
    }
}