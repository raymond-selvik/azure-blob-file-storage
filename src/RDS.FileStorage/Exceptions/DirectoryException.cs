using System;

namespace RDS.FileStorage.Exceptions
{
    public class DirectoryException : Exception
    {
        public DirectoryException()
        {
            
        }

                
        public DirectoryException(string message) : base(message)
        {
            
        }

        public DirectoryException(string message, Exception inner) : base(message, inner)
        {
            
        } 
    }
}