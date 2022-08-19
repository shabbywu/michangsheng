using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000E48 RID: 3656
	[CommandInfo("iTween", "Shake Rotation", "Randomly shakes a GameObject's rotation by a diminishing amount over time.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class ShakeRotation : iTweenCommand
	{
		// Token: 0x060066DA RID: 26330 RVA: 0x00287EF0 File Offset: 0x002860F0
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

		// Token: 0x060066DB RID: 26331 RVA: 0x00287FCF File Offset: 0x002861CF
		public override bool HasReference(Variable variable)
		{
			return this._amount.vector3Ref == variable || base.HasReference(variable);
		}

		// Token: 0x060066DC RID: 26332 RVA: 0x00287FF0 File Offset: 0x002861F0
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.amountOLD != default(Vector3))
			{
				this._amount.Value = this.amountOLD;
				this.amountOLD = default(Vector3);
			}
		}

		// Token: 0x0400580B RID: 22539
		[Tooltip("A rotation offset in space the GameObject will animate to")]
		[SerializeField]
		protected Vector3Data _amount;

		// Token: 0x0400580C RID: 22540
		[Tooltip("Apply the transformation in either the world coordinate or local cordinate system")]
		[SerializeField]
		protected Space space = 1;

		// Token: 0x0400580D RID: 22541
		[HideInInspector]
		[FormerlySerializedAs("amount")]
		public Vector3 amountOLD;
	}
}
