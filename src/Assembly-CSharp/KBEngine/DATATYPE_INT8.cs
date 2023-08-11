using System;

namespace KBEngine;

public class DATATYPE_INT8 : DATATYPE_BASE
{
	public override object createFromStream(MemoryStream stream)
	{
		return stream.readInt8();
	}

	public override void addToStream(Bundle stream, object v)
	{
		stream.writeInt8(Convert.ToSByte(v));
	}

	public override object parseDefaultValStr(string v)
	{
		sbyte result = 0;
		sbyte.TryParse(v, out result);
		return result;
	}

	public override bool isSameType(object v)
	{
		if (!KBEMath.isNumeric(v))
		{
			return false;
		}
		decimal num = Convert.ToDecimal(v);
		if (num >= -128m)
		{
			return num <= 127m;
		}
		return false;
	}
}
