using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02001263 RID: 4707
	[CommandInfo("iTween", "Punch Position", "Applies a jolt of force to a GameObject's position and wobbles it back to its initial position.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class PunchPosition : iTweenCommand
	{
		// Token: 0x06007255 RID: 29269 RVA: 0x002A7E08 File Offset: 0x002A6008
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
			iTween.PunchPosition(this._targetObject.Value, hashtable);
		}

		// Token: 0x06007256 RID: 29270 RVA: 0x0004DCF7 File Offset: 0x0004BEF7
		public override bool HasReference(Variable variable)
		{
			return variable == this._amount.vector3Ref;
		}

		// Token: 0x06007257 RID: 29271 RVA: 0x002A7EE8 File Offset: 0x002A60E8
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.amountOLD != default(Vector3))
			{
				this._amount.Value = this.amountOLD;
				this.amountOLD = default(Vector3);
			}
		}

		// Token: 0x0400648C RID: 25740
		[Tooltip("A translation offset in space the GameObject will animate to")]
		[SerializeField]
		protected Vector3Data _amount;

		// Token: 0x0400648D RID: 25741
		[Tooltip("Apply the transformation in either the world coordinate or local cordinate system")]
		[SerializeField]
		protected Space space = 1;

		// Token: 0x0400648E RID: 25742
		[HideInInspector]
		[FormerlySerializedAs("amount")]
		public Vector3 amountOLD;
	}
}
