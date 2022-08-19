using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000646 RID: 1606
	public class CraftingQueue : MonoBehaviour
	{
		// Token: 0x06003335 RID: 13109 RVA: 0x00168338 File Offset: 0x00166538
		private void Start()
		{
			if (!this.m_Inventory)
			{
				Debug.LogError("The inventory is not assigned as a reference in the inspector!", this);
				return;
			}
			this.m_QueueElementTemplate.gameObject.SetActive(false);
			MonoSingleton<InventoryController>.Instance.CraftItem.SetTryer(new Attempt<CraftData>.GenericTryerDelegate(this.Try_CraftItem));
		}

		// Token: 0x06003336 RID: 13110 RVA: 0x0016838C File Offset: 0x0016658C
		private bool Try_CraftItem(CraftData craftData)
		{
			int num = base.GetComponentsInChildren<QueueElement>().Length;
			if (num < this.m_MaxElements)
			{
				QueueElement queueElement = Object.Instantiate<QueueElement>(this.m_QueueElementTemplate);
				queueElement.gameObject.SetActive(true);
				queueElement.transform.SetParent(this.m_QueueParent);
				queueElement.transform.SetAsFirstSibling();
				queueElement.transform.localPosition = Vector3.zero;
				queueElement.transform.localScale = Vector3.one;
				queueElement.Initialize(craftData, this.m_Inventory);
				queueElement.Cancel.AddListener(new Action<QueueElement>(this.On_CraftingCanceled));
				if (num == 0)
				{
					queueElement.StartCrafting();
					queueElement.Complete.AddListener(new Action(this.StartNext));
					this.m_ActiveElement = queueElement;
				}
				else
				{
					this.m_Queue.Insert(0, queueElement);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06003337 RID: 13111 RVA: 0x00168460 File Offset: 0x00166660
		private void StartNext()
		{
			if (this.m_Queue.Count > 0)
			{
				QueueElement queueElement = this.m_Queue[this.m_Queue.Count - 1];
				this.m_Queue.Remove(queueElement);
				queueElement.StartCrafting();
				queueElement.Complete.AddListener(new Action(this.StartNext));
				this.m_ActiveElement = queueElement;
			}
		}

		// Token: 0x06003338 RID: 13112 RVA: 0x001684C5 File Offset: 0x001666C5
		private void On_CraftingCanceled(QueueElement queueElement)
		{
			if (this.m_Queue.Contains(queueElement))
			{
				this.m_Queue.Remove(queueElement);
			}
			if (queueElement == this.m_ActiveElement)
			{
				this.StartNext();
			}
		}

		// Token: 0x04002D67 RID: 11623
		[SerializeField]
		private ItemContainer m_Inventory;

		// Token: 0x04002D68 RID: 11624
		[SerializeField]
		private QueueElement m_QueueElementTemplate;

		// Token: 0x04002D69 RID: 11625
		[SerializeField]
		private Transform m_QueueParent;

		// Token: 0x04002D6A RID: 11626
		[SerializeField]
		private int m_MaxElements = 8;

		// Token: 0x04002D6B RID: 11627
		private List<QueueElement> m_Queue = new List<QueueElement>();

		// Token: 0x04002D6C RID: 11628
		private QueueElement m_ActiveElement;
	}
}
