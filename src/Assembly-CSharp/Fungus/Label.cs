using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DE3 RID: 3555
	[CommandInfo("Flow", "Label", "Marks a position in the command list for execution to jump to.", 0)]
	[AddComponentMenu("")]
	public class Label : Command
	{
		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x060064D2 RID: 25810 RVA: 0x00280D0E File Offset: 0x0027EF0E
		public virtual string Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x060064D3 RID: 25811 RVA: 0x0005E3AF File Offset: 0x0005C5AF
		public override void OnEnter()
		{
			this.Continue();
		}

		// Token: 0x060064D4 RID: 25812 RVA: 0x00280D0E File Offset: 0x0027EF0E
		public override string GetSummary()
		{
			return this.key;
		}

		// Token: 0x060064D5 RID: 25813 RVA: 0x00280D16 File Offset: 0x0027EF16
		public override Color GetButtonColor()
		{
			return new Color32(200, 200, 253, byte.MaxValue);
		}

		// Token: 0x040056C0 RID: 22208
		[Tooltip("Display name for the label")]
		[SerializeField]
		protected string key = "";
	}
}
