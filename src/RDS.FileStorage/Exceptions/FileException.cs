using System;

namespace RDS.FileStorage.Exceptions
{
    public class FileException : Exception
    {
        public FileException()
        {
            
        }

                
        public FileException(string message) : base(message)
        {
            
        }

        public FileException(string message, Exception inner) : base(message, inner)
        {
            
        }
    }
}