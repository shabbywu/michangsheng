using System;
using UnityEngine;
using UnityEngine.Events;

namespace PaiMai
{
	// Token: 0x0200071B RID: 1819
	public class PlayerCommand : MonoBehaviour
	{
		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06003A2F RID: 14895 RVA: 0x0018FCB9 File Offset: 0x0018DEB9
		// (set) Token: 0x06003A30 RID: 14896 RVA: 0x0018FCC1 File Offset: 0x0018DEC1
		public bool CanClick { get; private set; }

		// Token: 0x06003A31 RID: 14897 RVA: 0x0018FCCC File Offset: 0x0018DECC
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

		// Token: 0x06003A32 RID: 14898 RVA: 0x0018FD1E File Offset: 0x0018DF1E
		public void AddMouseEnterListener(UnityAction action)
		{
			if (this._btn == null)
			{
				this._btn = base.GetComponentInChildren<FpBtn>();
			}
			this._btn.mouseEnterEvent.AddListener(action);
		}

		// Token: 0x06003A33 RID: 14899 RVA: 0x0018FD4B File Offset: 0x0018DF4B
		public void AddMouseOutsListener(UnityAction action)
		{
			if (this._btn == null)
			{
				this._btn = base.GetComponentInChildren<FpBtn>();
			}
			this._btn.mouseOutEvent.AddListener(action);
		}

		// Token: 0x06003A34 RID: 14900 RVA: 0x0018FD78 File Offset: 0x0018DF78
		public void PlayToBack()
		{
			this._btn.SetCanClick(false);
			this.CanClick = false;
			this._animator.Play("ToBack");
		}

		// Token: 0x06003A35 RID: 14901 RVA: 0x0018FD9D File Offset: 0x0018DF9D
		public void PlayBackTo()
		{
			this._animator.Play("BackTo");
		}

		// Token: 0x06003A36 RID: 14902 RVA: 0x0018FDAF File Offset: 0x0018DFAF
		public void BackTo()
		{
			this.CanClick = true;
			this._btn.SetCanClick(true);
		}

		// Token: 0x0400324F RID: 12879
		private FpBtn _btn;

		// Token: 0x04003250 RID: 12880
		private Animator _animator;

		// Token: 0x04003251 RID: 12881
		public CommandType CommandType;

		// Token: 0x04003252 RID: 12882
		public CeLueType CeLueType;

		// Token: 0x04003253 RID: 12883
		private UnityAction _toBack;
	}
}
