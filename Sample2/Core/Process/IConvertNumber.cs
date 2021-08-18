using Core.DBLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Process
{
    public interface IConvertNumber
    {
        string ProcessPart(int i, char[] number);
    }

}
