using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LinkManagerClientWPF.Exceptions
{
    internal class ExecutableFileNotFoundException : Exception
    {
        public string? FileName { get; }
        public ExecutableFileNotFoundException(string? fileName = null)
        {
            FileName = fileName;
        }

        public ExecutableFileNotFoundException(string? message, string? fileName = null) : base(message)
        {
            FileName = fileName;
        }

        public ExecutableFileNotFoundException(string? message, Exception? innerException, string? fileName = null) : base(message, innerException)
        {
            FileName = fileName;
        }

        protected ExecutableFileNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
