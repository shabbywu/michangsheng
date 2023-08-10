using System;

namespace KBEngine;

public class DATATYPE_STRING : DATATYPE_BASE
{
	public override object createFromStream(MemoryStream stream)
	{
		return stream.readString();
	}

	public override void addToStream(Bundle stream, object v)
	{
		stream.writeString(Convert.ToString(v));
	}

	public override object parseDefaultValStr(string v)
	{
		return v;
	}

	public override bool isSameType(object v)
	{
		if (v != null)
		{
			return v.GetType() == typeof(string);
		}
		return false;
	}
}
