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
    {
        T Data { get; }
    }

    public class ServiceResponse : IServiceResponse
    {
        public Guid ResponseKey { get; set; }
    }

    public class ServiceResponse<T> : IServiceResponse<T>
    {
        public Guid ResponseKey { get; set; }

        public T Data { get; set; }
    }
}
