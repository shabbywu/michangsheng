using System;
using KBEngine;
using UnityEngine;

// Token: 0x020005F4 RID: 1524
public class XiuLian : MonoBehaviour
{
	// Token: 0x0600263F RID: 9791 RVA: 0x00017C2D File Offset: 0x00015E2D
	private void Start()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06002640 RID: 9792 RVA: 0x0012E190 File Offset: 0x0012C390
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

	// Token: 0x06002641 RID: 9793 RVA: 0x00017C2D File Offset: 0x00015E2D
	public void close()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06002642 RID: 9794 RVA: 0x0012E1FC File Offset: 0x0012C3FC
	public static bool CheckCanUse(int time)
	{
		string screenName = Tools.getScreenName();
		return ZulinContorl.GetTimeSum(Tools.instance.getPlayer().zulinContorl.getResidueTime(screenName)) >= time;
	}

	// Token: 0x06002643 RID: 9795 RVA: 0x0012E230 File Offset: 0x0012C430
	public static bool CheckCanUseDay(int timeDay)
	{
		int wuXin = (int)Tools.instance.getPlayer().wuXin;
		float num = (8f * (float)Math.Pow(10.0, -6.0) * (float)Math.Pow((double)wuXin, 3.0) - 0.0042f * (float)Math.Pow((double)wuXin, 2.0) + 0.7922f * (float)wuXin - 0.3381f) / 100f + 1f;
		timeDay = (int)((float)timeDay / num);
		string screenName = Tools.getScreenName();
		DateTime residueTime = Tools.instance.getPlayer().zulinContorl.getResidueTime(screenName);
		return (residueTime.Year - 1) * 365 + (residueTime.Month - 1) * 30 + residueTime.Day >= timeDay;
	}

	// Token: 0x06002644 RID: 9796 RVA: 0x0012E300 File Offset: 0x0012C500
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

	// Token: 0x040020B1 RID: 8369
	public UILabel Name;

	// Token: 0x040020B2 RID: 8370
	public UILabel Time;

	// Token: 0x040020B3 RID: 8371
	public InitLinWu initLinWu;
}
