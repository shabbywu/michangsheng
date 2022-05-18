using System;
using KBEngine;
using UnityEngine;

// Token: 0x020005EA RID: 1514
public class UI_KaiFang : MonoBehaviour
{
	// Token: 0x06002600 RID: 9728 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06002601 RID: 9729 RVA: 0x0012D0F8 File Offset: 0x0012B2F8
	public void ClickConfim()
	{
		Avatar player = Tools.instance.getPlayer();
		if ((int)player.money < this.GetUseMoney())
		{
			UIPopTip.Inst.Pop("金币不足", PopTipIconType.叹号);
			return;
		}
		string scenName = this.ScenName;
		int addyear = int.Parse(this.biguan.getInputYear.value);
		int addMonth = int.Parse(this.biguan.getInputMonth.value);
		player.zulinContorl.KZAddTime(scenName, 0, addMonth, addyear);
		player.money -= (ulong)((long)this.GetUseMoney());
		this.biguan.close();
	}

	// Token: 0x06002602 RID: 9730 RVA: 0x0001E5EF File Offset: 0x0001C7EF
	private void Update()
	{
		this.updateCastMoney();
	}

	// Token: 0x06002603 RID: 9731 RVA: 0x0012D194 File Offset: 0x0012B394
	public int GetUseMoney()
	{
		int num = int.Parse(this.biguan.getInputYear.value);
		int num2 = int.Parse(this.biguan.getInputMonth.value);
		return this.castMoney * (12 * num + num2);
	}

	// Token: 0x06002604 RID: 9732 RVA: 0x0012D1DC File Offset: 0x0012B3DC
	public void updateCastMoney()
	{
		if (Tools.instance.getPlayer() == null)
		{
			return;
		}
		int useMoney = this.GetUseMoney();
		string str = string.Concat(useMoney);
		this.castMonsy.text = (((long)useMoney > (long)Tools.instance.getPlayer().money) ? ("[FF0000]" + str) : ("[62C4CB]" + str));
	}

	// Token: 0x04002085 RID: 8325
	public int castMoney = 10;

	// Token: 0x04002086 RID: 8326
	public UIBiGuan biguan;

	// Token: 0x04002087 RID: 8327
	public string ScenName = "";

	// Token: 0x04002088 RID: 8328
	public UILabel castMonsy;

	// Token: 0x04002089 RID: 8329
	public UILabel hasMoney;
}
