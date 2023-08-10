using System;

namespace KBEngine;

public class DATATYPE_FLOAT : DATATYPE_BASE
{
	public override object createFromStream(MemoryStream stream)
	{
		return stream.readFloat();
	}

	public override void addToStream(Bundle stream, object v)
	{
		stream.writeFloat((float)Convert.ToDouble(v));
	}

	public override object parseDefaultValStr(string v)
	{
		float result = 0f;
		float.TryParse(v, out result);
		return result;
	}

	public override bool isSameType(object v)
	{
		if (v is float)
		{
			if ((float)v >= float.MinValue)
			{
				return (float)v <= float.MaxValue;
			}
			return false;
		}
		if (v is double)
		{
			if ((double)v >= -3.4028234663852886E+38)
			{
				return (double)v <= 3.4028234663852886E+38;
			}
			return false;
		}
		return false;
	}
}
