using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Fungus
{
	// Token: 0x02000E95 RID: 3733
	[EventHandlerInfo("UI", "Button Clicked", "The block will execute when the user clicks on the target UI button object.")]
	[AddComponentMenu("")]
	public class ButtonClicked : EventHandler
	{
		// Token: 0x060069D7 RID: 27095 RVA: 0x0029219F File Offset: 0x0029039F
		public virtual void Start()
		{
			if (this.targetButton != null)
			{
				this.targetButton.onClick.AddListener(new UnityAction(this.OnButtonClick));
			}
		}

		// Token: 0x060069D8 RID: 27096 RVA: 0x002921CC File Offset: 0x002903CC
		protected virtual void OnButtonClick()
		{
			this.ExecuteBlock();
		}

		// Token: 0x060069D9 RID: 27097 RVA: 0x002921D5 File Offset: 0x002903D5
		public override string GetSummary()
		{
			if (this.targetButton != null)
			{
				return this.targetButton.name;
			}
			return "None";
		}

		// Token: 0x040059C1 RID: 22977
		[Tooltip("The UI Button that the user can click on")]
		[SerializeField]
		protected Button targetButton;
	}
}
