using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITOReinforcedLearning.src
{
    class Tile
    {
        public List<PossibleDirections> Walls;
        public Dictionary<string, float> Coordinates;
        public List<float> LearningHistory = new List<float>();

        Tile(
            List<PossibleDirections> walls,
            Dictionary<string, float> coordinates
        )
        {
            Walls = walls;
            Coordinates = coordinates;
        }

        public float getAverageReward()
        {
            return LearningHistory.Count > 0 ? LearningHistory.Average() : 0;
        }
    }
}
