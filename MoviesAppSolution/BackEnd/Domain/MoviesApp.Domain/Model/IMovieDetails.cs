using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Domain.Model
{
    public interface IMovieDetails : IMovie
    {   
        string OverView { get; set; }
    }
}
