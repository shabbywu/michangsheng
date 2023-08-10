using System.Collections.Generic;

namespace KBEngine;

public class DATATYPE_FIXED_DICT : DATATYPE_BASE
{
	public string implementedBy = "";

	public Dictionary<string, object> dicttype = new Dictionary<string, object>();

	public override void bind()
	{
		string[] array = new string[dicttype.Keys.Count];
		dicttype.Keys.CopyTo(array, 0);
		string[] array2 = array;
		foreach (string key in array2)
		{
			object obj = dicttype[key];
			if (obj.GetType().BaseType.ToString() == "KBEngine.DATATYPE_BASE")
			{
				((DATATYPE_BASE)obj).bind();
			}
			else if (EntityDef.id2datatypes.ContainsKey((ushort)obj))
			{
				dicttype[key] = EntityDef.id2datatypes[(ushort)obj];
			}
		}
	}

	public override object createFromStream(MemoryStream stream)
	{
		Dictionary<string, object> result = new Dictionary<string, object>();
		foreach (string key in dicttype.Keys)
		{
			_ = key;
		}
		return result;
	}

	public override void addToStream(Bundle stream, object v)
	{
		foreach (string key in dicttype.Keys)
		{
			_ = key;
		}
	}

	public override object parseDefaultValStr(string v)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		foreach (string key in dicttype.Keys)
		{
			dictionary[key] = ((DATATYPE_BASE)dicttype[key]).parseDefaultValStr("");
		}
		return dictionary;
	}

	public override bool isSameType(object v)
	{
		if (v == null || v.GetType() != typeof(Dictionary<string, object>))
		{
			return false;
		}
		foreach (KeyValuePair<string, object> item in dicttype)
		{
			if (((Dictionary<string, object>)v).TryGetValue(item.Key, out var value))
			{
				if (!((DATATYPE_BASE)item.Value).isSameType(value))
				{
					return false;
				}
				continue;
			}
			return false;
		}
		return true;
	}
}
