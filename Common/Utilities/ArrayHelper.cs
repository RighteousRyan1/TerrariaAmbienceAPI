using System;
using System.Linq;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna;
using System.Collections.Generic;
using System.Reflection;

namespace TerrariaAmbienceAPI.Common.Utilities
{
    public static class ArrayHelper
    {
        /// <summary>
        /// Converts a Vector2 into a 2-Dimensional Array.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static object[,] To2DArray(this Vector2 input)
        {
            var twoD = new object[,]
            {
                { (int)input.X, (int)input.Y }
            };
            return twoD;
        }
        /// <summary>
        /// Converts a Vector2 into a Tile object.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Tile ToTile(this Vector2 input)
        {
            Tile tile = Main.tile[(int)input.X / 16, (int)input.Y / 16];
            return tile;
        }
    }
}