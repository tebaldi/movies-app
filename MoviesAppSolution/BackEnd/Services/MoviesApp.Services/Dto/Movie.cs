﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Services.Dto
{
    public class Movie : MovieKey
    {
        public string MovieName { get; set; }

        public string Genre { get; set; }

        public string ImagePath { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
