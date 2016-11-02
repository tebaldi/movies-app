using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Services.Dto
{
    public class MovieDetails : IDataTransferObject
    {
        public int ID { get; set; }
        public string MovieName { get; set; }
        public string PosterImage { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string OverView { get; set; }
    }
}
