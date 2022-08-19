using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DBE RID: 3518
	[AddComponentMenu("")]
	public abstract class Condition : Command
	{
		// Token: 0x06006413 RID: 25619 RVA: 0x0027D598 File Offset: 0x0027B798
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

		// Token: 0x06006414 RID: 25620 RVA: 0x0027D620 File Offset: 0x0027B820
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

		// Token: 0x06006415 RID: 25621 RVA: 0x00024C5F File Offset: 0x00022E5F
		public override bool OpenBlock()
		{
			return true;
		}

		// Token: 0x06006416 RID: 25622 RVA: 0x0027D723 File Offset: 0x0027B923
		protected virtual void EvaluateAndContinue()
		{
			if (this.EvaluateCondition())
			{
				this.OnTrue();
				return;
			}
			this.OnFalse();
		}

		// Token: 0x06006417 RID: 25623 RVA: 0x0005E3AF File Offset: 0x0005C5AF
		protected virtual void OnTrue()
		{
			this.Continue();
		}

		// Token: 0x06006418 RID: 25624 RVA: 0x0027D73C File Offset: 0x0027B93C
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

		// Token: 0x06006419 RID: 25625
		protected abstract bool EvaluateCondition();

		// Token: 0x0600641A RID: 25626
		protected abstract bool HasNeededProperties();

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x0600641B RID: 25627 RVA: 0x0000280F File Offset: 0x00000A0F
		protected virtual bool IsElseIf
		{
			get
			{
				return false;
			}
		}
	}
}
