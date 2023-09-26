using Book.Store.Core.Repository;
using Store.Entities;
using Store.Service.Models;
namespace Store.Service
{
    public class BookService
    {
        private ServiceFactory _factory;
        public BookService(ServiceFactory factory)
        {
            _factory = factory;
        }
        public bool AddBook(BooksModels model)
        {
            try
            {
                var obj = new EBook()
                {
                    Id = model.Id,
                    Author = model.Author,
                    Category = model.Category,
                    Price = model.Price,
                    Quantity = model.Quantity,
                    Title = model.Title,
                    Genre = model.Genre,
                };

                if (DateTime.TryParse(model.CreatedDate, out DateTime createdDate))
                {
                    obj.CreatedDate = createdDate;
                }
                else
                { 
                    throw new Exception("Invalid CreatedDate format");
                }

                var saveBook = _factory.GetInstance<EBook>();
                var result = saveBook.Add(obj);
                if (result != null)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately, e.g., log it or rethrow it.
                throw ex;
            }
        }

        public bool UpdateBook(EBook model)
        {
            try
            {
                var bookService = _factory.GetInstance<EBook>();
                EBook booksResult = bookService.Update(model);
                if (booksResult != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IQueryable<EBook> GetBooks()
        {
            try
            {
                return _factory.GetInstance<EBook>().List();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteBook(int bookId)
        {
            try
            {
                var result = _factory.GetInstance<EBook>();
                EBook eBookService = result.List().Where(x => x.Id == bookId).FirstOrDefault();
                result.Remove(eBookService);
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}