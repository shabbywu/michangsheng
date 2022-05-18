using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x020012B2 RID: 4786
	[CommandInfo("Flow", "Wait", "Waits for period of time before executing the next command in the block.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class Wait : Command
	{
		// Token: 0x060073CF RID: 29647 RVA: 0x0004F081 File Offset: 0x0004D281
		protected virtual void OnWaitComplete()
		{
			Tools.canClickFlag = true;
			this.Continue();
		}

		// Token: 0x060073D0 RID: 29648 RVA: 0x0004F08F File Offset: 0x0004D28F
		public override void OnEnter()
		{
			Tools.canClickFlag = false;
			base.Invoke("OnWaitComplete", this._duration.Value);
		}

		// Token: 0x060073D1 RID: 29649 RVA: 0x002AC898 File Offset: 0x002AAA98
		public override string GetSummary()
		{
			return this._duration.Value.ToString() + " seconds";
		}

		// Token: 0x060073D2 RID: 29650 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x060073D3 RID: 29651 RVA: 0x0004F0AD File Offset: 0x0004D2AD
		public override bool HasReference(Variable variable)
		{
			return this._duration.floatRef == variable || base.HasReference(variable);
		}

		// Token: 0x060073D4 RID: 29652 RVA: 0x0004F0CB File Offset: 0x0004D2CB
		protected virtual void OnEnable()
		{
			if (this.durationOLD != 0f)
			{
				this._duration.Value = this.durationOLD;
				this.durationOLD = 0f;
			}
		}

		// Token: 0x040065B2 RID: 26034
		[Tooltip("Duration to wait for")]
		[SerializeField]
		protected FloatData _duration = new FloatData(1f);

		// Token: 0x040065B3 RID: 26035
		[HideInInspector]
		[FormerlySerializedAs("duration")]
		public float durationOLD;
	}
}
