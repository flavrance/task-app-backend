﻿using System;

namespace TaskApp.WebApi.UseCases.Register
{
    public sealed class RegisterRequest
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
    }
}
