using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000B65 RID: 2917
	public class DATATYPE_FIXED_DICT : DATATYPE_BASE
	{
		// Token: 0x0600516A RID: 20842 RVA: 0x00221D68 File Offset: 0x0021FF68
		public override void bind()
		{
			string[] array = new string[this.dicttype.Keys.Count];
			this.dicttype.Keys.CopyTo(array, 0);
			foreach (string key in array)
			{
				object obj = this.dicttype[key];
				if (obj.GetType().BaseType.ToString() == "KBEngine.DATATYPE_BASE")
				{
					((DATATYPE_BASE)obj).bind();
				}
				else if (EntityDef.id2datatypes.ContainsKey((ushort)obj))
				{
					this.dicttype[key] = EntityDef.id2datatypes[(ushort)obj];
				}
			}
		}

		// Token: 0x0600516B RID: 20843 RVA: 0x00221E1C File Offset: 0x0022001C
		public override object createFromStream(MemoryStream stream)
		{
			Dictionary<string, object> result = new Dictionary<string, object>();
			foreach (string text in this.dicttype.Keys)
			{
			}
			return result;
		}

		// Token: 0x0600516C RID: 20844 RVA: 0x00221E74 File Offset: 0x00220074
		public override void addToStream(Bundle stream, object v)
		{
			foreach (string text in this.dicttype.Keys)
			{
			}
		}

		// Token: 0x0600516D RID: 20845 RVA: 0x00221EC8 File Offset: 0x002200C8
		public override object parseDefaultValStr(string v)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			foreach (string key in this.dicttype.Keys)
			{
				dictionary[key] = ((DATATYPE_BASE)this.dicttype[key]).parseDefaultValStr("");
			}
			return dictionary;
		}

		// Token: 0x0600516E RID: 20846 RVA: 0x00221F44 File Offset: 0x00220144
		public override bool isSameType(object v)
		{
			if (v == null || v.GetType() != typeof(Dictionary<string, object>))
			{
				return false;
			}
			foreach (KeyValuePair<string, object> keyValuePair in this.dicttype)
			{
				object v2;
				if (!((Dictionary<string, object>)v).TryGetValue(keyValuePair.Key, out v2))
				{
					return false;
				}
				if (!((DATATYPE_BASE)keyValuePair.Value).isSameType(v2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04004F82 RID: 20354
		public string implementedBy = "";

		// Token: 0x04004F83 RID: 20355
		public Dictionary<string, object> dicttype = new Dictionary<string, object>();
	}
}
