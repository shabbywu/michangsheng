using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;

namespace script.EventMsg
{
	// Token: 0x02000A48 RID: 2632
	[Serializable]
	public class EventMag
	{
		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06004832 RID: 18482 RVA: 0x001E776C File Offset: 0x001E596C
		public static EventMag Inst
		{
			get
			{
				return Tools.instance.getPlayer().StreamData.EventMag;
			}
		}

		// Token: 0x06004833 RID: 18483 RVA: 0x001E7784 File Offset: 0x001E5984
		public void Init()
		{
			if (this.List == null)
			{
				this.List = new List<EventData>();
			}
			if (this.OldList == null)
			{
				this.OldList = new List<EventData>();
				foreach (LiShiChuanWen liShiChuanWen in LiShiChuanWen.DataList)
				{
					EventData eventData = new EventData();
					eventData.OutYear = liShiChuanWen.cunZaiShiJian;
					eventData.Id = liShiChuanWen.id;
					eventData.Type = 0;
					eventData.StartYear = liShiChuanWen.StartTime;
					eventData.TypeId = liShiChuanWen.TypeID;
					this.OldList.Add(eventData);
				}
			}
		}

		// Token: 0x06004834 RID: 18484 RVA: 0x001E7844 File Offset: 0x001E5A44
		public void SaveEvent(int npcId, int eventType)
		{
			int i = jsonData.instance.AvatarJsonData[npcId.ToString()]["Type"].I;
			if (!ChuanWenTypeData.DataDict.ContainsKey(i))
			{
				return;
			}
			DongTaiChuanWenBaio dongTaiChuanWenBaio = DongTaiChuanWenBaio.DataDict[eventType];
			if (dongTaiChuanWenBaio.isshili == 1 && jsonData.instance.AvatarJsonData[npcId.ToString()]["MenPai"].I != (int)Tools.instance.getPlayer().menPai)
			{
				return;
			}
			int chuanWenType = ChuanWenTypeData.DataDict[i].ChuanWenType;
			EventData item = new EventData(dongTaiChuanWenBaio.cunZaiShiJian, NpcJieSuanManager.inst.GetNowTime().Year, 1, chuanWenType, dongTaiChuanWenBaio.id, jsonData.instance.AvatarRandomJsonData[npcId.ToString()]["Name"].Str);
			this.List.Add(item);
		}

		// Token: 0x06004835 RID: 18485 RVA: 0x001E793C File Offset: 0x001E5B3C
		public List<EventData> GetList(int typeId)
		{
			List<EventData> list = new List<EventData>();
			DateTime nowTime = Tools.instance.getPlayer().worldTimeMag.getNowTime();
			foreach (EventData eventData in this.OldList)
			{
				if (this.IsCanGet(eventData, typeId, nowTime))
				{
					list.Add(eventData);
				}
			}
			foreach (EventData eventData2 in this.List)
			{
				if (this.IsCanGet(eventData2, typeId, nowTime))
				{
					list.Add(eventData2);
				}
			}
			return list;
		}

		// Token: 0x06004836 RID: 18486 RVA: 0x001E7A08 File Offset: 0x001E5C08
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
				if (liShiChuanWen.EventLv.Count > 0 && !this.ManZuValue(player.StaticValue.Value[liShiChuanWen.EventLv[0]], liShiChuanWen.EventLv[1], liShiChuanWen.fuhao))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004837 RID: 18487 RVA: 0x001E7ABB File Offset: 0x001E5CBB
		public bool ManZuValue(int value, int num, string type)
		{
			if (type == "=")
			{
				return value == num;
			}
			if (type == "<")
			{
				return value < num;
			}
			return !(type == ">") || value > num;
		}

		// Token: 0x040048DB RID: 18651
		public List<EventData> List;

		// Token: 0x040048DC RID: 18652
		[NonSerialized]
		public List<EventData> OldList;
	}
}
