using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000A6F RID: 2671
	public class Store : MonoBehaviour
	{
		// Token: 0x06004B10 RID: 19216 RVA: 0x001FEEE5 File Offset: 0x001FD0E5
		private void Start()
		{
			this.InitNumInput();
			this.HideNumInput();
			this.datebase = jsonData.instance.gameObject.GetComponent<ItemDatebase>();
			this.initStore();
		}

		// Token: 0x06004B11 RID: 19217 RVA: 0x001FEF0E File Offset: 0x001FD10E
		private void Update()
		{
			this.SetMaxShop();
		}

		// Token: 0x06004B12 RID: 19218 RVA: 0x001FEF18 File Offset: 0x001FD118
		private void InitNumInput()
		{
			this.Sure = base.transform.Find("Win/NumInput/Sure").gameObject;
			this.Cancel = base.transform.Find("Win/NumInput/Cancel").gameObject;
			UIEventListener.Get(this.Sure).onClick = new UIEventListener.VoidDelegate(this.SureClick);
			UIEventListener.Get(this.Cancel).onClick = new UIEventListener.VoidDelegate(this.CancelClick);
		}

		// Token: 0x06004B13 RID: 19219 RVA: 0x001FEF93 File Offset: 0x001FD193
		private void SureClick(GameObject button)
		{
			if (this.NumInput.GetComponentInChildren<UIInput>().value != "" && this.num > 0)
			{
				this.Shop_Item(this.ShopID);
				this.HideNumInput();
			}
		}

		// Token: 0x06004B14 RID: 19220 RVA: 0x001FEFCC File Offset: 0x001FD1CC
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

		// Token: 0x06004B15 RID: 19221 RVA: 0x001FF0C6 File Offset: 0x001FD2C6
		private void CancelClick(GameObject button)
		{
			this.HideNumInput();
		}

		// Token: 0x06004B16 RID: 19222 RVA: 0x001FF0CE File Offset: 0x001FD2CE
		private void initStore()
		{
			base.GetComponentInChildren<UIGrid>().repositionNow = true;
			base.GetComponentInChildren<UIScrollView>().Press(true);
			this.StoreUI.SetActive(false);
			this.Notice.SetActive(false);
		}

		// Token: 0x06004B17 RID: 19223 RVA: 0x00004095 File Offset: 0x00002295
		private void OnGUI()
		{
		}

		// Token: 0x06004B18 RID: 19224 RVA: 0x001FF100 File Offset: 0x001FD300
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

		// Token: 0x06004B19 RID: 19225 RVA: 0x001FF182 File Offset: 0x001FD382
		public void ShowNumInput(int id)
		{
			this.NumInput.SetActive(true);
			this.ShopID = id;
			if (this.store[id].itemType != item.ItemType.Potion)
			{
				this.NumInput.GetComponentInChildren<UIInput>().value = "1";
			}
		}

		// Token: 0x06004B1A RID: 19226 RVA: 0x001FF1C0 File Offset: 0x001FD3C0
		public void HideNumInput()
		{
			this.NumInput.SetActive(false);
		}

		// Token: 0x06004B1B RID: 19227 RVA: 0x001FF1D0 File Offset: 0x001FD3D0
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

		// Token: 0x06004B1C RID: 19228 RVA: 0x001FF2D7 File Offset: 0x001FD4D7
		private IEnumerator ShowNotice(string s)
		{
			this.Notice.SetActive(true);
			this.Notice.GetComponentInChildren<UILabel>().text = "提示:" + s;
			yield return new WaitForSeconds(3f);
			this.Notice.SetActive(false);
			yield break;
		}

		// Token: 0x04004A39 RID: 19001
		public List<item> store = new List<item>();

		// Token: 0x04004A3A RID: 19002
		public GameObject StoreUI;

		// Token: 0x04004A3B RID: 19003
		public GameObject NumInput;

		// Token: 0x04004A3C RID: 19004
		public GameObject Sure;

		// Token: 0x04004A3D RID: 19005
		public GameObject Cancel;

		// Token: 0x04004A3E RID: 19006
		public GameObject Notice;

		// Token: 0x04004A3F RID: 19007
		private ItemDatebase datebase;

		// Token: 0x04004A40 RID: 19008
		private bool Show_Store;

		// Token: 0x04004A41 RID: 19009
		private int ShopID;

		// Token: 0x04004A42 RID: 19010
		private int num = 1;
	}
}
