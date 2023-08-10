using System.Collections.Generic;
using Bag;
using Boo.Lang.Runtime;
using JSONClass;
using UnityEngine;
using script.ExchangeMeeting.Logic.Interface;

namespace script.ExchangeMeeting.Logic;

public class ExchangeIO : IExchangeIO
{
	public override void Add(IExchangeData data)
	{
		if (data is PlayerExchangeData)
		{
			base.source.playerList.Add(data);
		}
		else if (data is SysExchangeData)
		{
			base.source.sysList.Add(data);
		}
	}

	public override void Remove(IExchangeData data)
	{
		if (data is PlayerExchangeData)
		{
			base.source.playerList.Remove(data);
		}
		else if (data is SysExchangeData)
		{
			base.source.sysList.Remove(data);
		}
	}

	public override bool NeedUpdateNpcExchange()
	{
		return base.source.NextUpdate <= PlayerEx.Player.worldTimeMag.getNowTime();
	}

	public override List<int> GetGuDingList()
	{
		List<int> list = new List<int>();
		foreach (GuDingExchangeData data in GuDingExchangeData.DataList)
		{
			if (base.source.guDingList.Contains(data.id))
			{
				continue;
			}
			if (data.EventValue.Count == 2)
			{
				int value = PlayerEx.Player.StaticValue.Value[data.EventValue[0]];
				int value2 = data.EventValue[1];
				if (!ToolsEx.IsMatching(value, value2, data.fuhao))
				{
					continue;
				}
			}
			else if (data.EventValue.Count > 0)
			{
				Debug.LogError((object)("判断数目异常已跳过,固定交易事件id:" + data.id));
				continue;
			}
			list.Add(data.id);
		}
		return list;
	}

	public override void CreateNpcExchange()
	{
		if (base.source.sysList == null)
		{
			base.source.sysList = new List<IExchangeData>();
			Debug.LogError((object)"sysList is null");
		}
		else
		{
			base.source.sysList.Clear();
		}
		List<int> guDingList = GetGuDingList();
		int num = 0;
		if (guDingList.Count > 0)
		{
			num += guDingList.Count;
			foreach (int item in guDingList)
			{
				base.source.sysList.Add(IExchangeMag.Inst.ExchangeDataFactory.Create(item, 2));
			}
		}
		num = ((num < 5) ? (minTotalNum - num) : minRandomNum);
		foreach (int randomExchange in GetRandomExchangeList(num))
		{
			base.source.sysList.Add(IExchangeMag.Inst.ExchangeDataFactory.Create(randomExchange, 1));
		}
		while (base.source.NextUpdate <= PlayerEx.Player.worldTimeMag.getNowTime())
		{
			base.source.NextUpdate = base.source.NextUpdate.AddYears(50);
		}
	}

	public override void CreatePlayerExchange(List<BaseItem> needs, List<BaseItem> gets)
	{
		Add(IExchangeMag.Inst.ExchangeDataFactory.Create(needs, gets));
	}

	protected override List<int> GetRandomExchangeList(int count)
	{
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		foreach (RandomExchangeData data in RandomExchangeData.DataList)
		{
			dictionary.Add(data.id, data.percent);
		}
		List<int> list = new List<int>();
		for (int i = 0; i < count; i++)
		{
			list.Add(GetRandomId(dictionary));
			dictionary.Remove(list[i]);
		}
		if (list.Count != count)
		{
			throw new RuntimeException($"随机指定数量系统交易事件失败,需求数量为:{count},实际数量为:{list.Count}," + "请检测GetRandomExchangeList方法");
		}
		return list;
	}

	public override List<IExchangeData> GetPlayerList()
	{
		return base.source.playerList;
	}

	public override List<IExchangeData> GetSystemList()
	{
		return base.source.sysList;
	}

	public override void SaveGuDingId(int id)
	{
		base.source.guDingList.Add(id);
	}

	private int GetRandomId(Dictionary<int, int> dict)
	{
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		int num = 0;
		foreach (int value in dict.Values)
		{
			num += value;
		}
		int num2 = random.Next(0, num + 1);
		int num3 = 0;
		foreach (int key in dict.Keys)
		{
			num3 += dict[key];
			if (num3 >= num2)
			{
				return key;
			}
		}
		throw new RuntimeException("GetRandomId出错，权重随机方法异常");
	}
}
