using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000EE1 RID: 3809
	public class DATATYPE_ARRAY : DATATYPE_BASE
	{
		// Token: 0x06005BA0 RID: 23456 RVA: 0x00250EC4 File Offset: 0x0024F0C4
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

		// Token: 0x06005BA1 RID: 23457 RVA: 0x00250F38 File Offset: 0x0024F138
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

		// Token: 0x06005BA2 RID: 23458 RVA: 0x00250F60 File Offset: 0x0024F160
		public override void addToStream(Bundle stream, object v)
		{
			stream.writeUint32((uint)((List<object>)v).Count);
			for (int i = 0; i < ((List<object>)v).Count; i++)
			{
			}
		}

		// Token: 0x06005BA3 RID: 23459 RVA: 0x0004082D File Offset: 0x0003EA2D
		public override object parseDefaultValStr(string v)
		{
			return new byte[0];
		}

		// Token: 0x06005BA4 RID: 23460 RVA: 0x00250F94 File Offset: 0x0024F194
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

		// Token: 0x04005A0C RID: 23052
		public object vtype;
	}
}
