using System;
using script.ExchangeMeeting.Logic.Interface;
using script.ItemSource.Interface;

namespace KBEngine
{
	// Token: 0x02000C8C RID: 3212
	public class WorldTime
	{
		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06005986 RID: 22918 RVA: 0x0025603E File Offset: 0x0025423E
		// (set) Token: 0x06005987 RID: 22919 RVA: 0x00256046 File Offset: 0x00254246
		public string nowTime
		{
			get
			{
				return this._nowTime;
			}
			set
			{
				this._nowTime = value;
				this.CheckNeedJieSuan();
			}
		}

		// Token: 0x06005988 RID: 22920 RVA: 0x00256058 File Offset: 0x00254258
		public void CheckNeedJieSuan()
		{
			if (this.isLoadDate)
			{
				this.isLoadDate = false;
				return;
			}
			if (jsonData.instance.SaveLock)
			{
				Tools.instance.IsNeedLaterCheck = true;
				return;
			}
			DateTime dateTime = DateTime.Parse(this._nowTime);
			DateTime dateTime2 = new DateTime(NpcJieSuanManager.inst.GetNowTime().Year, NpcJieSuanManager.inst.GetNowTime().Month, NpcJieSuanManager.inst.GetNowTime().Day);
			if (NpcJieSuanManager.inst.isCanJieSuan && dateTime >= dateTime2)
			{
				int num = 0;
				while (dateTime2 <= dateTime)
				{
					dateTime2 = dateTime2.AddMonths(1);
					num++;
				}
				ABItemSourceMag.Inst.Update.Update(num);
				IExchangeMag.Inst.IUpdateExchange.UpdateProcess(num);
				NpcJieSuanManager.inst.CheckImportantEvent(this._nowTime);
				NpcJieSuanManager.inst.NpcJieSuan(num, true);
			}
		}

		// Token: 0x0600598A RID: 22922 RVA: 0x00256154 File Offset: 0x00254354
		public DateTime getNowTime()
		{
			return DateTime.Parse(this.nowTime);
		}

		// Token: 0x0600598B RID: 22923 RVA: 0x00256164 File Offset: 0x00254364
		public DateTime addTime(int addday, int addMonth = 0, int Addyear = 0)
		{
			DateTime result = DateTime.Parse(this.nowTime).AddYears(Addyear).AddMonths(addMonth).AddDays((double)addday);
			this.nowTime = result.ToString("yyyy/MM/dd");
			return result;
		}

		// Token: 0x04005227 RID: 21031
		private string _nowTime = "0001-1-1";

		// Token: 0x04005228 RID: 21032
		public bool isLoadDate;
	}
}
