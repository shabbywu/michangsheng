using System;

namespace KBEngine;

public class DATATYPE_UINT32 : DATATYPE_BASE
{
	public override object createFromStream(MemoryStream stream)
	{
		return stream.readUint32();
	}

	public override void addToStream(Bundle stream, object v)
	{
		stream.writeUint32(Convert.ToUInt32(v));
	}

	public override object parseDefaultValStr(string v)
	{
		uint result = 0u;
		uint.TryParse(v, out result);
		return result;
	}

	public override bool isSameType(object v)
	{
		if (!KBEMath.isNumeric(v))
		{
			return false;
		}
		decimal num = Convert.ToDecimal(v);
		if (num >= 0m)
		{
			return num <= 4294967295m;
		}
		return false;
	}
}
