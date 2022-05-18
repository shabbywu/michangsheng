using System;
using UnityEngine;
using UnityEngine.Events;

namespace PaiMai
{
	// Token: 0x02000A6E RID: 2670
	public class PlayerCommand : MonoBehaviour
	{
		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x060044BE RID: 17598 RVA: 0x0003124F File Offset: 0x0002F44F
		// (set) Token: 0x060044BF RID: 17599 RVA: 0x00031257 File Offset: 0x0002F457
		public bool CanClick { get; private set; }

		// Token: 0x060044C0 RID: 17600 RVA: 0x001D7204 File Offset: 0x001D5404
		public void AddClickAction(UnityAction action)
		{
			if (this._btn == null)
			{
				this._btn = base.GetComponentInChildren<FpBtn>();
				this._btn.mouseUpEvent.AddListener(action);
			}
			if (this._animator == null)
			{
				this._animator = base.GetComponent<Animator>();
			}
		}

		// Token: 0x060044C1 RID: 17601 RVA: 0x00031260 File Offset: 0x0002F460
		public void AddMouseEnterListener(UnityAction action)
		{
			if (this._btn == null)
			{
				this._btn = base.GetComponentInChildren<FpBtn>();
			}
			this._btn.mouseEnterEvent.AddListener(action);
		}

		// Token: 0x060044C2 RID: 17602 RVA: 0x0003128D File Offset: 0x0002F48D
		public void AddMouseOutsListener(UnityAction action)
		{
			if (this._btn == null)
			{
				this._btn = base.GetComponentInChildren<FpBtn>();
			}
			this._btn.mouseOutEvent.AddListener(action);
		}

		// Token: 0x060044C3 RID: 17603 RVA: 0x000312BA File Offset: 0x0002F4BA
		public void PlayToBack()
		{
			this._btn.SetCanClick(false);
			this.CanClick = false;
			this._animator.Play("ToBack");
		}

		// Token: 0x060044C4 RID: 17604 RVA: 0x000312DF File Offset: 0x0002F4DF
		public void PlayBackTo()
		{
			this._animator.Play("BackTo");
		}

		// Token: 0x060044C5 RID: 17605 RVA: 0x000312F1 File Offset: 0x0002F4F1
		public void BackTo()
		{
			this.CanClick = true;
			this._btn.SetCanClick(true);
		}

		// Token: 0x04003CE0 RID: 15584
		private FpBtn _btn;

		// Token: 0x04003CE1 RID: 15585
		private Animator _animator;

		// Token: 0x04003CE2 RID: 15586
		public CommandType CommandType;

		// Token: 0x04003CE3 RID: 15587
		public CeLueType CeLueType;

		// Token: 0x04003CE4 RID: 15588
		private UnityAction _toBack;
	}
}
