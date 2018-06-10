using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITOReinforcedLearning.Learning
{
    class Grid
    {
        private Tile[][] tiles;
        public int Dimension;

        public Grid(
            int dimension
        )
        {
            tiles = new Tile[dimension][];
            Dimension = dimension;
        }

        public void AddTile(Tile tile)
        {
            int index = tiles.Length + 1;
            int row = index / Dimension;
            int column = index % Dimension;

            tiles[row][column] = tile;
        }

        public Tile GetTileByCoordinates(int x, int y)
        {
            return tiles[x][y];
        }

        public bool IsExit(int[] coordinates)
        {
            return tiles[coordinates[0]][coordinates[1]].Exit;
        }
    }
}
