using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITOReinforcedLearning.src
{
    class Tile
    {
        public PossibleDirections Wall;
        public Dictionary<string, float> Coordinates;
        public List<float> LearningHistory;

        public float getAverageReward()
        {
            return LearningHistory.Count > 0 ? LearningHistory.Average() : 0;
        }
    }
}
