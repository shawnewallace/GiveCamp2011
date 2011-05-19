namespace Lib.Common
{
	public static class BooleanExtensions
	{
		public static int ToInt(this bool instance)
		{
			return instance ? 1 : 0;
		}

		public static string ToYorN(this bool instance)
		{
			return instance ? "Y" : "N";
		}

		public static string ToYorNo(this bool? instance)
		{
			if (instance.HasValue) return instance.Value.ToYorN();

			return null;
		}
	}
}
