using System.Text;

namespace KBEngine;

public class DATATYPE_UNICODE : DATATYPE_BASE
{
	public override object createFromStream(MemoryStream stream)
	{
		return Encoding.UTF8.GetString(stream.readBlob());
	}

	public override void addToStream(Bundle stream, object v)
	{
		stream.writeBlob(Encoding.UTF8.GetBytes((string)v));
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
