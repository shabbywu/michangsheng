using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02001223 RID: 4643
	[CommandInfo("Flow", "Jump", "Move execution to a specific Label command in the same block", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class Jump : Command
	{
		// Token: 0x06007158 RID: 29016 RVA: 0x002A56C0 File Offset: 0x002A38C0
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

		// Token: 0x06007159 RID: 29017 RVA: 0x0004CFFF File Offset: 0x0004B1FF
		public override string GetSummary()
		{
			if (this._targetLabel.Value == "")
			{
				return "Error: No label selected";
			}
			return this._targetLabel.Value;
		}

		// Token: 0x0600715A RID: 29018 RVA: 0x0004C5A3 File Offset: 0x0004A7A3
		public override Color GetButtonColor()
		{
			return new Color32(253, 253, 150, byte.MaxValue);
		}

		// Token: 0x0600715B RID: 29019 RVA: 0x0004D029 File Offset: 0x0004B229
		public override bool HasReference(Variable variable)
		{
			return this._targetLabel.stringRef == variable || base.HasReference(variable);
		}

		// Token: 0x0600715C RID: 29020 RVA: 0x0004D047 File Offset: 0x0004B247
		protected virtual void OnEnable()
		{
			if (this.targetLabelOLD != null)
			{
				this._targetLabel.Value = this.targetLabelOLD.Key;
				this.targetLabelOLD = null;
			}
		}

		// Token: 0x040063C5 RID: 25541
		[Tooltip("Name of a label in this block to jump to")]
		[SerializeField]
		protected StringData _targetLabel = new StringData("");

		// Token: 0x040063C6 RID: 25542
		[HideInInspector]
		[FormerlySerializedAs("targetLabel")]
		public Label targetLabelOLD;
	}
}
