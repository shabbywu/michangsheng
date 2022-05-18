using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Fungus
{
	// Token: 0x020012C9 RID: 4809
	public class DialogInput : MonoBehaviour
	{
		// Token: 0x060074A2 RID: 29858 RVA: 0x0004F977 File Offset: 0x0004DB77
		protected virtual void Awake()
		{
			this.writer = base.GetComponent<Writer>();
			this.CheckEventSystem();
		}

		// Token: 0x060074A3 RID: 29859 RVA: 0x002AE198 File Offset: 0x002AC398
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

		// Token: 0x060074A4 RID: 29860 RVA: 0x002AE1D8 File Offset: 0x002AC3D8
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

		// Token: 0x060074A5 RID: 29861 RVA: 0x0004F98B File Offset: 0x0004DB8B
		public virtual void SetNextLineFlag()
		{
			this.nextLineInputFlag = true;
		}

		// Token: 0x060074A6 RID: 29862 RVA: 0x0004F994 File Offset: 0x0004DB94
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

		// Token: 0x060074A7 RID: 29863 RVA: 0x0004F9C0 File Offset: 0x0004DBC0
		public virtual void SetButtonClickedFlag()
		{
			if (this.clickMode != ClickMode.Disabled)
			{
				this.SetNextLineFlag();
			}
		}

		// Token: 0x04006645 RID: 26181
		[Tooltip("Click to advance story")]
		[SerializeField]
		protected ClickMode clickMode;

		// Token: 0x04006646 RID: 26182
		[Tooltip("Delay between consecutive clicks. Useful to prevent accidentally clicking through story.")]
		[SerializeField]
		protected float nextClickDelay;

		// Token: 0x04006647 RID: 26183
		[Tooltip("Allow holding Cancel to fast forward text")]
		[SerializeField]
		protected bool cancelEnabled = true;

		// Token: 0x04006648 RID: 26184
		[Tooltip("Ignore input if a Menu dialog is currently active")]
		[SerializeField]
		protected bool ignoreMenuClicks = true;

		// Token: 0x04006649 RID: 26185
		protected bool dialogClickedFlag;

		// Token: 0x0400664A RID: 26186
		protected bool nextLineInputFlag;

		// Token: 0x0400664B RID: 26187
		protected float ignoreClickTimer;

		// Token: 0x0400664C RID: 26188
		protected StandaloneInputModule currentStandaloneInputModule;

		// Token: 0x0400664D RID: 26189
		protected Writer writer;
	}
}
