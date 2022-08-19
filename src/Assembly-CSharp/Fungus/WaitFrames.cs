using System;
using System.Collections;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E5E RID: 3678
	[CommandInfo("Flow", "Wait Frames", "Waits for a number of frames before executing the next command in the block.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class WaitFrames : Command
	{
		// Token: 0x06006748 RID: 26440 RVA: 0x00289EC7 File Offset: 0x002880C7
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

		// Token: 0x06006749 RID: 26441 RVA: 0x00289ED6 File Offset: 0x002880D6
		public override void OnEnter()
		{
			base.StartCoroutine(this.WaitForFrames());
		}

		// Token: 0x0600674A RID: 26442 RVA: 0x00289EE8 File Offset: 0x002880E8
		public override string GetSummary()
		{
			return this.frameCount.Value.ToString() + " frames";
		}

		// Token: 0x0600674B RID: 26443 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x0600674C RID: 26444 RVA: 0x00289F12 File Offset: 0x00288112
		public override bool HasReference(Variable variable)
		{
			return this.frameCount.integerRef == variable || base.HasReference(variable);
		}

		// Token: 0x04005855 RID: 22613
		[Tooltip("Number of frames to wait for")]
		[SerializeField]
		protected IntegerData frameCount = new IntegerData(1);
	}
}
