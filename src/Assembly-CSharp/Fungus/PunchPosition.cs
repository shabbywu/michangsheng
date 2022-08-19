using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000E16 RID: 3606
	[CommandInfo("iTween", "Punch Position", "Applies a jolt of force to a GameObject's position and wobbles it back to its initial position.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class PunchPosition : iTweenCommand
	{
		// Token: 0x060065C7 RID: 26055 RVA: 0x00283FE4 File Offset: 0x002821E4
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

		// Token: 0x060065C8 RID: 26056 RVA: 0x002840C3 File Offset: 0x002822C3
		public override bool HasReference(Variable variable)
		{
			return variable == this._amount.vector3Ref;
		}

		// Token: 0x060065C9 RID: 26057 RVA: 0x002840D8 File Offset: 0x002822D8
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.amountOLD != default(Vector3))
			{
				this._amount.Value = this.amountOLD;
				this.amountOLD = default(Vector3);
			}
		}

		// Token: 0x04005757 RID: 22359
		[Tooltip("A translation offset in space the GameObject will animate to")]
		[SerializeField]
		protected Vector3Data _amount;

		// Token: 0x04005758 RID: 22360
		[Tooltip("Apply the transformation in either the world coordinate or local cordinate system")]
		[SerializeField]
		protected Space space = 1;

		// Token: 0x04005759 RID: 22361
		[HideInInspector]
		[FormerlySerializedAs("amount")]
		public Vector3 amountOLD;
	}
}
