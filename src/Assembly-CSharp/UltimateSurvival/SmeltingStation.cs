using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x0200060A RID: 1546
	public class SmeltingStation : InteractableObject, IInventoryTrigger
	{
		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06003173 RID: 12659 RVA: 0x0015F336 File Offset: 0x0015D536
		// (set) Token: 0x06003174 RID: 12660 RVA: 0x0015F33E File Offset: 0x0015D53E
		public ItemHolder FuelSlot { get; private set; }

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06003175 RID: 12661 RVA: 0x0015F347 File Offset: 0x0015D547
		// (set) Token: 0x06003176 RID: 12662 RVA: 0x0015F34F File Offset: 0x0015D54F
		public ItemHolder InputSlot { get; private set; }

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06003177 RID: 12663 RVA: 0x0015F358 File Offset: 0x0015D558
		// (set) Token: 0x06003178 RID: 12664 RVA: 0x0015F360 File Offset: 0x0015D560
		public List<ItemHolder> LootSlots { get; private set; }

		// Token: 0x06003179 RID: 12665 RVA: 0x0015F369 File Offset: 0x0015D569
		public override void OnInteract(PlayerEventHandler player)
		{
			if (this.m_Type == SmeltingStationType.Campfire)
			{
				MonoSingleton<InventoryController>.Instance.OpenCampfire.Try(this);
				return;
			}
			if (this.m_Type == SmeltingStationType.Furnace)
			{
				MonoSingleton<InventoryController>.Instance.OpenFurnace.Try(this);
			}
		}

		// Token: 0x0600317A RID: 12666 RVA: 0x0015F3A0 File Offset: 0x0015D5A0
		private void Start()
		{
			this.FuelSlot = new ItemHolder();
			this.InputSlot = new ItemHolder();
			this.FuelSlot.Updated.AddListener(new Action<ItemHolder>(this.On_HolderUpdated));
			this.InputSlot.Updated.AddListener(new Action<ItemHolder>(this.On_HolderUpdated));
			this.LootSlots = new List<ItemHolder>();
			for (int i = 0; i < this.m_LootContainerSize; i++)
			{
				this.LootSlots.Add(new ItemHolder());
			}
			this.IsBurning.AddChangeListener(new Action(this.OnChanged_IsBurning));
			this.IsBurning.SetAndForceUpdate(false);
		}

		// Token: 0x0600317B RID: 12667 RVA: 0x0015F44C File Offset: 0x0015D64C
		private void On_HolderUpdated(ItemHolder holder)
		{
			bool flag = false;
			if (this.FuelSlot.HasItem && this.InputSlot.HasItem)
			{
				if (this.FuelSlot.CurrentItem.HasProperty("Fuel Time") && this.InputSlot.CurrentItem.HasProperty("Burn Time") && this.InputSlot.CurrentItem.HasProperty("Burn Result"))
				{
					if (this.IsBurning.Is(false))
					{
						this.m_FuelTimeProperty = this.FuelSlot.CurrentItem.GetPropertyValue("Fuel Time");
						this.m_BurnTimeProperty = this.InputSlot.CurrentItem.GetPropertyValue("Burn Time");
						this.m_ItemResult = this.InputSlot.CurrentItem.GetPropertyValue("Burn Result").String;
						this.IsBurning.Set(true);
						this.m_BurningHandler = base.StartCoroutine(this.C_Burn());
						return;
					}
				}
				else
				{
					flag = true;
				}
			}
			else
			{
				flag = true;
			}
			if (this.IsBurning.Is(true) && flag)
			{
				this.StopBurning();
			}
		}

		// Token: 0x0600317C RID: 12668 RVA: 0x0015F568 File Offset: 0x0015D768
		private void OnChanged_IsBurning()
		{
			if (this.IsBurning.Is(true))
			{
				this.m_Fire.Play(true);
				GameController.Audio.LerpVolumeOverTime(this.m_FireSource, this.m_FireVolume, 3f);
				if (this.m_DamageArea)
				{
					this.m_DamageArea.Active = true;
				}
			}
			else
			{
				this.m_Fire.Stop(true);
				GameController.Audio.LerpVolumeOverTime(this.m_FireSource, 0f, 3f);
				if (this.m_DamageArea)
				{
					this.m_DamageArea.Active = false;
				}
				this.Progress.Set(0f);
			}
			this.m_FireLight.Toggle(this.IsBurning.Get());
		}

		// Token: 0x0600317D RID: 12669 RVA: 0x0015F62A File Offset: 0x0015D82A
		private IEnumerator C_Burn()
		{
			for (;;)
			{
				yield return this.m_UpdateInterval;
				if (!this.FuelSlot.CurrentItem || !this.InputSlot.CurrentItem)
				{
					break;
				}
				ItemProperty.Float @float = this.m_BurnTimeProperty.Float;
				@float.Current -= 0.1f;
				this.m_BurnTimeProperty.SetValue(ItemProperty.Type.Float, @float);
				this.Progress.Set(1f - @float.Ratio);
				if (@float.Current <= 0f)
				{
					ItemData itemData;
					if (GameController.ItemDatabase.FindItemByName(this.m_ItemResult, out itemData))
					{
						CollectionUtils.AddItem(itemData, 1, this.LootSlots, null);
					}
					else
					{
						Debug.LogWarning("The item has burned but no result was given, make sure the item has the 'Burn Result' property, so we know what to add as a result of burning / smelting.", this);
					}
					if (this.InputSlot.CurrentItem.CurrentInStack == 1)
					{
						goto Block_3;
					}
					@float.Current = @float.Default;
					this.m_BurnTimeProperty.SetValue(ItemProperty.Type.Float, @float);
					SavableItem currentItem = this.InputSlot.CurrentItem;
					int currentInStack = currentItem.CurrentInStack;
					currentItem.CurrentInStack = currentInStack - 1;
				}
				ItemProperty.Float float2 = this.m_FuelTimeProperty.Float;
				float2.Current -= 0.1f;
				this.m_FuelTimeProperty.SetValue(ItemProperty.Type.Float, float2);
				if (float2.Current <= 0f)
				{
					if (this.FuelSlot.CurrentItem.CurrentInStack == 1)
					{
						goto Block_5;
					}
					float2.Current = float2.Default;
					this.m_FuelTimeProperty.SetValue(ItemProperty.Type.Float, float2);
					SavableItem currentItem2 = this.FuelSlot.CurrentItem;
					int currentInStack = currentItem2.CurrentInStack;
					currentItem2.CurrentInStack = currentInStack - 1;
				}
			}
			this.StopBurning();
			yield break;
			Block_3:
			this.InputSlot.SetItem(null);
			this.StopBurning();
			yield break;
			Block_5:
			this.FuelSlot.SetItem(null);
			this.StopBurning();
			yield break;
			yield break;
		}

		// Token: 0x0600317E RID: 12670 RVA: 0x0015F63C File Offset: 0x0015D83C
		private void StopBurning()
		{
			this.m_FuelTimeProperty = null;
			this.m_BurnTimeProperty = null;
			this.m_ItemResult = string.Empty;
			this.IsBurning.Set(false);
			if (this.m_BurningHandler != null)
			{
				base.StopCoroutine(this.m_BurningHandler);
			}
			this.m_BurningHandler = null;
		}

		// Token: 0x04002BA2 RID: 11170
		private const float UPDATE_INTERVAL = 0.1f;

		// Token: 0x04002BA3 RID: 11171
		public Value<bool> IsBurning = new Value<bool>(false);

		// Token: 0x04002BA4 RID: 11172
		public Value<float> Progress = new Value<float>(0f);

		// Token: 0x04002BA5 RID: 11173
		public Message BurnedItem = new Message();

		// Token: 0x04002BA9 RID: 11177
		[SerializeField]
		private SmeltingStationType m_Type;

		// Token: 0x04002BAA RID: 11178
		[SerializeField]
		[Range(1f, 18f)]
		private int m_LootContainerSize = 6;

		// Token: 0x04002BAB RID: 11179
		[Header("Fire")]
		[SerializeField]
		private ParticleSystem m_Fire;

		// Token: 0x04002BAC RID: 11180
		[SerializeField]
		private AudioSource m_FireSource;

		// Token: 0x04002BAD RID: 11181
		[SerializeField]
		private Firelight m_FireLight;

		// Token: 0x04002BAE RID: 11182
		[SerializeField]
		[Range(0f, 1f)]
		private float m_FireVolume = 0.6f;

		// Token: 0x04002BAF RID: 11183
		[Header("Damage (Optional)")]
		[SerializeField]
		private DamageArea m_DamageArea;

		// Token: 0x04002BB0 RID: 11184
		private WaitForSeconds m_UpdateInterval = new WaitForSeconds(0.1f);

		// Token: 0x04002BB1 RID: 11185
		private Coroutine m_BurningHandler;

		// Token: 0x04002BB2 RID: 11186
		private ItemProperty.Value m_BurnTimeProperty;

		// Token: 0x04002BB3 RID: 11187
		private ItemProperty.Value m_FuelTimeProperty;

		// Token: 0x04002BB4 RID: 11188
		private string m_ItemResult;
	}
}
