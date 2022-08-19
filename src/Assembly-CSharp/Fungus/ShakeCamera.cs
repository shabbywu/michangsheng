using System;
using System.Collections;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E46 RID: 3654
	[CommandInfo("Camera", "Shake Camera", "Applies a camera shake effect to the main camera.", 0)]
	[AddComponentMenu("")]
	public class ShakeCamera : Command
	{
		// Token: 0x060066D1 RID: 26321 RVA: 0x00287C44 File Offset: 0x00285E44
		protected virtual void OniTweenComplete(object param)
		{
			Command command = param as Command;
			if (command != null && command.Equals(this) && this.waitUntilFinished)
			{
				this.Continue();
			}
		}

		// Token: 0x060066D2 RID: 26322 RVA: 0x00287C78 File Offset: 0x00285E78
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

		// Token: 0x060066D3 RID: 26323 RVA: 0x00287D11 File Offset: 0x00285F11
		public override string GetSummary()
		{
			return "For " + this.duration + " seconds.";
		}

		// Token: 0x060066D4 RID: 26324 RVA: 0x0027EC04 File Offset: 0x0027CE04
		public override Color GetButtonColor()
		{
			return new Color32(216, 228, 170, byte.MaxValue);
		}

		// Token: 0x04005804 RID: 22532
		[Tooltip("Time for camera shake effect to complete")]
		[SerializeField]
		protected float duration = 0.5f;

		// Token: 0x04005805 RID: 22533
		[Tooltip("Magnitude of shake effect in x & y axes")]
		[SerializeField]
		protected Vector2 amount = new Vector2(1f, 1f);

		// Token: 0x04005806 RID: 22534
		[Tooltip("Wait until the shake effect has finished before executing next command")]
		[SerializeField]
		protected bool waitUntilFinished;
	}
}
