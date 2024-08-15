namespace Book_Store.Repositories;

public class MemoryBookRepository : IBookRepository
{
    private readonly List<Book> _books = [];
    public void Save(Book newBook)
    {
        var existingBookIndex = _books.FindIndex(book => book.Id == newBook.Id);
        
        if (existingBookIndex == -1)
        {
            _books.Add(newBook);
            return;
        }

        _books[existingBookIndex] = newBook;
    }

    public List<Book> GetAll()
    {
        return _books;
    }

    public Book? GetById(string id)
    {
        return _books.Find(book => book.Id == id);
    }

    public void Delete(string id)
    {
        if (_books.Exists(book => book.Id == id))
            _books.Remove(_books.Find(book => book.Id == id)!);
    }
}