using System;
using System.Collections.Generic;
using System.Text;

namespace Hashcode.Terminal.Models
{
    public class Board
    {
        private int[,] map;
        private ValuesMap _valueMap;
        public Board(ValuesMap valueMap)
        {
            _valueMap = valueMap;
            map = new int[valueMap.rows, valueMap.columns];
        }


    }
}
