using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesRental.Logic
{
    public class Rental
    {
        private Movie _movie;
        private int _deyRented;

        public Rental(Movie movie, int dayRented)
        {
            _movie = movie;
            _deyRented = dayRented;
        }

        public int getDayRented()
        {
            return _deyRented;
        }

        public Movie getMovie()
        {
            return _movie;
        }

    }
}
