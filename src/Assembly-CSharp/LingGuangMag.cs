using System;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200043A RID: 1082
public class LingGuangMag : MonoBehaviour
{
	// Token: 0x0600226A RID: 8810 RVA: 0x000ECB94 File Offset: 0x000EAD94
	private void Start()
	{
		Event.registerOut("ClickLingGuangCell", this, "ClickLingGuangCell");
	}

	// Token: 0x0600226B RID: 8811 RVA: 0x000826BE File Offset: 0x000808BE
	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	// Token: 0x0600226C RID: 8812 RVA: 0x000ECBA8 File Offset: 0x000EADA8
	public void Init()
	{
		Avatar player = Tools.instance.getPlayer();
		this.clear();
		foreach (object obj in this.TempObj.transform.parent)
		{
			Transform transform = (Transform)obj;
			if (transform.gameObject.activeSelf)
			{
				Object.Destroy(transform.gameObject);
			}
		}
		foreach (JSONObject jsonobject in player.LingGuang.list)
		{
			LingGuangCell component = Tools.InstantiateGameObject(this.TempObj.gameObject, this.TempObj.transform.parent).GetComponent<LingGuangCell>();
			component.Info = jsonobject;
			component.init(this.getDesc(jsonobject) + "\n" + this.GetShengYuShiJian(jsonobject));
		}
	}

	// Token: 0x0600226D RID: 8813 RVA: 0x000ECCBC File Offset: 0x000EAEBC
	public void ClickLingGuangCell(JSONObject json)
	{
		int wuXin = (int)Tools.instance.getPlayer().wuXin;
		float num = (8f * (float)Math.Pow(10.0, -6.0) * (float)Math.Pow((double)wuXin, 3.0) - 0.0042f * (float)Math.Pow((double)wuXin, 2.0) + 0.7922f * (float)wuXin - 0.3381f) / 100f + 1f;
		this.NowJson = json;
		int day = (int)(json["studyTime"].n / num);
		this.Name.text = (Tools.Code64(json["name"].str) ?? "");
		this.desc.text = "<color=#DE5900>思绪：</color>" + Tools.Code64(json["desc"].str);
		this.guoshiTime.text = "<color=#DE5900>遗忘：</color>" + Tools.Code64(this.GetShengYuShiJian(json));
		string text = Tools.Code64(jsonData.instance.WuDaoAllTypeJson[json["type"].I.ToString()]["name"].str);
		string text2 = Tools.getStr("ItemColor" + json["quality"].I).Replace("[", "").Replace("]", "");
		this.Type.text = string.Concat(new object[]
		{
			"<color=#DE5900>说明：</color>对",
			text,
			"之道的感悟提升<color=#",
			text2,
			">",
			(float)json["studyTime"].I * jsonData.instance.WuDaoExBeiLuJson["1"]["lingguang" + json["quality"].I].n,
			"</color>点"
		});
		this.GanWuTime.text = string.Format("<color=#DE5900>感悟时间：</color>{0}年{1}月{2}日", Tools.DayToYear(day), Tools.DayToMonth(day), Tools.DayToDay(day));
	}

	// Token: 0x0600226E RID: 8814 RVA: 0x000ECF1C File Offset: 0x000EB11C
	public void GanWu()
	{
		if (this.NowJson != null)
		{
			Avatar player = Tools.instance.getPlayer();
			if (!XiuLian.CheckCanUseDay(player.wuDaoMag.CalcGanWuTime(this.NowJson)))
			{
				UIPopTip.Inst.Pop("房间剩余时间不足，无法感悟", PopTipIconType.叹号);
				return;
			}
			if (!Tools.canClickFlag)
			{
				return;
			}
			Tools.instance.playFader("正在感悟天道...", null);
			player.wuDaoMag.StudyLingGuang(this.NowJson["uuid"].str);
			player.HP = player.HP_Max;
			this.Init();
		}
	}

	// Token: 0x0600226F RID: 8815 RVA: 0x000ECFAF File Offset: 0x000EB1AF
	public void YiWang()
	{
		if (this.NowJson != null)
		{
			Tools.instance.getPlayer().wuDaoMag.removeLingGuang(this.NowJson["uuid"].str);
			this.Init();
		}
	}

	// Token: 0x06002270 RID: 8816 RVA: 0x000ECFE8 File Offset: 0x000EB1E8
	public void clear()
	{
		this.NowJson = null;
		this.Name.text = "";
		this.desc.text = "";
		this.guoshiTime.text = "";
		this.Type.text = "";
		this.GanWuTime.text = "";
	}

	// Token: 0x06002271 RID: 8817 RVA: 0x000ED04C File Offset: 0x000EB24C
	public string GetShengYuShiJian(JSONObject json)
	{
		Avatar player = Tools.instance.getPlayer();
		DateTime endTime = DateTime.Parse(json["startTime"].str).AddDays((double)json["guoqiTime"].I);
		string str = Tools.TimeToShengYuTime(Tools.getShengYuShiJian(player.worldTimeMag.getNowTime(), endTime), "");
		return "这条思绪将在" + str + "后被遗忘。";
	}

	// Token: 0x06002272 RID: 8818 RVA: 0x000ED0BD File Offset: 0x000EB2BD
	public string getDesc(JSONObject info)
	{
		return Tools.Code64(info["desc"].str);
	}

	// Token: 0x06002273 RID: 8819 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04001BD4 RID: 7124
	public LingGuangCell TempObj;

	// Token: 0x04001BD5 RID: 7125
	public UILabel Name;

	// Token: 0x04001BD6 RID: 7126
	public Text desc;

	// Token: 0x04001BD7 RID: 7127
	public Text guoshiTime;

	// Token: 0x04001BD8 RID: 7128
	public Text Type;

	// Token: 0x04001BD9 RID: 7129
	public Text GanWuTime;

	// Token: 0x04001BDA RID: 7130
	public JSONObject NowJson;
}
