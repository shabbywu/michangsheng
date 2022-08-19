using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000E47 RID: 3655
	[CommandInfo("iTween", "Shake Position", "Randomly shakes a GameObject's position by a diminishing amount over time.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class ShakePosition : iTweenCommand
	{
		// Token: 0x060066D6 RID: 26326 RVA: 0x00287D58 File Offset: 0x00285F58
		public override void DoTween()
		{
			Hashtable hashtable = new Hashtable();
			hashtable.Add("name", this._tweenName.Value);
			hashtable.Add("amount", this._amount.Value);
			switch (this.axis)
			{
			case iTweenAxis.X:
				hashtable.Add("axis", "x");
				break;
			case iTweenAxis.Y:
				hashtable.Add("axis", "y");
				break;
			case iTweenAxis.Z:
				hashtable.Add("axis", "z");
				break;
			}
			hashtable.Add("time", this._duration.Value);
			hashtable.Add("easetype", this.easeType);
			hashtable.Add("looptype", this.loopType);
			hashtable.Add("isLocal", this.isLocal);
			hashtable.Add("oncomplete", "OniTweenComplete");
			hashtable.Add("oncompletetarget", base.gameObject);
			hashtable.Add("oncompleteparams", this);
			iTween.ShakePosition(this._targetObject.Value, hashtable);
		}

		// Token: 0x060066D7 RID: 26327 RVA: 0x00287E88 File Offset: 0x00286088
		public override bool HasReference(Variable variable)
		{
			return this._amount.vector3Ref == variable || base.HasReference(variable);
		}

		// Token: 0x060066D8 RID: 26328 RVA: 0x00287EA8 File Offset: 0x002860A8
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.amountOLD != default(Vector3))
			{
				this._amount.Value = this.amountOLD;
				this.amountOLD = default(Vector3);
			}
		}

		// Token: 0x04005807 RID: 22535
		[Tooltip("A translation offset in space the GameObject will animate to")]
		[SerializeField]
		protected Vector3Data _amount;

		// Token: 0x04005808 RID: 22536
		[Tooltip("Whether to animate in world space or relative to the parent. False by default.")]
		[SerializeField]
		protected bool isLocal;

		// Token: 0x04005809 RID: 22537
		[Tooltip("Restricts rotation to the supplied axis only")]
		[SerializeField]
		protected iTweenAxis axis;

		// Token: 0x0400580A RID: 22538
		[HideInInspector]
		[FormerlySerializedAs("amount")]
		public Vector3 amountOLD;
	}
}
