using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020011F4 RID: 4596
	[AddComponentMenu("")]
	public abstract class Condition : Command
	{
		// Token: 0x06007080 RID: 28800 RVA: 0x002A26D8 File Offset: 0x002A08D8
		public static string GetOperatorDescription(CompareOperator compareOperator)
		{
			string text = "";
			switch (compareOperator)
			{
			case CompareOperator.Equals:
				text += "==";
				break;
			case CompareOperator.NotEquals:
				text += "!=";
				break;
			case CompareOperator.LessThan:
				text += "<";
				break;
			case CompareOperator.GreaterThan:
				text += ">";
				break;
			case CompareOperator.LessThanOrEquals:
				text += "<=";
				break;
			case CompareOperator.GreaterThanOrEquals:
				text += ">=";
				break;
			}
			return text;
		}

		// Token: 0x06007081 RID: 28801 RVA: 0x002A2760 File Offset: 0x002A0960
		public override void OnEnter()
		{
			if (this.ParentBlock == null)
			{
				return;
			}
			if (!this.HasNeededProperties())
			{
				this.Continue();
				return;
			}
			if (!this.IsElseIf)
			{
				this.EvaluateAndContinue();
				return;
			}
			Type previousActiveCommandType = this.ParentBlock.GetPreviousActiveCommandType();
			if (this.ParentBlock.GetPreviousActiveCommandIndent() == this.IndentLevel && previousActiveCommandType.IsSubclassOf(typeof(Condition)))
			{
				this.EvaluateAndContinue();
				return;
			}
			if (this.CommandIndex >= this.ParentBlock.CommandList.Count - 1)
			{
				this.StopParentBlock();
				return;
			}
			int indentLevel = this.indentLevel;
			for (int i = this.CommandIndex + 1; i < this.ParentBlock.CommandList.Count; i++)
			{
				Command command = this.ParentBlock.CommandList[i];
				if (command.IndentLevel == indentLevel && command.GetType() == typeof(End))
				{
					this.Continue(command.CommandIndex + 1);
					return;
				}
			}
			this.StopParentBlock();
		}

		// Token: 0x06007082 RID: 28802 RVA: 0x0000A093 File Offset: 0x00008293
		public override bool OpenBlock()
		{
			return true;
		}

		// Token: 0x06007083 RID: 28803 RVA: 0x0004C70B File Offset: 0x0004A90B
		protected virtual void EvaluateAndContinue()
		{
			if (this.EvaluateCondition())
			{
				this.OnTrue();
				return;
			}
			this.OnFalse();
		}

		// Token: 0x06007084 RID: 28804 RVA: 0x00011424 File Offset: 0x0000F624
		protected virtual void OnTrue()
		{
			this.Continue();
		}

		// Token: 0x06007085 RID: 28805 RVA: 0x002A2864 File Offset: 0x002A0A64
		protected virtual void OnFalse()
		{
			if (this.CommandIndex >= this.ParentBlock.CommandList.Count)
			{
				this.StopParentBlock();
				return;
			}
			for (int i = this.CommandIndex + 1; i < this.ParentBlock.CommandList.Count; i++)
			{
				Command command = this.ParentBlock.CommandList[i];
				if (!(command == null) && command.enabled && !(command.GetType() == typeof(Comment)) && !(command.GetType() == typeof(Label)) && command.IndentLevel == this.indentLevel)
				{
					Type type = command.GetType();
					if (type == typeof(Else) || type == typeof(End))
					{
						if (i < this.ParentBlock.CommandList.Count - 1)
						{
							this.Continue(command.CommandIndex + 1);
							return;
						}
						this.StopParentBlock();
					}
					else if (type.IsSubclassOf(typeof(Condition)) && (command as Condition).IsElseIf)
					{
						this.Continue(i);
						return;
					}
				}
			}
			this.StopParentBlock();
		}

		// Token: 0x06007086 RID: 28806
		protected abstract bool EvaluateCondition();

		// Token: 0x06007087 RID: 28807
		protected abstract bool HasNeededProperties();

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x06007088 RID: 28808 RVA: 0x00004050 File Offset: 0x00002250
		protected virtual bool IsElseIf
		{
			get
			{
				return false;
			}
		}
	}
}
