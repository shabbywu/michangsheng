using System;
using KBEngine;
using UnityEngine;

// Token: 0x0200018D RID: 397
public class LevelUpUI : MonoBehaviour
{
	// Token: 0x060010F2 RID: 4338 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x060010F3 RID: 4339 RVA: 0x00065233 File Offset: 0x00063433
	public void close()
	{
		base.transform.localPosition = new Vector3(0f, 1000f, 0f);
	}

	// Token: 0x060010F4 RID: 4340 RVA: 0x00065254 File Offset: 0x00063454
	public void showLevelMax(Avatar avatar)
	{
		this.desc.text = "体内真元逐渐饱和，你的修为已经达到了[ffe118]" + Tools.instance.Code64ToString(jsonData.instance.LevelUpDataJsonData[string.Concat(avatar.level)]["Name"].str) + "[-]的瓶颈，如果无法突破，就再难提升了";
	}

	// Token: 0x060010F5 RID: 4341 RVA: 0x000652B4 File Offset: 0x000634B4
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

	// Token: 0x060010F6 RID: 4342 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04000C2A RID: 3114
	public UILabel desc;

	// Token: 0x04000C2B RID: 3115
	public UILabel shenshi;

	// Token: 0x04000C2C RID: 3116
	public UILabel qixue;

	// Token: 0x04000C2D RID: 3117
	public UILabel shouyuan;

	// Token: 0x04000C2E RID: 3118
	public UILabel dunsu;
}
