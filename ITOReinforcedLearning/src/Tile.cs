using System.Collections.Generic;
using System.Linq;

namespace ITOReinforcedLearning.Learning
{
    class Tile
    {
        public PossibleDirections? Wall;
        public bool Exit;
        public int[] Coordinates;
        public List<int> LearningHistory = new List<int>();

        public Tile(
            PossibleDirections? wall,
            int[] coordinates,
            bool exit = false
        )
        {
            Wall = wall;
            Coordinates = coordinates;
            LearningHistory = new List<int> { 0 };
            Exit = exit;
        }

        public double getAverageReward()
        {
            return LearningHistory.Average();
        }
    }
}
