using System;
using System.Collections;
using UnityEngine;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000639 RID: 1593
	[RequireComponent(typeof(Animator))]
	public class Window : MonoBehaviour
	{
		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x0600328F RID: 12943 RVA: 0x00165C7A File Offset: 0x00163E7A
		// (set) Token: 0x06003290 RID: 12944 RVA: 0x00165C82 File Offset: 0x00163E82
		public bool IsOpen { get; private set; }

		// Token: 0x06003291 RID: 12945 RVA: 0x00165C8C File Offset: 0x00163E8C
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

		// Token: 0x06003292 RID: 12946 RVA: 0x00165CF0 File Offset: 0x00163EF0
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

		// Token: 0x06003293 RID: 12947 RVA: 0x00165D51 File Offset: 0x00163F51
		public void Refresh()
		{
			this.m_Animator.SetTrigger("Refresh");
		}

		// Token: 0x06003294 RID: 12948 RVA: 0x00165D63 File Offset: 0x00163F63
		protected virtual void Start()
		{
			this.IsOpen = true;
			this.m_Animator = base.GetComponent<Animator>();
			this.UpdateAnimatorParams();
			MonoSingleton<InventoryController>.Instance.State.AddChangeListener(new Action(this.OnChanged_InventoryState));
			this.Close(true);
		}

		// Token: 0x06003295 RID: 12949 RVA: 0x00165DA0 File Offset: 0x00163FA0
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

		// Token: 0x06003296 RID: 12950 RVA: 0x00165DFB File Offset: 0x00163FFB
		private IEnumerator C_DisableWithDelay()
		{
			yield return new WaitForSeconds(0.5f);
			this.Disable();
			yield break;
		}

		// Token: 0x06003297 RID: 12951 RVA: 0x00165E0A File Offset: 0x0016400A
		private void Disable()
		{
			base.GetComponent<CanvasGroup>().interactable = false;
			base.GetComponent<CanvasGroup>().blocksRaycasts = false;
		}

		// Token: 0x06003298 RID: 12952 RVA: 0x00165E24 File Offset: 0x00164024
		private void OnValidate()
		{
			this.UpdateAnimatorParams();
		}

		// Token: 0x06003299 RID: 12953 RVA: 0x00165E2C File Offset: 0x0016402C
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

		// Token: 0x04002D02 RID: 11522
		[Header("Animation Speed")]
		[SerializeField]
		private float m_HideSpeed = 1.3f;

		// Token: 0x04002D03 RID: 11523
		[SerializeField]
		private float m_ShowSpeed = 1.3f;

		// Token: 0x04002D04 RID: 11524
		[SerializeField]
		private float m_RefreshSpeed = 1.3f;

		// Token: 0x04002D05 RID: 11525
		[Header("How Is Opened")]
		[SerializeField]
		[Tooltip("Inventory Opened - will open when the inventory is opened. \nSpecific State - will open when the inventory opens in a specific state (e.g. Furnace-mode, Campfire-mode). \nManual - will have to be manually opened from another script.")]
		private Window.OpenTrigger m_OpenTrigger;

		// Token: 0x04002D06 RID: 11526
		[SerializeField]
		private ET.InventoryState m_StateTrigger = ET.InventoryState.Normal;

		// Token: 0x04002D07 RID: 11527
		private Animator m_Animator;

		// Token: 0x020014DD RID: 5341
		public enum OpenTrigger
		{
			// Token: 0x04006D98 RID: 28056
			InventoryOpened,
			// Token: 0x04006D99 RID: 28057
			SpecificState,
			// Token: 0x04006D9A RID: 28058
			Manual
		}
	}
}
