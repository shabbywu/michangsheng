using System;
using System.Collections;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020012B3 RID: 4787
	[CommandInfo("Flow", "Wait Frames", "Waits for a number of frames before executing the next command in the block.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class WaitFrames : Command
	{
		// Token: 0x060073D6 RID: 29654 RVA: 0x0004F10E File Offset: 0x0004D30E
		protected virtual IEnumerator WaitForFrames()
		{
			int num;
			for (int count = this.frameCount.Value; count > 0; count = num - 1)
			{
				yield return new WaitForEndOfFrame();
				num = count;
			}
			this.Continue();
			yield break;
		}

		// Token: 0x060073D7 RID: 29655 RVA: 0x0004F11D File Offset: 0x0004D31D
		public override void OnEnter()
		{
			base.StartCoroutine(this.WaitForFrames());
		}

		// Token: 0x060073D8 RID: 29656 RVA: 0x002AC8C4 File Offset: 0x002AAAC4
		public override string GetSummary()
		{
			return this.frameCount.Value.ToString() + " frames";
		}

		// Token: 0x060073D9 RID: 29657 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x060073DA RID: 29658 RVA: 0x0004F12C File Offset: 0x0004D32C
		public override bool HasReference(Variable variable)
		{
			return this.frameCount.integerRef == variable || base.HasReference(variable);
		}

		// Token: 0x040065B4 RID: 26036
		[Tooltip("Number of frames to wait for")]
		[SerializeField]
		protected IntegerData frameCount = new IntegerData(1);
	}
}
