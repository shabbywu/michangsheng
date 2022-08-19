using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000318 RID: 792
public class LunDaoHuiHe : MonoBehaviour
{
	// Token: 0x06001B7F RID: 7039 RVA: 0x000C3ECA File Offset: 0x000C20CA
	public void Init()
	{
		this.totalHui = 5;
		this.curHui = 1;
		this.shengYuHuiHe = this.totalHui - this.curHui;
		this.upDateHuiHeText();
	}

	// Token: 0x06001B80 RID: 7040 RVA: 0x000C3EF3 File Offset: 0x000C20F3
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

	// Token: 0x06001B81 RID: 7041 RVA: 0x000C3F31 File Offset: 0x000C2131
	private void upDateHuiHeText()
	{
		this.curHuiText.text = this.curHui.ToCNNumber();
		this.shengYuHuiHeText.text = "(剩余" + this.shengYuHuiHe.ToCNNumber() + "回合)";
	}

	// Token: 0x040015FC RID: 5628
	public int totalHui;

	// Token: 0x040015FD RID: 5629
	public int curHui;

	// Token: 0x040015FE RID: 5630
	public int shengYuHuiHe;

	// Token: 0x040015FF RID: 5631
	[SerializeField]
	private Text curHuiText;

	// Token: 0x04001600 RID: 5632
	[SerializeField]
	private Text shengYuHuiHeText;
}
