namespace KBEngine;

public class DATATYPE_PYTHON : DATATYPE_BASE
{
	public override object createFromStream(MemoryStream stream)
	{
		return stream.readBlob();
	}

	public override void addToStream(Bundle stream, object v)
	{
		stream.writeBlob((byte[])v);
	}

	public override object parseDefaultValStr(string v)
	{
		return new byte[0];
	}

	public override bool isSameType(object v)
	{
		if (v != null)
		{
			return v.GetType() == typeof(byte[]);
		}
		return false;
	}
}
