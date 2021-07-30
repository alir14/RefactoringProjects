using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DBLayer
{
    public interface INumberDataSet
    {
        string GetBaseNumberDataSet(double index);

        string GetTensNumberDataSet(double index);
    }
}
