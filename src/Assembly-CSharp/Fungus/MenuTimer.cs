using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000E05 RID: 3589
	[CommandInfo("Narrative", "Menu Timer", "Displays a timer bar and executes a target block if the player fails to select a menu option in time.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class MenuTimer : Command
	{
		// Token: 0x06006562 RID: 25954 RVA: 0x00282E9C File Offset: 0x0028109C
		public override void OnEnter()
		{
			MenuDialog menuDialog = MenuDialog.GetMenuDialog();
			if (menuDialog != null && this.targetBlock != null)
			{
				menuDialog.ShowTimer(this._duration.Value, this.targetBlock);
			}
			this.Continue();
		}

		// Token: 0x06006563 RID: 25955 RVA: 0x00282EE3 File Offset: 0x002810E3
		public override void GetConnectedBlocks(ref List<Block> connectedBlocks)
		{
			if (this.targetBlock != null)
			{
				connectedBlocks.Add(this.targetBlock);
			}
		}

		// Token: 0x06006564 RID: 25956 RVA: 0x00282F00 File Offset: 0x00281100
		public override string GetSummary()
		{
			if (this.targetBlock == null)
			{
				return "Error: No target block selected";
			}
			return this.targetBlock.BlockName;
		}

		// Token: 0x06006565 RID: 25957 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006566 RID: 25958 RVA: 0x00282F21 File Offset: 0x00281121
		public override bool HasReference(Variable variable)
		{
			return this._duration.floatRef == variable || base.HasReference(variable);
		}

		// Token: 0x06006567 RID: 25959 RVA: 0x00282F3F File Offset: 0x0028113F
		protected virtual void OnEnable()
		{
			if (this.durationOLD != 0f)
			{
				this._duration.Value = this.durationOLD;
				this.durationOLD = 0f;
			}
		}

		// Token: 0x0400571C RID: 22300
		[Tooltip("Length of time to display the timer for")]
		[SerializeField]
		protected FloatData _duration = new FloatData(1f);

		// Token: 0x0400571D RID: 22301
		[FormerlySerializedAs("targetSequence")]
		[Tooltip("Block to execute when the timer expires")]
		[SerializeField]
		protected Block targetBlock;

		// Token: 0x0400571E RID: 22302
		[HideInInspector]
		[FormerlySerializedAs("duration")]
		public float durationOLD;
	}
}
