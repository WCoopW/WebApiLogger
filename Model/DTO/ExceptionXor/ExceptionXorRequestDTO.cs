namespace WebApiRis.Model.DTO.ExceptionXor
{
	public class ExceptionXorRequestDTO
	{
		public string? Message { get; set; }
		public string? TargetSite { get; set; }
		public DateTime? dateTimeExc { get; set; }
		public int? IndexForm { get; set; }
	}
}
