using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Fungus
{
	// Token: 0x0200133A RID: 4922
	[EventHandlerInfo("UI", "Toggle Changed", "The block will execute when the state of the target UI toggle object changes. The state of the toggle is stored in the Toggle State boolean variable.")]
	[AddComponentMenu("")]
	public class ToggleChanged : EventHandler
	{
		// Token: 0x06007790 RID: 30608 RVA: 0x000518AE File Offset: 0x0004FAAE
		public virtual void Start()
		{
			if (this.targetToggle != null)
			{
				this.targetToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggleChanged));
			}
		}

		// Token: 0x06007791 RID: 30609 RVA: 0x000518DB File Offset: 0x0004FADB
		protected virtual void OnToggleChanged(bool state)
		{
			if (this.toggleState != null)
			{
				this.toggleState.Value = state;
			}
			this.ExecuteBlock();
		}

		// Token: 0x06007792 RID: 30610 RVA: 0x000518FE File Offset: 0x0004FAFE
		public override string GetSummary()
		{
			if (this.targetToggle != null)
			{
				return this.targetToggle.name;
			}
			return "None";
		}

		// Token: 0x0400682E RID: 26670
		[Tooltip("The block will execute when the state of the target UI toggle object changes.")]
		[SerializeField]
		protected Toggle targetToggle;

		// Token: 0x0400682F RID: 26671
		[Tooltip("The new state of the UI toggle object is stored in this boolean variable.")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable toggleState;
	}
}
