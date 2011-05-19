using System;

namespace Lib.Infrastructure {
	/// <summary>
	/// Maps an enum to a human-readable description string.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	public class DescriptionAttribute : Attribute, IMapToValue<string> {
		public string Description { get; set; }
		public DescriptionAttribute(string description) { this.Description = description; }

		public string Value { get { return Description; } }
	}

}
