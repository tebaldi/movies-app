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
    {
        IServiceResponse<TResponse> ExecuteService();
    }

    public interface IService<TRequest, out TResponse> : IService
    {
        IServiceResponse<TResponse> ExecuteService(IServiceRequest<TRequest> request);
    }
}
