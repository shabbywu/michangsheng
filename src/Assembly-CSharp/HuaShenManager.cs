using System;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000638 RID: 1592
public class HuaShenManager : MonoBehaviour
{
	// Token: 0x06002784 RID: 10116 RVA: 0x0001F498 File Offset: 0x0001D698
	private void Awake()
	{
		HuaShenManager.Inst = this;
		this.player = PlayerEx.Player;
	}

	// Token: 0x06002785 RID: 10117 RVA: 0x0001F4AB File Offset: 0x0001D6AB
	private void Update()
	{
		this.RefreshUI();
	}

	// Token: 0x06002786 RID: 10118 RVA: 0x00134AFC File Offset: 0x00132CFC
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

	// Token: 0x06002787 RID: 10119 RVA: 0x00134BD8 File Offset: 0x00132DD8
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

	// Token: 0x06002788 RID: 10120 RVA: 0x00134C30 File Offset: 0x00132E30
	public void StartXianTaiAnim()
	{
		Debug.Log("切换到仙胎");
		this.isXianTai = true;
		this.TaiText.text = "仙 胎";
		this.SceneAnimator.Play("HuaShenFanTiToXianTaiAnim");
		this.animChangeCount++;
	}

	// Token: 0x06002789 RID: 10121 RVA: 0x0001F4B3 File Offset: 0x0001D6B3
	private void OnDestroy()
	{
		HuaShenManager.Inst = null;
	}

	// Token: 0x04002174 RID: 8564
	public static HuaShenManager Inst;

	// Token: 0x04002175 RID: 8565
	public UIHuaShenBuffShow FanXingShow;

	// Token: 0x04002176 RID: 8566
	public UIHuaShenBuffShow XianXingShow;

	// Token: 0x04002177 RID: 8567
	public UIHuaShenBuffShow CuiTiShow;

	// Token: 0x04002178 RID: 8568
	public UIHuaShenBuffShow SuHunShow;

	// Token: 0x04002179 RID: 8569
	public Text TaiText;

	// Token: 0x0400217A RID: 8570
	public Animator SceneAnimator;

	// Token: 0x0400217B RID: 8571
	private Avatar player;

	// Token: 0x0400217C RID: 8572
	private bool isXianTai = true;

	// Token: 0x0400217D RID: 8573
	private int animChangeCount;
}
