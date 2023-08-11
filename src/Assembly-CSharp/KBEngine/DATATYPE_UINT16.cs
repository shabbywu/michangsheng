using System;

namespace KBEngine;

public class DATATYPE_UINT16 : DATATYPE_BASE
{
	public override object createFromStream(MemoryStream stream)
	{
		return stream.readUint16();
	}

	public override void addToStream(Bundle stream, object v)
	{
		stream.writeUint16(Convert.ToUInt16(v));
	}

	public override object parseDefaultValStr(string v)
	{
		ushort result = 0;
		ushort.TryParse(v, out result);
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
			return num <= 65535m;
		}
		return false;
	}
}
