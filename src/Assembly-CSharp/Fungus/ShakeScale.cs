using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000E49 RID: 3657
	[CommandInfo("iTween", "Shake Scale", "Randomly shakes a GameObject's rotation by a diminishing amount over time.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class ShakeScale : iTweenCommand
	{
		// Token: 0x060066DE RID: 26334 RVA: 0x00288048 File Offset: 0x00286248
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
			iTween.ShakeScale(this._targetObject.Value, hashtable);
		}

		// Token: 0x060066DF RID: 26335 RVA: 0x00288111 File Offset: 0x00286311
		public override bool HasReference(Variable variable)
		{
			return this._amount.vector3Ref == variable || base.HasReference(variable);
		}

		// Token: 0x060066E0 RID: 26336 RVA: 0x00288130 File Offset: 0x00286330
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.amountOLD != default(Vector3))
			{
				this._amount.Value = this.amountOLD;
				this.amountOLD = default(Vector3);
			}
		}

		// Token: 0x0400580E RID: 22542
		[Tooltip("A scale offset in space the GameObject will animate to")]
		[SerializeField]
		protected Vector3Data _amount;

		// Token: 0x0400580F RID: 22543
		[HideInInspector]
		[FormerlySerializedAs("amount")]
		public Vector3 amountOLD;
	}
}
