using System;

namespace KBEngine;

public class DATATYPE_INT32 : DATATYPE_BASE
{
	public override object createFromStream(MemoryStream stream)
	{
		return stream.readInt32();
	}

	public override void addToStream(Bundle stream, object v)
	{
		stream.writeInt32(Convert.ToInt32(v));
	}

	public override object parseDefaultValStr(string v)
	{
		int result = 0;
		int.TryParse(v, out result);
		return result;
	}

	public override bool isSameType(object v)
	{
		if (!KBEMath.isNumeric(v))
		{
			return false;
		}
		decimal num = Convert.ToDecimal(v);
		if (num >= -2147483648m)
		{
			return num <= 2147483647m;
		}
		return false;
	}
}
