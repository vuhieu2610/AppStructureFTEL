using System;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel;

namespace FTEL.Common.SqlService
{
	/// <summary>
	/// Map data from a source into a target object
	/// by copying public property values.
	/// </summary>
	/// <remarks></remarks>
	public static class DataMapper
	{
 
		/// <summary>
		/// Sets an object's property with the specified value,
		/// coercing that value to the appropriate type if possible.
		/// </summary>
		/// <param name="target">Object containing the property to set.</param>
		/// <param name="propertyName">Name of the property to set.</param>
		/// <param name="value">Value to set into the property.</param>
		public static void SetPropertyValue( object target, string propertyName, object value)
		{
			List<PropertyInfo> propertyInfos = GetPropertyByDataName(target, propertyName);
			foreach (PropertyInfo propertyInfo in propertyInfos)
			{
				if (value == null)
					propertyInfo.SetValue(target, value, null);
				else
				{
					Type pType = GetPropertyType(propertyInfo.PropertyType);
					Type vType = GetPropertyType(value.GetType());
					if (pType.Equals(vType))
					{
						// types match, just copy value
						propertyInfo.SetValue(target, value, null);
					}
					else
					{
						// types don't match, try to coerce
						if (pType.Equals(typeof(Guid)))
							propertyInfo.SetValue( target, new Guid(value.ToString()), null);
						else if (pType.IsEnum && vType.Equals(typeof(string)))
							propertyInfo.SetValue(target, Enum.Parse(pType, value.ToString()), null);
						else if (pType.Equals(typeof(string)))
							propertyInfo.SetValue(target, Convert.ToString(value), null);
						else
							propertyInfo.SetValue( target, Convert.ChangeType(value, pType), null);
					}
				}
			}
		}

		/// <summary>
		/// Gets an object's property with the specified value,
		/// coercing that value to the appropriate type if possible.
		/// </summary>
		/// <param name="target">Object containing the property to get.</param>
		/// <param name="propertyName">Name of the property to get.</param>
		/// <returns>Value to get into the property.</returns>
		public static object GetPropertyValue(object target, string propertyName)
		{
			Type type = target.GetType();
			PropertyInfo propertyInfo = type.GetProperty(propertyName);
			return propertyInfo.GetValue(target, null);
		}
		private static List<PropertyInfo> GetPropertyByDataName(object target, string propertyName)
		{
			Type type = target.GetType();
			List<PropertyInfo> props = new List<PropertyInfo>(DataMapper.GetSourceProperties(type));
			return props.FindAll(delegate(PropertyInfo pi)
			{
				return DataMapper.MatchPropertyInfo(pi, propertyName);
			});
		}

		public static bool MatchPropertyInfo(PropertyInfo pi, string dataName)
		{
            DataFieldAttribute[] da = (DataFieldAttribute[])pi.GetCustomAttributes(typeof(DataFieldAttribute), true);
			if (da.Length == 0)
			{
				return pi.Name.ToLower() == dataName.ToLower() ? true : false;///[vantienpros]:Cho phép set giá trị vào property không phân biệt chữ hoa, chữ thường
			}
            else
			{
				return da[0].DataFieldName.ToLower() == dataName.ToLower() ? true : false;///[vantienpros]:Cho phép set giá trị vào property không phân biệt chữ hoa, chữ thường
			}
        }

		public static PropertyInfo[] GetSourceProperties(Type sourceType)
		{
			List<PropertyInfo> result = new List<PropertyInfo>();
			PropertyDescriptorCollection props =
				TypeDescriptor.GetProperties(sourceType);
			foreach (PropertyDescriptor item in props)
				if (item.IsBrowsable)
					result.Add(sourceType.GetProperty(item.Name));
			return result.ToArray();
		}

		/// <summary>
		/// Returns a property's type, dealing with
		/// Nullable(Of T) if necessary.
		/// </summary>
		/// <param name="propertyType">Type of the
		/// property as returned by reflection.</param>
		public static Type GetPropertyType(Type propertyType)
		{
			Type type = propertyType;
			if (type.IsGenericType &&
				(type.GetGenericTypeDefinition() == typeof(Nullable<>)))
				return Nullable.GetUnderlyingType(type);
			return type;
		}
	}
}
