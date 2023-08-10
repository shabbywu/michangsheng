using UnityEngine;

namespace KBEngine;

public class DATATYPE_VECTOR3 : DATATYPE_BASE
{
	public override object createFromStream(MemoryStream stream)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		return (object)new Vector3(stream.readFloat(), stream.readFloat(), stream.readFloat());
	}

	public override void addToStream(Bundle stream, object v)
	{
		stream.writeFloat(((Vector3)v).x);
		stream.writeFloat(((Vector3)v).y);
		stream.writeFloat(((Vector3)v).z);
	}

	public override object parseDefaultValStr(string v)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		return (object)new Vector3(0f, 0f, 0f);
	}

	public override bool isSameType(object v)
	{
		if (v != null)
		{
			return v.GetType() == typeof(Vector3);
		}
		return false;
	}
}
