using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000E18 RID: 3608
	[CommandInfo("iTween", "Punch Scale", "Applies a jolt of force to a GameObject's scale and wobbles it back to its initial scale.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class PunchScale : iTweenCommand
	{
		// Token: 0x060065CF RID: 26063 RVA: 0x0028427C File Offset: 0x0028247C
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

		// Token: 0x060065D0 RID: 26064 RVA: 0x00284345 File Offset: 0x00282545
		public override bool HasReference(Variable variable)
		{
			return variable == this._amount.vector3Ref;
		}

		// Token: 0x060065D1 RID: 26065 RVA: 0x00284358 File Offset: 0x00282558
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.amountOLD != default(Vector3))
			{
				this._amount.Value = this.amountOLD;
				this.amountOLD = default(Vector3);
			}
		}

		// Token: 0x0400575D RID: 22365
		[Tooltip("A scale offset in space the GameObject will animate to")]
		[SerializeField]
		protected Vector3Data _amount;

		// Token: 0x0400575E RID: 22366
		[HideInInspector]
		[FormerlySerializedAs("amount")]
		public Vector3 amountOLD;
	}
}
