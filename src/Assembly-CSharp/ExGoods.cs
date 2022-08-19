using System;
using GUIPackage;
using KBEngine;
using UnityEngine;

// Token: 0x020004AB RID: 1195
public class ExGoods : MonoBehaviour
{
	// Token: 0x0600259B RID: 9627 RVA: 0x00104418 File Offset: 0x00102618
	private void Start()
	{
		this.itemDatebase = jsonData.instance.GetComponent<ItemDatebase>();
	}

	// Token: 0x0600259C RID: 9628 RVA: 0x0010442C File Offset: 0x0010262C
	private void Update()
	{
		if (this.ExGoodsID == -1)
		{
			if (this.shopExchenge_UI.ShopID == -1)
			{
				return;
			}
			JSONObject jsonobject = jsonData.instance.jiaoHuanShopGoods.list.Find((JSONObject aa) => aa["ShopID"].I == this.shopExchenge_UI.ShopID);
			if (jsonobject != null)
			{
				this.ExGoodsID = jsonobject["EXGoodsID"].I;
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

	// Token: 0x04001E59 RID: 7769
	public int money;

	// Token: 0x04001E5A RID: 7770
	public GameObject Label;

	// Token: 0x04001E5B RID: 7771
	public UITexture icon;

	// Token: 0x04001E5C RID: 7772
	public ShopExchenge_UI shopExchenge_UI;

	// Token: 0x04001E5D RID: 7773
	public int ExGoodsID = -1;

	// Token: 0x04001E5E RID: 7774
	public ItemDatebase itemDatebase;

	// Token: 0x04001E5F RID: 7775
	public bool IsJiaoHuan = true;
}
