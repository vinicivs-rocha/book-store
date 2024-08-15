using Microsoft.AspNetCore.Mvc;

namespace Book_Store.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IBookRepository _bookRepository;

    public BookController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Book), 201)]
    public IActionResult Create([FromBody] BookDto book)
    {
        var newBook = new Book
        {
            Id = Guid.NewGuid().ToString(),
            Title = book.Title,
            Author = book.Author,
            Genre = book.Genre,
            Price = book.Price,
            Quantity = book.Quantity
        };

        _bookRepository.Save(newBook);

        return Created(
            HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + newBook.Id,
            newBook);
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Book>), 200)]
    public IActionResult GetAll()
    {
        return Ok(_bookRepository.GetAll());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Book), 200)]
    [ProducesResponseType(404)]
    public IActionResult GetById([FromRoute] string id)
    {
        var book = _bookRepository.GetById(id);

        if (book == null)
        {
            return NotFound("Book not found");
        }

        return Ok(book);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(typeof(Book), 200)]
    [ProducesResponseType(404)]
    public IActionResult Update([FromRoute] string id, [FromBody] UpdateBookDto updateBookDto)
    {
        var bookToUpdate = _bookRepository.GetById(id);

        if (bookToUpdate == null)
        {
            return NotFound("Book not found");
        }

        var updatedBook = bookToUpdate with
        {
            Title = updateBookDto.Title ?? bookToUpdate.Title,
            Author = updateBookDto.Author ?? bookToUpdate.Author,
            Genre = updateBookDto.Genre ?? bookToUpdate.Genre,
            Price = updateBookDto.Price ?? bookToUpdate.Price,
            Quantity = updateBookDto.Quantity ?? bookToUpdate.Quantity,
        };

        _bookRepository.Save(updatedBook);

        return Ok("Book updated successfully");
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(404)]
    public IActionResult Delete([FromRoute] string id)
    {
        var bookToDelete = _bookRepository.GetById(id);

        if (bookToDelete == null)
        {
            return NotFound("Book not found");
        }

        _bookRepository.Delete(id);

        return Ok("Book deleted successfully");
    }
}