namespace ITOReinforcedLearning.src
{
    static class LearningConstants
    {
        public static double Epsilon = 0.5;
        public static double Gamma = 1.0;
        public static double Alpha = 10;
        public static int LearningRounds = 10; // arbitrary number of learning rounds
        public static int RandomSeed = 42;
        public static double GetAlpha()
        {
            return Alpha;
        }
    }
}
