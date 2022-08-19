using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DE8 RID: 3560
	[CommandInfo("LeanTween", "StopTweens", "Stops the LeanTweens on a target GameObject", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class StopTweensLean : Command
	{
		// Token: 0x060064E9 RID: 25833 RVA: 0x002812EF File Offset: 0x0027F4EF
		public override void OnEnter()
		{
			if (this._targetObject.Value != null)
			{
				LeanTween.cancel(this._targetObject.Value);
			}
			this.Continue();
		}

		// Token: 0x060064EA RID: 25834 RVA: 0x0028131A File Offset: 0x0027F51A
		public override string GetSummary()
		{
			if (this._targetObject.Value == null)
			{
				return "Error: No target object selected";
			}
			return "Stop all LeanTweens on " + this._targetObject.Value.name;
		}

		// Token: 0x060064EB RID: 25835 RVA: 0x00280ACF File Offset: 0x0027ECCF
		public override Color GetButtonColor()
		{
			return new Color32(233, 163, 180, byte.MaxValue);
		}

		// Token: 0x060064EC RID: 25836 RVA: 0x0028134F File Offset: 0x0027F54F
		public override bool HasReference(Variable variable)
		{
			return this._targetObject.gameObjectRef == variable;
		}

		// Token: 0x040056D4 RID: 22228
		[Tooltip("Target game object stop LeanTweens on")]
		[SerializeField]
		protected GameObjectData _targetObject;
	}
}
