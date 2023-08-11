using System;

namespace KBEngine;

public class DATATYPE_DOUBLE : DATATYPE_BASE
{
	public override object createFromStream(MemoryStream stream)
	{
		return stream.readDouble();
	}

	public override void addToStream(Bundle stream, object v)
	{
		stream.writeDouble(Convert.ToDouble(v));
	}

	public override object parseDefaultValStr(string v)
	{
		double result = 0.0;
		double.TryParse(v, out result);
		return result;
	}

	public override bool isSameType(object v)
	{
		if (v is float)
		{
			if ((double)(float)v >= double.MinValue)
			{
				return (double)(float)v <= double.MaxValue;
			}
			return false;
		}
		if (v is double)
		{
			if ((double)v >= double.MinValue)
			{
				return (double)v <= double.MaxValue;
			}
			return false;
		}
		return false;
	}
}
