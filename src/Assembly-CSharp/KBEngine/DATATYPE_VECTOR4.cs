using UnityEngine;

namespace KBEngine;

public class DATATYPE_VECTOR4 : DATATYPE_BASE
{
	public override object createFromStream(MemoryStream stream)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		return (object)new Vector4(stream.readFloat(), stream.readFloat(), stream.readFloat(), stream.readFloat());
	}

	public override void addToStream(Bundle stream, object v)
	{
		stream.writeFloat(((Vector4)v).x);
		stream.writeFloat(((Vector4)v).y);
		stream.writeFloat(((Vector4)v).z);
		stream.writeFloat(((Vector4)v).w);
	}

	public override object parseDefaultValStr(string v)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		return (object)new Vector4(0f, 0f, 0f, 0f);
	}

	public override bool isSameType(object v)
	{
		if (v != null)
		{
			return v.GetType() == typeof(Vector4);
		}
		return false;
	}
}
