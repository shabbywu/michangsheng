using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Fungus
{
	// Token: 0x02000EB1 RID: 3761
	[EventHandlerInfo("UI", "Toggle Changed", "The block will execute when the state of the target UI toggle object changes. The state of the toggle is stored in the Toggle State boolean variable.")]
	[AddComponentMenu("")]
	public class ToggleChanged : EventHandler
	{
		// Token: 0x06006A53 RID: 27219 RVA: 0x00292E1D File Offset: 0x0029101D
		public virtual void Start()
		{
			if (this.targetToggle != null)
			{
				this.targetToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggleChanged));
			}
		}

		// Token: 0x06006A54 RID: 27220 RVA: 0x00292E4A File Offset: 0x0029104A
		protected virtual void OnToggleChanged(bool state)
		{
			if (this.toggleState != null)
			{
				this.toggleState.Value = state;
			}
			this.ExecuteBlock();
		}

		// Token: 0x06006A55 RID: 27221 RVA: 0x00292E6D File Offset: 0x0029106D
		public override string GetSummary()
		{
			if (this.targetToggle != null)
			{
				return this.targetToggle.name;
			}
			return "None";
		}

		// Token: 0x040059E7 RID: 23015
		[Tooltip("The block will execute when the state of the target UI toggle object changes.")]
		[SerializeField]
		protected Toggle targetToggle;

		// Token: 0x040059E8 RID: 23016
		[Tooltip("The new state of the UI toggle object is stored in this boolean variable.")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable toggleState;
	}
}
