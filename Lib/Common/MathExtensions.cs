using System;

namespace Lib.Common {

	public static class MathExtensions {

		public static double GetPercentageOf(this double p_piece, double p_whole) {
			var pct =  (p_whole == 0)
				? 0.0d
				: p_piece / p_whole;

			return pct * 100.0d;
		}

		public static double GetPercentageOf(this int p_piece, int p_whole) {
			var piece = Convert.ToDouble(p_piece);
			var whole = Convert.ToDouble(p_whole);

			return piece.GetPercentageOf(whole);
		}

		public static double GetPercentageOf(this int? p_piece, int? p_whole) {
			var piece = Convert.ToDouble(p_piece ?? 0);
			var whole = Convert.ToDouble(p_whole ?? 0);

			return piece.GetPercentageOf(whole);
		}

		public static double GetPercentageOf(this double? p_piece, double? p_whole) {
			var piece = p_piece ?? 0.0d;
			var whole = p_whole ?? 0.0d;

			return piece.GetPercentageOf(whole);
		}

		public static double GetPercentageOf(this object p_piece, object p_whole) {
			var piece = Convert.ToDouble(p_piece);
			var whole = Convert.ToDouble(p_whole);

			return piece.GetPercentageOf(whole);
		}
	}
}
