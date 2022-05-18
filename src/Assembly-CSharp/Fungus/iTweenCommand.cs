using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02001222 RID: 4642
	[ExecuteInEditMode]
	public abstract class iTweenCommand : Command
	{
		// Token: 0x06007150 RID: 29008 RVA: 0x002A551C File Offset: 0x002A371C
		protected virtual void OniTweenComplete(object param)
		{
			Command command = param as Command;
			if (command != null && command.Equals(this) && this.waitUntilFinished)
			{
				this.Continue();
			}
		}

		// Token: 0x06007151 RID: 29009 RVA: 0x002A5550 File Offset: 0x002A3750
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

		// Token: 0x06007152 RID: 29010 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void DoTween()
		{
		}

		// Token: 0x06007153 RID: 29011 RVA: 0x002A55CC File Offset: 0x002A37CC
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

		// Token: 0x06007154 RID: 29012 RVA: 0x0004CF7D File Offset: 0x0004B17D
		public override Color GetButtonColor()
		{
			return new Color32(233, 163, 180, byte.MaxValue);
		}

		// Token: 0x06007155 RID: 29013 RVA: 0x0004CF9D File Offset: 0x0004B19D
		public override bool HasReference(Variable variable)
		{
			return this._targetObject.gameObjectRef == variable || this._tweenName.stringRef == variable || base.HasReference(variable);
		}

		// Token: 0x06007156 RID: 29014 RVA: 0x002A5634 File Offset: 0x002A3834
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

		// Token: 0x040063BB RID: 25531
		[Tooltip("Target game object to apply the Tween to")]
		[SerializeField]
		protected GameObjectData _targetObject;

		// Token: 0x040063BC RID: 25532
		[Tooltip("An individual name useful for stopping iTweens by name")]
		[SerializeField]
		protected StringData _tweenName;

		// Token: 0x040063BD RID: 25533
		[Tooltip("The time in seconds the animation will take to complete")]
		[SerializeField]
		protected FloatData _duration = new FloatData(1f);

		// Token: 0x040063BE RID: 25534
		[Tooltip("The shape of the easing curve applied to the animation")]
		[SerializeField]
		protected iTween.EaseType easeType = iTween.EaseType.easeInOutQuad;

		// Token: 0x040063BF RID: 25535
		[Tooltip("The type of loop to apply once the animation has completed")]
		[SerializeField]
		protected iTween.LoopType loopType;

		// Token: 0x040063C0 RID: 25536
		[Tooltip("Stop any previously added iTweens on this object before adding this iTween")]
		[SerializeField]
		protected bool stopPreviousTweens;

		// Token: 0x040063C1 RID: 25537
		[Tooltip("Wait until the tween has finished before executing the next command")]
		[SerializeField]
		protected bool waitUntilFinished = true;

		// Token: 0x040063C2 RID: 25538
		[HideInInspector]
		[FormerlySerializedAs("target")]
		[FormerlySerializedAs("targetObject")]
		public GameObject targetObjectOLD;

		// Token: 0x040063C3 RID: 25539
		[HideInInspector]
		[FormerlySerializedAs("tweenName")]
		public string tweenNameOLD = "";

		// Token: 0x040063C4 RID: 25540
		[HideInInspector]
		[FormerlySerializedAs("duration")]
		public float durationOLD;
	}
}
