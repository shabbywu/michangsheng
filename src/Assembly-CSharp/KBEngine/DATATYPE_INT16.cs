using System;

namespace KBEngine;

public class DATATYPE_INT16 : DATATYPE_BASE
{
	public override object createFromStream(MemoryStream stream)
	{
		return stream.readInt16();
	}

	public override void addToStream(Bundle stream, object v)
	{
		stream.writeInt16(Convert.ToInt16(v));
	}

	public override object parseDefaultValStr(string v)
	{
		short result = 0;
		short.TryParse(v, out result);
		return result;
	}

	public override bool isSameType(object v)
	{
		if (!KBEMath.isNumeric(v))
		{
			return false;
		}
		decimal num = Convert.ToDecimal(v);
		if (num >= -32768m)
		{
			return num <= 32767m;
		}
		return false;
	}
}
