using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DC6 RID: 3526
	[CommandInfo("Scripting", "Debug Log", "Writes a log message to the debug console.", 0)]
	[AddComponentMenu("")]
	public class DebugLog : Command
	{
		// Token: 0x06006449 RID: 25673 RVA: 0x0027E2E4 File Offset: 0x0027C4E4
		public override void OnEnter()
		{
			string text = this.GetFlowchart().SubstituteVariables(this.logMessage.Value);
			switch (this.logType)
			{
			case DebugLogType.Info:
				Debug.Log(text);
				break;
			case DebugLogType.Warning:
				Debug.LogWarning(text);
				break;
			case DebugLogType.Error:
				Debug.LogError(text);
				break;
			}
			this.Continue();
		}

		// Token: 0x0600644A RID: 25674 RVA: 0x0027E33F File Offset: 0x0027C53F
		public override string GetSummary()
		{
			return this.logMessage.GetDescription();
		}

		// Token: 0x0600644B RID: 25675 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x0600644C RID: 25676 RVA: 0x0027E34C File Offset: 0x0027C54C
		public override bool HasReference(Variable variable)
		{
			return this.logMessage.stringRef == variable || base.HasReference(variable);
		}

		// Token: 0x0400563E RID: 22078
		[Tooltip("Display type of debug log info")]
		[SerializeField]
		protected DebugLogType logType;

		// Token: 0x0400563F RID: 22079
		[Tooltip("Text to write to the debug log. Supports variable substitution, e.g. {$Myvar}")]
		[SerializeField]
		protected StringDataMulti logMessage;
	}
}
