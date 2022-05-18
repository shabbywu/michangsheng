using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02001271 RID: 4721
	[CommandInfo("iTween", "Rotate Add", "Rotates a game object by the specified angles over time.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class RotateAdd : iTweenCommand
	{
		// Token: 0x0600728B RID: 29323 RVA: 0x002A84B4 File Offset: 0x002A66B4
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

		// Token: 0x0600728C RID: 29324 RVA: 0x0004E047 File Offset: 0x0004C247
		public override bool HasReference(Variable variable)
		{
			return this._offset.vector3Ref == variable || base.HasReference(variable);
		}

		// Token: 0x0600728D RID: 29325 RVA: 0x002A8594 File Offset: 0x002A6794
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.offsetOLD != default(Vector3))
			{
				this._offset.Value = this.offsetOLD;
				this.offsetOLD = default(Vector3);
			}
		}

		// Token: 0x040064B5 RID: 25781
		[Tooltip("A rotation offset in space the GameObject will animate to")]
		[SerializeField]
		protected Vector3Data _offset;

		// Token: 0x040064B6 RID: 25782
		[Tooltip("Apply the transformation in either the world coordinate or local cordinate system")]
		[SerializeField]
		protected Space space = 1;

		// Token: 0x040064B7 RID: 25783
		[HideInInspector]
		[FormerlySerializedAs("offset")]
		public Vector3 offsetOLD;
	}
}
