using System;
using script.ExchangeMeeting.Logic.Interface;
using script.ItemSource.Interface;

namespace KBEngine;

public class WorldTime
{
	private string _nowTime = "0001-1-1";

	public bool isLoadDate;

	public string nowTime
	{
		get
		{
			return _nowTime;
		}
		set
		{
			_nowTime = value;
			CheckNeedJieSuan();
		}
	}

	public void CheckNeedJieSuan()
	{
		if (isLoadDate)
		{
			isLoadDate = false;
			return;
		}
		if (jsonData.instance.SaveLock)
		{
			Tools.instance.IsNeedLaterCheck = true;
			return;
		}
		DateTime dateTime = DateTime.Parse(_nowTime);
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
			NpcJieSuanManager.inst.CheckImportantEvent(_nowTime);
			NpcJieSuanManager.inst.NpcJieSuan(num);
		}
	}

	public DateTime getNowTime()
	{
		return DateTime.Parse(nowTime);
	}

	public DateTime addTime(int addday, int addMonth = 0, int Addyear = 0)
	{
		DateTime result = DateTime.Parse(nowTime).AddYears(Addyear).AddMonths(addMonth)
			.AddDays(addday);
		nowTime = result.ToString("yyyy/MM/dd");
		return result;
	}
}
