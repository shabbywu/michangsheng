using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x0200129A RID: 4762
	[CommandInfo("iTween", "Shake Scale", "Randomly shakes a GameObject's rotation by a diminishing amount over time.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class ShakeScale : iTweenCommand
	{
		// Token: 0x0600736C RID: 29548 RVA: 0x002AAF50 File Offset: 0x002A9150
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

		// Token: 0x0600736D RID: 29549 RVA: 0x0004EBFD File Offset: 0x0004CDFD
		public override bool HasReference(Variable variable)
		{
			return this._amount.vector3Ref == variable || base.HasReference(variable);
		}

		// Token: 0x0600736E RID: 29550 RVA: 0x002AB01C File Offset: 0x002A921C
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.amountOLD != default(Vector3))
			{
				this._amount.Value = this.amountOLD;
				this.amountOLD = default(Vector3);
			}
		}

		// Token: 0x04006552 RID: 25938
		[Tooltip("A scale offset in space the GameObject will animate to")]
		[SerializeField]
		protected Vector3Data _amount;

		// Token: 0x04006553 RID: 25939
		[HideInInspector]
		[FormerlySerializedAs("amount")]
		public Vector3 amountOLD;
	}
}
