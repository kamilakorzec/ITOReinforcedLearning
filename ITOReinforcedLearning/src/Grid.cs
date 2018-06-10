using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITOReinforcedLearning.src
{
    class Grid
    {
        private Tile[][] tiles;
        private List<int[]> exits;
        public int Dimension;

        public Grid(
            int dimension,
            List<int[]> exitCoordinates
        )
        {
            tiles = new Tile[dimension][];
            Dimension = dimension;
            exits = exitCoordinates;
        }

        public Tile GetTileByCoordinates(int x, int y)
        {
            return tiles[x][y];
        }

        public bool IsExit(int[] coordinates)
        {
            return exits.Contains(coordinates);
        }
    }
}
