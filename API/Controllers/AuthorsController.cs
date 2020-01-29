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
using API.Models.InputModels;
using API.Models.OutputModels;
using AutoMapper;
using Model;
using Model.Entities;
using Repository.Abstract;

namespace API.Controllers
{
    public class AuthorsController : ApiController
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorsController(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Funkcja do pobierania wszystkich autorów
        /// </summary>
        /// <returns></returns>
        // GET: api/Author
        public async Task<IHttpActionResult> GetAuthors()
        {
            var authors = await _authorRepository.GetAllAsync();
            return Ok(_mapper.Map<List<AuthorOutputModel>>(authors));
        }
        /// <summary>
        /// Funkcja do pobierania autora o danym ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Author/5
        public async Task<IHttpActionResult> GetAuthor(int id)
        {
            Author author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AuthorOutputModel>(author));
        }

        // PUT: api/Authors/5
        /// <summary>
        /// Funkcja do edycji autora o podanym ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> PutAuthor(int id, AuthorInputModel inputModel)
        {
            if (inputModel == null)
                return BadRequest(message: "empty");

            if (id != inputModel.ID)
            {
                return BadRequest();
            }

            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
                return NotFound();

            author.FirstName = inputModel.FirstName;
            author.LastName = inputModel.LastName;

            var result = await _authorRepository.SaveAsync(author);
            if (!result)
                return InternalServerError();
            return Ok();
        }

        /// <summary>
        /// Funkcja do dodawania nowego autora
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        // POST: api/Authors
        public async Task<IHttpActionResult> PostAuthor(AuthorInputModel author)
        {
            if (author == null)
                return BadRequest(message: "empty");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authorRepository.SaveAsync(_mapper.Map<Author>(author));
            if (!result)
                return InternalServerError();
            return Ok();
        }

        /// <summary>
        /// Funkcja do usuwania autora o podanym ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Authors/5
        public async Task<IHttpActionResult> DeleteAuthor(int id)
        {
            Author author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            var result = await _authorRepository.DeleteAsync(author);
            if (!result)
                return InternalServerError();
            return Ok(author);
        }
    }
}