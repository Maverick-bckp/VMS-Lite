namespace Tapfin.Api.Models.Entities
{
    public class WorkerBckgEvalValidationTypes
    {
        public int Id { get; set; }

        public string StatusTypeDesc { get; set; } = null!;

        public bool? IsActive { get; set; }
    }
}
