using System;
using UnityEngine;
using UnityEngine.Events;

namespace YSGame.Fight
{
	// Token: 0x02000ABD RID: 2749
	public class UIFightCenterButtonController : MonoBehaviour
	{
		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06004D0E RID: 19726 RVA: 0x0020FC9F File Offset: 0x0020DE9F
		// (set) Token: 0x06004D0F RID: 19727 RVA: 0x0020FCA8 File Offset: 0x0020DEA8
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

		// Token: 0x06004D10 RID: 19728 RVA: 0x0020FD44 File Offset: 0x0020DF44
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

		// Token: 0x06004D11 RID: 19729 RVA: 0x0020FDCA File Offset: 0x0020DFCA
		private void AllHide()
		{
			this.EndRoundBtnObj.SetActive(false);
			this.OnlyOKBtnObj.SetActive(false);
			this.OkCancelBtnObj.SetActive(false);
		}

		// Token: 0x06004D12 RID: 19730 RVA: 0x0020FDF0 File Offset: 0x0020DFF0
		public void SetOkCancelEvent(UnityAction ok, UnityAction cancel)
		{
			this.OkBtn.mouseUpEvent.RemoveAllListeners();
			this.OkBtn.mouseUpEvent.AddListener(ok);
			this.CancelBtn.mouseUpEvent.RemoveAllListeners();
			this.CancelBtn.mouseUpEvent.AddListener(cancel);
		}

		// Token: 0x06004D13 RID: 19731 RVA: 0x0020FE3F File Offset: 0x0020E03F
		public void SetOnlyOKEvent(UnityAction ok)
		{
			this.OnlyOKBtn.mouseUpEvent.RemoveAllListeners();
			this.OnlyOKBtn.mouseUpEvent.AddListener(ok);
		}

		// Token: 0x04004C22 RID: 19490
		public GameObject EndRoundBtnObj;

		// Token: 0x04004C23 RID: 19491
		public GameObject OnlyOKBtnObj;

		// Token: 0x04004C24 RID: 19492
		public GameObject OkCancelBtnObj;

		// Token: 0x04004C25 RID: 19493
		public FpBtn EndRoundBtn;

		// Token: 0x04004C26 RID: 19494
		public FpBtn OnlyOKBtn;

		// Token: 0x04004C27 RID: 19495
		public FpBtn OkBtn;

		// Token: 0x04004C28 RID: 19496
		public FpBtn CancelBtn;

		// Token: 0x04004C29 RID: 19497
		private UIFightCenterButtonType buttonType;
	}
}
