using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesRental.Logic
{
    public class Customer
    {
        private string _name;
        private List<Rental> _rentals = new List<Rental>();

        public Customer(string name)
        {
            _name = name;
        }

        public void addRental(Rental arg)
        {
            _rentals.Add(arg);
        }

        public string getName()
        {
            return _name;
        }

        public string Statement()
        {
            double totalAmount = 0;
            int frequentRentalPoint = 0;

            string result = $"Rental record for {getName()} \n";

            foreach (var item in _rentals)
            {
                double thisAmount = 0;
                switch (item.getMovie().getPriceCode())
                {
                    case Movie.REGULAR:
                        thisAmount += 2;
                        if (item.getDayRented() > 2)
                            thisAmount += (item.getDayRented() - 2) * 1.5;
                        break;
                    case Movie.NEW_RELEASE:
                        thisAmount += item.getDayRented() * 3;
                        break;
                    case Movie.CHILDRENS:
                        thisAmount += 1.5;
                        if (item.getDayRented() > 3)
                            thisAmount += (item.getDayRented() - 3) * 1.5;
                        break;
                    default:
                        break;
                }

                frequentRentalPoint++;

                if (item.getMovie().getPriceCode() == Movie.NEW_RELEASE &&
                    item.getDayRented() > 1)
                    frequentRentalPoint++;

                result += $"\t {item.getMovie().getTitle()} \t {thisAmount} \n";

                totalAmount += thisAmount;
            }

            result += $"Amount owned is {totalAmount} \n";
            result += $"you earned {frequentRentalPoint} frequent renter points";

            return result;
        }
    }
}
