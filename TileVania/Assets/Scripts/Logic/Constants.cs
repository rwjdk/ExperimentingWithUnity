namespace Logic
{
    public struct Constants
    {
        public struct Tags
        {
            public const string Enemy = "Enemy";
            public const string Finish = "Finish";
            public const string Coin = "Coin";
        }

        public struct AnimatorParameters
        {
            public const string IsRunning = "isRunning";
            public const string IsClimbing = "isClimbing";
        }

        public struct AnimatorTrigger
        {
            public const string Dying = "Dying";
        }

        public struct Layers
        {
            public const string Ground = "Ground";
            public const string Ladder = "Ladder";
            public const string Enemy = "Enemy";
            public const string Hazard = "Hazard";
            public const string Water = "Water";
        }
    }
}