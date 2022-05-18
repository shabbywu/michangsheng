using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001288 RID: 4744
	[CommandInfo("Sprite", "Set Draggable 2D", "Sets a Draggable2D component to be draggable / non-draggable.", 0)]
	[AddComponentMenu("")]
	public class SetDraggable2D : Command
	{
		// Token: 0x06007305 RID: 29445 RVA: 0x0004E5F8 File Offset: 0x0004C7F8
		public override void OnEnter()
		{
			if (this.targetDraggable2D != null)
			{
				this.targetDraggable2D.DragEnabled = this.activeState.Value;
			}
			this.Continue();
		}

		// Token: 0x06007306 RID: 29446 RVA: 0x0004E624 File Offset: 0x0004C824
		public override string GetSummary()
		{
			if (this.targetDraggable2D == null)
			{
				return "Error: No Draggable2D component selected";
			}
			return this.targetDraggable2D.gameObject.name;
		}

		// Token: 0x06007307 RID: 29447 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x06007308 RID: 29448 RVA: 0x0004E64A File Offset: 0x0004C84A
		public override bool HasReference(Variable variable)
		{
			return this.activeState.booleanRef == variable || base.HasReference(variable);
		}

		// Token: 0x04006516 RID: 25878
		[Tooltip("Reference to Draggable2D component on a gameobject")]
		[SerializeField]
		protected Draggable2D targetDraggable2D;

		// Token: 0x04006517 RID: 25879
		[Tooltip("Set to true to enable the component")]
		[SerializeField]
		protected BooleanData activeState;
	}
}
