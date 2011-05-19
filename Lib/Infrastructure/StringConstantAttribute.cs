using System;

namespace Lib.Infrastructure {
	/// <summary>
	/// Maps an enum to a string constant used to represent it in the DB (and legacy code).
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	public class StringConstantAttribute : Attribute, IMapToValue<string> {
		public string Value { get; set; }
		public StringConstantAttribute(string code) { this.Value = code; }
	}

}
