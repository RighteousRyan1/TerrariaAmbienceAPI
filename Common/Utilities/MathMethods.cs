using System;
using System.Linq;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna;
using System.Collections.Generic;
using System.Reflection;

namespace TerrariaAmbienceAPI.Common.Utilities
{
    public static class MathMethods
    {
		/// <summary>
		/// Step value towards goal by an amount of step every call.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="goal"></param>
		/// <param name="step"></param>
		/// <returns></returns>
		public static float RoughStep(ref float value, float goal, float step)
		{
			if (value < goal)
			{
				value += step;

				if (value > goal)
				{
					return goal;
				}
			}
			else if (value > goal)
			{
				value -= step;

				if (value < goal)
				{
					return goal;
				}
			}

			return value;
		}
	}
}