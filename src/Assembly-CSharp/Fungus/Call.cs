using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000DBA RID: 3514
	[CommandInfo("Flow", "Call", "Execute another block in the same Flowchart as the command, or in a different Flowchart.", 0)]
	[AddComponentMenu("")]
	public class Call : Command, INoCommand
	{
		// Token: 0x06006400 RID: 25600 RVA: 0x0027D1D8 File Offset: 0x0027B3D8
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

		// Token: 0x06006401 RID: 25601 RVA: 0x0027D369 File Offset: 0x0027B569
		public override void GetConnectedBlocks(ref List<Block> connectedBlocks)
		{
			if (this.targetBlock != null)
			{
				connectedBlocks.Add(this.targetBlock);
			}
		}

		// Token: 0x06006402 RID: 25602 RVA: 0x0027D388 File Offset: 0x0027B588
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

		// Token: 0x06006403 RID: 25603 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x06006404 RID: 25604 RVA: 0x0027D3FB File Offset: 0x0027B5FB
		public override bool HasReference(Variable variable)
		{
			return this.startLabel.stringRef == variable || base.HasReference(variable);
		}

		// Token: 0x0400560E RID: 22030
		[Tooltip("Flowchart which contains the block to execute. If none is specified then the current Flowchart is used.")]
		[SerializeField]
		protected Flowchart targetFlowchart;

		// Token: 0x0400560F RID: 22031
		[FormerlySerializedAs("targetSequence")]
		[Tooltip("Block to start executing")]
		[SerializeField]
		protected Block targetBlock;

		// Token: 0x04005610 RID: 22032
		[Tooltip("Label to start execution at. Takes priority over startIndex.")]
		[SerializeField]
		protected StringData startLabel;

		// Token: 0x04005611 RID: 22033
		[Tooltip("Command index to start executing")]
		[FormerlySerializedAs("commandIndex")]
		[SerializeField]
		protected int startIndex;

		// Token: 0x04005612 RID: 22034
		[Tooltip("Select if the calling block should stop or continue executing commands, or wait until the called block finishes.")]
		[SerializeField]
		protected CallMode callMode;
	}
}
