static class Constants
{
    public static class Input
    {
        public static class Axis
        {
            public const string horizontal = "Horizontal";
        }

        public static class Keys
        {
            public const string jump = "Jump";
        }

    }

    public static class Collision
    {
        public static class Layers
        {
            public const int player = 9;
            public const int enemy = 10;
        }

        public static class Tags
        {
            public const string player = "Player";
            public const string enemy = "Enemy";
            public const string exit = "Exit";
            public const string gem = "Gem";
        }

    }

}
