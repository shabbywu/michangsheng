using System;
using System.Collections;
using UltimateSurvival.GUISystem;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005F9 RID: 1529
	public class Anvil : InteractableObject, IInventoryTrigger
	{
		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06003116 RID: 12566 RVA: 0x0015DEBC File Offset: 0x0015C0BC
		public float RequirementRatio
		{
			get
			{
				return this.m_RequirementRatio;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06003117 RID: 12567 RVA: 0x0015DEC4 File Offset: 0x0015C0C4
		// (set) Token: 0x06003118 RID: 12568 RVA: 0x0015DECC File Offset: 0x0015C0CC
		public ItemHolder InputHolder { get; private set; }

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06003119 RID: 12569 RVA: 0x0015DED5 File Offset: 0x0015C0D5
		// (set) Token: 0x0600311A RID: 12570 RVA: 0x0015DEDD File Offset: 0x0015C0DD
		public ItemHolder ResultHolder { get; private set; }

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x0600311B RID: 12571 RVA: 0x0015DEE6 File Offset: 0x0015C0E6
		// (set) Token: 0x0600311C RID: 12572 RVA: 0x0015DEEE File Offset: 0x0015C0EE
		public Anvil.ItemToRepair InputItem { get; private set; }

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x0600311D RID: 12573 RVA: 0x0015DEF7 File Offset: 0x0015C0F7
		// (set) Token: 0x0600311E RID: 12574 RVA: 0x0015DEFF File Offset: 0x0015C0FF
		public Anvil.RequiredItem[] RequiredItems { get; private set; }

		// Token: 0x0600311F RID: 12575 RVA: 0x0015DF08 File Offset: 0x0015C108
		public override void OnInteract(PlayerEventHandler player)
		{
			MonoSingleton<InventoryController>.Instance.OpenAnvil.Try(this);
		}

		// Token: 0x06003120 RID: 12576 RVA: 0x0015DF1C File Offset: 0x0015C11C
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

		// Token: 0x06003121 RID: 12577 RVA: 0x0015DFEC File Offset: 0x0015C1EC
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

		// Token: 0x06003122 RID: 12578 RVA: 0x0015E069 File Offset: 0x0015C269
		private bool TryStart_Repairing()
		{
			if (this.InputItemReadyForRepair.Is(false) || !this.HasRequiredItems())
			{
				return false;
			}
			base.StartCoroutine(this.C_Repair());
			return true;
		}

		// Token: 0x06003123 RID: 12579 RVA: 0x0015E091 File Offset: 0x0015C291
		private void OnStop_Repairing()
		{
			base.StopAllCoroutines();
			this.RepairProgress.Set(0f);
		}

		// Token: 0x06003124 RID: 12580 RVA: 0x0015E0AC File Offset: 0x0015C2AC
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

		// Token: 0x06003125 RID: 12581 RVA: 0x0015E114 File Offset: 0x0015C314
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

		// Token: 0x06003126 RID: 12582 RVA: 0x0015E214 File Offset: 0x0015C414
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

		// Token: 0x06003127 RID: 12583 RVA: 0x0015E2A8 File Offset: 0x0015C4A8
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

		// Token: 0x06003128 RID: 12584 RVA: 0x0015E2F6 File Offset: 0x0015C4F6
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

		// Token: 0x04002B47 RID: 11079
		private const float UPDATE_INTERVAL = 0.1f;

		// Token: 0x04002B48 RID: 11080
		public Activity Repairing = new Activity();

		// Token: 0x04002B49 RID: 11081
		public Value<float> RepairProgress = new Value<float>(0f);

		// Token: 0x04002B4A RID: 11082
		public Value<bool> InputItemReadyForRepair = new Value<bool>(false);

		// Token: 0x04002B4B RID: 11083
		public Message RepairFinished = new Message();

		// Token: 0x04002B50 RID: 11088
		[SerializeField]
		[Range(0f, 1f)]
		[Tooltip("1 - the requirements will be the same as if crafting the item (when durability is low).\n0 - the requirements will be very low.")]
		private float m_RequirementRatio = 0.6f;

		// Token: 0x04002B51 RID: 11089
		[Header("Audio")]
		[SerializeField]
		private AudioSource m_AudioSource;

		// Token: 0x04002B52 RID: 11090
		[SerializeField]
		private SoundPlayer m_RepairAudio;

		// Token: 0x04002B53 RID: 11091
		private WaitForSeconds m_UpdateInterval;

		// Token: 0x04002B54 RID: 11092
		private ItemContainer m_Inventory;

		// Token: 0x020014C1 RID: 5313
		public class ItemToRepair
		{
			// Token: 0x17000AEB RID: 2795
			// (get) Token: 0x060081C6 RID: 33222 RVA: 0x002D9772 File Offset: 0x002D7972
			// (set) Token: 0x060081C7 RID: 33223 RVA: 0x002D977A File Offset: 0x002D797A
			public Recipe Recipe { get; set; }

			// Token: 0x17000AEC RID: 2796
			// (get) Token: 0x060081C8 RID: 33224 RVA: 0x002D9783 File Offset: 0x002D7983
			// (set) Token: 0x060081C9 RID: 33225 RVA: 0x002D978B File Offset: 0x002D798B
			public ItemProperty.Value DurabilityProperty { get; set; }
		}

		// Token: 0x020014C2 RID: 5314
		public struct RequiredItem
		{
			// Token: 0x17000AED RID: 2797
			// (get) Token: 0x060081CB RID: 33227 RVA: 0x002D9794 File Offset: 0x002D7994
			// (set) Token: 0x060081CC RID: 33228 RVA: 0x002D979C File Offset: 0x002D799C
			public string Name { get; private set; }

			// Token: 0x17000AEE RID: 2798
			// (get) Token: 0x060081CD RID: 33229 RVA: 0x002D97A5 File Offset: 0x002D79A5
			// (set) Token: 0x060081CE RID: 33230 RVA: 0x002D97AD File Offset: 0x002D79AD
			public int Needs { get; private set; }

			// Token: 0x17000AEF RID: 2799
			// (get) Token: 0x060081CF RID: 33231 RVA: 0x002D97B6 File Offset: 0x002D79B6
			// (set) Token: 0x060081D0 RID: 33232 RVA: 0x002D97BE File Offset: 0x002D79BE
			public int Has { get; private set; }

			// Token: 0x060081D1 RID: 33233 RVA: 0x002D97C7 File Offset: 0x002D79C7
			public RequiredItem(string name, int needs, int has)
			{
				this = default(Anvil.RequiredItem);
				this.Name = name;
				this.Needs = needs;
				this.Has = has;
			}

			// Token: 0x060081D2 RID: 33234 RVA: 0x002D97E5 File Offset: 0x002D79E5
			public bool HasEnough()
			{
				return this.Has >= this.Needs;
			}
		}
	}
}
