using System;

namespace Extensions
{
	public static class FloorRoundCeil
	{
		public static float FloorFrom(float value, int pos)
		{
			if(pos < 1)
				throw new ArgumentException("\'pos\' should be at least 1.");

			var powOf10 = Math.Pow(10, pos-1);

			return (float)(Math.Floor(value*powOf10) / powOf10);
		}

		public static float RoundFrom(float value, int pos)
		{
			if(pos < 1)
				throw new ArgumentException("\'pos\' should be at least 1.");

			var powOf10 = Math.Pow(10, pos-1);

			return (float)(Math.Round(value*powOf10) / powOf10);
		}

		public static float CeilFrom(float value, int pos)		
		{
			if(pos < 1)
				throw new ArgumentException("\'pos\' should be at least 1.");

			var powOf10 = Math.Pow(10, pos-1);

			return (float)(Math.Ceiling(value*powOf10) / powOf10);
		}
		
		
		public static double FloorFrom(double value, int pos)
		{
			if(pos < 1)
				throw new ArgumentException("\'pos\' should be at least 1.");

			var powOf10 = Math.Pow(10, pos-1);

			return Math.Floor((float)value*powOf10) / powOf10;
		}

		public static double RoundFrom(double value, int pos)
		{
			if(pos < 1)
				throw new ArgumentException("\'pos\' should be at least 1.");

			var powOf10 = Math.Pow(10, pos-1);

			return Math.Round((float)value*powOf10) / powOf10;
		}

		public static double CeilFrom(double value, int pos)		
		{
			if(pos < 1)
				throw new ArgumentException("\'pos\' should be at least 1.");

			var powOf10 = Math.Pow(10, pos-1);

			return Math.Ceiling((float)value*powOf10) / powOf10;
		}
	}
}