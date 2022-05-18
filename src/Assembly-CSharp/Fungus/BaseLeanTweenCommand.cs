using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001225 RID: 4645
	[ExecuteInEditMode]
	public abstract class BaseLeanTweenCommand : Command
	{
		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06007163 RID: 29027 RVA: 0x0004D0C7 File Offset: 0x0004B2C7
		public bool IsInFromMode
		{
			get
			{
				return this._toFrom == BaseLeanTweenCommand.ToFrom.From;
			}
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06007164 RID: 29028 RVA: 0x0004D0D2 File Offset: 0x0004B2D2
		public bool IsInAddativeMode
		{
			get
			{
				return this._absAdd == BaseLeanTweenCommand.AbsAdd.Additive;
			}
		}

		// Token: 0x06007165 RID: 29029 RVA: 0x00011424 File Offset: 0x0000F624
		protected virtual void OnTweenComplete()
		{
			this.Continue();
		}

		// Token: 0x06007166 RID: 29030 RVA: 0x002A5768 File Offset: 0x002A3968
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

		// Token: 0x06007167 RID: 29031
		public abstract LTDescr ExecuteTween();

		// Token: 0x06007168 RID: 29032 RVA: 0x002A580C File Offset: 0x002A3A0C
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

		// Token: 0x06007169 RID: 29033 RVA: 0x0004CF7D File Offset: 0x0004B17D
		public override Color GetButtonColor()
		{
			return new Color32(233, 163, 180, byte.MaxValue);
		}

		// Token: 0x0600716A RID: 29034 RVA: 0x0004D0DD File Offset: 0x0004B2DD
		public override bool HasReference(Variable variable)
		{
			return variable == this._targetObject.gameObjectRef || variable == this._duration.floatRef;
		}

		// Token: 0x040063C8 RID: 25544
		[Tooltip("Target game object to apply the Tween to")]
		[SerializeField]
		protected GameObjectData _targetObject;

		// Token: 0x040063C9 RID: 25545
		[Tooltip("The time in seconds the animation will take to complete")]
		[SerializeField]
		protected FloatData _duration = new FloatData(1f);

		// Token: 0x040063CA RID: 25546
		[Tooltip("Does the tween act from current TO destination or is it reversed and act FROM destination to its current")]
		[SerializeField]
		protected BaseLeanTweenCommand.ToFrom _toFrom;

		// Token: 0x040063CB RID: 25547
		[Tooltip("Does the tween use the value as a target or as a delta to be added to current.")]
		[SerializeField]
		protected BaseLeanTweenCommand.AbsAdd _absAdd;

		// Token: 0x040063CC RID: 25548
		[Tooltip("The shape of the easing curve applied to the animation")]
		[SerializeField]
		protected LeanTweenType easeType = LeanTweenType.easeInOutQuad;

		// Token: 0x040063CD RID: 25549
		[Tooltip("The type of loop to apply once the animation has completed")]
		[SerializeField]
		protected LeanTweenType loopType = LeanTweenType.once;

		// Token: 0x040063CE RID: 25550
		[Tooltip("Number of times to repeat the tween, -1 is infinite.")]
		[SerializeField]
		protected int repeats;

		// Token: 0x040063CF RID: 25551
		[Tooltip("Stop any previously LeanTweens on this object before adding this one. Warning; expensive.")]
		[SerializeField]
		protected bool stopPreviousTweens;

		// Token: 0x040063D0 RID: 25552
		[Tooltip("Wait until the tween has finished before executing the next command")]
		[SerializeField]
		protected bool waitUntilFinished = true;

		// Token: 0x040063D1 RID: 25553
		[HideInInspector]
		protected LTDescr ourTween;

		// Token: 0x02001226 RID: 4646
		public enum ToFrom
		{
			// Token: 0x040063D3 RID: 25555
			To,
			// Token: 0x040063D4 RID: 25556
			From
		}

		// Token: 0x02001227 RID: 4647
		public enum AbsAdd
		{
			// Token: 0x040063D6 RID: 25558
			Absolute,
			// Token: 0x040063D7 RID: 25559
			Additive
		}
	}
}
