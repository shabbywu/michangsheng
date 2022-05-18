using System;
using System.Collections.Generic;

namespace UltimateSurvival.AI
{
	// Token: 0x0200097C RID: 2428
	public class StateData
	{
		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06003E18 RID: 15896 RVA: 0x0002CB8D File Offset: 0x0002AD8D
		// (set) Token: 0x06003E19 RID: 15897 RVA: 0x0002CB95 File Offset: 0x0002AD95
		public Dictionary<string, object> m_Dictionary { get; private set; }

		// Token: 0x06003E1A RID: 15898 RVA: 0x0002CB9E File Offset: 0x0002AD9E
		public StateData()
		{
			this.m_Dictionary = new Dictionary<string, object>();
		}

		// Token: 0x06003E1B RID: 15899 RVA: 0x0002CBB1 File Offset: 0x0002ADB1
		public StateData(Dictionary<string, object> conditions)
		{
			this.m_Dictionary = conditions;
		}

		// Token: 0x06003E1C RID: 15900 RVA: 0x0002CBC0 File Offset: 0x0002ADC0
		public void Add(string key, object value)
		{
			this.m_Dictionary.Add(key, value);
		}

		// Token: 0x06003E1D RID: 15901 RVA: 0x0002CBCF File Offset: 0x0002ADCF
		public void Clear()
		{
			this.m_Dictionary.Clear();
		}

		// Token: 0x06003E1E RID: 15902 RVA: 0x0002CBDC File Offset: 0x0002ADDC
		public static void OverrideValue(KeyValuePair<string, object> value, StateData data)
		{
			data.m_Dictionary[value.Key] = value.Value;
		}

		// Token: 0x06003E1F RID: 15903 RVA: 0x0002CBF7 File Offset: 0x0002ADF7
		public static void OverrideValue(string key, object value, StateData data)
		{
			StateData.OverrideValue(new KeyValuePair<string, object>(key, value), data);
		}

		// Token: 0x06003E20 RID: 15904 RVA: 0x001B6908 File Offset: 0x001B4B08
		public static void OverrideValues(StateData overrider, StateData data)
		{
			foreach (KeyValuePair<string, object> keyValuePair in overrider.m_Dictionary)
			{
				data.m_Dictionary[keyValuePair.Key] = keyValuePair.Value;
			}
		}
	}
}
