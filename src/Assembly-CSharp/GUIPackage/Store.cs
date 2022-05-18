using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000D9D RID: 3485
	public class Store : MonoBehaviour
	{
		// Token: 0x0600541E RID: 21534 RVA: 0x0003C2B2 File Offset: 0x0003A4B2
		private void Start()
		{
			this.InitNumInput();
			this.HideNumInput();
			this.datebase = jsonData.instance.gameObject.GetComponent<ItemDatebase>();
			this.initStore();
		}

		// Token: 0x0600541F RID: 21535 RVA: 0x0003C2DB File Offset: 0x0003A4DB
		private void Update()
		{
			this.SetMaxShop();
		}

		// Token: 0x06005420 RID: 21536 RVA: 0x00230CA4 File Offset: 0x0022EEA4
		private void InitNumInput()
		{
			this.Sure = base.transform.Find("Win/NumInput/Sure").gameObject;
			this.Cancel = base.transform.Find("Win/NumInput/Cancel").gameObject;
			UIEventListener.Get(this.Sure).onClick = new UIEventListener.VoidDelegate(this.SureClick);
			UIEventListener.Get(this.Cancel).onClick = new UIEventListener.VoidDelegate(this.CancelClick);
		}

		// Token: 0x06005421 RID: 21537 RVA: 0x0003C2E3 File Offset: 0x0003A4E3
		private void SureClick(GameObject button)
		{
			if (this.NumInput.GetComponentInChildren<UIInput>().value != "" && this.num > 0)
			{
				this.Shop_Item(this.ShopID);
				this.HideNumInput();
			}
		}

		// Token: 0x06005422 RID: 21538 RVA: 0x00230D20 File Offset: 0x0022EF20
		private void SetMaxShop()
		{
			if (this.NumInput.activeSelf && this.NumInput.GetComponentInChildren<UIInput>().value != "")
			{
				this.num = int.Parse(this.NumInput.GetComponentInChildren<UIInput>().value);
				if (this.num > 0)
				{
					if (this.store[this.ShopID].itemType == item.ItemType.Potion)
					{
						if (this.num > this.store[this.ShopID].itemMaxNum)
						{
							this.NumInput.GetComponentInChildren<UIInput>().value = this.store[this.ShopID].itemMaxNum.ToString();
							return;
						}
					}
					else if (this.num > Singleton.inventory.GetSoltNum())
					{
						this.NumInput.GetComponentInChildren<UIInput>().value = Singleton.inventory.GetSoltNum().ToString();
					}
				}
			}
		}

		// Token: 0x06005423 RID: 21539 RVA: 0x0003C31C File Offset: 0x0003A51C
		private void CancelClick(GameObject button)
		{
			this.HideNumInput();
		}

		// Token: 0x06005424 RID: 21540 RVA: 0x0003C324 File Offset: 0x0003A524
		private void initStore()
		{
			base.GetComponentInChildren<UIGrid>().repositionNow = true;
			base.GetComponentInChildren<UIScrollView>().Press(true);
			this.StoreUI.SetActive(false);
			this.Notice.SetActive(false);
		}

		// Token: 0x06005425 RID: 21541 RVA: 0x000042DD File Offset: 0x000024DD
		private void OnGUI()
		{
		}

		// Token: 0x06005426 RID: 21542 RVA: 0x00230E1C File Offset: 0x0022F01C
		private void Show()
		{
			this.Show_Store = !this.Show_Store;
			if (!this.Show_Store)
			{
				Singleton.inventory.showTooltip = false;
			}
			if (this.Show_Store)
			{
				base.transform.Find("Win").position = base.transform.position;
			}
			this.StoreUI.SetActive(this.Show_Store);
			Singleton.UI.UI_Top(this.StoreUI.transform.parent);
		}

		// Token: 0x06005427 RID: 21543 RVA: 0x0003C356 File Offset: 0x0003A556
		public void ShowNumInput(int id)
		{
			this.NumInput.SetActive(true);
			this.ShopID = id;
			if (this.store[id].itemType != item.ItemType.Potion)
			{
				this.NumInput.GetComponentInChildren<UIInput>().value = "1";
			}
		}

		// Token: 0x06005428 RID: 21544 RVA: 0x0003C394 File Offset: 0x0003A594
		public void HideNumInput()
		{
			this.NumInput.SetActive(false);
		}

		// Token: 0x06005429 RID: 21545 RVA: 0x00230EA0 File Offset: 0x0022F0A0
		public void Shop_Item(int ID)
		{
			if (Singleton.inventory.is_Full(this.store[ID], this.num))
			{
				base.StartCoroutine(this.ShowNotice("背包已满"));
				return;
			}
			if (Singleton.money.money >= this.store[ID].itemPrice * this.num)
			{
				for (int i = 0; i < this.num; i++)
				{
					Singleton.inventory.AddItem(this.store[ID].itemID);
					Tools.instance.getPlayer().addItem(this.store[ID].itemID, this.store[ID].Seid, 1);
				}
				Singleton.money.Set_money(Singleton.money.money - this.store[ID].itemPrice * this.num, false);
				return;
			}
			base.StartCoroutine(this.ShowNotice("金币不足,无法购买"));
		}

		// Token: 0x0600542A RID: 21546 RVA: 0x0003C3A2 File Offset: 0x0003A5A2
		private IEnumerator ShowNotice(string s)
		{
			this.Notice.SetActive(true);
			this.Notice.GetComponentInChildren<UILabel>().text = "提示:" + s;
			yield return new WaitForSeconds(3f);
			this.Notice.SetActive(false);
			yield break;
		}

		// Token: 0x040053DA RID: 21466
		public List<item> store = new List<item>();

		// Token: 0x040053DB RID: 21467
		public GameObject StoreUI;

		// Token: 0x040053DC RID: 21468
		public GameObject NumInput;

		// Token: 0x040053DD RID: 21469
		public GameObject Sure;

		// Token: 0x040053DE RID: 21470
		public GameObject Cancel;

		// Token: 0x040053DF RID: 21471
		public GameObject Notice;

		// Token: 0x040053E0 RID: 21472
		private ItemDatebase datebase;

		// Token: 0x040053E1 RID: 21473
		private bool Show_Store;

		// Token: 0x040053E2 RID: 21474
		private int ShopID;

		// Token: 0x040053E3 RID: 21475
		private int num = 1;
	}
}
