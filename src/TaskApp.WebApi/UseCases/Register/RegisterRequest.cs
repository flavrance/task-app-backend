using System;
using System.ComponentModel.DataAnnotations;

namespace TaskApp.WebApi.UseCases.Register
{
    public sealed class RegisterRequest
    {
        [Required]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required]
        public int Status { get; set; }
    }
}
