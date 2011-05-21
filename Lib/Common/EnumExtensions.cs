using System;
using System.Linq;
using System.Web.Mvc;
using Lib.Infrastructure;

namespace Lib.Common {

	public static class EnumExtensions {
		/// <summary>
		/// Returns the value of the StringConstant attribute associated with this enum. If no
		/// attribute exists, returns the ToString() result.
		/// </summary>
		public static string ToStringConstant(this Enum enumValue) {
			return enumValue.GetStringAttribute<StringConstantAttribute>();
		}
		
		/// <summary>
		/// Returns the value of the Description attribute associated with this enum. If no
		/// attribute exists, returns the ToString() result.
		/// </summary>
		public static string ToDescription(this Enum enumValue) {
			return enumValue.GetStringAttribute<DescriptionAttribute>();
		}

		public static T[] GetAttributes<T>(this Enum p_enumValue) where T:Attribute {
			string enumString = p_enumValue.ToString();

			var attributes = (T[])p_enumValue
				.GetType()
				.GetField(enumString)
				.GetCustomAttributes(typeof(T), false);

			return attributes;
		}

		public static string GetStringAttribute<T>(this Enum p_enumValue) where T : Attribute, IMapToValue<string> {

			var attributes = p_enumValue.GetAttributes<T>();

			return (attributes.Length > 0)
				? attributes[0].Value
				: p_enumValue.ToString();
		}
	}

	public static class EnumHelper {
		public static T Parse<T>(string p_description) {
			// If the string directly matches an enum's ToString() value then use it
			try {
				return (T)Enum.Parse(typeof(T), p_description);
			}
			catch (ArgumentException) {
				// Look for a StringConstant or Description match
				foreach (T enumValue in Enum.GetValues(typeof(T))) {
					
					string enumString = enumValue.ToString();

					var stringConstantAttrs = (StringConstantAttribute[])enumValue
						.GetType()
						.GetField(enumString)
						.GetCustomAttributes(typeof(StringConstantAttribute), false);

					if ((stringConstantAttrs.Length > 0) && (stringConstantAttrs[0].Value == p_description))
						return enumValue;

					var descriptionAttrs = (DescriptionAttribute[])enumValue
						.GetType()
						.GetField(enumString)
						.GetCustomAttributes(typeof(DescriptionAttribute), false);

					if ((descriptionAttrs.Length > 0) && (descriptionAttrs[0].Description == p_description))
						return enumValue;
				}

				throw;
			}
		}

		public static T Parse<T>(string p_description, T p_defaultValue) {
			// There's no TryParse for enum, so we just catch the exception
			try {
				return EnumHelper.Parse<T>(p_description);
			}
			catch (ArgumentException) {
				return p_defaultValue;
			}
		}

		public static Nullable<T> ParseAsNullable<T>(string p_description) where T:struct {
			// There's no TryParse for enum, so we just catch the exception
			try {
				return EnumHelper.Parse<T>(p_description);
			}
			catch (ArgumentException) {
				return null;
			}
		}

        public static SelectList ToSelectList<TEnum>(this TEnum enumObj)
        {
            var values = from TEnum e in Enum.GetValues(typeof(TEnum))
                         select new { ID = e, Name = e.ToString() };

            return new SelectList(values, "Id", "Name", enumObj);
        }

        public static SelectList ToDropDownList<TEnum>(this TEnum enumObj)
        {
            var values = from TEnum e in Enum.GetValues(typeof(TEnum))
                         select new { Value = e, Text = e.ToString() };

            return new SelectList(values, "Value", "Text", enumObj);
        }


        public static JsonResult ToJsonList<TEnum>(this TEnum enumObj)
        {
            var jsonResult = new JsonResult
            {
                Data = (from TEnum e in Enum.GetValues(typeof(TEnum))
                        select new { Value = e, Text = e.ToString(), Selected = e.Equals(enumObj) }).ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
            return jsonResult;
        }

	}
}
