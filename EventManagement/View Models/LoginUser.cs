using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EventManagementSystem.View_Models
{
    public class LoginUser
    {
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Remember Me")]
        public bool RememberMe { get; set; }
    }
}
