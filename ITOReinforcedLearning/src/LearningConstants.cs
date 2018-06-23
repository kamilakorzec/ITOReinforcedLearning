namespace ITOReinforcedLearning.Learning
{
    static class LearningConstants
    {
        public readonly static double Epsilon = 0.2;
        public readonly static double Gamma = 0.5;
        public static double Alpha = 0.5;
        public readonly static int LearningRounds = 150; // arbitrary number of learning rounds
        public static double GetAlpha()
        {
            return Alpha;
        }
    }
}
