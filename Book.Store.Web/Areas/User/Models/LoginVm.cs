using System.ComponentModel.DataAnnotations;

namespace Book.Store.Web.Areas.User.Models
{
    public class LoginVm
    {
        [Required(ErrorMessage = "User Name Is Required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
