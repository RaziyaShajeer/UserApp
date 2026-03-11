using System.ComponentModel.DataAnnotations;

namespace UserApplication.API.UserModule.DTO
{
	public class SignupRequest
	{
		[Required]
        public string UserName { get; set; }
        [EmailAddress]
		public string EmailId { get; set; }
		[Required]
		public string Password { get; set; }
		[Phone]
		public string PhoneNumber { get; set; }
		[Required]

        public IFormFile ProfileImage { get; set; }
        public string Country { get; set; }
    }
}
