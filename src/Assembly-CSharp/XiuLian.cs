using System;
using KBEngine;
using UnityEngine;

// Token: 0x0200043D RID: 1085
public class XiuLian : MonoBehaviour
{
	// Token: 0x06002280 RID: 8832 RVA: 0x000B5E62 File Offset: 0x000B4062
	private void Start()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06002281 RID: 8833 RVA: 0x000ED300 File Offset: 0x000EB500
	public void open()
	{
		string screenName = Tools.getScreenName();
		DateTime residueTime = Tools.instance.getPlayer().zulinContorl.getResidueTime(screenName);
		DateTime t = DateTime.Parse("0001-1-1");
		if (residueTime > t)
		{
			base.gameObject.SetActive(true);
			YSAutoSaveGame.IsSave = false;
			this.initLinWu.updateItem();
			return;
		}
		UIPopTip.Inst.Pop("剩余时间不足，请到客栈老板处续费", PopTipIconType.叹号);
	}

	// Token: 0x06002282 RID: 8834 RVA: 0x000B5E62 File Offset: 0x000B4062
	public void close()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06002283 RID: 8835 RVA: 0x000ED36C File Offset: 0x000EB56C
	public static bool CheckCanUse(int time)
	{
		string screenName = Tools.getScreenName();
		return ZulinContorl.GetTimeSum(Tools.instance.getPlayer().zulinContorl.getResidueTime(screenName)) >= time;
	}

	// Token: 0x06002284 RID: 8836 RVA: 0x000ED3A0 File Offset: 0x000EB5A0
	public static bool CheckCanUseDay(int timeDay)
	{
		int wuXin = (int)Tools.instance.getPlayer().wuXin;
		float num = (8f * (float)Math.Pow(10.0, -6.0) * (float)Math.Pow((double)wuXin, 3.0) - 0.0042f * (float)Math.Pow((double)wuXin, 2.0) + 0.7922f * (float)wuXin - 0.3381f) / 100f + 1f;
		timeDay = (int)((float)timeDay / num);
		string screenName = Tools.getScreenName();
		DateTime residueTime = Tools.instance.getPlayer().zulinContorl.getResidueTime(screenName);
		return (residueTime.Year - 1) * 365 + (residueTime.Month - 1) * 30 + residueTime.Day >= timeDay;
	}

	// Token: 0x06002285 RID: 8837 RVA: 0x000ED470 File Offset: 0x000EB670
	private void Update()
	{
		string screenName = Tools.getScreenName();
		DateTime residueTime = Tools.instance.getPlayer().zulinContorl.getResidueTime(screenName);
		if (residueTime.Year < 5000)
		{
			this.Time.text = string.Concat(new object[]
			{
				"剩余",
				residueTime.Year - 1,
				"年",
				residueTime.Month - 1,
				"月",
				residueTime.Day - 1,
				"日"
			});
		}
		else
		{
			this.Time.text = "";
		}
		this.Name.text = (Tools.instance.Code64ToString(jsonData.instance.SceneNameJsonData[screenName]["EventName"].str) ?? "");
	}

	// Token: 0x04001BE5 RID: 7141
	public UILabel Name;

	// Token: 0x04001BE6 RID: 7142
	public UILabel Time;

	// Token: 0x04001BE7 RID: 7143
	public InitLinWu initLinWu;
}
