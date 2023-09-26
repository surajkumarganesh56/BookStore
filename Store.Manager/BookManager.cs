using Book.Store.Core.Repository;
using Services.Common;
using Store.Entities;
using Store.Service;
using Store.Service.Models;

namespace Store.Manager
{
    public class BookManager : IDisposable
    {
        public ServiceResult<IQueryable<BooksModels>> GetBooks()
        {
            try
            {
                using (ServiceFactory factory = new ServiceFactory())
                {
                    IQueryable<BooksModels> result = (from c in factory.GetInstance<EBook>().List()
                                                      select new BooksModels()
                                                      {
                                                          Id = c.Id,
                                                          Author = c.Author,
                                                          CreatedDate = c.CreatedDate.ToString("yyyy/MM/dd"),
                                                          Category = c.Category,
                                                          Price = c.Price,
                                                          Genre = c.Genre,
                                                          Quantity = c.Quantity,
                                                          Title = c.Title,
                                                      }).OrderByDescending(x => x.Id).AsQueryable();
                    return new ServiceResult<IQueryable<BooksModels>>()
                    {
                        Data = result,
                        Message = "Books list Fetched Successfully !...",
                        Status = ResultStatus.Ok
                    };
                }
            }
            catch (Exception)
            {
                return new ServiceResult<IQueryable<BooksModels>>()
                {
                    Data = null,
                    Message = "Error fetching books list !...",
                    Status = ResultStatus.processError
                };
            }
        }
        public ServiceResult<bool> AddBook(BooksModels model)
        {
            using (ServiceFactory factory = new ServiceFactory())
            {
                try
                {
                    BookService service = new BookService(factory);
                    var result = service.AddBook(model);
                    return new ServiceResult<bool>()
                    {
                        Data = result,
                        Message = "Book Added Successfully ! ...",
                        Status = ResultStatus.Ok
                    };

                }
                catch (Exception ex)
                {
                    return new ServiceResult<bool>()
                    {
                        Data = false,
                        Message = "Error In Adding Book !...",
                        Status = ResultStatus.processError
                    };
                }
            }
        }
        public ServiceResult<bool> UpdateBook(EBook model)
        {
            try
            {
                using (ServiceFactory factory = new ServiceFactory())
                {
                    BookService service = new BookService(factory);
                    service.UpdateBook(model);
                    return new ServiceResult<bool>()
                    {
                        Data = true,
                        Message = "Book Successfully Updated ! ....",
                        Status = ResultStatus.Ok
                    };
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>()
                {
                    Data = false,
                    Message = "Error Updating Donation",
                    Status = ResultStatus.processError
                };
            }
        }


        public ServiceResult<List<EBook>> FilterBooks(string? Genre, string? author)
        {
            try
            {
                using (ServiceFactory factory = new ServiceFactory())
                {
                    BookService service = new BookService(factory);
                    IQueryable<EBook> reportQueryable = service.GetBooks();

                    if (!String.IsNullOrEmpty(Genre))
                    {
                        reportQueryable = reportQueryable.Where(x => x.Genre.Trim().Contains(Genre.Trim()));
                    }
                    if (!String.IsNullOrEmpty(author))
                    {
                        reportQueryable = reportQueryable.Where(x => x.Author.Trim().Contains(author.Trim()));
                    }
                    return new ServiceResult<List<EBook>>()
                    {
                        Data = reportQueryable.OrderByDescending(x => x.Id).Take(10).ToList(),
                        Message = "",
                        Status = ResultStatus.Ok
                    };
                }
            }
            catch (Exception)
            {
                return new ServiceResult<List<EBook>>()
                {
                    Data = new List<EBook>(),
                    Message = "Error applying filter",
                    Status = ResultStatus.processError
                };
            }
        }

        public ServiceResult<bool> DeleteBook(int bookId)
        {
            try
            {
                using (ServiceFactory factory = new ServiceFactory())
                {
                    BookService service = new BookService(factory);
                    var result = service.DeleteBook(bookId);
                    return new ServiceResult<bool>()
                    {
                        Data = result,
                        Message = "Book Deleted Successfully !....",
                        Status = ResultStatus.Ok
                    };
                }
            }
            catch (Exception)
            {
                return new ServiceResult<bool>()
                {
                    Data = false,
                    Message = "Error Deleting Donation History",
                    Status = ResultStatus.processError
                };
            }
        }
        public void Dispose()
        {

        }
    }
}