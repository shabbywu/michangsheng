using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000E22 RID: 3618
	[CommandInfo("iTween", "Rotate Add", "Rotates a game object by the specified angles over time.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class RotateAdd : iTweenCommand
	{
		// Token: 0x060065FD RID: 26109 RVA: 0x002849E4 File Offset: 0x00282BE4
		public override void DoTween()
		{
			Hashtable hashtable = new Hashtable();
			hashtable.Add("name", this._tweenName.Value);
			hashtable.Add("amount", this._offset.Value);
			hashtable.Add("space", this.space);
			hashtable.Add("time", this._duration.Value);
			hashtable.Add("easetype", this.easeType);
			hashtable.Add("looptype", this.loopType);
			hashtable.Add("oncomplete", "OniTweenComplete");
			hashtable.Add("oncompletetarget", base.gameObject);
			hashtable.Add("oncompleteparams", this);
			iTween.RotateAdd(this._targetObject.Value, hashtable);
		}

		// Token: 0x060065FE RID: 26110 RVA: 0x00284AC3 File Offset: 0x00282CC3
		public override bool HasReference(Variable variable)
		{
			return this._offset.vector3Ref == variable || base.HasReference(variable);
		}

		// Token: 0x060065FF RID: 26111 RVA: 0x00284AE4 File Offset: 0x00282CE4
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.offsetOLD != default(Vector3))
			{
				this._offset.Value = this.offsetOLD;
				this.offsetOLD = default(Vector3);
			}
		}

		// Token: 0x04005778 RID: 22392
		[Tooltip("A rotation offset in space the GameObject will animate to")]
		[SerializeField]
		protected Vector3Data _offset;

		// Token: 0x04005779 RID: 22393
		[Tooltip("Apply the transformation in either the world coordinate or local cordinate system")]
		[SerializeField]
		protected Space space = 1;

		// Token: 0x0400577A RID: 22394
		[HideInInspector]
		[FormerlySerializedAs("offset")]
		public Vector3 offsetOLD;
	}
}
