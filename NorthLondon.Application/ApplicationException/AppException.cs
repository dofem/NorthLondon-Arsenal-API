using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthLondon.Application.ApplicationException
{
    public class PlayerAlreadyExistException : Exception
    {
        public PlayerAlreadyExistException(string shirtNumber) :
            base($"Player with shirt number '{shirtNumber}' already exists.")
        {
            
        }
    }

    public class PlayerDoesntExistException : Exception
    {
        public PlayerDoesntExistException(string shirtNumber) :
            base($"Player with shirt number '{shirtNumber}' doesnt exists.")
        {

        }
    }
}
