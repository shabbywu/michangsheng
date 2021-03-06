using System;
using System.Collections;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001297 RID: 4759
	[CommandInfo("Camera", "Shake Camera", "Applies a camera shake effect to the main camera.", 0)]
	[AddComponentMenu("")]
	public class ShakeCamera : Command
	{
		// Token: 0x0600735F RID: 29535 RVA: 0x002AABE0 File Offset: 0x002A8DE0
		protected virtual void OniTweenComplete(object param)
		{
			Command command = param as Command;
			if (command != null && command.Equals(this) && this.waitUntilFinished)
			{
				this.Continue();
			}
		}

		// Token: 0x06007360 RID: 29536 RVA: 0x002AAC14 File Offset: 0x002A8E14
		public override void OnEnter()
		{
			Vector3 vector = default(Vector3);
			vector = this.amount;
			Hashtable hashtable = new Hashtable();
			hashtable.Add("amount", vector);
			hashtable.Add("time", this.duration);
			hashtable.Add("oncomplete", "OniTweenComplete");
			hashtable.Add("oncompletetarget", base.gameObject);
			hashtable.Add("oncompleteparams", this);
			iTween.ShakePosition(Camera.main.gameObject, hashtable);
			if (!this.waitUntilFinished)
			{
				this.Continue();
			}
		}

		// Token: 0x06007361 RID: 29537 RVA: 0x0004EB6E File Offset: 0x0004CD6E
		public override string GetSummary()
		{
			return "For " + this.duration + " seconds.";
		}

		// Token: 0x06007362 RID: 29538 RVA: 0x0004CAB8 File Offset: 0x0004ACB8
		public override Color GetButtonColor()
		{
			return new Color32(216, 228, 170, byte.MaxValue);
		}

		// Token: 0x04006548 RID: 25928
		[Tooltip("Time for camera shake effect to complete")]
		[SerializeField]
		protected float duration = 0.5f;

		// Token: 0x04006549 RID: 25929
		[Tooltip("Magnitude of shake effect in x & y axes")]
		[SerializeField]
		protected Vector2 amount = new Vector2(1f, 1f);

		// Token: 0x0400654A RID: 25930
		[Tooltip("Wait until the shake effect has finished before executing next command")]
		[SerializeField]
		protected bool waitUntilFinished;
	}
}
