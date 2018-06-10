namespace ITOReinforcedLearning.Learning
{
    static class LearningConstants
    {
        public readonly static double Epsilon = 0.5;
        public readonly static double Gamma = 1.0;
        public static double Alpha = 10;
        public readonly static int LearningRounds = 10; // arbitrary number of learning rounds
        public static double GetAlpha()
        {
            return Alpha;
        }
    }
}
