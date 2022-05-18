using System;
using KBEngine;
using UnityEngine;

// Token: 0x020005A2 RID: 1442
public class UI_Deng : MonoBehaviour
{
	// Token: 0x06002457 RID: 9303 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06002458 RID: 9304 RVA: 0x001282F4 File Offset: 0x001264F4
	public void SetHPDeng()
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		this.text.text = avatar.HP + "/" + avatar.HP_Max;
		this.SetHuo(avatar.HP, avatar.HP_Max);
		this.SetDengZhao(avatar.HP, avatar.HP_Max);
	}

	// Token: 0x06002459 RID: 9305 RVA: 0x00128360 File Offset: 0x00126560
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

	// Token: 0x0600245A RID: 9306 RVA: 0x0001D3D9 File Offset: 0x0001B5D9
	public void SetDengZhao(int num, int Max)
	{
		this.dengzhao.alpha = (float)num / (float)Max;
	}

	// Token: 0x0600245B RID: 9307 RVA: 0x0001D3EB File Offset: 0x0001B5EB
	public void SetHuo(int num, int Max)
	{
		this.huo.transform.localScale = Vector3.one * (0.5f + (float)num / (float)Max / 2f);
	}

	// Token: 0x0600245C RID: 9308 RVA: 0x0001D418 File Offset: 0x0001B618
	private void Update()
	{
		if (this.type == 1)
		{
			this.SetHPDeng();
			return;
		}
		this.SetEXPDeng();
	}

	// Token: 0x04001F43 RID: 8003
	public int type = 1;

	// Token: 0x04001F44 RID: 8004
	public UI2DSprite dengzhao;

	// Token: 0x04001F45 RID: 8005
	public UI2DSprite huo;

	// Token: 0x04001F46 RID: 8006
	public UILabel text;
}
