using System;

namespace RBox.WinForms
{
    public class ServiceException : Exception
    {
        public ServiceException(string message, params object[] args) : base(string.Format(message, args))
        {

        }
    }
}
