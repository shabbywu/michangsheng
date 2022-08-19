using System;
using KBEngine;
using UnityEngine;

// Token: 0x020003F3 RID: 1011
public class UI_Deng : MonoBehaviour
{
	// Token: 0x060020A5 RID: 8357 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x060020A6 RID: 8358 RVA: 0x000E603C File Offset: 0x000E423C
	public void SetHPDeng()
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		this.text.text = avatar.HP + "/" + avatar.HP_Max;
		this.SetHuo(avatar.HP, avatar.HP_Max);
		this.SetDengZhao(avatar.HP, avatar.HP_Max);
	}

	// Token: 0x060020A7 RID: 8359 RVA: 0x000E60A8 File Offset: 0x000E42A8
	public void SetEXPDeng()
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		ulong num = 99999999999UL;
		if (jsonData.instance.LevelUpDataJsonData[string.Concat(avatar.level)] != null)
		{
			num = (ulong)jsonData.instance.LevelUpDataJsonData[string.Concat(avatar.level)]["MaxExp"].n;
		}
		string str = num.ToCNNumberWithUnit();
		string str2 = avatar.exp.ToCNNumberWithUnit();
		this.text.text = str2 + "/" + str;
		this.SetHuo((int)avatar.exp, (int)num);
		this.SetDengZhao((int)avatar.exp, (int)num);
	}

	// Token: 0x060020A8 RID: 8360 RVA: 0x000E6168 File Offset: 0x000E4368
	public void SetDengZhao(int num, int Max)
	{
		this.dengzhao.alpha = (float)num / (float)Max;
	}

	// Token: 0x060020A9 RID: 8361 RVA: 0x000E617A File Offset: 0x000E437A
	public void SetHuo(int num, int Max)
	{
		this.huo.transform.localScale = Vector3.one * (0.5f + (float)num / (float)Max / 2f);
	}

	// Token: 0x060020AA RID: 8362 RVA: 0x000E61A7 File Offset: 0x000E43A7
	private void Update()
	{
		if (this.type == 1)
		{
			this.SetHPDeng();
			return;
		}
		this.SetEXPDeng();
	}

	// Token: 0x04001A8A RID: 6794
	public int type = 1;

	// Token: 0x04001A8B RID: 6795
	public UI2DSprite dengzhao;

	// Token: 0x04001A8C RID: 6796
	public UI2DSprite huo;

	// Token: 0x04001A8D RID: 6797
	public UILabel text;
}
