using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02001265 RID: 4709
	[CommandInfo("iTween", "Punch Scale", "Applies a jolt of force to a GameObject's scale and wobbles it back to its initial scale.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class PunchScale : iTweenCommand
	{
		// Token: 0x0600725D RID: 29277 RVA: 0x002A8058 File Offset: 0x002A6258
		public override void DoTween()
		{
			Hashtable hashtable = new Hashtable();
			hashtable.Add("name", this._tweenName.Value);
			hashtable.Add("amount", this._amount.Value);
			hashtable.Add("time", this._duration.Value);
			hashtable.Add("easetype", this.easeType);
			hashtable.Add("looptype", this.loopType);
			hashtable.Add("oncomplete", "OniTweenComplete");
			hashtable.Add("oncompletetarget", base.gameObject);
			hashtable.Add("oncompleteparams", this);
			iTween.PunchScale(this._targetObject.Value, hashtable);
		}

		// Token: 0x0600725E RID: 29278 RVA: 0x0004DD3B File Offset: 0x0004BF3B
		public override bool HasReference(Variable variable)
		{
			return variable == this._amount.vector3Ref;
		}

		// Token: 0x0600725F RID: 29279 RVA: 0x002A8124 File Offset: 0x002A6324
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.amountOLD != default(Vector3))
			{
				this._amount.Value = this.amountOLD;
				this.amountOLD = default(Vector3);
			}
		}

		// Token: 0x04006492 RID: 25746
		[Tooltip("A scale offset in space the GameObject will animate to")]
		[SerializeField]
		protected Vector3Data _amount;

		// Token: 0x04006493 RID: 25747
		[HideInInspector]
		[FormerlySerializedAs("amount")]
		public Vector3 amountOLD;
	}
}
