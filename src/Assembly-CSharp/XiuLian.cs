using System;
using KBEngine;
using UnityEngine;

public class XiuLian : MonoBehaviour
{
	public UILabel Name;

	public UILabel Time;

	public InitLinWu initLinWu;

	private void Start()
	{
		((Component)this).gameObject.SetActive(false);
	}

	public void open()
	{
		string screenName = Tools.getScreenName();
		DateTime residueTime = Tools.instance.getPlayer().zulinContorl.getResidueTime(screenName);
		DateTime dateTime = DateTime.Parse("0001-1-1");
		if (residueTime > dateTime)
		{
			((Component)this).gameObject.SetActive(true);
			YSAutoSaveGame.IsSave = false;
			initLinWu.updateItem();
		}
		else
		{
			UIPopTip.Inst.Pop("剩余时间不足，请到客栈老板处续费");
		}
	}

	public void close()
	{
		((Component)this).gameObject.SetActive(false);
	}

	public static bool CheckCanUse(int time)
	{
		string screenName = Tools.getScreenName();
		if (ZulinContorl.GetTimeSum(Tools.instance.getPlayer().zulinContorl.getResidueTime(screenName)) >= time)
		{
			return true;
		}
		return false;
	}

	public static bool CheckCanUseDay(int timeDay)
	{
		int wuXin = (int)Tools.instance.getPlayer().wuXin;
		float num = (8f * (float)Math.Pow(10.0, -6.0) * (float)Math.Pow(wuXin, 3.0) - 0.0042f * (float)Math.Pow(wuXin, 2.0) + 0.7922f * (float)wuXin - 0.3381f) / 100f + 1f;
		timeDay = (int)((float)timeDay / num);
		string screenName = Tools.getScreenName();
		DateTime residueTime = Tools.instance.getPlayer().zulinContorl.getResidueTime(screenName);
		if ((residueTime.Year - 1) * 365 + (residueTime.Month - 1) * 30 + residueTime.Day >= timeDay)
		{
			return true;
		}
		return false;
	}

	private void Update()
	{
		string screenName = Tools.getScreenName();
		DateTime residueTime = Tools.instance.getPlayer().zulinContorl.getResidueTime(screenName);
		if (residueTime.Year < 5000)
		{
			Time.text = "剩余" + (residueTime.Year - 1) + "年" + (residueTime.Month - 1) + "月" + (residueTime.Day - 1) + "日";
		}
		else
		{
			Time.text = "";
		}
		Name.text = Tools.instance.Code64ToString(jsonData.instance.SceneNameJsonData[screenName]["EventName"].str) ?? "";
	}
}
