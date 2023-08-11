using System.Collections;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem;

public class QueueElement : MonoBehaviour
{
	public Message<QueueElement> Cancel = new Message<QueueElement>();

	public Message Complete = new Message();

	[SerializeField]
	private Image m_Icon;

	[SerializeField]
	private Text m_RemainedTime;

	[SerializeField]
	private Image m_ProgressBar;

	private CraftData m_CraftData;

	private ItemData m_Result;

	private ItemContainer m_Inventory;

	private int m_AmountToCraftRemained;

	private bool m_Initialized;

	public void Initialize(CraftData craftData, ItemContainer inventory)
	{
		if (!m_Initialized)
		{
			RequiredItem[] requiredItems = craftData.Result.Recipe.RequiredItems;
			for (int i = 0; i < requiredItems.Length; i++)
			{
				_ = requiredItems[i];
			}
			m_Result = craftData.Result;
			m_Icon.sprite = m_Result.Icon;
			m_RemainedTime.text = $"{craftData.Result.Recipe.Duration}s (x{craftData.Amount})";
			m_ProgressBar.fillAmount = 0f;
			m_AmountToCraftRemained = craftData.Amount;
			m_CraftData = craftData;
			m_Inventory = inventory;
			m_Initialized = true;
		}
	}

	public void StartCrafting()
	{
		if (m_Initialized)
		{
			((MonoBehaviour)this).StartCoroutine(C_Update(m_CraftData, m_Inventory));
		}
	}

	public void CancelCrafting()
	{
		if (m_AmountToCraftRemained > 0)
		{
			RequiredItem[] requiredItems = m_CraftData.Result.Recipe.RequiredItems;
			foreach (RequiredItem requiredItem in requiredItems)
			{
				MonoSingleton<InventoryController>.Instance.Database.FindItemByName(requiredItem.Name, out var _);
			}
		}
		Cancel.Send(this);
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	private IEnumerator C_Update(CraftData craftData, ItemContainer inventory)
	{
		int oldm_AmountToCraftRemained = m_AmountToCraftRemained;
		while (m_AmountToCraftRemained > 0)
		{
			float remainedTime = craftData.Result.Recipe.Duration;
			while (remainedTime > 0f)
			{
				yield return null;
				remainedTime = Mathf.Clamp(remainedTime - Time.deltaTime, 0f, float.PositiveInfinity);
				m_RemainedTime.text = $"{Mathf.Ceil(remainedTime)}s (x{m_AmountToCraftRemained})";
				m_ProgressBar.fillAmount = 1f - remainedTime / (float)craftData.Result.Recipe.Duration;
			}
			m_AmountToCraftRemained--;
			m_ProgressBar.fillAmount = 0f;
			m_RemainedTime.text = $"{0}s (x{m_AmountToCraftRemained})";
		}
		((Avatar)KBEngineApp.app.player())?.StartCrafting(m_Result.Id, oldm_AmountToCraftRemained);
		Complete.Send();
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}
}
