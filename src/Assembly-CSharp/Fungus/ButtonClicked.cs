using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Fungus
{
	// Token: 0x0200130E RID: 4878
	[EventHandlerInfo("UI", "Button Clicked", "The block will execute when the user clicks on the target UI button object.")]
	[AddComponentMenu("")]
	public class ButtonClicked : EventHandler
	{
		// Token: 0x06007702 RID: 30466 RVA: 0x00050FC0 File Offset: 0x0004F1C0
		public virtual void Start()
		{
			if (this.targetButton != null)
			{
				this.targetButton.onClick.AddListener(new UnityAction(this.OnButtonClick));
			}
		}

		// Token: 0x06007703 RID: 30467 RVA: 0x00050FED File Offset: 0x0004F1ED
		protected virtual void OnButtonClick()
		{
			this.ExecuteBlock();
		}

		// Token: 0x06007704 RID: 30468 RVA: 0x00050FF6 File Offset: 0x0004F1F6
		public override string GetSummary()
		{
			if (this.targetButton != null)
			{
				return this.targetButton.name;
			}
			return "None";
		}

		// Token: 0x040067D0 RID: 26576
		[Tooltip("The UI Button that the user can click on")]
		[SerializeField]
		protected Button targetButton;
	}
}
