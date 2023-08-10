using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival;

public class SmeltingStation : InteractableObject, IInventoryTrigger
{
	private const float UPDATE_INTERVAL = 0.1f;

	public Value<bool> IsBurning = new Value<bool>(initialValue: false);

	public Value<float> Progress = new Value<float>(0f);

	public Message BurnedItem = new Message();

	[SerializeField]
	private SmeltingStationType m_Type;

	[SerializeField]
	[Range(1f, 18f)]
	private int m_LootContainerSize = 6;

	[Header("Fire")]
	[SerializeField]
	private ParticleSystem m_Fire;

	[SerializeField]
	private AudioSource m_FireSource;

	[SerializeField]
	private Firelight m_FireLight;

	[SerializeField]
	[Range(0f, 1f)]
	private float m_FireVolume = 0.6f;

	[Header("Damage (Optional)")]
	[SerializeField]
	private DamageArea m_DamageArea;

	private WaitForSeconds m_UpdateInterval = new WaitForSeconds(0.1f);

	private Coroutine m_BurningHandler;

	private ItemProperty.Value m_BurnTimeProperty;

	private ItemProperty.Value m_FuelTimeProperty;

	private string m_ItemResult;

	public ItemHolder FuelSlot { get; private set; }

	public ItemHolder InputSlot { get; private set; }

	public List<ItemHolder> LootSlots { get; private set; }

	public override void OnInteract(PlayerEventHandler player)
	{
		if (m_Type == SmeltingStationType.Campfire)
		{
			MonoSingleton<InventoryController>.Instance.OpenCampfire.Try(this);
		}
		else if (m_Type == SmeltingStationType.Furnace)
		{
			MonoSingleton<InventoryController>.Instance.OpenFurnace.Try(this);
		}
	}

	private void Start()
	{
		FuelSlot = new ItemHolder();
		InputSlot = new ItemHolder();
		FuelSlot.Updated.AddListener(On_HolderUpdated);
		InputSlot.Updated.AddListener(On_HolderUpdated);
		LootSlots = new List<ItemHolder>();
		for (int i = 0; i < m_LootContainerSize; i++)
		{
			LootSlots.Add(new ItemHolder());
		}
		IsBurning.AddChangeListener(OnChanged_IsBurning);
		IsBurning.SetAndForceUpdate(value: false);
	}

	private void On_HolderUpdated(ItemHolder holder)
	{
		bool flag = false;
		if (FuelSlot.HasItem && InputSlot.HasItem)
		{
			if (FuelSlot.CurrentItem.HasProperty("Fuel Time") && InputSlot.CurrentItem.HasProperty("Burn Time") && InputSlot.CurrentItem.HasProperty("Burn Result"))
			{
				if (IsBurning.Is(value: false))
				{
					m_FuelTimeProperty = FuelSlot.CurrentItem.GetPropertyValue("Fuel Time");
					m_BurnTimeProperty = InputSlot.CurrentItem.GetPropertyValue("Burn Time");
					m_ItemResult = InputSlot.CurrentItem.GetPropertyValue("Burn Result").String;
					IsBurning.Set(value: true);
					m_BurningHandler = ((MonoBehaviour)this).StartCoroutine(C_Burn());
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
		if (IsBurning.Is(value: true) && flag)
		{
			StopBurning();
		}
	}

	private void OnChanged_IsBurning()
	{
		if (IsBurning.Is(value: true))
		{
			m_Fire.Play(true);
			GameController.Audio.LerpVolumeOverTime(m_FireSource, m_FireVolume, 3f);
			if (Object.op_Implicit((Object)(object)m_DamageArea))
			{
				m_DamageArea.Active = true;
			}
		}
		else
		{
			m_Fire.Stop(true);
			GameController.Audio.LerpVolumeOverTime(m_FireSource, 0f, 3f);
			if (Object.op_Implicit((Object)(object)m_DamageArea))
			{
				m_DamageArea.Active = false;
			}
			Progress.Set(0f);
		}
		m_FireLight.Toggle(IsBurning.Get());
	}

	private IEnumerator C_Burn()
	{
		while (true)
		{
			yield return m_UpdateInterval;
			if (!FuelSlot.CurrentItem || !InputSlot.CurrentItem)
			{
				StopBurning();
				yield break;
			}
			ItemProperty.Float @float = m_BurnTimeProperty.Float;
			@float.Current -= 0.1f;
			m_BurnTimeProperty.SetValue(ItemProperty.Type.Float, @float);
			Progress.Set(1f - @float.Ratio);
			if (@float.Current <= 0f)
			{
				if (GameController.ItemDatabase.FindItemByName(m_ItemResult, out var itemData))
				{
					CollectionUtils.AddItem(itemData, 1, LootSlots);
				}
				else
				{
					Debug.LogWarning((object)"The item has burned but no result was given, make sure the item has the 'Burn Result' property, so we know what to add as a result of burning / smelting.", (Object)(object)this);
				}
				if (InputSlot.CurrentItem.CurrentInStack == 1)
				{
					InputSlot.SetItem(null);
					StopBurning();
					yield break;
				}
				@float.Current = @float.Default;
				m_BurnTimeProperty.SetValue(ItemProperty.Type.Float, @float);
				InputSlot.CurrentItem.CurrentInStack--;
			}
			ItemProperty.Float float2 = m_FuelTimeProperty.Float;
			float2.Current -= 0.1f;
			m_FuelTimeProperty.SetValue(ItemProperty.Type.Float, float2);
			if (float2.Current <= 0f)
			{
				if (FuelSlot.CurrentItem.CurrentInStack == 1)
				{
					break;
				}
				float2.Current = float2.Default;
				m_FuelTimeProperty.SetValue(ItemProperty.Type.Float, float2);
				FuelSlot.CurrentItem.CurrentInStack--;
			}
		}
		FuelSlot.SetItem(null);
		StopBurning();
	}

	private void StopBurning()
	{
		m_FuelTimeProperty = null;
		m_BurnTimeProperty = null;
		m_ItemResult = string.Empty;
		IsBurning.Set(value: false);
		if (m_BurningHandler != null)
		{
			((MonoBehaviour)this).StopCoroutine(m_BurningHandler);
		}
		m_BurningHandler = null;
	}
}
