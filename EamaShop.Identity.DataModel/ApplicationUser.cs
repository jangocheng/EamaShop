using System;
using System.ComponentModel.DataAnnotations;

namespace EamaShop.Identity.DataModel
{
    public class ApplicationUser
    {
        
        public long Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 8)]
        public string AccountName { get; set; }
        [StringLength(18, MinimumLength = 6)]
        [Required]
        public string Password { get; set; }
        [Phone]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string NickName { get; set; }

        [Required]
        [Url]
        public string HeadImageUri { get; set; }
        [Required]
        public string Salt { get; set; }
    }
}
