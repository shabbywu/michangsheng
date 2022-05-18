using System;

namespace Fungus
{
	// Token: 0x0200125F RID: 4703
	[CommandInfo("Priority Signals", "Get Priority Count", "Copy the value of the Priority Count to a local IntegerVariable, intended primarily to assist with debugging use of Priority.", 0)]
	public class FungusPriorityCount : Command
	{
		// Token: 0x0600724B RID: 29259 RVA: 0x0004DC89 File Offset: 0x0004BE89
		public override void OnEnter()
		{
			this.outVar.Value = FungusPrioritySignals.CurrentPriorityDepth;
			this.Continue();
		}

		// Token: 0x0600724C RID: 29260 RVA: 0x0004DCA1 File Offset: 0x0004BEA1
		public override string GetSummary()
		{
			if (this.outVar == null)
			{
				return "Error: No out var supplied";
			}
			return this.outVar.Key;
		}

		// Token: 0x0600724D RID: 29261 RVA: 0x0004DCC2 File Offset: 0x0004BEC2
		public override bool HasReference(Variable variable)
		{
			return this.outVar == variable;
		}

		// Token: 0x0400648B RID: 25739
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		public IntegerVariable outVar;
	}
}
