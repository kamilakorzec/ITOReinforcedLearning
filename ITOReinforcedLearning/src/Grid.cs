using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITOReinforcedLearning.Learning
{
    class Grid
    {
        private List<Tile> tiles;
        public int Dimension;

        public Grid(
            int dimension
        )
        {
            tiles = new List<Tile> { };
            Dimension = dimension;
        }

        public void AddTile(Tile tile)
        {
            tiles.Add(tile);
        }

        public Tile GetTileByCoordinates(int x, int y)
        {
            return tiles[Dimension * x + y];
        }

        public bool IsExit(int[] coordinates)
        {
            return tiles[Dimension * coordinates[0] + coordinates[1]].Exit;
        }
    }
}
