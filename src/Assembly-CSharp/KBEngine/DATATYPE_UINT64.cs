using System;

namespace KBEngine;

public class DATATYPE_UINT64 : DATATYPE_BASE
{
	public override object createFromStream(MemoryStream stream)
	{
		return stream.readUint64();
	}

	public override void addToStream(Bundle stream, object v)
	{
		stream.writeUint64(Convert.ToUInt64(v));
	}

	public override object parseDefaultValStr(string v)
	{
		ulong result = 0uL;
		ulong.TryParse(v, out result);
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
			return num <= 18446744073709551615m;
		}
		return false;
	}
}
