using System;
using KBEngine;
using UnityEngine;

// Token: 0x02000433 RID: 1075
public class UI_KaiFang : MonoBehaviour
{
	// Token: 0x06002241 RID: 8769 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06002242 RID: 8770 RVA: 0x000EBFD8 File Offset: 0x000EA1D8
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

	// Token: 0x06002243 RID: 8771 RVA: 0x000EC071 File Offset: 0x000EA271
	private void Update()
	{
		this.updateCastMoney();
	}

	// Token: 0x06002244 RID: 8772 RVA: 0x000EC07C File Offset: 0x000EA27C
	public int GetUseMoney()
	{
		int num = int.Parse(this.biguan.getInputYear.value);
		int num2 = int.Parse(this.biguan.getInputMonth.value);
		return this.castMoney * (12 * num + num2);
	}

	// Token: 0x06002245 RID: 8773 RVA: 0x000EC0C4 File Offset: 0x000EA2C4
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

	// Token: 0x04001BB9 RID: 7097
	public int castMoney = 10;

	// Token: 0x04001BBA RID: 7098
	public UIBiGuan biguan;

	// Token: 0x04001BBB RID: 7099
	public string ScenName = "";

	// Token: 0x04001BBC RID: 7100
	public UILabel castMonsy;

	// Token: 0x04001BBD RID: 7101
	public UILabel hasMoney;
}
