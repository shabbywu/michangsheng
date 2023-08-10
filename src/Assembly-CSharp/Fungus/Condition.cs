using System;
using UnityEngine;

namespace Fungus;

[AddComponentMenu("")]
public abstract class Condition : Command
{
	protected virtual bool IsElseIf => false;

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

	public override void OnEnter()
	{
		if ((Object)(object)ParentBlock == (Object)null)
		{
			return;
		}
		if (!HasNeededProperties())
		{
			Continue();
			return;
		}
		if (!IsElseIf)
		{
			EvaluateAndContinue();
			return;
		}
		Type previousActiveCommandType = ParentBlock.GetPreviousActiveCommandType();
		if (ParentBlock.GetPreviousActiveCommandIndent() == IndentLevel && previousActiveCommandType.IsSubclassOf(typeof(Condition)))
		{
			EvaluateAndContinue();
			return;
		}
		if (CommandIndex >= ParentBlock.CommandList.Count - 1)
		{
			StopParentBlock();
			return;
		}
		int num = indentLevel;
		for (int i = CommandIndex + 1; i < ParentBlock.CommandList.Count; i++)
		{
			Command command = ParentBlock.CommandList[i];
			if (command.IndentLevel == num && ((object)command).GetType() == typeof(End))
			{
				Continue(command.CommandIndex + 1);
				return;
			}
		}
		StopParentBlock();
	}

	public override bool OpenBlock()
	{
		return true;
	}

	protected virtual void EvaluateAndContinue()
	{
		if (EvaluateCondition())
		{
			OnTrue();
		}
		else
		{
			OnFalse();
		}
	}

	protected virtual void OnTrue()
	{
		Continue();
	}

	protected virtual void OnFalse()
	{
		if (CommandIndex >= ParentBlock.CommandList.Count)
		{
			StopParentBlock();
			return;
		}
		for (int i = CommandIndex + 1; i < ParentBlock.CommandList.Count; i++)
		{
			Command command = ParentBlock.CommandList[i];
			if ((Object)(object)command == (Object)null || !((Behaviour)command).enabled || ((object)command).GetType() == typeof(Comment) || ((object)command).GetType() == typeof(Label) || command.IndentLevel != indentLevel)
			{
				continue;
			}
			Type type = ((object)command).GetType();
			if (type == typeof(Else) || type == typeof(End))
			{
				if (i < ParentBlock.CommandList.Count - 1)
				{
					Continue(command.CommandIndex + 1);
					return;
				}
				StopParentBlock();
			}
			else if (type.IsSubclassOf(typeof(Condition)) && (command as Condition).IsElseIf)
			{
				Continue(i);
				return;
			}
		}
		StopParentBlock();
	}

	protected abstract bool EvaluateCondition();

	protected abstract bool HasNeededProperties();
}
