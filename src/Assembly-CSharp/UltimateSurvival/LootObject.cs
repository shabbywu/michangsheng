using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008DF RID: 2271
	public class LootObject : InteractableObject, IInventoryTrigger
	{
		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06003A55 RID: 14933 RVA: 0x0002A650 File Offset: 0x00028850
		// (set) Token: 0x06003A56 RID: 14934 RVA: 0x0002A658 File Offset: 0x00028858
		public List<ItemHolder> ItemHolders { get; private set; }

		// Token: 0x06003A57 RID: 14935 RVA: 0x0002A661 File Offset: 0x00028861
		public override void OnInteract(PlayerEventHandler player)
		{
			if (base.enabled && MonoSingleton<InventoryController>.Instance.OpenLootContainer.Try(this))
			{
				this.On_InventoryOpened();
				MonoSingleton<InventoryController>.Instance.State.AddChangeListener(new Action(this.OnChanged_InventoryController_State));
			}
		}

		// Token: 0x06003A58 RID: 14936 RVA: 0x001A7EF8 File Offset: 0x001A60F8
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

		// Token: 0x06003A59 RID: 14937 RVA: 0x0002A69E File Offset: 0x0002889E
		private void OnChanged_InventoryController_State()
		{
			if (MonoSingleton<InventoryController>.Instance.IsClosed)
			{
				this.On_InventoryClosed();
			}
		}

		// Token: 0x06003A5A RID: 14938 RVA: 0x0002A6B2 File Offset: 0x000288B2
		private void On_InventoryOpened()
		{
			if (this.m_Cover != null)
			{
				base.StopAllCoroutines();
				base.StartCoroutine(this.C_OpenCover(true));
			}
		}

		// Token: 0x06003A5B RID: 14939 RVA: 0x0002A6D6 File Offset: 0x000288D6
		private void On_InventoryClosed()
		{
			if (this.m_Cover != null)
			{
				base.StopAllCoroutines();
				base.StartCoroutine(this.C_OpenCover(false));
			}
		}

		// Token: 0x06003A5C RID: 14940 RVA: 0x0002A6FA File Offset: 0x000288FA
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

		// Token: 0x0400346B RID: 13419
		[SerializeField]
		[Range(1f, 50f)]
		protected int m_Capacity = 8;

		// Token: 0x0400346C RID: 13420
		[SerializeField]
		protected ItemToGenerate[] m_InitialItems;

		// Token: 0x0400346D RID: 13421
		[Header("Box Opening")]
		[SerializeField]
		private Transform m_Cover;

		// Token: 0x0400346E RID: 13422
		[SerializeField]
		private float m_OpenSpeed = 6f;

		// Token: 0x0400346F RID: 13423
		[SerializeField]
		private float m_ClosedRotation;

		// Token: 0x04003470 RID: 13424
		[SerializeField]
		private float m_OpenRotation = 60f;

		// Token: 0x04003471 RID: 13425
		private float m_CurrentRotation;
	}
}
