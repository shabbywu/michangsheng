using System;
using UnityEngine;
using UnityEngine.Events;

namespace YSGame.Fight
{
	// Token: 0x02000DFA RID: 3578
	public class UIFightCenterButtonController : MonoBehaviour
	{
		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x0600565A RID: 22106 RVA: 0x0003DB6C File Offset: 0x0003BD6C
		// (set) Token: 0x0600565B RID: 22107 RVA: 0x002404C0 File Offset: 0x0023E6C0
		public UIFightCenterButtonType ButtonType
		{
			get
			{
				return this.buttonType;
			}
			set
			{
				this.buttonType = value;
				this.AllHide();
				if (this.buttonType == UIFightCenterButtonType.EndRound)
				{
					this.EndRoundBtnObj.SetActive(true);
					this.EndRoundBtn.mouseUpEvent.RemoveAllListeners();
					this.EndRoundBtn.mouseUpEvent.AddListener(delegate()
					{
						RoundManager.instance.PlayerEndRound(true);
					});
					return;
				}
				if (this.buttonType == UIFightCenterButtonType.OnlyOK)
				{
					this.OnlyOKBtnObj.SetActive(true);
					return;
				}
				if (this.buttonType == UIFightCenterButtonType.OkCancel)
				{
					this.OkCancelBtnObj.SetActive(true);
				}
			}
		}

		// Token: 0x0600565C RID: 22108 RVA: 0x0024055C File Offset: 0x0023E75C
		private void Update()
		{
			if (Input.GetKeyUp(27))
			{
				if (this.ButtonType == UIFightCenterButtonType.OkCancel)
				{
					this.CancelBtn.mouseUpEvent.Invoke();
				}
				else if (this.ButtonType == UIFightCenterButtonType.EndRound)
				{
					this.EndRoundBtn.mouseUpEvent.Invoke();
				}
			}
			if (Input.GetKeyUp(32))
			{
				if (this.ButtonType == UIFightCenterButtonType.OkCancel)
				{
					this.OkBtn.mouseUpEvent.Invoke();
					return;
				}
				if (this.ButtonType == UIFightCenterButtonType.OnlyOK)
				{
					this.OnlyOKBtn.mouseUpEvent.Invoke();
				}
			}
		}

		// Token: 0x0600565D RID: 22109 RVA: 0x0003DB74 File Offset: 0x0003BD74
		private void AllHide()
		{
			this.EndRoundBtnObj.SetActive(false);
			this.OnlyOKBtnObj.SetActive(false);
			this.OkCancelBtnObj.SetActive(false);
		}

		// Token: 0x0600565E RID: 22110 RVA: 0x002405E4 File Offset: 0x0023E7E4
		public void SetOkCancelEvent(UnityAction ok, UnityAction cancel)
		{
			this.OkBtn.mouseUpEvent.RemoveAllListeners();
			this.OkBtn.mouseUpEvent.AddListener(ok);
			this.CancelBtn.mouseUpEvent.RemoveAllListeners();
			this.CancelBtn.mouseUpEvent.AddListener(cancel);
		}

		// Token: 0x0600565F RID: 22111 RVA: 0x0003DB9A File Offset: 0x0003BD9A
		public void SetOnlyOKEvent(UnityAction ok)
		{
			this.OnlyOKBtn.mouseUpEvent.RemoveAllListeners();
			this.OnlyOKBtn.mouseUpEvent.AddListener(ok);
		}

		// Token: 0x040055FA RID: 22010
		public GameObject EndRoundBtnObj;

		// Token: 0x040055FB RID: 22011
		public GameObject OnlyOKBtnObj;

		// Token: 0x040055FC RID: 22012
		public GameObject OkCancelBtnObj;

		// Token: 0x040055FD RID: 22013
		public FpBtn EndRoundBtn;

		// Token: 0x040055FE RID: 22014
		public FpBtn OnlyOKBtn;

		// Token: 0x040055FF RID: 22015
		public FpBtn OkBtn;

		// Token: 0x04005600 RID: 22016
		public FpBtn CancelBtn;

		// Token: 0x04005601 RID: 22017
		private UIFightCenterButtonType buttonType;
	}
}
