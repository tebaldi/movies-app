using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoviesApp.Services
{
    public interface IServiceResponse
    {
        Guid ResponseKey { get; }
    }

    public interface IServiceResponse<out T> : IServiceResponse
        where T: IDataTransferObject
    {
        T Data { get; }
    }
}
