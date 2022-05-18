using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000D78 RID: 3448
	public class Equip_Manager : MonoBehaviour
	{
		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x060052D2 RID: 21202 RVA: 0x0003B4D6 File Offset: 0x000396D6
		// (set) Token: 0x060052D3 RID: 21203 RVA: 0x0003B4DE File Offset: 0x000396DE
		public List<item> Equip
		{
			get
			{
				return this._Equip;
			}
			set
			{
				this._Equip = value;
			}
		}

		// Token: 0x060052D4 RID: 21204 RVA: 0x0003B4E7 File Offset: 0x000396E7
		private void Awake()
		{
			this.is_draged = false;
			this.initEuip();
		}

		// Token: 0x060052D5 RID: 21205 RVA: 0x000042DD File Offset: 0x000024DD
		private void Start()
		{
		}

		// Token: 0x060052D6 RID: 21206 RVA: 0x0003B4F6 File Offset: 0x000396F6
		private void Update()
		{
			if (Input.GetKeyDown(101))
			{
				this.Show();
			}
		}

		// Token: 0x060052D7 RID: 21207 RVA: 0x002280BC File Offset: 0x002262BC
		private void Show()
		{
			this.showEquipment = !this.showEquipment;
			if (!this.showEquipment)
			{
				Singleton.inventory.showTooltip = false;
			}
			if (this.showEquipment)
			{
				this.EquipUI.transform.Find("Win").position = this.EquipUI.transform.position;
			}
			this.EquipUI.SetActive(this.showEquipment);
			Singleton.UI.UI_Top(this.EquipUI.transform);
		}

		// Token: 0x060052D8 RID: 21208 RVA: 0x00228144 File Offset: 0x00226344
		private void initEuip()
		{
			for (int i = 0; i < 15; i++)
			{
				this.Equip.Add(new item());
			}
			this.EquipUI.SetActive(this.showEquipment);
		}

		// Token: 0x060052D9 RID: 21209 RVA: 0x000042DD File Offset: 0x000024DD
		public void SaveEquipment()
		{
		}

		// Token: 0x060052DA RID: 21210 RVA: 0x00228180 File Offset: 0x00226380
		public void LoadEquipment()
		{
			for (int i = 0; i < this.Equip.Count; i++)
			{
				this.Equip[i] = new item();
			}
			foreach (ITEM_INFO item_INFO in Tools.instance.getPlayer().equipItemList.values)
			{
				for (int j = 0; j < this.Equip.Count; j++)
				{
					if (j == item_INFO.itemIndex)
					{
						this.Equip[j] = Singleton.inventory.datebase.items[item_INFO.itemId].Clone();
						this.Equip[j].UUID = item_INFO.uuid;
						this.Equip[j].Seid = item_INFO.Seid;
						break;
					}
				}
			}
		}

		// Token: 0x060052DB RID: 21211 RVA: 0x00228284 File Offset: 0x00226484
		public int GetEquipID(string name)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
			if (num <= 1380655422U)
			{
				if (num != 377761645U)
				{
					if (num != 1274866620U)
					{
						if (num == 1380655422U)
						{
							if (name == "LinZhou")
							{
								return 14;
							}
						}
					}
					else if (name == "Trousers")
					{
						return 5;
					}
				}
				else if (name == "Clothing")
				{
					return 1;
				}
			}
			else if (num <= 2005354379U)
			{
				if (num != 1489107955U)
				{
					if (num == 2005354379U)
					{
						if (name == "Ring")
						{
							return 2;
						}
					}
				}
				else if (name == "Shoes")
				{
					return 3;
				}
			}
			else if (num != 2547530665U)
			{
				if (num == 3082879841U)
				{
					if (name == "Weapon")
					{
						return 0;
					}
				}
			}
			else if (name == "Weapon2")
			{
				return 6;
			}
			return -1;
		}

		// Token: 0x060052DC RID: 21212 RVA: 0x0022837C File Offset: 0x0022657C
		public void addEquip(string UUID, int key = 0)
		{
			Avatar avatar = (Avatar)KBEngineApp.app.player();
			ITEM_INFO item_INFO = avatar.FindItemByUUID(UUID);
			if (item_INFO == null || item_INFO.itemId < 0)
			{
				return;
			}
			avatar.YSequipItem(UUID, (int)jsonData.instance.ItemJsonData[string.Concat(item_INFO.itemId)]["type"].n, key);
		}

		// Token: 0x060052DD RID: 21213 RVA: 0x0003B507 File Offset: 0x00039707
		public void UnEquip(string UUID, int index = 0)
		{
			((Avatar)KBEngineApp.app.player()).YSUnequipItem(UUID, index);
		}

		// Token: 0x060052DE RID: 21214 RVA: 0x000042DD File Offset: 0x000024DD
		private void OnDestroy()
		{
		}

		// Token: 0x040052DC RID: 21212
		public GameObject EquipUI;

		// Token: 0x040052DD RID: 21213
		public GameObject Temp;

		// Token: 0x040052DE RID: 21214
		private bool showEquipment = true;

		// Token: 0x040052DF RID: 21215
		private List<item> _Equip = new List<item>();

		// Token: 0x040052E0 RID: 21216
		public bool is_draged;
	}
}
