using System;
using UnityEngine;

// Token: 0x02000320 RID: 800
public class UIDongFu : MonoBehaviour
{
	// Token: 0x06001798 RID: 6040 RVA: 0x00014D54 File Offset: 0x00012F54
	private void Awake()
	{
		UIDongFu.Inst = this;
	}

	// Token: 0x06001799 RID: 6041 RVA: 0x00014D5C File Offset: 0x00012F5C
	public void InitData()
	{
		this.DongFu = new DongFuData(DongFuManager.NowDongFuID);
		this.DongFu.Load();
	}

	// Token: 0x0600179A RID: 6042 RVA: 0x00014D79 File Offset: 0x00012F79
	public void ShowLingTianPanel()
	{
		this.InitData();
		this.ScaleObj.SetActive(true);
		this.LingTian.gameObject.SetActive(true);
		this.LingTian.RefreshUI();
		ESCCloseManager.Inst.RegisterClose(this.LingTian);
	}

	// Token: 0x0600179B RID: 6043 RVA: 0x00014DB9 File Offset: 0x00012FB9
	public void HideLingTianPanel()
	{
		this.LingTian.gameObject.SetActive(false);
		this.ScaleObj.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this.LingTian);
	}

	// Token: 0x0600179C RID: 6044 RVA: 0x00014DE8 File Offset: 0x00012FE8
	public void ShowJuLingZhenPanel()
	{
		this.InitData();
		this.ScaleObj.SetActive(true);
		this.JuLingZhen.gameObject.SetActive(true);
		this.JuLingZhen.RefreshUI();
		ESCCloseManager.Inst.RegisterClose(this.JuLingZhen);
	}

	// Token: 0x0600179D RID: 6045 RVA: 0x00014E28 File Offset: 0x00013028
	public void HideJuLingZhenPanel()
	{
		this.JuLingZhen.gameObject.SetActive(false);
		this.ScaleObj.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this.JuLingZhen);
	}

	// Token: 0x040012EA RID: 4842
	public static UIDongFu Inst;

	// Token: 0x040012EB RID: 4843
	public GameObject ScaleObj;

	// Token: 0x040012EC RID: 4844
	public UILingTianPanel LingTian;

	// Token: 0x040012ED RID: 4845
	public UIJuLingZhenPanel JuLingZhen;

	// Token: 0x040012EE RID: 4846
	[HideInInspector]
	public DongFuData DongFu;
}
