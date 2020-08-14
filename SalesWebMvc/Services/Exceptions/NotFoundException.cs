using System;

namespace SalesWebMvc.Services.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        // Exception personalizada NotFound
        public NotFoundException(string message) : base(message)
        { 
        }
    }
}
