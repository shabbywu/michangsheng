using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;

namespace script.EventMsg;

[Serializable]
public class EventMag
{
	public List<EventData> List;

	[NonSerialized]
	public List<EventData> OldList;

	public static EventMag Inst => Tools.instance.getPlayer().StreamData.EventMag;

	public void Init()
	{
		if (List == null)
		{
			List = new List<EventData>();
		}
		if (OldList != null)
		{
			return;
		}
		OldList = new List<EventData>();
		foreach (LiShiChuanWen data in LiShiChuanWen.DataList)
		{
			EventData eventData = new EventData();
			eventData.OutYear = data.cunZaiShiJian;
			eventData.Id = data.id;
			eventData.Type = 0;
			eventData.StartYear = data.StartTime;
			eventData.TypeId = data.TypeID;
			OldList.Add(eventData);
		}
	}

	public void SaveEvent(int npcId, int eventType)
	{
		int i = jsonData.instance.AvatarJsonData[npcId.ToString()]["Type"].I;
		if (ChuanWenTypeData.DataDict.ContainsKey(i))
		{
			DongTaiChuanWenBaio dongTaiChuanWenBaio = DongTaiChuanWenBaio.DataDict[eventType];
			if (dongTaiChuanWenBaio.isshili != 1 || jsonData.instance.AvatarJsonData[npcId.ToString()]["MenPai"].I == Tools.instance.getPlayer().menPai)
			{
				int chuanWenType = ChuanWenTypeData.DataDict[i].ChuanWenType;
				EventData item = new EventData(dongTaiChuanWenBaio.cunZaiShiJian, NpcJieSuanManager.inst.GetNowTime().Year, 1, chuanWenType, dongTaiChuanWenBaio.id, jsonData.instance.AvatarRandomJsonData[npcId.ToString()]["Name"].Str);
				List.Add(item);
			}
		}
	}

	public List<EventData> GetList(int typeId)
	{
		List<EventData> list = new List<EventData>();
		DateTime nowTime = Tools.instance.getPlayer().worldTimeMag.getNowTime();
		foreach (EventData old in OldList)
		{
			if (IsCanGet(old, typeId, nowTime))
			{
				list.Add(old);
			}
		}
		foreach (EventData item in List)
		{
			if (IsCanGet(item, typeId, nowTime))
			{
				list.Add(item);
			}
		}
		return list;
	}

	private bool IsCanGet(EventData eventData, int typeId, DateTime nowTime)
	{
		Avatar player = Tools.instance.getPlayer();
		if (eventData.TypeId != typeId)
		{
			return false;
		}
		if (eventData.StartYear < 1)
		{
			return false;
		}
		if (nowTime.Year < eventData.StartYear)
		{
			return false;
		}
		if (nowTime.Year - eventData.StartYear > eventData.OutYear)
		{
			return false;
		}
		if (eventData.Type == 0)
		{
			LiShiChuanWen liShiChuanWen = LiShiChuanWen.DataDict[eventData.Id];
			if (liShiChuanWen.EventLv.Count > 0 && !ManZuValue(player.StaticValue.Value[liShiChuanWen.EventLv[0]], liShiChuanWen.EventLv[1], liShiChuanWen.fuhao))
			{
				return false;
			}
		}
		return true;
	}

	public bool ManZuValue(int value, int num, string type)
	{
		return type switch
		{
			"=" => value == num, 
			"<" => value < num, 
			">" => value > num, 
			_ => true, 
		};
	}
}
