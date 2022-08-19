using System;
using System.Collections.Generic;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000D06 RID: 3334
	public class CustomConvertersCollection
	{
		// Token: 0x06005D59 RID: 23897 RVA: 0x002627D8 File Offset: 0x002609D8
		internal CustomConvertersCollection()
		{
			for (int i = 0; i < this.m_Script2Clr.Length; i++)
			{
				this.m_Script2Clr[i] = new Dictionary<Type, Func<DynValue, object>>();
			}
		}

		// Token: 0x06005D5A RID: 23898 RVA: 0x00262824 File Offset: 0x00260A24
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

		// Token: 0x06005D5B RID: 23899 RVA: 0x0026286D File Offset: 0x00260A6D
		public Func<DynValue, object> GetScriptToClrCustomConversion(DataType scriptDataType, Type clrDataType)
		{
			if (scriptDataType > (DataType)this.m_Script2Clr.Length)
			{
				return null;
			}
			return this.m_Script2Clr[(int)scriptDataType].GetOrDefault(clrDataType);
		}

		// Token: 0x06005D5C RID: 23900 RVA: 0x0026288A File Offset: 0x00260A8A
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

		// Token: 0x06005D5D RID: 23901 RVA: 0x002628B8 File Offset: 0x00260AB8
		public void SetClrToScriptCustomConversion<T>(Func<Script, T, DynValue> converter = null)
		{
			this.SetClrToScriptCustomConversion(typeof(T), (Script s, object o) => converter(s, (T)((object)o)));
		}

		// Token: 0x06005D5E RID: 23902 RVA: 0x002628EE File Offset: 0x00260AEE
		public Func<Script, object, DynValue> GetClrToScriptCustomConversion(Type clrDataType)
		{
			return this.m_Clr2Script.GetOrDefault(clrDataType);
		}

		// Token: 0x06005D5F RID: 23903 RVA: 0x002628FC File Offset: 0x00260AFC
		[Obsolete("This method is deprecated. Use the overloads accepting functions with a Script argument.")]
		public void SetClrToScriptCustomConversion(Type clrDataType, Func<object, DynValue> converter = null)
		{
			this.SetClrToScriptCustomConversion(clrDataType, (Script s, object o) => converter(o));
		}

		// Token: 0x06005D60 RID: 23904 RVA: 0x0026292C File Offset: 0x00260B2C
		[Obsolete("This method is deprecated. Use the overloads accepting functions with a Script argument.")]
		public void SetClrToScriptCustomConversion<T>(Func<T, DynValue> converter = null)
		{
			this.SetClrToScriptCustomConversion(typeof(T), (object o) => converter((T)((object)o)));
		}

		// Token: 0x06005D61 RID: 23905 RVA: 0x00262964 File Offset: 0x00260B64
		public void Clear()
		{
			this.m_Clr2Script.Clear();
			for (int i = 0; i < this.m_Script2Clr.Length; i++)
			{
				this.m_Script2Clr[i].Clear();
			}
		}

		// Token: 0x040053DB RID: 21467
		private Dictionary<Type, Func<DynValue, object>>[] m_Script2Clr = new Dictionary<Type, Func<DynValue, object>>[11];

		// Token: 0x040053DC RID: 21468
		private Dictionary<Type, Func<Script, object, DynValue>> m_Clr2Script = new Dictionary<Type, Func<Script, object, DynValue>>();
	}
}
