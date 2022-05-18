using System;
using System.Collections.Generic;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x020010E5 RID: 4325
	public class CustomConvertersCollection
	{
		// Token: 0x06006877 RID: 26743 RVA: 0x0028B4E4 File Offset: 0x002896E4
		internal CustomConvertersCollection()
		{
			for (int i = 0; i < this.m_Script2Clr.Length; i++)
			{
				this.m_Script2Clr[i] = new Dictionary<Type, Func<DynValue, object>>();
			}
		}

		// Token: 0x06006878 RID: 26744 RVA: 0x0028B530 File Offset: 0x00289730
		public void SetScriptToClrCustomConversion(DataType scriptDataType, Type clrDataType, Func<DynValue, object> converter = null)
		{
			if (scriptDataType > (DataType)this.m_Script2Clr.Length)
			{
				throw new ArgumentException("scriptDataType");
			}
			Dictionary<Type, Func<DynValue, object>> dictionary = this.m_Script2Clr[(int)scriptDataType];
			if (converter == null)
			{
				if (dictionary.ContainsKey(clrDataType))
				{
					dictionary.Remove(clrDataType);
					return;
				}
			}
			else
			{
				dictionary[clrDataType] = converter;
			}
		}

		// Token: 0x06006879 RID: 26745 RVA: 0x00047B60 File Offset: 0x00045D60
		public Func<DynValue, object> GetScriptToClrCustomConversion(DataType scriptDataType, Type clrDataType)
		{
			if (scriptDataType > (DataType)this.m_Script2Clr.Length)
			{
				return null;
			}
			return this.m_Script2Clr[(int)scriptDataType].GetOrDefault(clrDataType);
		}

		// Token: 0x0600687A RID: 26746 RVA: 0x00047B7D File Offset: 0x00045D7D
		public void SetClrToScriptCustomConversion(Type clrDataType, Func<Script, object, DynValue> converter = null)
		{
			if (converter == null)
			{
				if (this.m_Clr2Script.ContainsKey(clrDataType))
				{
					this.m_Clr2Script.Remove(clrDataType);
					return;
				}
			}
			else
			{
				this.m_Clr2Script[clrDataType] = converter;
			}
		}

		// Token: 0x0600687B RID: 26747 RVA: 0x0028B57C File Offset: 0x0028977C
		public void SetClrToScriptCustomConversion<T>(Func<Script, T, DynValue> converter = null)
		{
			this.SetClrToScriptCustomConversion(typeof(T), (Script s, object o) => converter(s, (T)((object)o)));
		}

		// Token: 0x0600687C RID: 26748 RVA: 0x00047BAB File Offset: 0x00045DAB
		public Func<Script, object, DynValue> GetClrToScriptCustomConversion(Type clrDataType)
		{
			return this.m_Clr2Script.GetOrDefault(clrDataType);
		}

		// Token: 0x0600687D RID: 26749 RVA: 0x0028B5B4 File Offset: 0x002897B4
		[Obsolete("This method is deprecated. Use the overloads accepting functions with a Script argument.")]
		public void SetClrToScriptCustomConversion(Type clrDataType, Func<object, DynValue> converter = null)
		{
			this.SetClrToScriptCustomConversion(clrDataType, (Script s, object o) => converter(o));
		}

		// Token: 0x0600687E RID: 26750 RVA: 0x0028B5E4 File Offset: 0x002897E4
		[Obsolete("This method is deprecated. Use the overloads accepting functions with a Script argument.")]
		public void SetClrToScriptCustomConversion<T>(Func<T, DynValue> converter = null)
		{
			this.SetClrToScriptCustomConversion(typeof(T), (object o) => converter((T)((object)o)));
		}

		// Token: 0x0600687F RID: 26751 RVA: 0x0028B61C File Offset: 0x0028981C
		public void Clear()
		{
			this.m_Clr2Script.Clear();
			for (int i = 0; i < this.m_Script2Clr.Length; i++)
			{
				this.m_Script2Clr[i].Clear();
			}
		}

		// Token: 0x04005FE5 RID: 24549
		private Dictionary<Type, Func<DynValue, object>>[] m_Script2Clr = new Dictionary<Type, Func<DynValue, object>>[11];

		// Token: 0x04005FE6 RID: 24550
		private Dictionary<Type, Func<Script, object, DynValue>> m_Clr2Script = new Dictionary<Type, Func<Script, object, DynValue>>();
	}
}
