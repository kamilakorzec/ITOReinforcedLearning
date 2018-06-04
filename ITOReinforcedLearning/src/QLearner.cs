using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITOReinforcedLearning.src
{
    class QLearner
    {
        private Dictionary<string, float[]> qTable;

        private float[] Zeros()
        {
            return new float[] { 0, 0, 0, 0 };
        }

        private string CreateDictKey(State state)
        {
            return state.ToString();
        }

        public float[] Q(State state)
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
