using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x0200089C RID: 2204
	public class ItemDatabase : ScriptableObject
	{
		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x060038C7 RID: 14535 RVA: 0x000294DE File Offset: 0x000276DE
		public ItemCategory[] Categories
		{
			get
			{
				return this.m_Categories;
			}
		}

		// Token: 0x060038C8 RID: 14536 RVA: 0x000042DD File Offset: 0x000024DD
		public static void onEnter()
		{
		}

		// Token: 0x060038C9 RID: 14537 RVA: 0x001A3760 File Offset: 0x001A1960
		public bool FindItemById(int id, out ItemData itemData)
		{
			for (int i = 0; i < this.m_Categories.Length; i++)
			{
				ItemCategory itemCategory = this.m_Categories[i];
				for (int j = 0; j < itemCategory.Items.Length; j++)
				{
					if (itemCategory.Items[j].Id == id)
					{
						itemData = itemCategory.Items[j];
						return true;
					}
				}
			}
			itemData = null;
			return false;
		}

		// Token: 0x060038CA RID: 14538 RVA: 0x001A37BC File Offset: 0x001A19BC
		public bool FindItemByName(string name, out ItemData itemData)
		{
			for (int i = 0; i < this.m_Categories.Length; i++)
			{
				ItemCategory itemCategory = this.m_Categories[i];
				for (int j = 0; j < itemCategory.Items.Length; j++)
				{
					if (itemCategory.Items[j].Name == name)
					{
						itemData = itemCategory.Items[j];
						return true;
					}
				}
			}
			itemData = null;
			return false;
		}

		// Token: 0x060038CB RID: 14539 RVA: 0x001A3820 File Offset: 0x001A1A20
		public bool FindRecipeById(int id, out Recipe recipe)
		{
			for (int i = 0; i < this.m_Categories.Length; i++)
			{
				ItemCategory itemCategory = this.m_Categories[i];
				for (int j = 0; j < itemCategory.Items.Length; j++)
				{
					if (itemCategory.Items[j].Id == id)
					{
						recipe = itemCategory.Items[j].Recipe;
						return true;
					}
				}
			}
			recipe = null;
			return false;
		}

		// Token: 0x060038CC RID: 14540 RVA: 0x001A3884 File Offset: 0x001A1A84
		public bool FindRecipeByName(string name, out Recipe recipe)
		{
			for (int i = 0; i < this.m_Categories.Length; i++)
			{
				ItemCategory itemCategory = this.m_Categories[i];
				for (int j = 0; j < itemCategory.Items.Length; j++)
				{
					if (itemCategory.Items[j].Name == name && itemCategory.Items[j].IsCraftable)
					{
						recipe = itemCategory.Items[j].Recipe;
						return true;
					}
				}
			}
			recipe = null;
			return false;
		}

		// Token: 0x060038CD RID: 14541 RVA: 0x001A38FC File Offset: 0x001A1AFC
		public List<string> GetAllItemNames()
		{
			List<string> list = new List<string>();
			for (int i = 0; i < this.m_Categories.Length; i++)
			{
				ItemCategory itemCategory = this.m_Categories[i];
				for (int j = 0; j < itemCategory.Items.Length; j++)
				{
					list.Add(itemCategory.Items[j].Name);
				}
			}
			return list;
		}

		// Token: 0x060038CE RID: 14542 RVA: 0x001A3954 File Offset: 0x001A1B54
		public int GetItemCount()
		{
			int num = 0;
			for (int i = 0; i < this.m_Categories.Length; i++)
			{
				num += this.m_Categories[i].Items.Length;
			}
			return num;
		}

		// Token: 0x060038CF RID: 14543 RVA: 0x001A398C File Offset: 0x001A1B8C
		public void cratfToJson()
		{
			JSONObject jsonobject = new JSONObject(JSONObject.Type.OBJECT);
			foreach (ItemCategory itemCategory in this.m_Categories)
			{
				for (int j = 0; j < itemCategory.Items.Length; j++)
				{
					if (itemCategory.Items[j].IsCraftable)
					{
						JSONObject jsonobject2 = new JSONObject(JSONObject.Type.OBJECT);
						jsonobject2.AddField("CraftingID", itemCategory.Items[j].Id);
						jsonobject2.AddField("time", itemCategory.Items[j].Recipe.Duration);
						JSONObject jsonobject3 = new JSONObject(JSONObject.Type.ARRAY);
						foreach (RequiredItem requiredItem in itemCategory.Items[j].Recipe.RequiredItems)
						{
							JSONObject jsonobject4 = new JSONObject(JSONObject.Type.OBJECT);
							ItemData itemData;
							this.FindItemByName(requiredItem.Name, out itemData);
							jsonobject4.AddField("costID", itemData.Id);
							jsonobject4.AddField("costnum", requiredItem.Amount);
							jsonobject3.Add(jsonobject4);
						}
						jsonobject2.AddField("cost", jsonobject3);
						jsonobject.AddField(string.Concat(itemCategory.Items[j].Id), jsonobject2);
					}
				}
			}
			string str = jsonobject.Print(false);
			StreamWriter streamWriter = new StreamWriter("D:/kbengine2.3.5/LGMGServer/scripts/data/d_crafting.py", false, Encoding.UTF8);
			streamWriter.Write("datas =" + str);
			streamWriter.Close();
		}

		// Token: 0x060038D0 RID: 14544 RVA: 0x001A3B14 File Offset: 0x001A1D14
		private void OnValidate()
		{
			int num = 0;
			foreach (ItemCategory itemCategory in this.m_Categories)
			{
				for (int j = 0; j < itemCategory.Items.Length; j++)
				{
					itemCategory.Items[j].Category = itemCategory.Name;
					num++;
				}
			}
		}

		// Token: 0x04003312 RID: 13074
		[SerializeField]
		private ItemCategory[] m_Categories;

		// Token: 0x04003313 RID: 13075
		[SerializeField]
		private ItemProperty.Definition[] m_ItemProperties;
	}
}
