using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000B64 RID: 2916
	public class DATATYPE_ARRAY : DATATYPE_BASE
	{
		// Token: 0x06005164 RID: 20836 RVA: 0x00221BF0 File Offset: 0x0021FDF0
		public override void bind()
		{
			if (this.vtype.GetType().BaseType.ToString() == "KBEngine.DATATYPE_BASE")
			{
				((DATATYPE_BASE)this.vtype).bind();
				return;
			}
			if (EntityDef.id2datatypes.ContainsKey((ushort)this.vtype))
			{
				this.vtype = EntityDef.id2datatypes[(ushort)this.vtype];
			}
		}

		// Token: 0x06005165 RID: 20837 RVA: 0x00221C64 File Offset: 0x0021FE64
		public override object createFromStream(MemoryStream stream)
		{
			uint num = stream.readUint32();
			List<object> result = new List<object>();
			while (num > 0U)
			{
				num -= 1U;
			}
			return result;
		}

		// Token: 0x06005166 RID: 20838 RVA: 0x00221C8C File Offset: 0x0021FE8C
		public override void addToStream(Bundle stream, object v)
		{
			stream.writeUint32((uint)((List<object>)v).Count);
			for (int i = 0; i < ((List<object>)v).Count; i++)
			{
			}
		}

		// Token: 0x06005167 RID: 20839 RVA: 0x00221B9F File Offset: 0x0021FD9F
		public override object parseDefaultValStr(string v)
		{
			return new byte[0];
		}

		// Token: 0x06005168 RID: 20840 RVA: 0x00221CC0 File Offset: 0x0021FEC0
		public override bool isSameType(object v)
		{
			if (this.vtype.GetType().BaseType.ToString() != "KBEngine.DATATYPE_BASE")
			{
				Dbg.ERROR_MSG(string.Format("DATATYPE_ARRAY::isSameType: has not bind! baseType={0}", this.vtype.GetType().BaseType.ToString()));
				return false;
			}
			if (v == null || v.GetType() != typeof(List<object>))
			{
				return false;
			}
			for (int i = 0; i < ((List<object>)v).Count; i++)
			{
				if (!((DATATYPE_BASE)this.vtype).isSameType(((List<object>)v)[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04004F81 RID: 20353
		public object vtype;
	}
}
