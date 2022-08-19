using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000E28 RID: 3624
	[CommandInfo("iTween", "Scale Add", "Changes a game object's scale by a specified offset over time.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class ScaleAdd : iTweenCommand
	{
		// Token: 0x0600662D RID: 26157 RVA: 0x002857C8 File Offset: 0x002839C8
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

		// Token: 0x0600662E RID: 26158 RVA: 0x00285891 File Offset: 0x00283A91
		public override bool HasReference(Variable variable)
		{
			return this._offset.vector3Ref == variable || base.HasReference(variable);
		}

		// Token: 0x0600662F RID: 26159 RVA: 0x002858B0 File Offset: 0x00283AB0
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.offsetOLD != default(Vector3))
			{
				this._offset.Value = this.offsetOLD;
				this.offsetOLD = default(Vector3);
			}
		}

		// Token: 0x040057A0 RID: 22432
		[Tooltip("A scale offset in space the GameObject will animate to")]
		[SerializeField]
		protected Vector3Data _offset;

		// Token: 0x040057A1 RID: 22433
		[HideInInspector]
		[FormerlySerializedAs("offset")]
		public Vector3 offsetOLD;
	}
}
