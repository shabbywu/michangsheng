using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000DE1 RID: 3553
	[ExecuteInEditMode]
	public abstract class iTweenCommand : Command
	{
		// Token: 0x060064C4 RID: 25796 RVA: 0x002809B8 File Offset: 0x0027EBB8
		protected virtual void OniTweenComplete(object param)
		{
			Command command = param as Command;
			if (command != null && command.Equals(this) && this.waitUntilFinished)
			{
				this.Continue();
			}
		}

		// Token: 0x060064C5 RID: 25797 RVA: 0x002809EC File Offset: 0x0027EBEC
		public override void OnEnter()
		{
			if (this._targetObject.Value == null)
			{
				this.Continue();
				return;
			}
			if (this.stopPreviousTweens)
			{
				foreach (iTween iTween in this._targetObject.Value.GetComponents<iTween>())
				{
					iTween.time = 0f;
					iTween.SendMessage("Update");
				}
			}
			this.DoTween();
			if (!this.waitUntilFinished)
			{
				this.Continue();
			}
		}

		// Token: 0x060064C6 RID: 25798 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void DoTween()
		{
		}

		// Token: 0x060064C7 RID: 25799 RVA: 0x00280A68 File Offset: 0x0027EC68
		public override string GetSummary()
		{
			if (this._targetObject.Value == null)
			{
				return "Error: No target object selected";
			}
			return string.Concat(new object[]
			{
				this._targetObject.Value.name,
				" over ",
				this._duration.Value,
				" seconds"
			});
		}

		// Token: 0x060064C8 RID: 25800 RVA: 0x00280ACF File Offset: 0x0027ECCF
		public override Color GetButtonColor()
		{
			return new Color32(233, 163, 180, byte.MaxValue);
		}

		// Token: 0x060064C9 RID: 25801 RVA: 0x00280AEF File Offset: 0x0027ECEF
		public override bool HasReference(Variable variable)
		{
			return this._targetObject.gameObjectRef == variable || this._tweenName.stringRef == variable || base.HasReference(variable);
		}

		// Token: 0x060064CA RID: 25802 RVA: 0x00280B20 File Offset: 0x0027ED20
		protected virtual void OnEnable()
		{
			if (this.targetObjectOLD != null)
			{
				this._targetObject.Value = this.targetObjectOLD;
				this.targetObjectOLD = null;
			}
			if (this.tweenNameOLD != "")
			{
				this._tweenName.Value = this.tweenNameOLD;
				this.tweenNameOLD = "";
			}
			if (this.durationOLD != 0f)
			{
				this._duration.Value = this.durationOLD;
				this.durationOLD = 0f;
			}
		}

		// Token: 0x040056B4 RID: 22196
		[Tooltip("Target game object to apply the Tween to")]
		[SerializeField]
		protected GameObjectData _targetObject;

		// Token: 0x040056B5 RID: 22197
		[Tooltip("An individual name useful for stopping iTweens by name")]
		[SerializeField]
		protected StringData _tweenName;

		// Token: 0x040056B6 RID: 22198
		[Tooltip("The time in seconds the animation will take to complete")]
		[SerializeField]
		protected FloatData _duration = new FloatData(1f);

		// Token: 0x040056B7 RID: 22199
		[Tooltip("The shape of the easing curve applied to the animation")]
		[SerializeField]
		protected iTween.EaseType easeType = iTween.EaseType.easeInOutQuad;

		// Token: 0x040056B8 RID: 22200
		[Tooltip("The type of loop to apply once the animation has completed")]
		[SerializeField]
		protected iTween.LoopType loopType;

		// Token: 0x040056B9 RID: 22201
		[Tooltip("Stop any previously added iTweens on this object before adding this iTween")]
		[SerializeField]
		protected bool stopPreviousTweens;

		// Token: 0x040056BA RID: 22202
		[Tooltip("Wait until the tween has finished before executing the next command")]
		[SerializeField]
		protected bool waitUntilFinished = true;

		// Token: 0x040056BB RID: 22203
		[HideInInspector]
		[FormerlySerializedAs("target")]
		[FormerlySerializedAs("targetObject")]
		public GameObject targetObjectOLD;

		// Token: 0x040056BC RID: 22204
		[HideInInspector]
		[FormerlySerializedAs("tweenName")]
		public string tweenNameOLD = "";

		// Token: 0x040056BD RID: 22205
		[HideInInspector]
		[FormerlySerializedAs("duration")]
		public float durationOLD;
	}
}
