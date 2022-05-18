using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02001252 RID: 4690
	[CommandInfo("Narrative", "Menu Timer", "Displays a timer bar and executes a target block if the player fails to select a menu option in time.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class MenuTimer : Command
	{
		// Token: 0x060071F0 RID: 29168 RVA: 0x002A7210 File Offset: 0x002A5410
		public override void OnEnter()
		{
			MenuDialog menuDialog = MenuDialog.GetMenuDialog();
			if (menuDialog != null && this.targetBlock != null)
			{
				menuDialog.ShowTimer(this._duration.Value, this.targetBlock);
			}
			this.Continue();
		}

		// Token: 0x060071F1 RID: 29169 RVA: 0x0004D7A7 File Offset: 0x0004B9A7
		public override void GetConnectedBlocks(ref List<Block> connectedBlocks)
		{
			if (this.targetBlock != null)
			{
				connectedBlocks.Add(this.targetBlock);
			}
		}

		// Token: 0x060071F2 RID: 29170 RVA: 0x0004D7C4 File Offset: 0x0004B9C4
		public override string GetSummary()
		{
			if (this.targetBlock == null)
			{
				return "Error: No target block selected";
			}
			return this.targetBlock.BlockName;
		}

		// Token: 0x060071F3 RID: 29171 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x060071F4 RID: 29172 RVA: 0x0004D7E5 File Offset: 0x0004B9E5
		public override bool HasReference(Variable variable)
		{
			return this._duration.floatRef == variable || base.HasReference(variable);
		}

		// Token: 0x060071F5 RID: 29173 RVA: 0x0004D803 File Offset: 0x0004BA03
		protected virtual void OnEnable()
		{
			if (this.durationOLD != 0f)
			{
				this._duration.Value = this.durationOLD;
				this.durationOLD = 0f;
			}
		}

		// Token: 0x04006451 RID: 25681
		[Tooltip("Length of time to display the timer for")]
		[SerializeField]
		protected FloatData _duration = new FloatData(1f);

		// Token: 0x04006452 RID: 25682
		[FormerlySerializedAs("targetSequence")]
		[Tooltip("Block to execute when the timer expires")]
		[SerializeField]
		protected Block targetBlock;

		// Token: 0x04006453 RID: 25683
		[HideInInspector]
		[FormerlySerializedAs("duration")]
		public float durationOLD;
	}
}
