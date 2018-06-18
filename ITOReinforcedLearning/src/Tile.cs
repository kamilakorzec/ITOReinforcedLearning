using System.Collections.Generic;
using System.Linq;

namespace ITOReinforcedLearning.Learning
{
    class Tile
    {
        public PossibleDirections? Wall;
        public bool Exit;
        public int[] Coordinates;
        public List<float> LearningHistory = new List<float>();

        public Tile(
            PossibleDirections? wall,
            int[] coordinates,
            bool exit = false
        )
        {
            Wall = wall;
            Coordinates = coordinates;
            LearningHistory = new List<float> { 0 };
            Exit = exit;
        }

        public float getAverageReward()
        {
            return LearningHistory.Average();
        }
    }
}
