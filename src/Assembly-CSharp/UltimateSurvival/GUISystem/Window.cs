using System;
using System.Collections;
using UnityEngine;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x0200092C RID: 2348
	[RequireComponent(typeof(Animator))]
	public class Window : MonoBehaviour
	{
		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06003BCF RID: 15311 RVA: 0x0002B3F7 File Offset: 0x000295F7
		// (set) Token: 0x06003BD0 RID: 15312 RVA: 0x0002B3FF File Offset: 0x000295FF
		public bool IsOpen { get; private set; }

		// Token: 0x06003BD1 RID: 15313 RVA: 0x001AF150 File Offset: 0x001AD350
		public virtual void Open()
		{
			if (this.IsOpen)
			{
				this.m_Animator.SetTrigger("Refresh");
				return;
			}
			base.StopAllCoroutines();
			base.GetComponent<CanvasGroup>().interactable = true;
			base.GetComponent<CanvasGroup>().blocksRaycasts = true;
			this.UpdateAnimatorParams();
			this.m_Animator.SetTrigger("Show");
			this.IsOpen = true;
		}

		// Token: 0x06003BD2 RID: 15314 RVA: 0x001AF1B4 File Offset: 0x001AD3B4
		public virtual void Close(bool instant = false)
		{
			if (!this.IsOpen)
			{
				return;
			}
			this.UpdateAnimatorParams();
			if (!instant)
			{
				this.m_Animator.SetTrigger("Hide");
				base.StartCoroutine(this.C_DisableWithDelay());
			}
			else
			{
				this.m_Animator.Play("Hide", 0, 1f);
				this.Disable();
			}
			this.IsOpen = false;
		}

		// Token: 0x06003BD3 RID: 15315 RVA: 0x0002B408 File Offset: 0x00029608
		public void Refresh()
		{
			this.m_Animator.SetTrigger("Refresh");
		}

		// Token: 0x06003BD4 RID: 15316 RVA: 0x0002B41A File Offset: 0x0002961A
		protected virtual void Start()
		{
			this.IsOpen = true;
			this.m_Animator = base.GetComponent<Animator>();
			this.UpdateAnimatorParams();
			MonoSingleton<InventoryController>.Instance.State.AddChangeListener(new Action(this.OnChanged_InventoryState));
			this.Close(true);
		}

		// Token: 0x06003BD5 RID: 15317 RVA: 0x001AF218 File Offset: 0x001AD418
		private void OnChanged_InventoryState()
		{
			bool flag = false;
			if (!MonoSingleton<InventoryController>.Instance.IsClosed)
			{
				flag = (this.m_OpenTrigger != Window.OpenTrigger.Manual && (this.m_OpenTrigger == Window.OpenTrigger.InventoryOpened || MonoSingleton<InventoryController>.Instance.State.Is(this.m_StateTrigger)));
			}
			if (flag)
			{
				this.Open();
				return;
			}
			this.Close(false);
		}

		// Token: 0x06003BD6 RID: 15318 RVA: 0x0002B457 File Offset: 0x00029657
		private IEnumerator C_DisableWithDelay()
		{
			yield return new WaitForSeconds(0.5f);
			this.Disable();
			yield break;
		}

		// Token: 0x06003BD7 RID: 15319 RVA: 0x0002B466 File Offset: 0x00029666
		private void Disable()
		{
			base.GetComponent<CanvasGroup>().interactable = false;
			base.GetComponent<CanvasGroup>().blocksRaycasts = false;
		}

		// Token: 0x06003BD8 RID: 15320 RVA: 0x0002B480 File Offset: 0x00029680
		private void OnValidate()
		{
			this.UpdateAnimatorParams();
		}

		// Token: 0x06003BD9 RID: 15321 RVA: 0x001AF274 File Offset: 0x001AD474
		private void UpdateAnimatorParams()
		{
			if (!this.m_Animator)
			{
				return;
			}
			this.m_Animator.SetFloat("Hide Speed", this.m_HideSpeed);
			this.m_Animator.SetFloat("Show Speed", this.m_ShowSpeed);
			this.m_Animator.SetFloat("Refresh Speed", this.m_RefreshSpeed);
		}

		// Token: 0x04003657 RID: 13911
		[Header("Animation Speed")]
		[SerializeField]
		private float m_HideSpeed = 1.3f;

		// Token: 0x04003658 RID: 13912
		[SerializeField]
		private float m_ShowSpeed = 1.3f;

		// Token: 0x04003659 RID: 13913
		[SerializeField]
		private float m_RefreshSpeed = 1.3f;

		// Token: 0x0400365A RID: 13914
		[Header("How Is Opened")]
		[SerializeField]
		[Tooltip("Inventory Opened - will open when the inventory is opened. \nSpecific State - will open when the inventory opens in a specific state (e.g. Furnace-mode, Campfire-mode). \nManual - will have to be manually opened from another script.")]
		private Window.OpenTrigger m_OpenTrigger;

		// Token: 0x0400365B RID: 13915
		[SerializeField]
		private ET.InventoryState m_StateTrigger = ET.InventoryState.Normal;

		// Token: 0x0400365C RID: 13916
		private Animator m_Animator;

		// Token: 0x0200092D RID: 2349
		public enum OpenTrigger
		{
			// Token: 0x0400365E RID: 13918
			InventoryOpened,
			// Token: 0x0400365F RID: 13919
			SpecificState,
			// Token: 0x04003660 RID: 13920
			Manual
		}
	}
}
