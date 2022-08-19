using System;

namespace Fungus
{
	// Token: 0x02000E12 RID: 3602
	[CommandInfo("Priority Signals", "Get Priority Count", "Copy the value of the Priority Count to a local IntegerVariable, intended primarily to assist with debugging use of Priority.", 0)]
	public class FungusPriorityCount : Command
	{
		// Token: 0x060065BD RID: 26045 RVA: 0x00283F75 File Offset: 0x00282175
		public override void OnEnter()
		{
			this.outVar.Value = FungusPrioritySignals.CurrentPriorityDepth;
			this.Continue();
		}

		// Token: 0x060065BE RID: 26046 RVA: 0x00283F8D File Offset: 0x0028218D
		public override string GetSummary()
		{
			if (this.outVar == null)
			{
				return "Error: No out var supplied";
			}
			return this.outVar.Key;
		}

		// Token: 0x060065BF RID: 26047 RVA: 0x00283FAE File Offset: 0x002821AE
		public override bool HasReference(Variable variable)
		{
			return this.outVar == variable;
		}

		// Token: 0x04005756 RID: 22358
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		public IntegerVariable outVar;
	}
}
