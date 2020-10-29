using System.ComponentModel.DataAnnotations;

namespace Mini_Social_Media.Models
{
    public class UserAddModel
    {
        [StringLength(25, MinimumLength=4, ErrorMessage="len=3..25")]
        public string Login { get; set; }
    }
}