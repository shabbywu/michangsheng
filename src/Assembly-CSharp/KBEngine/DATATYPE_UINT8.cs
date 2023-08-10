using System;

namespace KBEngine;

public class DATATYPE_UINT8 : DATATYPE_BASE
{
	public override object createFromStream(MemoryStream stream)
	{
		return stream.readUint8();
	}

	public override void addToStream(Bundle stream, object v)
	{
		stream.writeUint8(Convert.ToByte(v));
	}

	public override object parseDefaultValStr(string v)
	{
		byte result = 0;
		byte.TryParse(v, out result);
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
			return num <= 255m;
		}
		return false;
	}
}
