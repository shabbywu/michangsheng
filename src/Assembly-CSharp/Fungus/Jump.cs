using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000DE2 RID: 3554
	[CommandInfo("Flow", "Jump", "Move execution to a specific Label command in the same block", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class Jump : Command
	{
		// Token: 0x060064CC RID: 25804 RVA: 0x00280BDC File Offset: 0x0027EDDC
		public override void OnEnter()
		{
			if (this._targetLabel.Value == "")
			{
				this.Continue();
				return;
			}
			List<Command> commandList = this.ParentBlock.CommandList;
			for (int i = 0; i < commandList.Count; i++)
			{
				Label label = commandList[i] as Label;
				if (label != null && label.Key == this._targetLabel.Value)
				{
					this.Continue(label.CommandIndex + 1);
					return;
				}
			}
			Debug.LogWarning("Label not found: " + this._targetLabel.Value);
			this.Continue();
		}

		// Token: 0x060064CD RID: 25805 RVA: 0x00280C81 File Offset: 0x0027EE81
		public override string GetSummary()
		{
			if (this._targetLabel.Value == "")
			{
				return "Error: No label selected";
			}
			return this._targetLabel.Value;
		}

		// Token: 0x060064CE RID: 25806 RVA: 0x0027D1B6 File Offset: 0x0027B3B6
		public override Color GetButtonColor()
		{
			return new Color32(253, 253, 150, byte.MaxValue);
		}

		// Token: 0x060064CF RID: 25807 RVA: 0x00280CAB File Offset: 0x0027EEAB
		public override bool HasReference(Variable variable)
		{
			return this._targetLabel.stringRef == variable || base.HasReference(variable);
		}

		// Token: 0x060064D0 RID: 25808 RVA: 0x00280CC9 File Offset: 0x0027EEC9
		protected virtual void OnEnable()
		{
			if (this.targetLabelOLD != null)
			{
				this._targetLabel.Value = this.targetLabelOLD.Key;
				this.targetLabelOLD = null;
			}
		}

		// Token: 0x040056BE RID: 22206
		[Tooltip("Name of a label in this block to jump to")]
		[SerializeField]
		protected StringData _targetLabel = new StringData("");

		// Token: 0x040056BF RID: 22207
		[HideInInspector]
		[FormerlySerializedAs("targetLabel")]
		public Label targetLabelOLD;
	}
}
