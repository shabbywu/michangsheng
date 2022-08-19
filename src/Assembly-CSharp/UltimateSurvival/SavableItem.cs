using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005D6 RID: 1494
	[Serializable]
	public class SavableItem
	{
		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06003019 RID: 12313 RVA: 0x00159997 File Offset: 0x00157B97
		// (set) Token: 0x0600301A RID: 12314 RVA: 0x0015999F File Offset: 0x00157B9F
		public bool Initialized { get; private set; }

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x0600301B RID: 12315 RVA: 0x001599A8 File Offset: 0x00157BA8
		// (set) Token: 0x0600301C RID: 12316 RVA: 0x001599B0 File Offset: 0x00157BB0
		public ItemData ItemData { get; private set; }

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x0600301D RID: 12317 RVA: 0x001599B9 File Offset: 0x00157BB9
		public int Id
		{
			get
			{
				return this.ItemData.Id;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x0600301E RID: 12318 RVA: 0x001599C6 File Offset: 0x00157BC6
		public string Name
		{
			get
			{
				return this.ItemData.Name;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x0600301F RID: 12319 RVA: 0x001599D3 File Offset: 0x00157BD3
		// (set) Token: 0x06003020 RID: 12320 RVA: 0x001599DB File Offset: 0x00157BDB
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

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06003021 RID: 12321 RVA: 0x001599EF File Offset: 0x00157BEF
		public List<ItemProperty.Value> CurrentPropertyValues
		{
			get
			{
				return this.m_CurrentPropertyValues;
			}
		}

		// Token: 0x06003022 RID: 12322 RVA: 0x00014667 File Offset: 0x00012867
		public static implicit operator bool(SavableItem item)
		{
			return item != null;
		}

		// Token: 0x06003023 RID: 12323 RVA: 0x001599F8 File Offset: 0x00157BF8
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

		// Token: 0x06003024 RID: 12324 RVA: 0x00159AA0 File Offset: 0x00157CA0
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

		// Token: 0x06003025 RID: 12325 RVA: 0x00159B30 File Offset: 0x00157D30
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

		// Token: 0x06003026 RID: 12326 RVA: 0x00159BA4 File Offset: 0x00157DA4
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

		// Token: 0x06003027 RID: 12327 RVA: 0x00159BF8 File Offset: 0x00157DF8
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

		// Token: 0x06003028 RID: 12328 RVA: 0x00159C5C File Offset: 0x00157E5C
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

		// Token: 0x06003029 RID: 12329 RVA: 0x00159CC0 File Offset: 0x00157EC0
		private List<ItemProperty.Value> CloneProperties(List<ItemProperty.Value> properties)
		{
			List<ItemProperty.Value> list = new List<ItemProperty.Value>();
			for (int i = 0; i < properties.Count; i++)
			{
				list.Add(properties[i].GetClone());
			}
			return list;
		}

		// Token: 0x0600302A RID: 12330 RVA: 0x00159CF7 File Offset: 0x00157EF7
		private void On_PropertyChanged(ItemProperty.Value propertyValue)
		{
			this.PropertyChanged.Send(propertyValue);
		}

		// Token: 0x04002A75 RID: 10869
		public Message<ItemProperty.Value> PropertyChanged = new Message<ItemProperty.Value>();

		// Token: 0x04002A76 RID: 10870
		public Message StackChanged = new Message();

		// Token: 0x04002A79 RID: 10873
		public Item m_Item;

		// Token: 0x04002A7A RID: 10874
		[SerializeField]
		private int m_CurrentInStack;

		// Token: 0x04002A7B RID: 10875
		[SerializeField]
		private List<ItemProperty.Value> m_CurrentPropertyValues;
	}
}
