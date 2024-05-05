using System.ComponentModel.DataAnnotations;

namespace WebApiRis.Model
{
	public class UserExceptions
	{
		[Key]
		public Guid Id { get; set; }
		public string? Message { get; set; }
		public string? TargetSite { get; set; }
		[DisplayFormat(DataFormatString = "{0:dd.MM.yyyy hh:mm:ss}", ApplyFormatInEditMode = true)]
		public DateTime? dateTimeExc { get; set; }
		public int? IndexForm { get; set; }
		public Guid UserId { get; set; }
		public bool? VerifySignature { get; set; }
	}
}
