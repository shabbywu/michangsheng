using System;

namespace KBEngine
{
	// Token: 0x02000C90 RID: 3216
	public class ZulinContorl
	{
		// Token: 0x0600599C RID: 22940 RVA: 0x00256682 File Offset: 0x00254882
		public ZulinContorl(Entity avater)
		{
			this.entity = (Avatar)avater;
		}

		// Token: 0x0600599D RID: 22941 RVA: 0x002566A4 File Offset: 0x002548A4
		public JSONObject getKeZhan(string name)
		{
			if (!this.entity.ZuLin.HasField(name))
			{
				this.entity.ZuLin.AddField(name, new JSONObject(JSONObject.Type.OBJECT));
				this.setJsonObject(this.entity.ZuLin[name], name, "0001-1-1");
			}
			return this.entity.ZuLin[name];
		}

		// Token: 0x0600599E RID: 22942 RVA: 0x0025670C File Offset: 0x0025490C
		public DateTime getResidueTime(string name)
		{
			if (jsonData.instance.WuXianBiGuanJsonData.list.Find((JSONObject aa) => aa["SceneName"].str == name) != null)
			{
				return DateTime.Parse("4999-12-12");
			}
			return DateTime.Parse(this.getKeZhan(name)[0].str);
		}

		// Token: 0x0600599F RID: 22943 RVA: 0x0025676F File Offset: 0x0025496F
		public static int GetTimeSum(DateTime time)
		{
			return (time.Year - 1) * 12 + time.Month - 1;
		}

		// Token: 0x060059A0 RID: 22944 RVA: 0x00256788 File Offset: 0x00254988
		public bool HasTime(string sName)
		{
			DateTime residueTime = this.getResidueTime(sName);
			return (residueTime.Year - 1) * 365 + (residueTime.Month - 1) * 30 + (residueTime.Day - 1) > 0;
		}

		// Token: 0x060059A1 RID: 22945 RVA: 0x002567C8 File Offset: 0x002549C8
		public void addTime(int addday, int addMonth = 0, int Addyear = 0)
		{
			foreach (string name in this.entity.ZuLin.keys)
			{
				this.KZReduceTime(name, addday, addMonth, Addyear);
			}
		}

		// Token: 0x060059A2 RID: 22946 RVA: 0x00256828 File Offset: 0x00254A28
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

		// Token: 0x060059A3 RID: 22947 RVA: 0x002568C4 File Offset: 0x00254AC4
		public void KZAddTime(string name, int addday, int addMonth, int Addyear)
		{
			JSONObject keZhan = this.getKeZhan(name);
			this.setJsonObject(keZhan, name, DateTime.Parse(keZhan[0].str).AddDays((double)addday).AddMonths(addMonth).AddYears(Addyear).ToString());
		}

		// Token: 0x060059A4 RID: 22948 RVA: 0x00256917 File Offset: 0x00254B17
		private void setJsonObject(JSONObject json, string name, string time)
		{
			json.SetField(name, time);
		}

		// Token: 0x0400522C RID: 21036
		public Avatar entity;

		// Token: 0x0400522D RID: 21037
		public string kezhanLastScence = "";
	}
}
