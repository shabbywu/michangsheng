using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02001298 RID: 4760
	[CommandInfo("iTween", "Shake Position", "Randomly shakes a GameObject's position by a diminishing amount over time.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class ShakePosition : iTweenCommand
	{
		// Token: 0x06007364 RID: 29540 RVA: 0x002AACB0 File Offset: 0x002A8EB0
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

		// Token: 0x06007365 RID: 29541 RVA: 0x0004EBB2 File Offset: 0x0004CDB2
		public override bool HasReference(Variable variable)
		{
			return this._amount.vector3Ref == variable || base.HasReference(variable);
		}

		// Token: 0x06007366 RID: 29542 RVA: 0x002AADE0 File Offset: 0x002A8FE0
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.amountOLD != default(Vector3))
			{
				this._amount.Value = this.amountOLD;
				this.amountOLD = default(Vector3);
			}
		}

		// Token: 0x0400654B RID: 25931
		[Tooltip("A translation offset in space the GameObject will animate to")]
		[SerializeField]
		protected Vector3Data _amount;

		// Token: 0x0400654C RID: 25932
		[Tooltip("Whether to animate in world space or relative to the parent. False by default.")]
		[SerializeField]
		protected bool isLocal;

		// Token: 0x0400654D RID: 25933
		[Tooltip("Restricts rotation to the supplied axis only")]
		[SerializeField]
		protected iTweenAxis axis;

		// Token: 0x0400654E RID: 25934
		[HideInInspector]
		[FormerlySerializedAs("amount")]
		public Vector3 amountOLD;
	}
}
