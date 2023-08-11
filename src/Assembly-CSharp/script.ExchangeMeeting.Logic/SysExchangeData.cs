using System;
using System.Collections.Generic;
using Bag;
using Boo.Lang.Runtime;
using script.ExchangeMeeting.Logic.Interface;

namespace script.ExchangeMeeting.Logic;

[Serializable]
public class SysExchangeData : IExchangeData
{
	public Dictionary<int, int> NeedTags = new Dictionary<int, int>();

	public bool IsGuDing;

	public SysExchangeData(List<BaseItem> needItems, Dictionary<int, int> needTags, List<BaseItem> giveItems, int eventId, bool isGuDing = false)
	{
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		if (needItems == null || needTags == null || giveItems == null)
		{
			throw new RuntimeException($"传入参数有Null,请检查.needItems IsNull {needItems == null}" + $" needTags IsNull {needTags == null}" + $" giveItems IsNull {giveItems == null}");
		}
		if ((needItems.Count < 1 && needTags.Count < 1) || giveItems.Count < 1)
		{
			throw new RuntimeException($"传入参数数量为0,请检查.needItems.Count < 1 ：{needItems.Count < 1}" + $" needTags.Count < 1 ：{needTags.Count < 1}" + $" giveItems.Count<1 ：{giveItems.Count < 1}");
		}
		foreach (BaseItem needItem in needItems)
		{
			NeedItems.Add(needItem.Clone());
		}
		foreach (int key in needTags.Keys)
		{
			NeedTags.Add(key, needTags[key]);
		}
		foreach (BaseItem giveItem in giveItems)
		{
			GiveItems.Add(giveItem.Clone());
		}
		IsGuDing = isGuDing;
		Id = eventId;
	}
}
