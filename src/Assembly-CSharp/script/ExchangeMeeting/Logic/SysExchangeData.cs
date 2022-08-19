using System;
using System.Collections.Generic;
using Bag;
using Boo.Lang.Runtime;
using script.ExchangeMeeting.Logic.Interface;

namespace script.ExchangeMeeting.Logic
{
	// Token: 0x02000A3B RID: 2619
	[Serializable]
	public class SysExchangeData : IExchangeData
	{
		// Token: 0x060047FC RID: 18428 RVA: 0x001E6730 File Offset: 0x001E4930
		public SysExchangeData(List<BaseItem> needItems, Dictionary<int, int> needTags, List<BaseItem> giveItems, int eventId, bool isGuDing = false)
		{
			if (needItems == null || needTags == null || giveItems == null)
			{
				throw new RuntimeException(string.Format("传入参数有Null,请检查.needItems IsNull {0}", needItems == null) + string.Format(" needTags IsNull {0}", needTags == null) + string.Format(" giveItems IsNull {0}", giveItems == null));
			}
			if ((needItems.Count < 1 && needTags.Count < 1) || giveItems.Count < 1)
			{
				throw new RuntimeException(string.Format("传入参数数量为0,请检查.needItems.Count < 1 ：{0}", needItems.Count < 1) + string.Format(" needTags.Count < 1 ：{0}", needTags.Count < 1) + string.Format(" giveItems.Count<1 ：{0}", giveItems.Count < 1));
			}
			foreach (BaseItem baseItem in needItems)
			{
				this.NeedItems.Add(baseItem.Clone());
			}
			foreach (int key in needTags.Keys)
			{
				this.NeedTags.Add(key, needTags[key]);
			}
			foreach (BaseItem baseItem2 in giveItems)
			{
				this.GiveItems.Add(baseItem2.Clone());
			}
			this.IsGuDing = isGuDing;
			this.Id = eventId;
		}

		// Token: 0x040048BE RID: 18622
		public Dictionary<int, int> NeedTags = new Dictionary<int, int>();

		// Token: 0x040048BF RID: 18623
		public bool IsGuDing;
	}
}
