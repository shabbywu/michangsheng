using System.Collections.Generic;

namespace KBEngine;

public class DATATYPE_ARRAY : DATATYPE_BASE
{
	public object vtype;

	public override void bind()
	{
		if (vtype.GetType().BaseType.ToString() == "KBEngine.DATATYPE_BASE")
		{
			((DATATYPE_BASE)vtype).bind();
		}
		else if (EntityDef.id2datatypes.ContainsKey((ushort)vtype))
		{
			vtype = EntityDef.id2datatypes[(ushort)vtype];
		}
	}

	public override object createFromStream(MemoryStream stream)
	{
		uint num = stream.readUint32();
		List<object> result = new List<object>();
		while (num != 0)
		{
			num--;
		}
		return result;
	}

	public override void addToStream(Bundle stream, object v)
	{
		stream.writeUint32((uint)((List<object>)v).Count);
		for (int i = 0; i < ((List<object>)v).Count; i++)
		{
		}
	}

	public override object parseDefaultValStr(string v)
	{
		return new byte[0];
	}

	public override bool isSameType(object v)
	{
		if (vtype.GetType().BaseType.ToString() != "KBEngine.DATATYPE_BASE")
		{
			Dbg.ERROR_MSG($"DATATYPE_ARRAY::isSameType: has not bind! baseType={vtype.GetType().BaseType.ToString()}");
			return false;
		}
		if (v == null || v.GetType() != typeof(List<object>))
		{
			return false;
		}
		for (int i = 0; i < ((List<object>)v).Count; i++)
		{
			if (!((DATATYPE_BASE)vtype).isSameType(((List<object>)v)[i]))
			{
				return false;
			}
		}
		return true;
	}
}
