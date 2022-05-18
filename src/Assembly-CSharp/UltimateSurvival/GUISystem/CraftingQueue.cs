using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000940 RID: 2368
	public class CraftingQueue : MonoBehaviour
	{
		// Token: 0x06003C91 RID: 15505 RVA: 0x001B125C File Offset: 0x001AF45C
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

		// Token: 0x06003C92 RID: 15506 RVA: 0x001B12B0 File Offset: 0x001AF4B0
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

		// Token: 0x06003C93 RID: 15507 RVA: 0x001B1384 File Offset: 0x001AF584
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

		// Token: 0x06003C94 RID: 15508 RVA: 0x0002BB8C File Offset: 0x00029D8C
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

		// Token: 0x040036D0 RID: 14032
		[SerializeField]
		private ItemContainer m_Inventory;

		// Token: 0x040036D1 RID: 14033
		[SerializeField]
		private QueueElement m_QueueElementTemplate;

		// Token: 0x040036D2 RID: 14034
		[SerializeField]
		private Transform m_QueueParent;

		// Token: 0x040036D3 RID: 14035
		[SerializeField]
		private int m_MaxElements = 8;

		// Token: 0x040036D4 RID: 14036
		private List<QueueElement> m_Queue = new List<QueueElement>();

		// Token: 0x040036D5 RID: 14037
		private QueueElement m_ActiveElement;
	}
}
