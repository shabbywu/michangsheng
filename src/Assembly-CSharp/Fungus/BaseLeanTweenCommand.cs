using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DE4 RID: 3556
	[ExecuteInEditMode]
	public abstract class BaseLeanTweenCommand : Command
	{
		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x060064D7 RID: 25815 RVA: 0x00280D49 File Offset: 0x0027EF49
		public bool IsInFromMode
		{
			get
			{
				return this._toFrom == BaseLeanTweenCommand.ToFrom.From;
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x060064D8 RID: 25816 RVA: 0x00280D54 File Offset: 0x0027EF54
		public bool IsInAddativeMode
		{
			get
			{
				return this._absAdd == BaseLeanTweenCommand.AbsAdd.Additive;
			}
		}

		// Token: 0x060064D9 RID: 25817 RVA: 0x0005E3AF File Offset: 0x0005C5AF
		protected virtual void OnTweenComplete()
		{
			this.Continue();
		}

		// Token: 0x060064DA RID: 25818 RVA: 0x00280D60 File Offset: 0x0027EF60
		public override void OnEnter()
		{
			if (this._targetObject.Value == null)
			{
				this.Continue();
				return;
			}
			if (this.stopPreviousTweens)
			{
				LeanTween.cancel(this._targetObject.Value);
			}
			this.ourTween = this.ExecuteTween();
			this.ourTween.setEase(this.easeType).setRepeat(this.repeats).setLoopType(this.loopType);
			if (this.waitUntilFinished)
			{
				if (this.ourTween != null)
				{
					this.ourTween.setOnComplete(new Action(this.OnTweenComplete));
					return;
				}
			}
			else
			{
				this.Continue();
			}
		}

		// Token: 0x060064DB RID: 25819
		public abstract LTDescr ExecuteTween();

		// Token: 0x060064DC RID: 25820 RVA: 0x00280E04 File Offset: 0x0027F004
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

		// Token: 0x060064DD RID: 25821 RVA: 0x00280ACF File Offset: 0x0027ECCF
		public override Color GetButtonColor()
		{
			return new Color32(233, 163, 180, byte.MaxValue);
		}

		// Token: 0x060064DE RID: 25822 RVA: 0x00280E6B File Offset: 0x0027F06B
		public override bool HasReference(Variable variable)
		{
			return variable == this._targetObject.gameObjectRef || variable == this._duration.floatRef;
		}

		// Token: 0x040056C1 RID: 22209
		[Tooltip("Target game object to apply the Tween to")]
		[SerializeField]
		protected GameObjectData _targetObject;

		// Token: 0x040056C2 RID: 22210
		[Tooltip("The time in seconds the animation will take to complete")]
		[SerializeField]
		protected FloatData _duration = new FloatData(1f);

		// Token: 0x040056C3 RID: 22211
		[Tooltip("Does the tween act from current TO destination or is it reversed and act FROM destination to its current")]
		[SerializeField]
		protected BaseLeanTweenCommand.ToFrom _toFrom;

		// Token: 0x040056C4 RID: 22212
		[Tooltip("Does the tween use the value as a target or as a delta to be added to current.")]
		[SerializeField]
		protected BaseLeanTweenCommand.AbsAdd _absAdd;

		// Token: 0x040056C5 RID: 22213
		[Tooltip("The shape of the easing curve applied to the animation")]
		[SerializeField]
		protected LeanTweenType easeType = LeanTweenType.easeInOutQuad;

		// Token: 0x040056C6 RID: 22214
		[Tooltip("The type of loop to apply once the animation has completed")]
		[SerializeField]
		protected LeanTweenType loopType = LeanTweenType.once;

		// Token: 0x040056C7 RID: 22215
		[Tooltip("Number of times to repeat the tween, -1 is infinite.")]
		[SerializeField]
		protected int repeats;

		// Token: 0x040056C8 RID: 22216
		[Tooltip("Stop any previously LeanTweens on this object before adding this one. Warning; expensive.")]
		[SerializeField]
		protected bool stopPreviousTweens;

		// Token: 0x040056C9 RID: 22217
		[Tooltip("Wait until the tween has finished before executing the next command")]
		[SerializeField]
		protected bool waitUntilFinished = true;

		// Token: 0x040056CA RID: 22218
		[HideInInspector]
		protected LTDescr ourTween;

		// Token: 0x020016B5 RID: 5813
		public enum ToFrom
		{
			// Token: 0x04007363 RID: 29539
			To,
			// Token: 0x04007364 RID: 29540
			From
		}

		// Token: 0x020016B6 RID: 5814
		public enum AbsAdd
		{
			// Token: 0x04007366 RID: 29542
			Absolute,
			// Token: 0x04007367 RID: 29543
			Additive
		}
	}
}
