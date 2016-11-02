using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoviesApp.Services
{
    public interface IService
    {
        
    }

    public interface IService<TResponse> : IService
        where TResponse : IDataTransferObject
    {
        IServiceResponse<TResponse> ExecuteService();
    }

    public interface IService<TRequest, TResponse> : IService
        where TRequest : IDataTransferObject
        where TResponse : IDataTransferObject
    {
        IServiceResponse<TResponse> ExecuteService(IServiceRequest<TRequest> request);
    }
}
