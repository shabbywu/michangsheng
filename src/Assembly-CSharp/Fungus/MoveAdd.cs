using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000E06 RID: 3590
	[CommandInfo("iTween", "Move Add", "Moves a game object by a specified offset over time.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class MoveAdd : iTweenCommand
	{
		// Token: 0x06006569 RID: 25961 RVA: 0x00282F84 File Offset: 0x00281184
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
			iTween.MoveAdd(this._targetObject.Value, hashtable);
		}

		// Token: 0x0600656A RID: 25962 RVA: 0x00283063 File Offset: 0x00281263
		public override bool HasReference(Variable variable)
		{
			return this._offset.vector3Ref == variable || base.HasReference(variable);
		}

		// Token: 0x0600656B RID: 25963 RVA: 0x00283084 File Offset: 0x00281284
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.offsetOLD != default(Vector3))
			{
				this._offset.Value = this.offsetOLD;
				this.offsetOLD = default(Vector3);
			}
		}

		// Token: 0x0400571F RID: 22303
		[Tooltip("A translation offset in space the GameObject will animate to")]
		[SerializeField]
		protected Vector3Data _offset;

		// Token: 0x04005720 RID: 22304
		[Tooltip("Apply the transformation in either the world coordinate or local cordinate system")]
		[SerializeField]
		protected Space space = 1;

		// Token: 0x04005721 RID: 22305
		[HideInInspector]
		[FormerlySerializedAs("offset")]
		public Vector3 offsetOLD;
	}
}
