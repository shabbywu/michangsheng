using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival.GUISystem;

public class CraftingQueue : MonoBehaviour
{
	[SerializeField]
	private ItemContainer m_Inventory;

	[SerializeField]
	private QueueElement m_QueueElementTemplate;

	[SerializeField]
	private Transform m_QueueParent;

	[SerializeField]
	private int m_MaxElements = 8;

	private List<QueueElement> m_Queue = new List<QueueElement>();

	private QueueElement m_ActiveElement;

	private void Start()
	{
		if (!Object.op_Implicit((Object)(object)m_Inventory))
		{
			Debug.LogError((object)"The inventory is not assigned as a reference in the inspector!", (Object)(object)this);
			return;
		}
		((Component)m_QueueElementTemplate).gameObject.SetActive(false);
		MonoSingleton<InventoryController>.Instance.CraftItem.SetTryer(Try_CraftItem);
	}

	private bool Try_CraftItem(CraftData craftData)
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		int num = ((Component)this).GetComponentsInChildren<QueueElement>().Length;
		if (num < m_MaxElements)
		{
			QueueElement queueElement = Object.Instantiate<QueueElement>(m_QueueElementTemplate);
			((Component)queueElement).gameObject.SetActive(true);
			((Component)queueElement).transform.SetParent(m_QueueParent);
			((Component)queueElement).transform.SetAsFirstSibling();
			((Component)queueElement).transform.localPosition = Vector3.zero;
			((Component)queueElement).transform.localScale = Vector3.one;
			queueElement.Initialize(craftData, m_Inventory);
			queueElement.Cancel.AddListener(On_CraftingCanceled);
			if (num == 0)
			{
				queueElement.StartCrafting();
				queueElement.Complete.AddListener(StartNext);
				m_ActiveElement = queueElement;
			}
			else
			{
				m_Queue.Insert(0, queueElement);
			}
			return true;
		}
		return false;
	}

	private void StartNext()
	{
		if (m_Queue.Count > 0)
		{
			QueueElement queueElement = m_Queue[m_Queue.Count - 1];
			m_Queue.Remove(queueElement);
			queueElement.StartCrafting();
			queueElement.Complete.AddListener(StartNext);
			m_ActiveElement = queueElement;
		}
	}

	private void On_CraftingCanceled(QueueElement queueElement)
	{
		if (m_Queue.Contains(queueElement))
		{
			m_Queue.Remove(queueElement);
		}
		if ((Object)(object)queueElement == (Object)(object)m_ActiveElement)
		{
			StartNext();
		}
	}
}
