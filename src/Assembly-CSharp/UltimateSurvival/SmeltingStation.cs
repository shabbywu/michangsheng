using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008E9 RID: 2281
	public class SmeltingStation : InteractableObject, IInventoryTrigger
	{
		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06003A82 RID: 14978 RVA: 0x0002A8A8 File Offset: 0x00028AA8
		// (set) Token: 0x06003A83 RID: 14979 RVA: 0x0002A8B0 File Offset: 0x00028AB0
		public ItemHolder FuelSlot { get; private set; }

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06003A84 RID: 14980 RVA: 0x0002A8B9 File Offset: 0x00028AB9
		// (set) Token: 0x06003A85 RID: 14981 RVA: 0x0002A8C1 File Offset: 0x00028AC1
		public ItemHolder InputSlot { get; private set; }

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06003A86 RID: 14982 RVA: 0x0002A8CA File Offset: 0x00028ACA
		// (set) Token: 0x06003A87 RID: 14983 RVA: 0x0002A8D2 File Offset: 0x00028AD2
		public List<ItemHolder> LootSlots { get; private set; }

		// Token: 0x06003A88 RID: 14984 RVA: 0x0002A8DB File Offset: 0x00028ADB
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

		// Token: 0x06003A89 RID: 14985 RVA: 0x001A8778 File Offset: 0x001A6978
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

		// Token: 0x06003A8A RID: 14986 RVA: 0x001A8824 File Offset: 0x001A6A24
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

		// Token: 0x06003A8B RID: 14987 RVA: 0x001A8940 File Offset: 0x001A6B40
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

		// Token: 0x06003A8C RID: 14988 RVA: 0x0002A911 File Offset: 0x00028B11
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

		// Token: 0x06003A8D RID: 14989 RVA: 0x001A8A04 File Offset: 0x001A6C04
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

		// Token: 0x040034A7 RID: 13479
		private const float UPDATE_INTERVAL = 0.1f;

		// Token: 0x040034A8 RID: 13480
		public Value<bool> IsBurning = new Value<bool>(false);

		// Token: 0x040034A9 RID: 13481
		public Value<float> Progress = new Value<float>(0f);

		// Token: 0x040034AA RID: 13482
		public Message BurnedItem = new Message();

		// Token: 0x040034AE RID: 13486
		[SerializeField]
		private SmeltingStationType m_Type;

		// Token: 0x040034AF RID: 13487
		[SerializeField]
		[Range(1f, 18f)]
		private int m_LootContainerSize = 6;

		// Token: 0x040034B0 RID: 13488
		[Header("Fire")]
		[SerializeField]
		private ParticleSystem m_Fire;

		// Token: 0x040034B1 RID: 13489
		[SerializeField]
		private AudioSource m_FireSource;

		// Token: 0x040034B2 RID: 13490
		[SerializeField]
		private Firelight m_FireLight;

		// Token: 0x040034B3 RID: 13491
		[SerializeField]
		[Range(0f, 1f)]
		private float m_FireVolume = 0.6f;

		// Token: 0x040034B4 RID: 13492
		[Header("Damage (Optional)")]
		[SerializeField]
		private DamageArea m_DamageArea;

		// Token: 0x040034B5 RID: 13493
		private WaitForSeconds m_UpdateInterval = new WaitForSeconds(0.1f);

		// Token: 0x040034B6 RID: 13494
		private Coroutine m_BurningHandler;

		// Token: 0x040034B7 RID: 13495
		private ItemProperty.Value m_BurnTimeProperty;

		// Token: 0x040034B8 RID: 13496
		private ItemProperty.Value m_FuelTimeProperty;

		// Token: 0x040034B9 RID: 13497
		private string m_ItemResult;
	}
}
