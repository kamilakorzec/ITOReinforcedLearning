﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITOReinforcedLearning.src
{
    class Grid
    {
        private Tile[][] tiles;
        private List<Tile> exits;

        public Tile GetTileByCoordinates(int x, int y)
        {
            return tiles[x][y];
        }
    }
}