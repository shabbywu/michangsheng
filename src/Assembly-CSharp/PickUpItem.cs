using System;
using KBEngine;
using UnityEngine;

// Token: 0x02000146 RID: 326
public class PickUpItem : MonoBehaviour
{
	// Token: 0x06000EB2 RID: 3762 RVA: 0x000599C4 File Offset: 0x00057BC4
	private void Start()
	{
		this._player = (GameObject)KBEngineApp.app.player().renderObj;
		if (this._player != null)
		{
			this._inventory = this._player.GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>();
		}
		base.InvokeRepeating("cheakPlayer", 0.5f, 0.5f);
	}

	// Token: 0x06000EB3 RID: 3763 RVA: 0x00059A2C File Offset: 0x00057C2C
	public void cheakPlayer()
	{
		if (Vector3.Distance(base.gameObject.transform.position, this._player.transform.position) <= 3f)
		{
			if (UI_MainUI.inst.skill1Text.text != "拾取")
			{
				this.nowAttakBtnStatus = 1;
				UI_MainUI.inst.setSkill1("拾取");
				return;
			}
		}
		else if (this.nowAttakBtnStatus == 1)
		{
			UI_MainUI.inst.skill1Text.text = "攻击";
			this.nowAttakBtnStatus = 0;
		}
	}

	// Token: 0x06000EB4 RID: 3764 RVA: 0x00059ABB File Offset: 0x00057CBB
	public void OnEnable()
	{
		UI_Game.ItemPick += this.OnItemPick;
	}

	// Token: 0x06000EB5 RID: 3765 RVA: 0x00059ACE File Offset: 0x00057CCE
	public void OnDisable()
	{
		UI_Game.ItemPick -= this.OnItemPick;
		UI_MainUI.inst.skill1Text.text = "攻击";
	}

	// Token: 0x06000EB6 RID: 3766 RVA: 0x00059AF8 File Offset: 0x00057CF8
	private void OnItemPick()
	{
		if (this._player == null)
		{
			this._player = (GameObject)KBEngineApp.app.player().renderObj;
		}
		if (this._inventory == null && this._player != null)
		{
			this._inventory = this._player.GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>();
		}
		if (this._inventory != null && Vector3.Distance(base.gameObject.transform.position, this._player.transform.position) <= 3f && this._inventory.ItemsInInventory.Count < this._inventory.width * this._inventory.height)
		{
			DroppedItem droppedItem = (DroppedItem)KBEngineApp.app.findEntity(Utility.getPostInt(base.gameObject.name));
			if (droppedItem != null)
			{
				droppedItem.pickUpRequest();
			}
		}
	}

	// Token: 0x04000AE4 RID: 2788
	public Item item;

	// Token: 0x04000AE5 RID: 2789
	private Inventory _inventory;

	// Token: 0x04000AE6 RID: 2790
	private GameObject _player;

	// Token: 0x04000AE7 RID: 2791
	private int nowAttakBtnStatus;
}
