using System;
using UnityEngine;

// Token: 0x0200020A RID: 522
public class UIDongFu : MonoBehaviour
{
	// Token: 0x060014EC RID: 5356 RVA: 0x00085E1C File Offset: 0x0008401C
	private void Awake()
	{
		UIDongFu.Inst = this;
	}

	// Token: 0x060014ED RID: 5357 RVA: 0x00085E24 File Offset: 0x00084024
	public void InitData()
	{
		this.DongFu = new DongFuData(DongFuManager.NowDongFuID);
		this.DongFu.Load();
	}

	// Token: 0x060014EE RID: 5358 RVA: 0x00085E41 File Offset: 0x00084041
	public void ShowLingTianPanel()
	{
		this.InitData();
		this.ScaleObj.SetActive(true);
		this.LingTian.gameObject.SetActive(true);
		this.LingTian.RefreshUI();
		ESCCloseManager.Inst.RegisterClose(this.LingTian);
	}

	// Token: 0x060014EF RID: 5359 RVA: 0x00085E81 File Offset: 0x00084081
	public void HideLingTianPanel()
	{
		this.LingTian.gameObject.SetActive(false);
		this.ScaleObj.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this.LingTian);
	}

	// Token: 0x060014F0 RID: 5360 RVA: 0x00085EB0 File Offset: 0x000840B0
	public void ShowJuLingZhenPanel()
	{
		this.InitData();
		this.ScaleObj.SetActive(true);
		this.JuLingZhen.gameObject.SetActive(true);
		this.JuLingZhen.RefreshUI();
		ESCCloseManager.Inst.RegisterClose(this.JuLingZhen);
	}

	// Token: 0x060014F1 RID: 5361 RVA: 0x00085EF0 File Offset: 0x000840F0
	public void HideJuLingZhenPanel()
	{
		this.JuLingZhen.gameObject.SetActive(false);
		this.ScaleObj.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this.JuLingZhen);
	}

	// Token: 0x04000FA2 RID: 4002
	public static UIDongFu Inst;

	// Token: 0x04000FA3 RID: 4003
	public GameObject ScaleObj;

	// Token: 0x04000FA4 RID: 4004
	public UILingTianPanel LingTian;

	// Token: 0x04000FA5 RID: 4005
	public UIJuLingZhenPanel JuLingZhen;

	// Token: 0x04000FA6 RID: 4006
	[HideInInspector]
	public DongFuData DongFu;
}
