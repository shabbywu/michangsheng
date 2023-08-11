using System;

namespace KBEngine;

public class DATATYPE_INT64 : DATATYPE_BASE
{
	public override object createFromStream(MemoryStream stream)
	{
		return stream.readInt64();
	}

	public override void addToStream(Bundle stream, object v)
	{
		stream.writeInt64(Convert.ToInt64(v));
	}

	public override object parseDefaultValStr(string v)
	{
		long result = 0L;
		long.TryParse(v, out result);
		return result;
	}

	public override bool isSameType(object v)
	{
		if (!KBEMath.isNumeric(v))
		{
			return false;
		}
		decimal num = Convert.ToDecimal(v);
		if (num >= -9223372036854775808m)
		{
			return num <= 9223372036854775807m;
		}
		return false;
	}
}
