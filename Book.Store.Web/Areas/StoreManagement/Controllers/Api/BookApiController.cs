using Book.Store.Web.Areas.StoreManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Common;
using Store.Entities;
using Store.Manager;
using Store.Service.Models;

namespace Book.Store.Web.Areas.StoreManagement.Controllers.Api
{

    [Route("api/[controller]")]
    [ApiController]
    public class BookApiController : ControllerBase
    {
        public IConfiguration _configuration;
        public BookApiController(IConfiguration iconfig)
        {
            _configuration = iconfig;
        }

        [HttpGet]
        [Route("GetBooks")]
        public ServiceResult<List<BookVm>> GetBooks(int skipCount = 0)
        {
            using (BookManager manager = new BookManager())
            {
                var result = manager.GetBooks();
                List<BookVm> tempResult = ParseIntoBooksList(result.Data.Skip(skipCount).Take(10).ToList());
                return new ServiceResult<List<BookVm>>()
                {
                    Data = tempResult,
                    Message = result.Message,
                    Status = result.Status
                };
            }
        }
        [HttpPost, Route("AddUpdateBook")]
        public ServiceResult<bool> AddUpdateBook([FromForm] BookVm model)
        {
            using (BookManager manager = new BookManager())
            {
                if (model.Id == 0)
                {
                    var result = manager.AddBook(model.ConvertObjectTo<BooksModels>());
                    return new ServiceResult<bool>()
                    {
                        Data = true,
                        Message = "Books Added Successfully ! ....",
                        Status = ResultStatus.Ok
                    };
                }
                else
                {
                    var result = manager.UpdateBook(model.ConvertObjectTo<EBook>());
                    return new ServiceResult<bool>()
                    {
                        Data = true,
                        Message = "Books Updated Successfully ! ....",
                        Status = ResultStatus.Ok
                    };
                }
            }
        }

        [HttpGet, Route("DeleteBook")]
        public ServiceResult<bool> DeleteBook(int bookId)
        {
            using (BookManager manager = new BookManager())
            {
                return manager.DeleteBook(bookId);
            }
        }

        [HttpPost, Route("FilterBooks")]
        public ServiceResult<List<BooksModels>> FilterBooks([FromForm] FilterVm model)
        {
            using (BookManager manager = new BookManager())
            {
                var result = manager.FilterBooks(model.Genre, model.Author);
                List<BooksModels> parsedResult = ParseIntoBooksFilterList(result.Data);
                return new ServiceResult<List<BooksModels>>()
                {
                    Data = parsedResult,
                    Message = result.Message,
                    Status = result.Status
                };
            }
        }


        #region Private functions
        private List<BooksModels> ParseIntoBooksFilterList(List<EBook> data)
        {
            List<BooksModels> resultList = new List<BooksModels>();
            foreach (var item in data)
            {
                BooksModels vm = new BooksModels()
                {
                    Id = item.Id,
                    Author = item.Author,
                    Category = item.Category,
                    CreatedDate = item.CreatedDate.ToString("yyyy/MM/dd"),
                    Genre = item.Genre,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    Title = item.Title,
                };
                resultList.Add(vm);
            }
            return resultList;

        }

        private List<BookVm> ParseIntoBooksList(List<BooksModels> list)
        {
            List<BookVm> resultList = new List<BookVm>();
            foreach (var item in list)
            {
                BookVm vm = new BookVm()
                {
                    Id = item.Id,
                    Author = item.Author,
                    Category = item.Category,
                    CreatedDate = item.CreatedDate,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    Genre = item.Genre,
                    Title = item.Title
                };
                resultList.Add(vm);
            }
            return resultList;
        }



        #endregion

    }
}
