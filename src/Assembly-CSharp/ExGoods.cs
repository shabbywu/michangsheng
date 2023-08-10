using GUIPackage;
using KBEngine;
using UnityEngine;

public class ExGoods : MonoBehaviour
{
	public int money;

	public GameObject Label;

	public UITexture icon;

	public ShopExchenge_UI shopExchenge_UI;

	public int ExGoodsID = -1;

	public ItemDatebase itemDatebase;

	public bool IsJiaoHuan = true;

	private void Start()
	{
		itemDatebase = ((Component)jsonData.instance).GetComponent<ItemDatebase>();
	}

	private void Update()
	{
		if (ExGoodsID == -1)
		{
			if (shopExchenge_UI.ShopID == -1)
			{
				return;
			}
			JSONObject jSONObject = jsonData.instance.jiaoHuanShopGoods.list.Find((JSONObject aa) => aa["ShopID"].I == shopExchenge_UI.ShopID);
			if (jSONObject != null)
			{
				ExGoodsID = jSONObject["EXGoodsID"].I;
			}
			if (ExGoodsID == -1)
			{
				return;
			}
		}
		Avatar player = Tools.instance.getPlayer();
		if (ExGoodsID == 10035)
		{
			Label.GetComponent<UILabel>().text = player.money.ToString();
		}
		else
		{
			Label.GetComponent<UILabel>().text = player.getItemNum(ExGoodsID).ToString();
		}
		icon.mainTexture = (Texture)(object)itemDatebase.items[ExGoodsID].itemIcon;
	}
}
