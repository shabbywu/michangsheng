using System;
using GUIPackage;
using KBEngine;
using UnityEngine;

// Token: 0x0200068B RID: 1675
public class ExGoods : MonoBehaviour
{
	// Token: 0x060029D7 RID: 10711 RVA: 0x0002083E File Offset: 0x0001EA3E
	private void Start()
	{
		this.itemDatebase = jsonData.instance.GetComponent<ItemDatebase>();
	}

	// Token: 0x060029D8 RID: 10712 RVA: 0x00143F58 File Offset: 0x00142158
	private void Update()
	{
		if (this.ExGoodsID == -1)
		{
			if (this.shopExchenge_UI.ShopID == -1)
			{
				return;
			}
			JSONObject jsonobject = jsonData.instance.jiaoHuanShopGoods.list.Find((JSONObject aa) => (int)aa["ShopID"].n == this.shopExchenge_UI.ShopID);
			if (jsonobject != null)
			{
				this.ExGoodsID = (int)jsonobject["EXGoodsID"].n;
			}
			if (this.ExGoodsID == -1)
			{
				return;
			}
		}
		Avatar player = Tools.instance.getPlayer();
		if (this.ExGoodsID == 10035)
		{
			this.Label.GetComponent<UILabel>().text = player.money.ToString();
		}
		else
		{
			this.Label.GetComponent<UILabel>().text = player.getItemNum(this.ExGoodsID).ToString();
		}
		this.icon.mainTexture = this.itemDatebase.items[this.ExGoodsID].itemIcon;
	}

	// Token: 0x0400237F RID: 9087
	public int money;

	// Token: 0x04002380 RID: 9088
	public GameObject Label;

	// Token: 0x04002381 RID: 9089
	public UITexture icon;

	// Token: 0x04002382 RID: 9090
	public ShopExchenge_UI shopExchenge_UI;

	// Token: 0x04002383 RID: 9091
	public int ExGoodsID = -1;

	// Token: 0x04002384 RID: 9092
	public ItemDatebase itemDatebase;

	// Token: 0x04002385 RID: 9093
	public bool IsJiaoHuan = true;
}
