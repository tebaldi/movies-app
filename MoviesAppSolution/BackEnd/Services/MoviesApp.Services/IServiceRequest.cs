using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoviesApp.Services
{
    public interface IServiceRequest
    {
        Guid RequestKey { get; }
    }

    public interface IServiceRequest<T> where T : IDataTransferObject
    {
        T Data { get; }
    }
}
