using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001224 RID: 4644
	[CommandInfo("Flow", "Label", "Marks a position in the command list for execution to jump to.", 0)]
	[AddComponentMenu("")]
	public class Label : Command
	{
		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x0600715E RID: 29022 RVA: 0x0004D08C File Offset: 0x0004B28C
		public virtual string Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x0600715F RID: 29023 RVA: 0x00011424 File Offset: 0x0000F624
		public override void OnEnter()
		{
			this.Continue();
		}

		// Token: 0x06007160 RID: 29024 RVA: 0x0004D08C File Offset: 0x0004B28C
		public override string GetSummary()
		{
			return this.key;
		}

		// Token: 0x06007161 RID: 29025 RVA: 0x0004D094 File Offset: 0x0004B294
		public override Color GetButtonColor()
		{
			return new Color32(200, 200, 253, byte.MaxValue);
		}

		// Token: 0x040063C7 RID: 25543
		[Tooltip("Display name for the label")]
		[SerializeField]
		protected string key = "";
	}
}
