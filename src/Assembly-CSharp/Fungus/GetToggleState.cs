using System;
using UnityEngine;
using UnityEngine.UI;

namespace Fungus
{
	// Token: 0x02001211 RID: 4625
	[CommandInfo("UI", "Get Toggle State", "Gets the state of a toggle UI object and stores it in a boolean variable.", 0)]
	public class GetToggleState : Command
	{
		// Token: 0x0600711D RID: 28957 RVA: 0x0004CD65 File Offset: 0x0004AF65
		public override void OnEnter()
		{
			if (this.toggle != null && this.toggleState != null)
			{
				this.toggleState.Value = this.toggle.isOn;
			}
			this.Continue();
		}

		// Token: 0x0600711E RID: 28958 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x0600711F RID: 28959 RVA: 0x0004CD9F File Offset: 0x0004AF9F
		public override string GetSummary()
		{
			if (this.toggle == null)
			{
				return "Error: Toggle object not selected";
			}
			if (this.toggleState == null)
			{
				return "Error: Toggle state variable not selected";
			}
			return this.toggle.name;
		}

		// Token: 0x06007120 RID: 28960 RVA: 0x0004CDD4 File Offset: 0x0004AFD4
		public override bool HasReference(Variable variable)
		{
			return this.toggleState == variable || base.HasReference(variable);
		}

		// Token: 0x04006370 RID: 25456
		[Tooltip("Target toggle object to get the value from")]
		[SerializeField]
		protected Toggle toggle;

		// Token: 0x04006371 RID: 25457
		[Tooltip("Boolean variable to store the state of the toggle value in.")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable toggleState;
	}
}
