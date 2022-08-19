using System;
using UnityEngine;
using UnityEngine.UI;

namespace Fungus
{
	// Token: 0x02000DD6 RID: 3542
	[CommandInfo("UI", "Get Toggle State", "Gets the state of a toggle UI object and stores it in a boolean variable.", 0)]
	public class GetToggleState : Command
	{
		// Token: 0x0600649B RID: 25755 RVA: 0x0027F853 File Offset: 0x0027DA53
		public override void OnEnter()
		{
			if (this.toggle != null && this.toggleState != null)
			{
				this.toggleState.Value = this.toggle.isOn;
			}
			this.Continue();
		}

		// Token: 0x0600649C RID: 25756 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x0600649D RID: 25757 RVA: 0x0027F88D File Offset: 0x0027DA8D
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

		// Token: 0x0600649E RID: 25758 RVA: 0x0027F8C2 File Offset: 0x0027DAC2
		public override bool HasReference(Variable variable)
		{
			return this.toggleState == variable || base.HasReference(variable);
		}

		// Token: 0x04005670 RID: 22128
		[Tooltip("Target toggle object to get the value from")]
		[SerializeField]
		protected Toggle toggle;

		// Token: 0x04005671 RID: 22129
		[Tooltip("Boolean variable to store the state of the toggle value in.")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable toggleState;
	}
}
