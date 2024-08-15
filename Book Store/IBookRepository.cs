namespace Book_Store;

public interface IBookRepository
{
    void Save(Book newBook);
    List<Book> GetAll();
    Book? GetById(string id);
    void Delete(string id);
}