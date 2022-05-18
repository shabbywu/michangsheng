using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x020011EF RID: 4591
	[CommandInfo("Flow", "Call", "Execute another block in the same Flowchart as the command, or in a different Flowchart.", 0)]
	[AddComponentMenu("")]
	public class Call : Command, INoCommand
	{
		// Token: 0x0600706B RID: 28779 RVA: 0x002A2440 File Offset: 0x002A0640
		public override void OnEnter()
		{
			Flowchart flowchart = this.GetFlowchart();
			if (this.targetBlock != null)
			{
				if (this.ParentBlock != null && this.ParentBlock.Equals(this.targetBlock))
				{
					this.Continue(0);
					return;
				}
				if (this.targetBlock.IsExecuting())
				{
					Debug.LogWarning(this.targetBlock.BlockName + " cannot be called/executed, it is already running.");
					this.Continue();
					return;
				}
				Action onComplete = null;
				if (this.callMode == CallMode.WaitUntilFinished)
				{
					onComplete = delegate()
					{
						flowchart.SelectedBlock = this.ParentBlock;
						this.Continue();
					};
				}
				int commandIndex = this.startIndex;
				if (this.startLabel.Value != "")
				{
					int labelIndex = this.targetBlock.GetLabelIndex(this.startLabel.Value);
					if (labelIndex != -1)
					{
						commandIndex = labelIndex;
					}
				}
				if (this.targetFlowchart == null || this.targetFlowchart.Equals(this.GetFlowchart()))
				{
					if (flowchart.SelectedBlock == this.ParentBlock)
					{
						flowchart.SelectedBlock = this.targetBlock;
					}
					if (this.callMode == CallMode.StopThenCall)
					{
						this.StopParentBlock();
					}
					base.StartCoroutine(this.targetBlock.Execute(commandIndex, onComplete));
				}
				else
				{
					if (this.callMode == CallMode.StopThenCall)
					{
						this.StopParentBlock();
					}
					this.targetFlowchart.ExecuteBlock(this.targetBlock, commandIndex, onComplete);
				}
			}
			if (this.callMode == CallMode.Stop)
			{
				this.StopParentBlock();
				return;
			}
			if (this.callMode == CallMode.Continue)
			{
				this.Continue();
			}
		}

		// Token: 0x0600706C RID: 28780 RVA: 0x0004C5C3 File Offset: 0x0004A7C3
		public override void GetConnectedBlocks(ref List<Block> connectedBlocks)
		{
			if (this.targetBlock != null)
			{
				connectedBlocks.Add(this.targetBlock);
			}
		}

		// Token: 0x0600706D RID: 28781 RVA: 0x002A25D4 File Offset: 0x002A07D4
		public override string GetSummary()
		{
			string str;
			if (this.targetBlock == null)
			{
				str = "<None>";
			}
			else
			{
				str = this.targetBlock.BlockName;
			}
			return str + " : " + this.callMode.ToString();
		}

		// Token: 0x0600706E RID: 28782 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x0600706F RID: 28783 RVA: 0x0004C600 File Offset: 0x0004A800
		public override bool HasReference(Variable variable)
		{
			return this.startLabel.stringRef == variable || base.HasReference(variable);
		}

		// Token: 0x04006301 RID: 25345
		[Tooltip("Flowchart which contains the block to execute. If none is specified then the current Flowchart is used.")]
		[SerializeField]
		protected Flowchart targetFlowchart;

		// Token: 0x04006302 RID: 25346
		[FormerlySerializedAs("targetSequence")]
		[Tooltip("Block to start executing")]
		[SerializeField]
		protected Block targetBlock;

		// Token: 0x04006303 RID: 25347
		[Tooltip("Label to start execution at. Takes priority over startIndex.")]
		[SerializeField]
		protected StringData startLabel;

		// Token: 0x04006304 RID: 25348
		[Tooltip("Command index to start executing")]
		[FormerlySerializedAs("commandIndex")]
		[SerializeField]
		protected int startIndex;

		// Token: 0x04006305 RID: 25349
		[Tooltip("Select if the calling block should stop or continue executing commands, or wait until the called block finishes.")]
		[SerializeField]
		protected CallMode callMode;
	}
}
