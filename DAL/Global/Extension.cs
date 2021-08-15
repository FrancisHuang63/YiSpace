using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#region Enum擴充

[AttributeUsage(AttributeTargets.Field)]
public class DescriptionAttribute : Attribute
{
    public string Description { get; set; }

    public DescriptionAttribute(string description)
    {
        Description = description;
    }
}

[AttributeUsage(AttributeTargets.Field)]
public class SortAttribute : Attribute
{
    public double Sort { get; set; }

    public SortAttribute(double sort)
    {
        Sort = sort;
    }
}

public static class EnumExt
{
    private static Dictionary<Enum, string> dic = new Dictionary<Enum, string>();
    public static string Description(this Enum item)
    {
        try
        {
            string text;
            if (!dic.TryGetValue(item, out text))
            {
                System.Reflection.FieldInfo fieldInfo = item.GetType().GetField(item.ToString());
                object[] attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs?.Length > 0)
                {
                    DescriptionAttribute attr = attrs[0] as DescriptionAttribute;
                    if (attr != null)
                        text = attr.Description;
                }

                if (string.IsNullOrWhiteSpace(text))
                    text = item.ToString();

                dic[item] = text;
            }

            return text;
        }
        catch (Exception ex)
        {
            return item.ToString();
        }
    }

    public static double Sort(this Enum item)
    {
        try
        {
            System.Reflection.FieldInfo fieldInfo = item.GetType().GetField(item.ToString());
            object[] attrs = fieldInfo.GetCustomAttributes(typeof(SortAttribute), true);
            if (attrs?.Length > 0)
            {
                SortAttribute attr = attrs[0] as SortAttribute;
                if (attr != null)
                    return attr.Sort;
            }

            return Convert.ToInt32(item);
        }
        catch
        {
            return Convert.ToInt32(item);
        }
    }

    /// <summary>
    /// 取得列舉內所有項目
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public static IEnumerable<string> GetItems(this Enum item)
    {
        List<string> lstItem = new List<string>();
        try
        {
            foreach (string name in Enum.GetNames(item.GetType()))
            {
                lstItem.Add(name);
            }

            return lstItem;
        }
        catch
        {
            return lstItem;
        }
    }

    /// <summary>
    /// 從列舉描述取得列舉物件
    /// </summary>
    /// <typeparam name="T">列舉型別</typeparam>
    /// <param name="description">描述</param>
    /// <returns></returns>
    public static T GetValueFromDescription<T>(string description)
    {
        var type = typeof(T);
        if (!type.IsEnum) throw new InvalidOperationException();
        foreach (var field in type.GetFields())
        {
            var attribute = Attribute.GetCustomAttribute(field,
                typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (attribute != null)
            {
                if (attribute.Description == description)
                    return (T)field.GetValue(null);
            }
            else
            {
                if (field.Name == description)
                    return (T)field.GetValue(null);
            }
        }
        throw new ArgumentException("Not found.", nameof(description));
        // or return default(T);
    }
}

#endregion

#region DateTime擴充

public static class DateTimeExt
{
    public static string ToString(this DateTime? date, string format)
    {
        if (date == null)
            return string.Empty;
        return ((DateTime)date).ToString(format);
    }
}

#endregion