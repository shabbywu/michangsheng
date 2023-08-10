using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace UltimateSurvival;

public class ItemDatabase : ScriptableObject
{
	[SerializeField]
	private ItemCategory[] m_Categories;

	[SerializeField]
	private ItemProperty.Definition[] m_ItemProperties;

	public ItemCategory[] Categories => m_Categories;

	public static void onEnter()
	{
	}

	public bool FindItemById(int id, out ItemData itemData)
	{
		for (int i = 0; i < m_Categories.Length; i++)
		{
			ItemCategory itemCategory = m_Categories[i];
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

	public bool FindItemByName(string name, out ItemData itemData)
	{
		for (int i = 0; i < m_Categories.Length; i++)
		{
			ItemCategory itemCategory = m_Categories[i];
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

	public bool FindRecipeById(int id, out Recipe recipe)
	{
		for (int i = 0; i < m_Categories.Length; i++)
		{
			ItemCategory itemCategory = m_Categories[i];
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

	public bool FindRecipeByName(string name, out Recipe recipe)
	{
		for (int i = 0; i < m_Categories.Length; i++)
		{
			ItemCategory itemCategory = m_Categories[i];
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

	public List<string> GetAllItemNames()
	{
		List<string> list = new List<string>();
		for (int i = 0; i < m_Categories.Length; i++)
		{
			ItemCategory itemCategory = m_Categories[i];
			for (int j = 0; j < itemCategory.Items.Length; j++)
			{
				list.Add(itemCategory.Items[j].Name);
			}
		}
		return list;
	}

	public int GetItemCount()
	{
		int num = 0;
		for (int i = 0; i < m_Categories.Length; i++)
		{
			num += m_Categories[i].Items.Length;
		}
		return num;
	}

	public void cratfToJson()
	{
		JSONObject jSONObject = new JSONObject(JSONObject.Type.OBJECT);
		ItemCategory[] categories = m_Categories;
		foreach (ItemCategory itemCategory in categories)
		{
			for (int j = 0; j < itemCategory.Items.Length; j++)
			{
				if (itemCategory.Items[j].IsCraftable)
				{
					JSONObject jSONObject2 = new JSONObject(JSONObject.Type.OBJECT);
					jSONObject2.AddField("CraftingID", itemCategory.Items[j].Id);
					jSONObject2.AddField("time", itemCategory.Items[j].Recipe.Duration);
					JSONObject jSONObject3 = new JSONObject(JSONObject.Type.ARRAY);
					RequiredItem[] requiredItems = itemCategory.Items[j].Recipe.RequiredItems;
					foreach (RequiredItem requiredItem in requiredItems)
					{
						JSONObject jSONObject4 = new JSONObject(JSONObject.Type.OBJECT);
						FindItemByName(requiredItem.Name, out var itemData);
						jSONObject4.AddField("costID", itemData.Id);
						jSONObject4.AddField("costnum", requiredItem.Amount);
						jSONObject3.Add(jSONObject4);
					}
					jSONObject2.AddField("cost", jSONObject3);
					jSONObject.AddField(string.Concat(itemCategory.Items[j].Id), jSONObject2);
				}
			}
		}
		string text = jSONObject.Print();
		StreamWriter streamWriter = new StreamWriter("D:/kbengine2.3.5/LGMGServer/scripts/data/d_crafting.py", append: false, Encoding.UTF8);
		streamWriter.Write("datas =" + text);
		streamWriter.Close();
	}

	private void OnValidate()
	{
		int num = 0;
		ItemCategory[] categories = m_Categories;
		foreach (ItemCategory itemCategory in categories)
		{
			for (int j = 0; j < itemCategory.Items.Length; j++)
			{
				itemCategory.Items[j].Category = itemCategory.Name;
				num++;
			}
		}
	}
}
