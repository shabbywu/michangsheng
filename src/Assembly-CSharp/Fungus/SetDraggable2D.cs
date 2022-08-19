using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E37 RID: 3639
	[CommandInfo("Sprite", "Set Draggable 2D", "Sets a Draggable2D component to be draggable / non-draggable.", 0)]
	[AddComponentMenu("")]
	public class SetDraggable2D : Command
	{
		// Token: 0x06006677 RID: 26231 RVA: 0x002866EE File Offset: 0x002848EE
		public override void OnEnter()
		{
			if (this.targetDraggable2D != null)
			{
				this.targetDraggable2D.DragEnabled = this.activeState.Value;
			}
			this.Continue();
		}

		// Token: 0x06006678 RID: 26232 RVA: 0x0028671A File Offset: 0x0028491A
		public override string GetSummary()
		{
			if (this.targetDraggable2D == null)
			{
				return "Error: No Draggable2D component selected";
			}
			return this.targetDraggable2D.gameObject.name;
		}

		// Token: 0x06006679 RID: 26233 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x0600667A RID: 26234 RVA: 0x00286740 File Offset: 0x00284940
		public override bool HasReference(Variable variable)
		{
			return this.activeState.booleanRef == variable || base.HasReference(variable);
		}

		// Token: 0x040057D2 RID: 22482
		[Tooltip("Reference to Draggable2D component on a gameobject")]
		[SerializeField]
		protected Draggable2D targetDraggable2D;

		// Token: 0x040057D3 RID: 22483
		[Tooltip("Set to true to enable the component")]
		[SerializeField]
		protected BooleanData activeState;
	}
}
