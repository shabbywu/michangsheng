using System;
using System.Collections;
using UltimateSurvival.GUISystem;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008D0 RID: 2256
	public class Anvil : InteractableObject, IInventoryTrigger
	{
		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06003A00 RID: 14848 RVA: 0x0002A2BB File Offset: 0x000284BB
		public float RequirementRatio
		{
			get
			{
				return this.m_RequirementRatio;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06003A01 RID: 14849 RVA: 0x0002A2C3 File Offset: 0x000284C3
		// (set) Token: 0x06003A02 RID: 14850 RVA: 0x0002A2CB File Offset: 0x000284CB
		public ItemHolder InputHolder { get; private set; }

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06003A03 RID: 14851 RVA: 0x0002A2D4 File Offset: 0x000284D4
		// (set) Token: 0x06003A04 RID: 14852 RVA: 0x0002A2DC File Offset: 0x000284DC
		public ItemHolder ResultHolder { get; private set; }

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06003A05 RID: 14853 RVA: 0x0002A2E5 File Offset: 0x000284E5
		// (set) Token: 0x06003A06 RID: 14854 RVA: 0x0002A2ED File Offset: 0x000284ED
		public Anvil.ItemToRepair InputItem { get; private set; }

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06003A07 RID: 14855 RVA: 0x0002A2F6 File Offset: 0x000284F6
		// (set) Token: 0x06003A08 RID: 14856 RVA: 0x0002A2FE File Offset: 0x000284FE
		public Anvil.RequiredItem[] RequiredItems { get; private set; }

		// Token: 0x06003A09 RID: 14857 RVA: 0x0002A307 File Offset: 0x00028507
		public override void OnInteract(PlayerEventHandler player)
		{
			MonoSingleton<InventoryController>.Instance.OpenAnvil.Try(this);
		}

		// Token: 0x06003A0A RID: 14858 RVA: 0x001A73F0 File Offset: 0x001A55F0
		private void Start()
		{
			this.InputItem = new Anvil.ItemToRepair();
			this.InputHolder = new ItemHolder();
			this.ResultHolder = new ItemHolder();
			this.InputHolder.Updated.AddListener(new Action<ItemHolder>(this.On_InputHolderUpdated));
			this.m_Inventory = MonoSingleton<GUIController>.Instance.GetContainer("Inventory");
			this.m_Inventory.Slot_Refreshed.AddListener(new Action<Slot>(this.On_InventorySlotRefreshed));
			this.Repairing.AddStartTryer(new TryerDelegate(this.TryStart_Repairing));
			this.Repairing.AddStopListener(new Action(this.OnStop_Repairing));
			this.RepairFinished.AddListener(new Action(this.On_RepairFinished));
			this.m_UpdateInterval = new WaitForSeconds(0.1f);
		}

		// Token: 0x06003A0B RID: 14859 RVA: 0x001A74C0 File Offset: 0x001A56C0
		private void On_InventorySlotRefreshed(Slot slot)
		{
			if (this.InputItemReadyForRepair.Is(true))
			{
				this.CalculateRequiredItems(this.InputItem.Recipe, this.InputItem.DurabilityProperty.Float.Ratio);
				this.InputItemReadyForRepair.SetAndForceUpdate(true);
			}
			if (this.Repairing.Active && this.InputItemReadyForRepair.Get() && !this.HasRequiredItems())
			{
				this.Repairing.ForceStop();
			}
		}

		// Token: 0x06003A0C RID: 14860 RVA: 0x0002A31A File Offset: 0x0002851A
		private bool TryStart_Repairing()
		{
			if (this.InputItemReadyForRepair.Is(false) || !this.HasRequiredItems())
			{
				return false;
			}
			base.StartCoroutine(this.C_Repair());
			return true;
		}

		// Token: 0x06003A0D RID: 14861 RVA: 0x0002A342 File Offset: 0x00028542
		private void OnStop_Repairing()
		{
			base.StopAllCoroutines();
			this.RepairProgress.Set(0f);
		}

		// Token: 0x06003A0E RID: 14862 RVA: 0x001A7540 File Offset: 0x001A5740
		private void On_RepairFinished()
		{
			SavableItem currentItem = this.InputHolder.CurrentItem;
			this.InputHolder.SetItem(null);
			this.ResultHolder.SetItem(currentItem);
			foreach (Anvil.RequiredItem requiredItem in this.RequiredItems)
			{
				this.m_Inventory.RemoveItems(requiredItem.Name, requiredItem.Needs);
			}
		}

		// Token: 0x06003A0F RID: 14863 RVA: 0x001A75A8 File Offset: 0x001A57A8
		private void On_InputHolderUpdated(ItemHolder holder)
		{
			ItemProperty.Value value = null;
			if (holder.HasItem && holder.CurrentItem.HasProperty("Durability"))
			{
				value = holder.CurrentItem.GetPropertyValue("Durability");
			}
			if (value != null && holder.CurrentItem.ItemData.IsCraftable && value.Float.Ratio != 1f && holder.CurrentItem.ItemData.IsCraftable)
			{
				this.InputItem.Recipe = holder.CurrentItem.ItemData.Recipe;
				this.InputItem.DurabilityProperty = value;
				this.CalculateRequiredItems(this.InputItem.Recipe, this.InputItem.DurabilityProperty.Float.Ratio);
				this.InputItemReadyForRepair.SetAndForceUpdate(true);
				return;
			}
			this.InputItemReadyForRepair.SetAndForceUpdate(false);
			if (this.Repairing.Active)
			{
				this.Repairing.ForceStop();
			}
		}

		// Token: 0x06003A10 RID: 14864 RVA: 0x001A76A8 File Offset: 0x001A58A8
		private void CalculateRequiredItems(Recipe recipe, float durabilityRatio)
		{
			this.RequiredItems = new Anvil.RequiredItem[recipe.RequiredItems.Length];
			for (int i = 0; i < recipe.RequiredItems.Length; i++)
			{
				int itemCount = this.m_Inventory.GetItemCount(recipe.RequiredItems[i].Name);
				int needs = Mathf.RoundToInt((float)recipe.RequiredItems[i].Amount * (1f - durabilityRatio) * this.m_RequirementRatio) + 1;
				this.RequiredItems[i] = new Anvil.RequiredItem(recipe.RequiredItems[i].Name, needs, itemCount);
			}
		}

		// Token: 0x06003A11 RID: 14865 RVA: 0x001A773C File Offset: 0x001A593C
		private bool HasRequiredItems()
		{
			for (int i = 0; i < this.RequiredItems.Length; i++)
			{
				if (!this.RequiredItems[i].HasEnough())
				{
					if (this.Repairing.Active)
					{
						this.Repairing.ForceStop();
					}
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003A12 RID: 14866 RVA: 0x0002A35A File Offset: 0x0002855A
		private IEnumerator C_Repair()
		{
			float requiredTime = (float)this.InputItem.Recipe.Duration * (1f - this.InputItem.DurabilityProperty.Float.Ratio);
			float elapsedTime = 0f;
			while (elapsedTime < requiredTime)
			{
				yield return this.m_UpdateInterval;
				elapsedTime += 0.1f;
				this.RepairProgress.Set(elapsedTime / requiredTime);
			}
			this.RepairProgress.Set(0f);
			ItemProperty.Float @float = this.InputItem.DurabilityProperty.Float;
			@float.Current = @float.Default;
			this.RepairFinished.Send();
			this.InputItem.DurabilityProperty.SetValue(ItemProperty.Type.Float, @float);
			yield break;
		}

		// Token: 0x0400342A RID: 13354
		private const float UPDATE_INTERVAL = 0.1f;

		// Token: 0x0400342B RID: 13355
		public Activity Repairing = new Activity();

		// Token: 0x0400342C RID: 13356
		public Value<float> RepairProgress = new Value<float>(0f);

		// Token: 0x0400342D RID: 13357
		public Value<bool> InputItemReadyForRepair = new Value<bool>(false);

		// Token: 0x0400342E RID: 13358
		public Message RepairFinished = new Message();

		// Token: 0x04003433 RID: 13363
		[SerializeField]
		[Range(0f, 1f)]
		[Tooltip("1 - the requirements will be the same as if crafting the item (when durability is low).\n0 - the requirements will be very low.")]
		private float m_RequirementRatio = 0.6f;

		// Token: 0x04003434 RID: 13364
		[Header("Audio")]
		[SerializeField]
		private AudioSource m_AudioSource;

		// Token: 0x04003435 RID: 13365
		[SerializeField]
		private SoundPlayer m_RepairAudio;

		// Token: 0x04003436 RID: 13366
		private WaitForSeconds m_UpdateInterval;

		// Token: 0x04003437 RID: 13367
		private ItemContainer m_Inventory;

		// Token: 0x020008D1 RID: 2257
		public class ItemToRepair
		{
			// Token: 0x17000623 RID: 1571
			// (get) Token: 0x06003A14 RID: 14868 RVA: 0x0002A369 File Offset: 0x00028569
			// (set) Token: 0x06003A15 RID: 14869 RVA: 0x0002A371 File Offset: 0x00028571
			public Recipe Recipe { get; set; }

			// Token: 0x17000624 RID: 1572
			// (get) Token: 0x06003A16 RID: 14870 RVA: 0x0002A37A File Offset: 0x0002857A
			// (set) Token: 0x06003A17 RID: 14871 RVA: 0x0002A382 File Offset: 0x00028582
			public ItemProperty.Value DurabilityProperty { get; set; }
		}

		// Token: 0x020008D2 RID: 2258
		public struct RequiredItem
		{
			// Token: 0x17000625 RID: 1573
			// (get) Token: 0x06003A19 RID: 14873 RVA: 0x0002A38B File Offset: 0x0002858B
			// (set) Token: 0x06003A1A RID: 14874 RVA: 0x0002A393 File Offset: 0x00028593
			public string Name { get; private set; }

			// Token: 0x17000626 RID: 1574
			// (get) Token: 0x06003A1B RID: 14875 RVA: 0x0002A39C File Offset: 0x0002859C
			// (set) Token: 0x06003A1C RID: 14876 RVA: 0x0002A3A4 File Offset: 0x000285A4
			public int Needs { get; private set; }

			// Token: 0x17000627 RID: 1575
			// (get) Token: 0x06003A1D RID: 14877 RVA: 0x0002A3AD File Offset: 0x000285AD
			// (set) Token: 0x06003A1E RID: 14878 RVA: 0x0002A3B5 File Offset: 0x000285B5
			public int Has { get; private set; }

			// Token: 0x06003A1F RID: 14879 RVA: 0x0002A3BE File Offset: 0x000285BE
			public RequiredItem(string name, int needs, int has)
			{
				this = default(Anvil.RequiredItem);
				this.Name = name;
				this.Needs = needs;
				this.Has = has;
			}

			// Token: 0x06003A20 RID: 14880 RVA: 0x0002A3DC File Offset: 0x000285DC
			public bool HasEnough()
			{
				return this.Has >= this.Needs;
			}
		}
	}
}
