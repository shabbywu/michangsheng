using GUIPackage;
using UnityEngine;

public class openDanlu : MonoBehaviour
{
	public UILabel label;

	public ItemCellEX ItemCellEX;

	private void Start()
	{
	}

	private void OnPress()
	{
		LianDanMag.instence.showChoiceDanLu();
	}

	private void Update()
	{
		if (ItemCellEX.inventory.inventory[0].itemID != -1 && ItemCellEX.inventory.inventory[0].Seid.HasField("NaiJiu"))
		{
			label.text = (int)ItemCellEX.inventory.inventory[0].Seid["NaiJiu"].n + "/" + 100;
		}
	}
}
