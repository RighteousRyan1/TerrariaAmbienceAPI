using System;
using System.Linq;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework.Input;

namespace TerrariaAmbienceAPI.Common.Utilities
{
    public static class CommonUtils
    {
        /// <summary>
        /// Returns true when a keyboard key is pressed.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool OnKeyPressed(this KeyboardState state, Keys key)
        {
            return state.IsKeyDown(key) && !Main.oldKeyState.IsKeyDown(key);
        }
        /// <summary>
        /// Is true when key is released.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool OnKeyRelease(this KeyboardState state, Keys key)
        {
            return !state.IsKeyDown(key) && Main.oldKeyState.IsKeyDown(key);
        }
        /// <summary>
        /// Checks if all Keys in <paramref name="keys"/> are pressed.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static bool AreKeysPressed(this KeyboardState state, params Keys[] keys)
        {
            bool shouldReturnTrue = keys.All(k => state.IsKeyDown(k));
            return shouldReturnTrue;
        }
        /// <summary>
        /// Pick a random T from the values array.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="values">The params array of objects.</param>
        /// <returns></returns>
        public static T Pick<T>(params T[] values)
        {
            return Main.rand.Next(values);
        }
    }
}