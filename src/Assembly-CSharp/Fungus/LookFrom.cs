using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x0200122F RID: 4655
	[CommandInfo("iTween", "Look From", "Instantly rotates a GameObject to look at the supplied Vector3 then returns it to it's starting rotation over time.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class LookFrom : iTweenCommand
	{
		// Token: 0x06007185 RID: 29061 RVA: 0x002A5E00 File Offset: 0x002A4000
		public override void DoTween()
		{
			Hashtable hashtable = new Hashtable();
			hashtable.Add("name", this._tweenName.Value);
			if (this._fromTransform.Value == null)
			{
				hashtable.Add("looktarget", this._fromPosition.Value);
			}
			else
			{
				hashtable.Add("looktarget", this._fromTransform.Value);
			}
			switch (this.axis)
			{
			case iTweenAxis.X:
				hashtable.Add("axis", "x");
				break;
			case iTweenAxis.Y:
				hashtable.Add("axis", "y");
				break;
			case iTweenAxis.Z:
				hashtable.Add("axis", "z");
				break;
			}
			hashtable.Add("time", this._duration.Value);
			hashtable.Add("easetype", this.easeType);
			hashtable.Add("looptype", this.loopType);
			hashtable.Add("oncomplete", "OniTweenComplete");
			hashtable.Add("oncompletetarget", base.gameObject);
			hashtable.Add("oncompleteparams", this);
			iTween.LookFrom(this._targetObject.Value, hashtable);
		}

		// Token: 0x06007186 RID: 29062 RVA: 0x0004D322 File Offset: 0x0004B522
		public override bool HasReference(Variable variable)
		{
			return this._fromTransform.transformRef == variable || this._fromPosition.vector3Ref == variable || base.HasReference(variable);
		}

		// Token: 0x06007187 RID: 29063 RVA: 0x002A5F48 File Offset: 0x002A4148
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.fromTransformOLD != null)
			{
				this._fromTransform.Value = this.fromTransformOLD;
				this.fromTransformOLD = null;
			}
			if (this.fromPositionOLD != default(Vector3))
			{
				this._fromPosition.Value = this.fromPositionOLD;
				this.fromPositionOLD = default(Vector3);
			}
		}

		// Token: 0x040063EC RID: 25580
		[Tooltip("Target transform that the GameObject will look at")]
		[SerializeField]
		protected TransformData _fromTransform;

		// Token: 0x040063ED RID: 25581
		[Tooltip("Target world position that the GameObject will look at, if no From Transform is set")]
		[SerializeField]
		protected Vector3Data _fromPosition;

		// Token: 0x040063EE RID: 25582
		[Tooltip("Restricts rotation to the supplied axis only")]
		[SerializeField]
		protected iTweenAxis axis;

		// Token: 0x040063EF RID: 25583
		[HideInInspector]
		[FormerlySerializedAs("fromTransform")]
		public Transform fromTransformOLD;

		// Token: 0x040063F0 RID: 25584
		[HideInInspector]
		[FormerlySerializedAs("fromPosition")]
		public Vector3 fromPositionOLD;
	}
}
