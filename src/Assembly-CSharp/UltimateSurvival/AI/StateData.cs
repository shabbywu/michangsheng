using System;
using System.Collections.Generic;

namespace UltimateSurvival.AI
{
	// Token: 0x02000670 RID: 1648
	public class StateData
	{
		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06003469 RID: 13417 RVA: 0x0016E034 File Offset: 0x0016C234
		// (set) Token: 0x0600346A RID: 13418 RVA: 0x0016E03C File Offset: 0x0016C23C
		public Dictionary<string, object> m_Dictionary { get; private set; }

		// Token: 0x0600346B RID: 13419 RVA: 0x0016E045 File Offset: 0x0016C245
		public StateData()
		{
			this.m_Dictionary = new Dictionary<string, object>();
		}

		// Token: 0x0600346C RID: 13420 RVA: 0x0016E058 File Offset: 0x0016C258
		public StateData(Dictionary<string, object> conditions)
		{
			this.m_Dictionary = conditions;
		}

		// Token: 0x0600346D RID: 13421 RVA: 0x0016E067 File Offset: 0x0016C267
		public void Add(string key, object value)
		{
			this.m_Dictionary.Add(key, value);
		}

		// Token: 0x0600346E RID: 13422 RVA: 0x0016E076 File Offset: 0x0016C276
		public void Clear()
		{
			this.m_Dictionary.Clear();
		}

		// Token: 0x0600346F RID: 13423 RVA: 0x0016E083 File Offset: 0x0016C283
		public static void OverrideValue(KeyValuePair<string, object> value, StateData data)
		{
			data.m_Dictionary[value.Key] = value.Value;
		}

		// Token: 0x06003470 RID: 13424 RVA: 0x0016E09E File Offset: 0x0016C29E
		public static void OverrideValue(string key, object value, StateData data)
		{
			StateData.OverrideValue(new KeyValuePair<string, object>(key, value), data);
		}

		// Token: 0x06003471 RID: 13425 RVA: 0x0016E0B0 File Offset: 0x0016C2B0
		public static void OverrideValues(StateData overrider, StateData data)
		{
			foreach (KeyValuePair<string, object> keyValuePair in overrider.m_Dictionary)
			{
				data.m_Dictionary[keyValuePair.Key] = keyValuePair.Value;
			}
		}
	}
}
