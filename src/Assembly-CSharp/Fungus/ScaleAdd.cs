using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02001279 RID: 4729
	[CommandInfo("iTween", "Scale Add", "Changes a game object's scale by a specified offset over time.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class ScaleAdd : iTweenCommand
	{
		// Token: 0x060072BB RID: 29371 RVA: 0x002A90E0 File Offset: 0x002A72E0
		public override void DoTween()
		{
			Hashtable hashtable = new Hashtable();
			hashtable.Add("name", this._tweenName.Value);
			hashtable.Add("amount", this._offset.Value);
			hashtable.Add("time", this._duration.Value);
			hashtable.Add("easetype", this.easeType);
			hashtable.Add("looptype", this.loopType);
			hashtable.Add("oncomplete", "OniTweenComplete");
			hashtable.Add("oncompletetarget", base.gameObject);
			hashtable.Add("oncompleteparams", this);
			iTween.ScaleAdd(this._targetObject.Value, hashtable);
		}

		// Token: 0x060072BC RID: 29372 RVA: 0x0004E1F9 File Offset: 0x0004C3F9
		public override bool HasReference(Variable variable)
		{
			return this._offset.vector3Ref == variable || base.HasReference(variable);
		}

		// Token: 0x060072BD RID: 29373 RVA: 0x002A91AC File Offset: 0x002A73AC
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.offsetOLD != default(Vector3))
			{
				this._offset.Value = this.offsetOLD;
				this.offsetOLD = default(Vector3);
			}
		}

		// Token: 0x040064E4 RID: 25828
		[Tooltip("A scale offset in space the GameObject will animate to")]
		[SerializeField]
		protected Vector3Data _offset;

		// Token: 0x040064E5 RID: 25829
		[HideInInspector]
		[FormerlySerializedAs("offset")]
		public Vector3 offsetOLD;
	}
}
