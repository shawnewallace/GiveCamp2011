using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.IO;

namespace Lib.Common {
	public static class ReflectionExtensions {

		/// <summary>
		/// Allows string-based access to an object's properties.
		/// </summary>
		public static object GetPropertyValue(this object p_object, string p_propertyName) {
			var propertyValue = p_object
				.GetType()
				.GetProperty(p_propertyName)
				.GetValue(p_object, null);

			return propertyValue;
		}

		/// <summary>
		/// Allows string-based access to an object's properties.
		/// </summary>
		public static T GetPropertyValue<T>(this object p_object, string p_propertyName) {
			var propertyValue = (T)p_object.GetPropertyValue(p_propertyName);
			return propertyValue;
		}

		/// <summary>
		/// Allows string-based access to an object's properties.
		/// </summary>
		public static void SetProperty(this object p_object, string p_propertyName, object p_propertyValue) {
			var t = p_object.GetType();
			if (t.IsNullOrEmpty()) {
				throw new ApplicationException("Cannot find type.");
			}

			var p = t.GetProperty(p_propertyName);
			if (p.IsNullOrEmpty()) {
				throw new ApplicationException("Cannot get type for property: " + p_propertyName);
			}

			if (p_propertyValue.IsNotNullOrEmpty()) {
				var val = Convert.ChangeType(p_propertyValue, p.PropertyType);
				p.SetValue(p_object, val, null);
			}

		}

		/// <summary>
		/// Returns a collection of static fields (e.g. constants) starting with a specified prefix.
		/// </summary>
		public static IEnumerable<FieldInfo> GetConstantsStartingWith(this Type p_type, string p_nameStartsWith) {
			var fields = p_type
				.GetFields(BindingFlags.Static | BindingFlags.GetField | BindingFlags.Public)
				.Where(f => f.Name.StartsWith(p_nameStartsWith));

			foreach (var field in fields)
				yield return field;
		}

		public static string Dump(this object p_object, int p_maxDepth) {
			var writer = new StringWriter();
			ObjectDumper.Write(p_object, p_maxDepth, writer);
			return writer.ToString();
		}
	}
}
