using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000A58 RID: 2648
	public class Equip_Manager : MonoBehaviour
	{
		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x060049E7 RID: 18919 RVA: 0x001F5670 File Offset: 0x001F3870
		// (set) Token: 0x060049E8 RID: 18920 RVA: 0x001F5678 File Offset: 0x001F3878
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

		// Token: 0x060049E9 RID: 18921 RVA: 0x001F5681 File Offset: 0x001F3881
		private void Awake()
		{
			this.is_draged = false;
			this.initEuip();
		}

		// Token: 0x060049EA RID: 18922 RVA: 0x00004095 File Offset: 0x00002295
		private void Start()
		{
		}

		// Token: 0x060049EB RID: 18923 RVA: 0x001F5690 File Offset: 0x001F3890
		private void Update()
		{
			if (Input.GetKeyDown(101))
			{
				this.Show();
			}
		}

		// Token: 0x060049EC RID: 18924 RVA: 0x001F56A4 File Offset: 0x001F38A4
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

		// Token: 0x060049ED RID: 18925 RVA: 0x001F572C File Offset: 0x001F392C
		private void initEuip()
		{
			for (int i = 0; i < 15; i++)
			{
				this.Equip.Add(new item());
			}
			this.EquipUI.SetActive(this.showEquipment);
		}

		// Token: 0x060049EE RID: 18926 RVA: 0x00004095 File Offset: 0x00002295
		public void SaveEquipment()
		{
		}

		// Token: 0x060049EF RID: 18927 RVA: 0x001F5768 File Offset: 0x001F3968
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

		// Token: 0x060049F0 RID: 18928 RVA: 0x001F586C File Offset: 0x001F3A6C
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

		// Token: 0x060049F1 RID: 18929 RVA: 0x001F5964 File Offset: 0x001F3B64
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

		// Token: 0x060049F2 RID: 18930 RVA: 0x001F59CD File Offset: 0x001F3BCD
		public void UnEquip(string UUID, int index = 0)
		{
			((Avatar)KBEngineApp.app.player()).YSUnequipItem(UUID, index);
		}

		// Token: 0x060049F3 RID: 18931 RVA: 0x00004095 File Offset: 0x00002295
		private void OnDestroy()
		{
		}

		// Token: 0x04004957 RID: 18775
		public GameObject EquipUI;

		// Token: 0x04004958 RID: 18776
		public GameObject Temp;

		// Token: 0x04004959 RID: 18777
		private bool showEquipment = true;

		// Token: 0x0400495A RID: 18778
		private List<item> _Equip = new List<item>();

		// Token: 0x0400495B RID: 18779
		public bool is_draged;
	}
}
