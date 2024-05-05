namespace WebApiRis.Model.DTO.Exception
{
	public class ExceptionRequestDTO
	{
		public string? Message { get; set; }
		public string? TargetSite { get; set; }
		public DateTime? dateTimeExc { get; set; }
		public int? IndexForm { get; set; }
		public string? signedHash { get; set; }
	}
}
