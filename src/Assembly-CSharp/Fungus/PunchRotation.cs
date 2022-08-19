using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000E17 RID: 3607
	[CommandInfo("iTween", "Punch Rotation", "Applies a jolt of force to a GameObject's rotation and wobbles it back to its initial rotation.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class PunchRotation : iTweenCommand
	{
		// Token: 0x060065CB RID: 26059 RVA: 0x00284130 File Offset: 0x00282330
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

		// Token: 0x060065CC RID: 26060 RVA: 0x0028420F File Offset: 0x0028240F
		public override bool HasReference(Variable variable)
		{
			return variable == this._amount.vector3Ref;
		}

		// Token: 0x060065CD RID: 26061 RVA: 0x00284224 File Offset: 0x00282424
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.amountOLD != default(Vector3))
			{
				this._amount.Value = this.amountOLD;
				this.amountOLD = default(Vector3);
			}
		}

		// Token: 0x0400575A RID: 22362
		[Tooltip("A rotation offset in space the GameObject will animate to")]
		[SerializeField]
		protected Vector3Data _amount;

		// Token: 0x0400575B RID: 22363
		[Tooltip("Apply the transformation in either the world coordinate or local cordinate system")]
		[SerializeField]
		protected Space space = 1;

		// Token: 0x0400575C RID: 22364
		[HideInInspector]
		[FormerlySerializedAs("amount")]
		public Vector3 amountOLD;
	}
}
