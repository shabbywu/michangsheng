using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000602 RID: 1538
	public class LootObject : InteractableObject, IInventoryTrigger
	{
		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06003152 RID: 12626 RVA: 0x0015EA8E File Offset: 0x0015CC8E
		// (set) Token: 0x06003153 RID: 12627 RVA: 0x0015EA96 File Offset: 0x0015CC96
		public List<ItemHolder> ItemHolders { get; private set; }

		// Token: 0x06003154 RID: 12628 RVA: 0x0015EA9F File Offset: 0x0015CC9F
		public override void OnInteract(PlayerEventHandler player)
		{
			if (base.enabled && MonoSingleton<InventoryController>.Instance.OpenLootContainer.Try(this))
			{
				this.On_InventoryOpened();
				MonoSingleton<InventoryController>.Instance.State.AddChangeListener(new Action(this.OnChanged_InventoryController_State));
			}
		}

		// Token: 0x06003155 RID: 12629 RVA: 0x0015EADC File Offset: 0x0015CCDC
		private void Start()
		{
			this.ItemHolders = new List<ItemHolder>();
			for (int i = 0; i < this.m_Capacity; i++)
			{
				this.ItemHolders.Add(new ItemHolder());
				SavableItem item;
				if (i < this.m_InitialItems.Length && this.m_InitialItems[i].TryGenerate(out item))
				{
					this.ItemHolders[i].SetItem(item);
				}
			}
		}

		// Token: 0x06003156 RID: 12630 RVA: 0x0015EB43 File Offset: 0x0015CD43
		private void OnChanged_InventoryController_State()
		{
			if (MonoSingleton<InventoryController>.Instance.IsClosed)
			{
				this.On_InventoryClosed();
			}
		}

		// Token: 0x06003157 RID: 12631 RVA: 0x0015EB57 File Offset: 0x0015CD57
		private void On_InventoryOpened()
		{
			if (this.m_Cover != null)
			{
				base.StopAllCoroutines();
				base.StartCoroutine(this.C_OpenCover(true));
			}
		}

		// Token: 0x06003158 RID: 12632 RVA: 0x0015EB7B File Offset: 0x0015CD7B
		private void On_InventoryClosed()
		{
			if (this.m_Cover != null)
			{
				base.StopAllCoroutines();
				base.StartCoroutine(this.C_OpenCover(false));
			}
		}

		// Token: 0x06003159 RID: 12633 RVA: 0x0015EB9F File Offset: 0x0015CD9F
		private IEnumerator C_OpenCover(bool open)
		{
			float targetRotation = open ? this.m_OpenRotation : this.m_ClosedRotation;
			while (Mathf.Abs(targetRotation - this.m_CurrentRotation) > 0.1f)
			{
				this.m_CurrentRotation = Mathf.Lerp(this.m_CurrentRotation, targetRotation, Time.deltaTime * this.m_OpenSpeed);
				this.m_Cover.transform.localRotation = Quaternion.Euler(this.m_CurrentRotation, 0f, 0f);
				yield return null;
			}
			yield break;
		}

		// Token: 0x04002B72 RID: 11122
		[SerializeField]
		[Range(1f, 50f)]
		protected int m_Capacity = 8;

		// Token: 0x04002B73 RID: 11123
		[SerializeField]
		protected ItemToGenerate[] m_InitialItems;

		// Token: 0x04002B74 RID: 11124
		[Header("Box Opening")]
		[SerializeField]
		private Transform m_Cover;

		// Token: 0x04002B75 RID: 11125
		[SerializeField]
		private float m_OpenSpeed = 6f;

		// Token: 0x04002B76 RID: 11126
		[SerializeField]
		private float m_ClosedRotation;

		// Token: 0x04002B77 RID: 11127
		[SerializeField]
		private float m_OpenRotation = 60f;

		// Token: 0x04002B78 RID: 11128
		private float m_CurrentRotation;
	}
}
