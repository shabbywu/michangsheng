using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000EE2 RID: 3810
	public class DATATYPE_FIXED_DICT : DATATYPE_BASE
	{
		// Token: 0x06005BA6 RID: 23462 RVA: 0x0025103C File Offset: 0x0024F23C
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

		// Token: 0x06005BA7 RID: 23463 RVA: 0x002510F0 File Offset: 0x0024F2F0
		public override object createFromStream(MemoryStream stream)
		{
			Dictionary<string, object> result = new Dictionary<string, object>();
			foreach (string text in this.dicttype.Keys)
			{
			}
			return result;
		}

		// Token: 0x06005BA8 RID: 23464 RVA: 0x00251148 File Offset: 0x0024F348
		public override void addToStream(Bundle stream, object v)
		{
			foreach (string text in this.dicttype.Keys)
			{
			}
		}

		// Token: 0x06005BA9 RID: 23465 RVA: 0x0025119C File Offset: 0x0024F39C
		public override object parseDefaultValStr(string v)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			foreach (string key in this.dicttype.Keys)
			{
				dictionary[key] = ((DATATYPE_BASE)this.dicttype[key]).parseDefaultValStr("");
			}
			return dictionary;
		}

		// Token: 0x06005BAA RID: 23466 RVA: 0x00251218 File Offset: 0x0024F418
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

		// Token: 0x04005A0D RID: 23053
		public string implementedBy = "";

		// Token: 0x04005A0E RID: 23054
		public Dictionary<string, object> dicttype = new Dictionary<string, object>();
	}
}
