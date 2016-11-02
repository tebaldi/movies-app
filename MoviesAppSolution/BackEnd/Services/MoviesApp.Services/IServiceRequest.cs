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

    public interface IServiceRequest<T> : IServiceRequest
    {
        T Data { get; }
    }

    public class ServiceRequest : IServiceRequest
    {
        public Guid RequestKey { get; set; }
    }

    public class ServiceRequest<T> : IServiceRequest<T>
    {
        public Guid RequestKey { get; set; }

        public T Data { get; set; }
    }
}
