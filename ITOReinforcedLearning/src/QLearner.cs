using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITOReinforcedLearning.src
{
    class QLearner
    {
        private Dictionary<string, Dictionary<PossibleDirections, double>> qTable;

        private Dictionary<PossibleDirections, double> Zeros()
        {
            return new Dictionary<PossibleDirections, double>{
                { PossibleDirections.UP, 0 },
                { PossibleDirections.DOWN, 0 },
                { PossibleDirections.LEFT, 0 },
                { PossibleDirections.RIGHT, 0 },
            };
        }

        private string CreateDictKey(State state)
        {
            return state.ToString();
        }

        public Dictionary<PossibleDirections, double> Q(State state)
        {
            string stateKey = CreateDictKey(state);

            if (!qTable.ContainsKey(stateKey))
            {
                qTable.Add(stateKey, Zeros());
            }

            return qTable[stateKey];
        }
    }
}