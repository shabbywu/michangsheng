using System;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

public class LingGuangMag : MonoBehaviour
{
	public LingGuangCell TempObj;

	public UILabel Name;

	public Text desc;

	public Text guoshiTime;

	public Text Type;

	public Text GanWuTime;

	public JSONObject NowJson;

	private void Start()
	{
		Event.registerOut("ClickLingGuangCell", this, "ClickLingGuangCell");
	}

	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	public void Init()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		Avatar player = Tools.instance.getPlayer();
		clear();
		foreach (Transform item in ((Component)TempObj).transform.parent)
		{
			Transform val = item;
			if (((Component)val).gameObject.activeSelf)
			{
				Object.Destroy((Object)(object)((Component)val).gameObject);
			}
		}
		foreach (JSONObject item2 in player.LingGuang.list)
		{
			LingGuangCell component = Tools.InstantiateGameObject(((Component)TempObj).gameObject, ((Component)TempObj).transform.parent).GetComponent<LingGuangCell>();
			component.Info = item2;
			component.init(getDesc(item2) + "\n" + GetShengYuShiJian(item2));
		}
	}

	public void ClickLingGuangCell(JSONObject json)
	{
		int wuXin = (int)Tools.instance.getPlayer().wuXin;
		float num = (8f * (float)Math.Pow(10.0, -6.0) * (float)Math.Pow(wuXin, 3.0) - 0.0042f * (float)Math.Pow(wuXin, 2.0) + 0.7922f * (float)wuXin - 0.3381f) / 100f + 1f;
		NowJson = json;
		int day = (int)(json["studyTime"].n / num);
		Name.text = Tools.Code64(json["name"].str) ?? "";
		desc.text = "<color=#DE5900>思绪：</color>" + Tools.Code64(json["desc"].str);
		guoshiTime.text = "<color=#DE5900>遗忘：</color>" + Tools.Code64(GetShengYuShiJian(json));
		string text = Tools.Code64(jsonData.instance.WuDaoAllTypeJson[json["type"].I.ToString()]["name"].str);
		string text2 = Tools.getStr("ItemColor" + json["quality"].I).Replace("[", "").Replace("]", "");
		Type.text = "<color=#DE5900>说明：</color>对" + text + "之道的感悟提升<color=#" + text2 + ">" + (float)json["studyTime"].I * jsonData.instance.WuDaoExBeiLuJson["1"]["lingguang" + json["quality"].I].n + "</color>点";
		GanWuTime.text = $"<color=#DE5900>感悟时间：</color>{Tools.DayToYear(day)}年{Tools.DayToMonth(day)}月{Tools.DayToDay(day)}日";
	}

	public void GanWu()
	{
		if (NowJson != null)
		{
			Avatar player = Tools.instance.getPlayer();
			if (!XiuLian.CheckCanUseDay(player.wuDaoMag.CalcGanWuTime(NowJson)))
			{
				UIPopTip.Inst.Pop("房间剩余时间不足，无法感悟");
			}
			else if (Tools.canClickFlag)
			{
				Tools.instance.playFader("正在感悟天道...");
				player.wuDaoMag.StudyLingGuang(NowJson["uuid"].str);
				player.HP = player.HP_Max;
				Init();
			}
		}
	}

	public void YiWang()
	{
		if (NowJson != null)
		{
			Tools.instance.getPlayer().wuDaoMag.removeLingGuang(NowJson["uuid"].str);
			Init();
		}
	}

	public void clear()
	{
		NowJson = null;
		Name.text = "";
		desc.text = "";
		guoshiTime.text = "";
		Type.text = "";
		GanWuTime.text = "";
	}

	public string GetShengYuShiJian(JSONObject json)
	{
		Avatar player = Tools.instance.getPlayer();
		string text = Tools.TimeToShengYuTime(Tools.getShengYuShiJian(endTime: DateTime.Parse(json["startTime"].str).AddDays(json["guoqiTime"].I), nowTime: player.worldTimeMag.getNowTime()), "");
		return "这条思绪将在" + text + "后被遗忘。";
	}

	public string getDesc(JSONObject info)
	{
		return Tools.Code64(info["desc"].str);
	}

	private void Update()
	{
	}
}
