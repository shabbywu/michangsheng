using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005D7 RID: 1495
	public class ItemDatabase : ScriptableObject
	{
		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x0600302B RID: 12331 RVA: 0x00159D05 File Offset: 0x00157F05
		public ItemCategory[] Categories
		{
			get
			{
				return this.m_Categories;
			}
		}

		// Token: 0x0600302C RID: 12332 RVA: 0x00004095 File Offset: 0x00002295
		public static void onEnter()
		{
		}

		// Token: 0x0600302D RID: 12333 RVA: 0x00159D10 File Offset: 0x00157F10
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

		// Token: 0x0600302E RID: 12334 RVA: 0x00159D6C File Offset: 0x00157F6C
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

		// Token: 0x0600302F RID: 12335 RVA: 0x00159DD0 File Offset: 0x00157FD0
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

		// Token: 0x06003030 RID: 12336 RVA: 0x00159E34 File Offset: 0x00158034
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

		// Token: 0x06003031 RID: 12337 RVA: 0x00159EAC File Offset: 0x001580AC
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

		// Token: 0x06003032 RID: 12338 RVA: 0x00159F04 File Offset: 0x00158104
		public int GetItemCount()
		{
			int num = 0;
			for (int i = 0; i < this.m_Categories.Length; i++)
			{
				num += this.m_Categories[i].Items.Length;
			}
			return num;
		}

		// Token: 0x06003033 RID: 12339 RVA: 0x00159F3C File Offset: 0x0015813C
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

		// Token: 0x06003034 RID: 12340 RVA: 0x0015A0C4 File Offset: 0x001582C4
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

		// Token: 0x04002A7C RID: 10876
		[SerializeField]
		private ItemCategory[] m_Categories;

		// Token: 0x04002A7D RID: 10877
		[SerializeField]
		private ItemProperty.Definition[] m_ItemProperties;
	}
}
