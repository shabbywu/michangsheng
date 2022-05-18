using System;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020005F1 RID: 1521
public class LingGuangMag : MonoBehaviour
{
	// Token: 0x06002629 RID: 9769 RVA: 0x0001E74C File Offset: 0x0001C94C
	private void Start()
	{
		Event.registerOut("ClickLingGuangCell", this, "ClickLingGuangCell");
	}

	// Token: 0x0600262A RID: 9770 RVA: 0x0001429C File Offset: 0x0001249C
	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	// Token: 0x0600262B RID: 9771 RVA: 0x0012DB58 File Offset: 0x0012BD58
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

	// Token: 0x0600262C RID: 9772 RVA: 0x0012DC6C File Offset: 0x0012BE6C
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

	// Token: 0x0600262D RID: 9773 RVA: 0x0012DECC File Offset: 0x0012C0CC
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

	// Token: 0x0600262E RID: 9774 RVA: 0x0001E75F File Offset: 0x0001C95F
	public void YiWang()
	{
		if (this.NowJson != null)
		{
			Tools.instance.getPlayer().wuDaoMag.removeLingGuang(this.NowJson["uuid"].str);
			this.Init();
		}
	}

	// Token: 0x0600262F RID: 9775 RVA: 0x0012DF60 File Offset: 0x0012C160
	public void clear()
	{
		this.NowJson = null;
		this.Name.text = "";
		this.desc.text = "";
		this.guoshiTime.text = "";
		this.Type.text = "";
		this.GanWuTime.text = "";
	}

	// Token: 0x06002630 RID: 9776 RVA: 0x0012DFC4 File Offset: 0x0012C1C4
	public string GetShengYuShiJian(JSONObject json)
	{
		Avatar player = Tools.instance.getPlayer();
		DateTime endTime = DateTime.Parse(json["startTime"].str).AddDays((double)json["guoqiTime"].I);
		string str = Tools.TimeToShengYuTime(Tools.getShengYuShiJian(player.worldTimeMag.getNowTime(), endTime), "");
		return "这条思绪将在" + str + "后被遗忘。";
	}

	// Token: 0x06002631 RID: 9777 RVA: 0x0001E798 File Offset: 0x0001C998
	public string getDesc(JSONObject info)
	{
		return Tools.Code64(info["desc"].str);
	}

	// Token: 0x06002632 RID: 9778 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x040020A0 RID: 8352
	public LingGuangCell TempObj;

	// Token: 0x040020A1 RID: 8353
	public UILabel Name;

	// Token: 0x040020A2 RID: 8354
	public Text desc;

	// Token: 0x040020A3 RID: 8355
	public Text guoshiTime;

	// Token: 0x040020A4 RID: 8356
	public Text Type;

	// Token: 0x040020A5 RID: 8357
	public Text GanWuTime;

	// Token: 0x040020A6 RID: 8358
	public JSONObject NowJson;
}
