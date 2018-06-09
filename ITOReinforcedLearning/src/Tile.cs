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
        public PossibleDirections Exit;
        public Dictionary<string, float> Coordinates;
        public List<float> LearningHistory = new List<float>();

        Tile(
            List<PossibleDirections> walls,
            Dictionary<string, float> coordinates,
            PossibleDirections? exit
        )
        {
            Walls = walls;
            Coordinates = coordinates;
            LearningHistory = new List<float> { 0 };
            if(exit != null)
            {
                Exit = (PossibleDirections) exit;
            }
        }

        public float getAverageReward()
        {
            return LearningHistory.Average();
        }
    }
}
