using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02001253 RID: 4691
	[CommandInfo("iTween", "Move Add", "Moves a game object by a specified offset over time.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class MoveAdd : iTweenCommand
	{
		// Token: 0x060071F7 RID: 29175 RVA: 0x002A7258 File Offset: 0x002A5458
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

		// Token: 0x060071F8 RID: 29176 RVA: 0x0004D846 File Offset: 0x0004BA46
		public override bool HasReference(Variable variable)
		{
			return this._offset.vector3Ref == variable || base.HasReference(variable);
		}

		// Token: 0x060071F9 RID: 29177 RVA: 0x002A7338 File Offset: 0x002A5538
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.offsetOLD != default(Vector3))
			{
				this._offset.Value = this.offsetOLD;
				this.offsetOLD = default(Vector3);
			}
		}

		// Token: 0x04006454 RID: 25684
		[Tooltip("A translation offset in space the GameObject will animate to")]
		[SerializeField]
		protected Vector3Data _offset;

		// Token: 0x04006455 RID: 25685
		[Tooltip("Apply the transformation in either the world coordinate or local cordinate system")]
		[SerializeField]
		protected Space space = 1;

		// Token: 0x04006456 RID: 25686
		[HideInInspector]
		[FormerlySerializedAs("offset")]
		public Vector3 offsetOLD;
	}
}
