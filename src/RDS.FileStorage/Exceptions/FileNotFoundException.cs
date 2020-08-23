using System;

namespace RDS.FileStorage.Exceptions
{
    public class FileNotFoundException : Exception
    {
        public FileNotFoundException()
        {
            
        }

                
        public FileNotFoundException(string message) : base(message)
        {
            
        }

        public FileNotFoundException(string message, Exception inner) : base(message, inner)
        {
            
        }
    }
}