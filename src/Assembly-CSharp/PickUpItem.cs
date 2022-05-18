using System;
using KBEngine;
using UnityEngine;

// Token: 0x0200021B RID: 539
public class PickUpItem : MonoBehaviour
{
	// Token: 0x060010D2 RID: 4306 RVA: 0x000A9D5C File Offset: 0x000A7F5C
	private void Start()
	{
		this._player = (GameObject)KBEngineApp.app.player().renderObj;
		if (this._player != null)
		{
			this._inventory = this._player.GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>();
		}
		base.InvokeRepeating("cheakPlayer", 0.5f, 0.5f);
	}

	// Token: 0x060010D3 RID: 4307 RVA: 0x000A9DC4 File Offset: 0x000A7FC4
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

	// Token: 0x060010D4 RID: 4308 RVA: 0x00010771 File Offset: 0x0000E971
	public void OnEnable()
	{
		UI_Game.ItemPick += this.OnItemPick;
	}

	// Token: 0x060010D5 RID: 4309 RVA: 0x00010784 File Offset: 0x0000E984
	public void OnDisable()
	{
		UI_Game.ItemPick -= this.OnItemPick;
		UI_MainUI.inst.skill1Text.text = "攻击";
	}

	// Token: 0x060010D6 RID: 4310 RVA: 0x000A9E54 File Offset: 0x000A8054
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

	// Token: 0x04000D7F RID: 3455
	public Item item;

	// Token: 0x04000D80 RID: 3456
	private Inventory _inventory;

	// Token: 0x04000D81 RID: 3457
	private GameObject _player;

	// Token: 0x04000D82 RID: 3458
	private int nowAttakBtnStatus;
}
