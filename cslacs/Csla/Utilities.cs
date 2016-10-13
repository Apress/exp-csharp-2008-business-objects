using System;
using System.ComponentModel;
using System.Reflection;
using System.Xml;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace Csla
{
  /// <summary>
  /// Contains utility methods used by the
  /// CSLA .NET framework.
  /// </summary>
  public static class Utilities
  {
    #region Replacements for VB runtime functionality

    /// <summary>
    /// Determines whether the specified
    /// value can be converted to a valid number.
    /// </summary>
    public static bool IsNumeric(object value)
    {
      double dbl;
      return double.TryParse(value.ToString(), System.Globalization.NumberStyles.Any,
        System.Globalization.NumberFormatInfo.InvariantInfo, out dbl);
    }

    /// <summary>
    /// Allows late bound invocation of
    /// properties and methods.
    /// </summary>
    /// <param name="target">Object implementing the property or method.</param>
    /// <param name="methodName">Name of the property or method.</param>
    /// <param name="callType">Specifies how to invoke the property or method.</param>
    /// <param name="args">List of arguments to pass to the method.</param>
    /// <returns>The result of the property or method invocation.</returns>
    public static object CallByName(
      object target, string methodName, CallType callType,
      params object[] args)
    {
      switch (callType)
      {
        case CallType.Get:
          {
            PropertyInfo p = target.GetType().GetProperty(methodName);
            return p.GetValue(target, args);
          }
        case CallType.Let:
        case CallType.Set:
          {
            PropertyInfo p = target.GetType().GetProperty(methodName);
            p.SetValue(target, args[0], null);
            return null;
          }
        case CallType.Method:
          {
            MethodInfo m = target.GetType().GetMethod(methodName);
            return m.Invoke(target, args);
          }
      }
      return null;
    }

    #endregion

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

    /// <summary>
    /// Returns the type of child object
    /// contained in a collection or list.
    /// </summary>
    /// <param name="listType">Type of the list.</param>
    public static Type GetChildItemType(Type listType)
    {
      Type result = null;
      if (listType.IsArray)
        result = listType.GetElementType();
      else
      {
        DefaultMemberAttribute indexer =
          (DefaultMemberAttribute)Attribute.GetCustomAttribute(
          listType, typeof(DefaultMemberAttribute));
        if (indexer != null)
          foreach (PropertyInfo prop in listType.GetProperties(
            BindingFlags.Public |
            BindingFlags.Instance |
            BindingFlags.FlattenHierarchy))
          {
            if (prop.Name == indexer.MemberName)
              result = Utilities.GetPropertyType(prop.PropertyType);
          }
      }
      return result;
    }

    #region  CoerceValue

    /// <summary>
    /// Attempts to coerce a value of one type into
    /// a value of a different type.
    /// </summary>
    /// <param name="desiredType">
    /// Type to which the value should be coerced.
    /// </param>
    /// <param name="valueType">
    /// Original type of the value.
    /// </param>
    /// <param name="oldValue">
    /// The previous value (if any) being replaced by
    /// the new coerced value.
    /// </param>
    /// <param name="value">
    /// The value to coerce.
    /// </param>
    /// <remarks>
    /// <para>
    /// If the desired type is a primitive type or Decimal, 
    /// empty string and null values will result in a 0 
    /// or equivalent.
    /// </para>
    /// <para>
    /// If the desired type is a Nullable type, empty string
    /// and null values will result in a null result.
    /// </para>
    /// <para>
    /// If the desired type is an enum the value's ToString()
    /// result is parsed to convert into the enum value.
    /// </para>
    /// </remarks>
    public static object CoerceValue(Type desiredType, Type valueType, object oldValue, object value)
    {
      if (desiredType.Equals(valueType))
      {
        // types match, just return value
        return value;
      }
      else
      {
        if (desiredType.IsGenericType)
        {
          if (desiredType.GetGenericTypeDefinition() == typeof(Nullable<>))
            if (value == null)
              return null;
            else if (valueType.Equals(typeof(string)) && System.Convert.ToString(value) == string.Empty)
              return null;
        }
        desiredType = Utilities.GetPropertyType(desiredType);
      }

      if (desiredType.IsEnum && (valueType.Equals(typeof(string)) || Enum.GetUnderlyingType(desiredType).Equals(valueType)))
        return System.Enum.Parse(desiredType, value.ToString());

      if (desiredType.Equals(typeof(SmartDate)) && oldValue != null)
      {
        if (value == null)
          value = string.Empty;
        var tmp = (SmartDate)oldValue;
        if (valueType.Equals(typeof(DateTime)))
          tmp.Date = (DateTime)value;
        else
          tmp.Text = value.ToString();
        return tmp;
      }

      if ((desiredType.IsPrimitive || desiredType.Equals(typeof(decimal))) &&
          valueType.Equals(typeof(string)) && string.IsNullOrEmpty((string)value))
        value = 0;

      try
      {
        if (desiredType.Equals(typeof(string)) && value != null)
        {
          return value.ToString();
        }
        else
          return Convert.ChangeType(value, desiredType);
      }
      catch
      {
        TypeConverter cnv = TypeDescriptor.GetConverter(desiredType);
        if (cnv != null && cnv.CanConvertFrom(valueType))
          return cnv.ConvertFrom(value);
        else
          throw;
      }
    }


    /// <summary>
    /// Attempts to coerce a value of one type into
    /// a value of a different type.
    /// </summary>
    /// <typeparam name="D">
    /// Type to which the value should be coerced.
    /// </typeparam>
    /// <param name="valueType">
    /// Original type of the value.
    /// </param>
    /// <param name="oldValue">
    /// The previous value (if any) being replaced by
    /// the new coerced value.
    /// </param>
    /// <param name="value">
    /// The value to coerce.
    /// </param>
    /// <remarks>
    /// <para>
    /// If the desired type is a primitive type or Decimal, 
    /// empty string and null values will result in a 0 
    /// or equivalent.
    /// </para>
    /// <para>
    /// If the desired type is a Nullable type, empty string
    /// and null values will result in a null result.
    /// </para>
    /// <para>
    /// If the desired type is an enum the value's ToString()
    /// result is parsed to convert into the enum value.
    /// </para>
    /// </remarks>
    public static D CoerceValue<D>(Type valueType, object oldValue, object value)
    {
      return (D)(CoerceValue(typeof(D), valueType, oldValue, value));
    }

    #endregion

    #region Serialization

    internal static string XmlSerialize(object graph)
    {
      using (var buffer = new MemoryStream())
      {
        XmlWriter writer = XmlWriter.Create(buffer);
        DataContractSerializer dcs = new DataContractSerializer(graph.GetType());
        dcs.WriteObject(writer, graph);
        writer.Flush();
        byte[] data = buffer.ToArray();
        return Encoding.UTF8.GetString(data, 0, data.Length);
      }

    }

    internal static T XmlDeserialize<T>(string xml)
    {
      using (var buffer = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
      {
        XmlReader reader = XmlReader.Create(buffer);
        DataContractSerializer dcs = new DataContractSerializer(typeof(T));
        return (T)dcs.ReadObject(reader);
      }
    }
    #endregion
  }

  /// <summary>
  /// Valid options for calling a property or method
  /// via the <see cref="Csla.Utilities.CallByName"/> method.
  /// </summary>
  public enum CallType
  {
    /// <summary>
    /// Gets a value from a property.
    /// </summary>
    Get,
    /// <summary>
    /// Sets a value into a property.
    /// </summary>
    Let,
    /// <summary>
    /// Invokes a method.
    /// </summary>
    Method,
    /// <summary>
    /// Sets a value into a property.
    /// </summary>
    Set
  }
}
