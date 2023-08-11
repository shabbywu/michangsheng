using System.Collections;
using UltimateSurvival.GUISystem;
using UnityEngine;

namespace UltimateSurvival;

public class Anvil : InteractableObject, IInventoryTrigger
{
	public class ItemToRepair
	{
		public Recipe Recipe { get; set; }

		public ItemProperty.Value DurabilityProperty { get; set; }
	}

	public struct RequiredItem
	{
		public string Name { get; private set; }

		public int Needs { get; private set; }

		public int Has { get; private set; }

		public RequiredItem(string name, int needs, int has)
		{
			this = default(RequiredItem);
			Name = name;
			Needs = needs;
			Has = has;
		}

		public bool HasEnough()
		{
			return Has >= Needs;
		}
	}

	private const float UPDATE_INTERVAL = 0.1f;

	public Activity Repairing = new Activity();

	public Value<float> RepairProgress = new Value<float>(0f);

	public Value<bool> InputItemReadyForRepair = new Value<bool>(initialValue: false);

	public Message RepairFinished = new Message();

	[SerializeField]
	[Range(0f, 1f)]
	[Tooltip("1 - the requirements will be the same as if crafting the item (when durability is low).\n0 - the requirements will be very low.")]
	private float m_RequirementRatio = 0.6f;

	[Header("Audio")]
	[SerializeField]
	private AudioSource m_AudioSource;

	[SerializeField]
	private SoundPlayer m_RepairAudio;

	private WaitForSeconds m_UpdateInterval;

	private ItemContainer m_Inventory;

	public float RequirementRatio => m_RequirementRatio;

	public ItemHolder InputHolder { get; private set; }

	public ItemHolder ResultHolder { get; private set; }

	public ItemToRepair InputItem { get; private set; }

	public RequiredItem[] RequiredItems { get; private set; }

	public override void OnInteract(PlayerEventHandler player)
	{
		MonoSingleton<InventoryController>.Instance.OpenAnvil.Try(this);
	}

	private void Start()
	{
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Expected O, but got Unknown
		InputItem = new ItemToRepair();
		InputHolder = new ItemHolder();
		ResultHolder = new ItemHolder();
		InputHolder.Updated.AddListener(On_InputHolderUpdated);
		m_Inventory = MonoSingleton<GUIController>.Instance.GetContainer("Inventory");
		m_Inventory.Slot_Refreshed.AddListener(On_InventorySlotRefreshed);
		Repairing.AddStartTryer(TryStart_Repairing);
		Repairing.AddStopListener(OnStop_Repairing);
		RepairFinished.AddListener(On_RepairFinished);
		m_UpdateInterval = new WaitForSeconds(0.1f);
	}

	private void On_InventorySlotRefreshed(Slot slot)
	{
		if (InputItemReadyForRepair.Is(value: true))
		{
			CalculateRequiredItems(InputItem.Recipe, InputItem.DurabilityProperty.Float.Ratio);
			InputItemReadyForRepair.SetAndForceUpdate(value: true);
		}
		if (Repairing.Active && InputItemReadyForRepair.Get() && !HasRequiredItems())
		{
			Repairing.ForceStop();
		}
	}

	private bool TryStart_Repairing()
	{
		if (InputItemReadyForRepair.Is(value: false) || !HasRequiredItems())
		{
			return false;
		}
		((MonoBehaviour)this).StartCoroutine(C_Repair());
		return true;
	}

	private void OnStop_Repairing()
	{
		((MonoBehaviour)this).StopAllCoroutines();
		RepairProgress.Set(0f);
	}

	private void On_RepairFinished()
	{
		SavableItem currentItem = InputHolder.CurrentItem;
		InputHolder.SetItem(null);
		ResultHolder.SetItem(currentItem);
		RequiredItem[] requiredItems = RequiredItems;
		for (int i = 0; i < requiredItems.Length; i++)
		{
			RequiredItem requiredItem = requiredItems[i];
			m_Inventory.RemoveItems(requiredItem.Name, requiredItem.Needs);
		}
	}

	private void On_InputHolderUpdated(ItemHolder holder)
	{
		ItemProperty.Value value = null;
		if (holder.HasItem && holder.CurrentItem.HasProperty("Durability"))
		{
			value = holder.CurrentItem.GetPropertyValue("Durability");
		}
		if (value != null && holder.CurrentItem.ItemData.IsCraftable && value.Float.Ratio != 1f && holder.CurrentItem.ItemData.IsCraftable)
		{
			InputItem.Recipe = holder.CurrentItem.ItemData.Recipe;
			InputItem.DurabilityProperty = value;
			CalculateRequiredItems(InputItem.Recipe, InputItem.DurabilityProperty.Float.Ratio);
			InputItemReadyForRepair.SetAndForceUpdate(value: true);
		}
		else
		{
			InputItemReadyForRepair.SetAndForceUpdate(value: false);
			if (Repairing.Active)
			{
				Repairing.ForceStop();
			}
		}
	}

	private void CalculateRequiredItems(Recipe recipe, float durabilityRatio)
	{
		RequiredItems = new RequiredItem[recipe.RequiredItems.Length];
		for (int i = 0; i < recipe.RequiredItems.Length; i++)
		{
			int itemCount = m_Inventory.GetItemCount(recipe.RequiredItems[i].Name);
			int needs = Mathf.RoundToInt((float)recipe.RequiredItems[i].Amount * (1f - durabilityRatio) * m_RequirementRatio) + 1;
			RequiredItems[i] = new RequiredItem(recipe.RequiredItems[i].Name, needs, itemCount);
		}
	}

	private bool HasRequiredItems()
	{
		for (int i = 0; i < RequiredItems.Length; i++)
		{
			if (!RequiredItems[i].HasEnough())
			{
				if (Repairing.Active)
				{
					Repairing.ForceStop();
				}
				return false;
			}
		}
		return true;
	}

	private IEnumerator C_Repair()
	{
		float requiredTime = (float)InputItem.Recipe.Duration * (1f - InputItem.DurabilityProperty.Float.Ratio);
		float elapsedTime = 0f;
		while (elapsedTime < requiredTime)
		{
			yield return m_UpdateInterval;
			elapsedTime += 0.1f;
			RepairProgress.Set(elapsedTime / requiredTime);
		}
		RepairProgress.Set(0f);
		ItemProperty.Float @float = InputItem.DurabilityProperty.Float;
		@float.Current = @float.Default;
		RepairFinished.Send();
		InputItem.DurabilityProperty.SetValue(ItemProperty.Type.Float, @float);
	}
}
