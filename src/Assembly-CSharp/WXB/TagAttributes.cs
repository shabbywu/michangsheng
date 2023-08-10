using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace WXB;

public class TagAttributes
{
	private Dictionary<string, string> d_attrs = new Dictionary<string, string>();

	public override string ToString()
	{
		PD<StringBuilder> sB = Pool.GetSB();
		try
		{
			StringBuilder value = sB.value;
			value.AppendFormat("count:{0}", d_attrs.Count);
			value.AppendLine();
			foreach (KeyValuePair<string, string> d_attr in d_attrs)
			{
				value.AppendLine("key:{0} value:{1}", d_attr.Key, d_attr.Value);
			}
			string result = value.ToString();
			value.Length = 0;
			return result;
		}
		finally
		{
			((IDisposable)sB).Dispose();
		}
	}

	public void Release()
	{
		d_attrs.Clear();
	}

	public void Add(string text)
	{
		int i = 0;
		int length = text.Length;
		string key = string.Empty;
		string empty = string.Empty;
		int num = 0;
		bool flag = false;
		for (; i < length; i++)
		{
			if (text[i] == '=')
			{
				key = text.Substring(num, i - num);
				num = i + 1;
				flag = true;
			}
			else if (text[i] == ' ')
			{
				if (!flag)
				{
					Debug.LogErrorFormat("error param!", Array.Empty<object>());
					return;
				}
				empty = text.Substring(num, i - num);
				num = i + 1;
				d_attrs[key] = empty;
				flag = false;
			}
		}
		if (flag)
		{
			empty = text.Substring(num);
			d_attrs[key] = empty;
		}
	}

	public void add(string attrName, string attrValue)
	{
		d_attrs[attrName] = attrValue;
	}

	public void remove(ref string attrName)
	{
		d_attrs.Remove(attrName);
	}

	public bool exists(string attrName)
	{
		return d_attrs.ContainsKey(attrName);
	}

	public int getCount()
	{
		return d_attrs.Count;
	}

	public string getValue(string attrName)
	{
		string value = "";
		d_attrs.TryGetValue(attrName, out value);
		return value;
	}

	public string getValueAsString(string attrName)
	{
		string value = "";
		if (!d_attrs.TryGetValue(attrName, out value))
		{
			return "";
		}
		return value;
	}

	public Color getValueAsColor(string attrName, Color color)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		string value = "";
		if (!d_attrs.TryGetValue(attrName, out value))
		{
			return color;
		}
		return Tools.ParseColor(value, 0, Color.white);
	}

	public string getValueAsString(string attrName, string def)
	{
		string value = "";
		if (!d_attrs.TryGetValue(attrName, out value))
		{
			return def;
		}
		return value;
	}

	public bool getValueAsBool(string attrName, bool def)
	{
		string value = "";
		if (!d_attrs.TryGetValue(attrName, out value))
		{
			return def;
		}
		bool result = def;
		if (!bool.TryParse(value, out result))
		{
			int result2 = 0;
			if (int.TryParse(value, out result2))
			{
				if (result2 != 0)
				{
					return true;
				}
				return false;
			}
			return def;
		}
		return result;
	}

	public int getValueAsInteger(string attrName, int def)
	{
		string value = "";
		if (!d_attrs.TryGetValue(attrName, out value))
		{
			return def;
		}
		int result = def;
		if (!int.TryParse(value, out result))
		{
			return def;
		}
		return result;
	}

	public float getValueAsFloat(string attrName, float def)
	{
		string value = "";
		if (!d_attrs.TryGetValue(attrName, out value))
		{
			return def;
		}
		float result = def;
		if (!float.TryParse(value, out result))
		{
			return def;
		}
		return result;
	}
}
