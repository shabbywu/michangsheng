using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02001299 RID: 4761
	[CommandInfo("iTween", "Shake Rotation", "Randomly shakes a GameObject's rotation by a diminishing amount over time.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class ShakeRotation : iTweenCommand
	{
		// Token: 0x06007368 RID: 29544 RVA: 0x002AAE28 File Offset: 0x002A9028
		public override void DoTween()
		{
			Hashtable hashtable = new Hashtable();
			hashtable.Add("name", this._tweenName.Value);
			hashtable.Add("amount", this._amount.Value);
			hashtable.Add("space", this.space);
			hashtable.Add("time", this._duration.Value);
			hashtable.Add("easetype", this.easeType);
			hashtable.Add("looptype", this.loopType);
			hashtable.Add("oncomplete", "OniTweenComplete");
			hashtable.Add("oncompletetarget", base.gameObject);
			hashtable.Add("oncompleteparams", this);
			iTween.ShakeRotation(this._targetObject.Value, hashtable);
		}

		// Token: 0x06007369 RID: 29545 RVA: 0x0004EBD0 File Offset: 0x0004CDD0
		public override bool HasReference(Variable variable)
		{
			return this._amount.vector3Ref == variable || base.HasReference(variable);
		}

		// Token: 0x0600736A RID: 29546 RVA: 0x002AAF08 File Offset: 0x002A9108
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.amountOLD != default(Vector3))
			{
				this._amount.Value = this.amountOLD;
				this.amountOLD = default(Vector3);
			}
		}

		// Token: 0x0400654F RID: 25935
		[Tooltip("A rotation offset in space the GameObject will animate to")]
		[SerializeField]
		protected Vector3Data _amount;

		// Token: 0x04006550 RID: 25936
		[Tooltip("Apply the transformation in either the world coordinate or local cordinate system")]
		[SerializeField]
		protected Space space = 1;

		// Token: 0x04006551 RID: 25937
		[HideInInspector]
		[FormerlySerializedAs("amount")]
		public Vector3 amountOLD;
	}
}
