using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200047B RID: 1147
public class LunDaoHuiHe : MonoBehaviour
{
	// Token: 0x06001EB2 RID: 7858 RVA: 0x000196BF File Offset: 0x000178BF
	public void Init()
	{
		this.totalHui = 5;
		this.curHui = 1;
		this.shengYuHuiHe = this.totalHui - this.curHui;
		this.upDateHuiHeText();
	}

	// Token: 0x06001EB3 RID: 7859 RVA: 0x000196E8 File Offset: 0x000178E8
	public void ReduceHuiHe()
	{
		this.shengYuHuiHe--;
		this.curHui++;
		if (this.curHui > this.totalHui)
		{
			LunDaoManager.inst.gameState = LunDaoManager.GameState.论道结束;
			return;
		}
		this.upDateHuiHeText();
	}

	// Token: 0x06001EB4 RID: 7860 RVA: 0x00019726 File Offset: 0x00017926
	private void upDateHuiHeText()
	{
		this.curHuiText.text = this.curHui.ToCNNumber();
		this.shengYuHuiHeText.text = "(剩余" + this.shengYuHuiHe.ToCNNumber() + "回合)";
	}

	// Token: 0x04001A17 RID: 6679
	public int totalHui;

	// Token: 0x04001A18 RID: 6680
	public int curHui;

	// Token: 0x04001A19 RID: 6681
	public int shengYuHuiHe;

	// Token: 0x04001A1A RID: 6682
	[SerializeField]
	private Text curHuiText;

	// Token: 0x04001A1B RID: 6683
	[SerializeField]
	private Text shengYuHuiHeText;
}
