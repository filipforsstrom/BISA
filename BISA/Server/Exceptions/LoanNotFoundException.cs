﻿using System.Runtime.Serialization;

namespace BISA.Server.Services.LoanService
{
    [Serializable]
    internal class LoanNotFoundException : Exception
    {
        public LoanNotFoundException()
        {
        }

        public LoanNotFoundException(string? message) : base(message)
        {
        }

        public LoanNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected LoanNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}