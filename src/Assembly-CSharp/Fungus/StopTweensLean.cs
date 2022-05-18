using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200122C RID: 4652
	[CommandInfo("LeanTween", "StopTweens", "Stops the LeanTweens on a target GameObject", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class StopTweensLean : Command
	{
		// Token: 0x06007175 RID: 29045 RVA: 0x0004D1E6 File Offset: 0x0004B3E6
		public override void OnEnter()
		{
			if (this._targetObject.Value != null)
			{
				LeanTween.cancel(this._targetObject.Value);
			}
			this.Continue();
		}

		// Token: 0x06007176 RID: 29046 RVA: 0x0004D211 File Offset: 0x0004B411
		public override string GetSummary()
		{
			if (this._targetObject.Value == null)
			{
				return "Error: No target object selected";
			}
			return "Stop all LeanTweens on " + this._targetObject.Value.name;
		}

		// Token: 0x06007177 RID: 29047 RVA: 0x0004CF7D File Offset: 0x0004B17D
		public override Color GetButtonColor()
		{
			return new Color32(233, 163, 180, byte.MaxValue);
		}

		// Token: 0x06007178 RID: 29048 RVA: 0x0004D246 File Offset: 0x0004B446
		public override bool HasReference(Variable variable)
		{
			return this._targetObject.gameObjectRef == variable;
		}

		// Token: 0x040063E5 RID: 25573
		[Tooltip("Target game object stop LeanTweens on")]
		[SerializeField]
		protected GameObjectData _targetObject;
	}
}
