using System;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200047A RID: 1146
public class HuaShenManager : MonoBehaviour
{
	// Token: 0x060023C7 RID: 9159 RVA: 0x000F4C25 File Offset: 0x000F2E25
	private void Awake()
	{
		HuaShenManager.Inst = this;
		this.player = PlayerEx.Player;
	}

	// Token: 0x060023C8 RID: 9160 RVA: 0x000F4C38 File Offset: 0x000F2E38
	private void Update()
	{
		this.RefreshUI();
	}

	// Token: 0x060023C9 RID: 9161 RVA: 0x000F4C40 File Offset: 0x000F2E40
	public void RefreshUI()
	{
		this.FanXingShow.SetNumber(this.player.buffmag.GetBuffSum(3132));
		this.XianXingShow.SetNumber(this.player.buffmag.GetBuffSum(3133));
		this.CuiTiShow.SetNumber(this.player.buffmag.GetBuffSum(3134));
		this.SuHunShow.SetNumber(this.player.buffmag.GetBuffSum(3135));
		int buffSum = this.player.buffmag.GetBuffSum(3130);
		int buffSum2 = this.player.buffmag.GetBuffSum(3131);
		if (buffSum > 0)
		{
			if (this.isXianTai)
			{
				this.StartFanTiAnim();
				return;
			}
		}
		else if (buffSum2 > 0 && !this.isXianTai)
		{
			this.StartXianTaiAnim();
		}
	}

	// Token: 0x060023CA RID: 9162 RVA: 0x000F4D1C File Offset: 0x000F2F1C
	public void StartFanTiAnim()
	{
		Debug.Log("切换到凡体");
		this.isXianTai = false;
		this.TaiText.text = "凡 体";
		if (this.animChangeCount > 0)
		{
			this.SceneAnimator.Play("HuaShenXianTaiToFanTiAnim");
		}
		this.animChangeCount++;
	}

	// Token: 0x060023CB RID: 9163 RVA: 0x000F4D74 File Offset: 0x000F2F74
	public void StartXianTaiAnim()
	{
		Debug.Log("切换到仙胎");
		this.isXianTai = true;
		this.TaiText.text = "仙 胎";
		this.SceneAnimator.Play("HuaShenFanTiToXianTaiAnim");
		this.animChangeCount++;
	}

	// Token: 0x060023CC RID: 9164 RVA: 0x000F4DC0 File Offset: 0x000F2FC0
	private void OnDestroy()
	{
		HuaShenManager.Inst = null;
	}

	// Token: 0x04001C99 RID: 7321
	public static HuaShenManager Inst;

	// Token: 0x04001C9A RID: 7322
	public UIHuaShenBuffShow FanXingShow;

	// Token: 0x04001C9B RID: 7323
	public UIHuaShenBuffShow XianXingShow;

	// Token: 0x04001C9C RID: 7324
	public UIHuaShenBuffShow CuiTiShow;

	// Token: 0x04001C9D RID: 7325
	public UIHuaShenBuffShow SuHunShow;

	// Token: 0x04001C9E RID: 7326
	public Text TaiText;

	// Token: 0x04001C9F RID: 7327
	public Animator SceneAnimator;

	// Token: 0x04001CA0 RID: 7328
	private Avatar player;

	// Token: 0x04001CA1 RID: 7329
	private bool isXianTai = true;

	// Token: 0x04001CA2 RID: 7330
	private int animChangeCount;
}
