using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000E53 RID: 3667
	[CommandInfo("iTween", "Stop Tween", "Stops an active iTween by name.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class StopTween : Command
	{
		// Token: 0x0600670A RID: 26378 RVA: 0x002887C6 File Offset: 0x002869C6
		public override void OnEnter()
		{
			iTween.StopByName(this._tweenName.Value);
			this.Continue();
		}

		// Token: 0x0600670B RID: 26379 RVA: 0x002887DE File Offset: 0x002869DE
		public override bool HasReference(Variable variable)
		{
			return this._tweenName.stringRef == variable || base.HasReference(variable);
		}

		// Token: 0x0600670C RID: 26380 RVA: 0x002887FC File Offset: 0x002869FC
		protected virtual void OnEnable()
		{
			if (this.tweenNameOLD != "")
			{
				this._tweenName.Value = this.tweenNameOLD;
				this.tweenNameOLD = "";
			}
		}

		// Token: 0x04005827 RID: 22567
		[Tooltip("Stop and destroy any Tweens in current scene with the supplied name")]
		[SerializeField]
		protected StringData _tweenName;

		// Token: 0x04005828 RID: 22568
		[HideInInspector]
		[FormerlySerializedAs("tweenName")]
		public string tweenNameOLD = "";
	}
}
