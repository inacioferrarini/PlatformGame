using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Constants used by the game itself.
/// </summary>
static class Constants
{
    /// <summary>
    /// Input related constants.
    /// </summary>
    public static class Input
    {
        /// <summary>
        /// Movement Axis.
        /// </summary>
        public static class Axis
        {
            public const string horizontal = "Horizontal";
        }

        /// <summary>
        /// Input Keys.
        /// </summary>
        public static class Keys
        {
            public const string jump = "Jump";
        }

    }

    /// <summary>
    /// Collision related constants.
    /// </summary>
    public static class Collision
    {
        /// <summary>
        /// Collision Layers.
        /// </summary>
        public static class Layers
        {
            public const int player = 9;
            public const int enemy = 10;
        }

        /// <summary>
        /// Game elements tags.
        /// </summary>
        public static class Tags
        {
            public const string player = "Player";
            public const string enemy = "Enemy";
            public const string exit = "Exit";
        }

    }

}
