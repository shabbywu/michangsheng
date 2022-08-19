using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000E5D RID: 3677
	[CommandInfo("Flow", "Wait", "Waits for period of time before executing the next command in the block.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class Wait : Command
	{
		// Token: 0x06006741 RID: 26433 RVA: 0x00289E0F File Offset: 0x0028800F
		protected virtual void OnWaitComplete()
		{
			Tools.canClickFlag = true;
			this.Continue();
		}

		// Token: 0x06006742 RID: 26434 RVA: 0x00289E1D File Offset: 0x0028801D
		public override void OnEnter()
		{
			Tools.canClickFlag = false;
			base.Invoke("OnWaitComplete", this._duration.Value);
		}

		// Token: 0x06006743 RID: 26435 RVA: 0x00289E3C File Offset: 0x0028803C
		public override string GetSummary()
		{
			return this._duration.Value.ToString() + " seconds";
		}

		// Token: 0x06006744 RID: 26436 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x06006745 RID: 26437 RVA: 0x00289E66 File Offset: 0x00288066
		public override bool HasReference(Variable variable)
		{
			return this._duration.floatRef == variable || base.HasReference(variable);
		}

		// Token: 0x06006746 RID: 26438 RVA: 0x00289E84 File Offset: 0x00288084
		protected virtual void OnEnable()
		{
			if (this.durationOLD != 0f)
			{
				this._duration.Value = this.durationOLD;
				this.durationOLD = 0f;
			}
		}

		// Token: 0x04005853 RID: 22611
		[Tooltip("Duration to wait for")]
		[SerializeField]
		protected FloatData _duration = new FloatData(1f);

		// Token: 0x04005854 RID: 22612
		[HideInInspector]
		[FormerlySerializedAs("duration")]
		public float durationOLD;
	}
}
