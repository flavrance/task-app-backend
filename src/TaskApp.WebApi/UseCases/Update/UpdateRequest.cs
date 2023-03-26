namespace TaskApp.WebApi.UseCases.Update
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public sealed class UpdateRequest
    {
        [Required]
        [Key]
        public Guid TaskId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required]
        public int Status { get; set; }
    }
}
