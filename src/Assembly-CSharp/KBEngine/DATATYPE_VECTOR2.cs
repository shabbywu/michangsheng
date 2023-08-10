using UnityEngine;

namespace KBEngine;

public class DATATYPE_VECTOR2 : DATATYPE_BASE
{
	public override object createFromStream(MemoryStream stream)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		return (object)new Vector2(stream.readFloat(), stream.readFloat());
	}

	public override void addToStream(Bundle stream, object v)
	{
		stream.writeFloat(((Vector2)v).x);
		stream.writeFloat(((Vector2)v).y);
	}

	public override object parseDefaultValStr(string v)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return (object)new Vector2(0f, 0f);
	}

	public override bool isSameType(object v)
	{
		if (v != null)
		{
			return v.GetType() == typeof(Vector2);
		}
		return false;
	}
}
