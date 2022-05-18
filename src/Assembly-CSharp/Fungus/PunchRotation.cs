using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02001264 RID: 4708
	[CommandInfo("iTween", "Punch Rotation", "Applies a jolt of force to a GameObject's rotation and wobbles it back to its initial rotation.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class PunchRotation : iTweenCommand
	{
		// Token: 0x06007259 RID: 29273 RVA: 0x002A7F30 File Offset: 0x002A6130
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
			iTween.PunchRotation(this._targetObject.Value, hashtable);
		}

		// Token: 0x0600725A RID: 29274 RVA: 0x0004DD19 File Offset: 0x0004BF19
		public override bool HasReference(Variable variable)
		{
			return variable == this._amount.vector3Ref;
		}

		// Token: 0x0600725B RID: 29275 RVA: 0x002A8010 File Offset: 0x002A6210
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.amountOLD != default(Vector3))
			{
				this._amount.Value = this.amountOLD;
				this.amountOLD = default(Vector3);
			}
		}

		// Token: 0x0400648F RID: 25743
		[Tooltip("A rotation offset in space the GameObject will animate to")]
		[SerializeField]
		protected Vector3Data _amount;

		// Token: 0x04006490 RID: 25744
		[Tooltip("Apply the transformation in either the world coordinate or local cordinate system")]
		[SerializeField]
		protected Space space = 1;

		// Token: 0x04006491 RID: 25745
		[HideInInspector]
		[FormerlySerializedAs("amount")]
		public Vector3 amountOLD;
	}
}
