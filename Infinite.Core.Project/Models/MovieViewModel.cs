﻿using System;

namespace Infinite.Core.Project.Models
{
    public class MovieViewModel
    {
        public int Id { get; set; }
        public string MovieName { get; set; }
        public string ProductionName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int GenreId { get; set; }
    }
}
