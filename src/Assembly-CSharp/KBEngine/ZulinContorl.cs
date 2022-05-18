using System;

namespace KBEngine
{
	// Token: 0x02001054 RID: 4180
	public class ZulinContorl
	{
		// Token: 0x0600645A RID: 25690 RVA: 0x00045103 File Offset: 0x00043303
		public ZulinContorl(Entity avater)
		{
			this.entity = (Avatar)avater;
		}

		// Token: 0x0600645B RID: 25691 RVA: 0x002816F8 File Offset: 0x0027F8F8
		public JSONObject getKeZhan(string name)
		{
			if (!this.entity.ZuLin.HasField(name))
			{
				this.entity.ZuLin.AddField(name, new JSONObject(JSONObject.Type.OBJECT));
				this.setJsonObject(this.entity.ZuLin[name], name, "0001-1-1");
			}
			return this.entity.ZuLin[name];
		}

		// Token: 0x0600645C RID: 25692 RVA: 0x00281760 File Offset: 0x0027F960
		public DateTime getResidueTime(string name)
		{
			if (jsonData.instance.WuXianBiGuanJsonData.list.Find((JSONObject aa) => aa["SceneName"].str == name) != null)
			{
				return DateTime.Parse("4999-12-12");
			}
			return DateTime.Parse(this.getKeZhan(name)[0].str);
		}

		// Token: 0x0600645D RID: 25693 RVA: 0x00045122 File Offset: 0x00043322
		public static int GetTimeSum(DateTime time)
		{
			return (time.Year - 1) * 12 + time.Month - 1;
		}

		// Token: 0x0600645E RID: 25694 RVA: 0x002817C4 File Offset: 0x0027F9C4
		public bool HasTime(string sName)
		{
			DateTime residueTime = this.getResidueTime(sName);
			return (residueTime.Year - 1) * 365 + (residueTime.Month - 1) * 30 + (residueTime.Day - 1) > 0;
		}

		// Token: 0x0600645F RID: 25695 RVA: 0x00281804 File Offset: 0x0027FA04
		public void addTime(int addday, int addMonth = 0, int Addyear = 0)
		{
			foreach (string name in this.entity.ZuLin.keys)
			{
				this.KZReduceTime(name, addday, addMonth, Addyear);
			}
		}

		// Token: 0x06006460 RID: 25696 RVA: 0x00281864 File Offset: 0x0027FA64
		public void KZReduceTime(string name, int addday, int addMonth, int Addyear)
		{
			JSONObject keZhan = this.getKeZhan(name);
			DateTime dateTime = DateTime.Parse(keZhan[0].str);
			DateTime dateTime2 = DateTime.Parse("0001-1-1").AddDays((double)addday).AddMonths(addMonth).AddYears(Addyear);
			if (dateTime > dateTime2)
			{
				this.setJsonObject(keZhan, name, DateTime.Parse("0001-1-1").AddDays((double)(dateTime - dateTime2).Days).ToString());
				return;
			}
			this.setJsonObject(keZhan, name, "0001-1-1");
		}

		// Token: 0x06006461 RID: 25697 RVA: 0x00281900 File Offset: 0x0027FB00
		public void KZAddTime(string name, int addday, int addMonth, int Addyear)
		{
			JSONObject keZhan = this.getKeZhan(name);
			this.setJsonObject(keZhan, name, DateTime.Parse(keZhan[0].str).AddDays((double)addday).AddMonths(addMonth).AddYears(Addyear).ToString());
		}

		// Token: 0x06006462 RID: 25698 RVA: 0x0004513A File Offset: 0x0004333A
		private void setJsonObject(JSONObject json, string name, string time)
		{
			json.SetField(name, time);
		}

		// Token: 0x04005DD9 RID: 24025
		public Avatar entity;

		// Token: 0x04005DDA RID: 24026
		public string kezhanLastScence = "";
	}
}
