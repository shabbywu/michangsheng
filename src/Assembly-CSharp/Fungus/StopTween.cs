using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x020012A4 RID: 4772
	[CommandInfo("iTween", "Stop Tween", "Stops an active iTween by name.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class StopTween : Command
	{
		// Token: 0x06007398 RID: 29592 RVA: 0x0004EDBC File Offset: 0x0004CFBC
		public override void OnEnter()
		{
			iTween.StopByName(this._tweenName.Value);
			this.Continue();
		}

		// Token: 0x06007399 RID: 29593 RVA: 0x0004EDD4 File Offset: 0x0004CFD4
		public override bool HasReference(Variable variable)
		{
			return this._tweenName.stringRef == variable || base.HasReference(variable);
		}

		// Token: 0x0600739A RID: 29594 RVA: 0x0004EDF2 File Offset: 0x0004CFF2
		protected virtual void OnEnable()
		{
			if (this.tweenNameOLD != "")
			{
				this._tweenName.Value = this.tweenNameOLD;
				this.tweenNameOLD = "";
			}
		}

		// Token: 0x0400656B RID: 25963
		[Tooltip("Stop and destroy any Tweens in current scene with the supplied name")]
		[SerializeField]
		protected StringData _tweenName;

		// Token: 0x0400656C RID: 25964
		[HideInInspector]
		[FormerlySerializedAs("tweenName")]
		public string tweenNameOLD = "";
	}
}
