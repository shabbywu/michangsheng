using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x0200089B RID: 2203
	[Serializable]
	public class SavableItem
	{
		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x060038B5 RID: 14517 RVA: 0x00029470 File Offset: 0x00027670
		// (set) Token: 0x060038B6 RID: 14518 RVA: 0x00029478 File Offset: 0x00027678
		public bool Initialized { get; private set; }

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x060038B7 RID: 14519 RVA: 0x00029481 File Offset: 0x00027681
		// (set) Token: 0x060038B8 RID: 14520 RVA: 0x00029489 File Offset: 0x00027689
		public ItemData ItemData { get; private set; }

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x060038B9 RID: 14521 RVA: 0x00029492 File Offset: 0x00027692
		public int Id
		{
			get
			{
				return this.ItemData.Id;
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x060038BA RID: 14522 RVA: 0x0002949F File Offset: 0x0002769F
		public string Name
		{
			get
			{
				return this.ItemData.Name;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x060038BB RID: 14523 RVA: 0x000294AC File Offset: 0x000276AC
		// (set) Token: 0x060038BC RID: 14524 RVA: 0x000294B4 File Offset: 0x000276B4
		public int CurrentInStack
		{
			get
			{
				return this.m_CurrentInStack;
			}
			set
			{
				this.m_CurrentInStack = value;
				this.StackChanged.Send();
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x060038BD RID: 14525 RVA: 0x000294C8 File Offset: 0x000276C8
		public List<ItemProperty.Value> CurrentPropertyValues
		{
			get
			{
				return this.m_CurrentPropertyValues;
			}
		}

		// Token: 0x060038BE RID: 14526 RVA: 0x000079B2 File Offset: 0x00005BB2
		public static implicit operator bool(SavableItem item)
		{
			return item != null;
		}

		// Token: 0x060038BF RID: 14527 RVA: 0x001A3460 File Offset: 0x001A1660
		public SavableItem(ItemData data, int currentInStack = 1, List<ItemProperty.Value> customPropertyValues = null)
		{
			this.CurrentInStack = Mathf.Clamp(currentInStack, 1, data.StackSize);
			if (customPropertyValues != null)
			{
				this.m_CurrentPropertyValues = this.CloneProperties(customPropertyValues);
			}
			else
			{
				this.m_CurrentPropertyValues = this.CloneProperties(data.PropertyValues);
			}
			this.ItemData = data;
			this.Initialized = true;
			for (int i = 0; i < this.m_CurrentPropertyValues.Count; i++)
			{
				this.m_CurrentPropertyValues[i].Changed.AddListener(new Action<ItemProperty.Value>(this.On_PropertyChanged));
			}
		}

		// Token: 0x060038C0 RID: 14528 RVA: 0x001A3508 File Offset: 0x001A1708
		public void OnLoad(ItemDatabase itemDatabase)
		{
			if (!itemDatabase)
			{
				Debug.LogError("[SavableItem] - This item couldn't be initialized and will not function properly. The item database provided is null!");
				return;
			}
			ItemData itemData;
			if (itemDatabase.FindItemById(this.Id, out itemData))
			{
				this.ItemData = itemData;
				this.Initialized = true;
				for (int i = 0; i < this.m_CurrentPropertyValues.Count; i++)
				{
					this.m_CurrentPropertyValues[i].Changed.AddListener(new Action<ItemProperty.Value>(this.On_PropertyChanged));
				}
				return;
			}
			Debug.LogErrorFormat("[SavableItem] - This item couldn't be initialized and will not function properly. No item with the name {0} was found in the database!", new object[]
			{
				this.Name
			});
		}

		// Token: 0x060038C1 RID: 14529 RVA: 0x001A3598 File Offset: 0x001A1798
		public string GetDescription(int index)
		{
			string result = string.Empty;
			if (index > -1 && this.ItemData.Descriptions.Length > index)
			{
				try
				{
					string format = this.ItemData.Descriptions[index];
					object[] args = this.m_CurrentPropertyValues.ToArray();
					result = string.Format(format, args);
				}
				catch
				{
					Debug.LogError("[SavableItem] - You tried to access a property through the item description, but the property doesn't exist. The item name is: " + this.Name);
				}
			}
			return result;
		}

		// Token: 0x060038C2 RID: 14530 RVA: 0x001A360C File Offset: 0x001A180C
		public bool HasProperty(string name)
		{
			if (!this.Initialized)
			{
				Debug.LogError("[SavableItem] - This SavableItem is not initialized, probably it was loaded and not initialized! (call OnLoad() after loading / deserializing).");
				return false;
			}
			for (int i = 0; i < this.m_CurrentPropertyValues.Count; i++)
			{
				if (this.m_CurrentPropertyValues[i].Name == name)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060038C3 RID: 14531 RVA: 0x001A3660 File Offset: 0x001A1860
		public ItemProperty.Value GetPropertyValue(string name)
		{
			ItemProperty.Value result = null;
			if (!this.Initialized)
			{
				Debug.LogError("[SavableItem] - This SavableItem is not initialized, probably it was loaded and not initialized! (call OnLoad() after loading / deserializing).");
				return null;
			}
			for (int i = 0; i < this.m_CurrentPropertyValues.Count; i++)
			{
				if (this.m_CurrentPropertyValues[i].Name == name)
				{
					result = this.m_CurrentPropertyValues[i];
					break;
				}
			}
			return result;
		}

		// Token: 0x060038C4 RID: 14532 RVA: 0x001A36C4 File Offset: 0x001A18C4
		public bool FindPropertyValue(string name, out ItemProperty.Value propertyValue)
		{
			propertyValue = null;
			if (!this.Initialized)
			{
				Debug.LogError("[SavableItem] - This SavableItem is not initialized, probably it was loaded and not initialized! (call OnLoad() after loading / deserializing).");
				return false;
			}
			for (int i = 0; i < this.m_CurrentPropertyValues.Count; i++)
			{
				if (this.m_CurrentPropertyValues[i].Name == name)
				{
					propertyValue = this.m_CurrentPropertyValues[i];
					return true;
				}
			}
			return false;
		}

		// Token: 0x060038C5 RID: 14533 RVA: 0x001A3728 File Offset: 0x001A1928
		private List<ItemProperty.Value> CloneProperties(List<ItemProperty.Value> properties)
		{
			List<ItemProperty.Value> list = new List<ItemProperty.Value>();
			for (int i = 0; i < properties.Count; i++)
			{
				list.Add(properties[i].GetClone());
			}
			return list;
		}

		// Token: 0x060038C6 RID: 14534 RVA: 0x000294D0 File Offset: 0x000276D0
		private void On_PropertyChanged(ItemProperty.Value propertyValue)
		{
			this.PropertyChanged.Send(propertyValue);
		}

		// Token: 0x0400330B RID: 13067
		public Message<ItemProperty.Value> PropertyChanged = new Message<ItemProperty.Value>();

		// Token: 0x0400330C RID: 13068
		public Message StackChanged = new Message();

		// Token: 0x0400330F RID: 13071
		public Item m_Item;

		// Token: 0x04003310 RID: 13072
		[SerializeField]
		private int m_CurrentInStack;

		// Token: 0x04003311 RID: 13073
		[SerializeField]
		private List<ItemProperty.Value> m_CurrentPropertyValues;
	}
}
