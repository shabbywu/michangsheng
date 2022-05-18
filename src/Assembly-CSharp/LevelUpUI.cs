using System;
using KBEngine;
using UnityEngine;

// Token: 0x02000274 RID: 628
public class LevelUpUI : MonoBehaviour
{
	// Token: 0x0600135C RID: 4956 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x0600135D RID: 4957 RVA: 0x000122F6 File Offset: 0x000104F6
	public void close()
	{
		base.transform.localPosition = new Vector3(0f, 1000f, 0f);
	}

	// Token: 0x0600135E RID: 4958 RVA: 0x000B3A28 File Offset: 0x000B1C28
	public void showLevelMax(Avatar avatar)
	{
		this.desc.text = "体内真元逐渐饱和，你的修为已经达到了[ffe118]" + Tools.instance.Code64ToString(jsonData.instance.LevelUpDataJsonData[string.Concat(avatar.level)]["Name"].str) + "[-]的瓶颈，如果无法突破，就再难提升了";
	}

	// Token: 0x0600135F RID: 4959 RVA: 0x000B3A88 File Offset: 0x000B1C88
	public void ShowlevelUpLabel(Avatar avatar, JSONObject jsonInfo, int oldhpmax, int oldshenshi, int oldshouyuan, int oldDunsu)
	{
		this.desc.text = "周边天地的灵气突然开始涌入你的体内，你感到体内的真元犹如沸腾的开水一般，迅速流动起来。灵气的波动足足持续了一个时辰才平息下来，你终于冲破瓶颈，境界提升至[ffe118]" + Tools.instance.Code64ToString(jsonData.instance.LevelUpDataJsonData[string.Concat(avatar.level)]["Name"].str) + "[-]";
		this.qixue.text = "气血: " + oldhpmax;
		this.shenshi.text = "神识: " + oldshenshi;
		this.shouyuan.text = "寿元: " + oldshouyuan;
		this.dunsu.text = "遁速: " + oldDunsu;
		this.qixue.transform.Find("NextLabel").GetComponent<UILabel>().text = string.Concat(avatar.HP_Max);
		this.shenshi.transform.Find("NextLabel").GetComponent<UILabel>().text = string.Concat(avatar.shengShi);
		this.shouyuan.transform.Find("NextLabel").GetComponent<UILabel>().text = string.Concat(avatar.shouYuan);
		this.dunsu.transform.Find("NextLabel").GetComponent<UILabel>().text = string.Concat(avatar.dunSu);
	}

	// Token: 0x06001360 RID: 4960 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x04000F11 RID: 3857
	public UILabel desc;

	// Token: 0x04000F12 RID: 3858
	public UILabel shenshi;

	// Token: 0x04000F13 RID: 3859
	public UILabel qixue;

	// Token: 0x04000F14 RID: 3860
	public UILabel shouyuan;

	// Token: 0x04000F15 RID: 3861
	public UILabel dunsu;
}
