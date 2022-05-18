using System;

namespace KBEngine
{
	// Token: 0x0200104F RID: 4175
	public class WorldTime
	{
		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06006443 RID: 25667 RVA: 0x00044F62 File Offset: 0x00043162
		// (set) Token: 0x06006444 RID: 25668 RVA: 0x00281278 File Offset: 0x0027F478
		public string nowTime
		{
			get
			{
				return this._nowTime;
			}
			set
			{
				this._nowTime = value;
				if (this.isLoadDate)
				{
					this.isLoadDate = false;
					return;
				}
				if (jsonData.instance.SaveLock)
				{
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
					NpcJieSuanManager.inst.CheckImportantEvent(this._nowTime);
					NpcJieSuanManager.inst.NpcJieSuan(num, true);
				}
			}
		}

		// Token: 0x06006446 RID: 25670 RVA: 0x00044F7D File Offset: 0x0004317D
		public DateTime getNowTime()
		{
			return DateTime.Parse(this.nowTime);
		}

		// Token: 0x06006447 RID: 25671 RVA: 0x00281340 File Offset: 0x0027F540
		public DateTime addTime(int addday, int addMonth = 0, int Addyear = 0)
		{
			DateTime result = DateTime.Parse(this.nowTime).AddYears(Addyear).AddMonths(addMonth).AddDays((double)addday);
			this.nowTime = result.ToString();
			return result;
		}

		// Token: 0x04005DD3 RID: 24019
		private string _nowTime = "0001-1-1";

		// Token: 0x04005DD4 RID: 24020
		public bool isLoadDate;
	}
}
