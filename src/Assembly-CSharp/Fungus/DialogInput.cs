using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Fungus
{
	// Token: 0x02000E6C RID: 3692
	public class DialogInput : MonoBehaviour
	{
		// Token: 0x060067F0 RID: 26608 RVA: 0x0028B5FE File Offset: 0x002897FE
		protected virtual void Awake()
		{
			this.writer = base.GetComponent<Writer>();
			this.CheckEventSystem();
		}

		// Token: 0x060067F1 RID: 26609 RVA: 0x0028B614 File Offset: 0x00289814
		protected virtual void CheckEventSystem()
		{
			if (Object.FindObjectOfType<EventSystem>() == null)
			{
				GameObject gameObject = Resources.Load<GameObject>("Prefabs/EventSystem");
				if (gameObject != null)
				{
					Object.Instantiate<GameObject>(gameObject).name = "EventSystem";
				}
			}
		}

		// Token: 0x060067F2 RID: 26610 RVA: 0x0028B654 File Offset: 0x00289854
		protected virtual void Update()
		{
			if (EventSystem.current == null)
			{
				return;
			}
			if (this.currentStandaloneInputModule == null)
			{
				this.currentStandaloneInputModule = EventSystem.current.GetComponent<StandaloneInputModule>();
			}
			if (this.writer != null && this.writer.IsWriting && (Input.GetButtonDown(this.currentStandaloneInputModule.submitButton) || (this.cancelEnabled && Input.GetButton(this.currentStandaloneInputModule.cancelButton))))
			{
				this.SetNextLineFlag();
			}
			switch (this.clickMode)
			{
			case ClickMode.ClickAnywhere:
				if (Input.GetMouseButtonDown(0))
				{
					this.SetNextLineFlag();
				}
				break;
			case ClickMode.ClickOnDialog:
				if (this.dialogClickedFlag)
				{
					this.SetNextLineFlag();
					this.dialogClickedFlag = false;
				}
				break;
			}
			if (this.ignoreClickTimer > 0f)
			{
				this.ignoreClickTimer = Mathf.Max(this.ignoreClickTimer - Time.deltaTime, 0f);
			}
			if (this.ignoreMenuClicks && MenuDialog.ActiveMenuDialog != null && MenuDialog.ActiveMenuDialog.IsActive() && MenuDialog.ActiveMenuDialog.DisplayedOptionsCount > 0)
			{
				this.dialogClickedFlag = false;
				this.nextLineInputFlag = false;
			}
			if (this.nextLineInputFlag)
			{
				IDialogInputListener[] componentsInChildren = base.gameObject.GetComponentsInChildren<IDialogInputListener>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].OnNextLineEvent();
				}
				this.nextLineInputFlag = false;
			}
		}

		// Token: 0x060067F3 RID: 26611 RVA: 0x0028B7B0 File Offset: 0x002899B0
		public virtual void SetNextLineFlag()
		{
			this.nextLineInputFlag = true;
		}

		// Token: 0x060067F4 RID: 26612 RVA: 0x0028B7B9 File Offset: 0x002899B9
		public virtual void SetDialogClickedFlag()
		{
			if (this.ignoreClickTimer > 0f)
			{
				return;
			}
			this.ignoreClickTimer = this.nextClickDelay;
			if (this.clickMode == ClickMode.ClickOnDialog)
			{
				this.dialogClickedFlag = true;
			}
		}

		// Token: 0x060067F5 RID: 26613 RVA: 0x0028B7E5 File Offset: 0x002899E5
		public virtual void SetButtonClickedFlag()
		{
			if (this.clickMode != ClickMode.Disabled)
			{
				this.SetNextLineFlag();
			}
		}

		// Token: 0x040058AD RID: 22701
		[Tooltip("Click to advance story")]
		[SerializeField]
		protected ClickMode clickMode;

		// Token: 0x040058AE RID: 22702
		[Tooltip("Delay between consecutive clicks. Useful to prevent accidentally clicking through story.")]
		[SerializeField]
		protected float nextClickDelay;

		// Token: 0x040058AF RID: 22703
		[Tooltip("Allow holding Cancel to fast forward text")]
		[SerializeField]
		protected bool cancelEnabled = true;

		// Token: 0x040058B0 RID: 22704
		[Tooltip("Ignore input if a Menu dialog is currently active")]
		[SerializeField]
		protected bool ignoreMenuClicks = true;

		// Token: 0x040058B1 RID: 22705
		protected bool dialogClickedFlag;

		// Token: 0x040058B2 RID: 22706
		protected bool nextLineInputFlag;

		// Token: 0x040058B3 RID: 22707
		protected float ignoreClickTimer;

		// Token: 0x040058B4 RID: 22708
		protected StandaloneInputModule currentStandaloneInputModule;

		// Token: 0x040058B5 RID: 22709
		protected Writer writer;
	}
}
