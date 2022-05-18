using System;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Execution
{
	// Token: 0x02001156 RID: 4438
	internal static class InstructionFieldUsage_Extensions
	{
		// Token: 0x06006BB5 RID: 27573 RVA: 0x002948E8 File Offset: 0x00292AE8
		internal static InstructionFieldUsage GetFieldUsage(this OpCode op)
		{
			switch (op)
			{
			case OpCode.Nop:
			case OpCode.Debug:
			case OpCode.Invalid:
				return InstructionFieldUsage.Name;
			case OpCode.Pop:
			case OpCode.Copy:
			case OpCode.TblInitI:
			case OpCode.Ret:
			case OpCode.MkTuple:
			case OpCode.Incr:
			case OpCode.ToNum:
			case OpCode.ExpTuple:
				return InstructionFieldUsage.NumVal;
			case OpCode.Swap:
			case OpCode.Clean:
				return InstructionFieldUsage.NumVal | InstructionFieldUsage.NumVal2;
			case OpCode.Literal:
			case OpCode.Index:
			case OpCode.IndexN:
			case OpCode.IndexL:
				return InstructionFieldUsage.Value;
			case OpCode.Closure:
				return InstructionFieldUsage.SymbolList | InstructionFieldUsage.NumVal;
			case OpCode.NewTable:
			case OpCode.TblInitN:
			case OpCode.Concat:
			case OpCode.LessEq:
			case OpCode.Less:
			case OpCode.Eq:
			case OpCode.Add:
			case OpCode.Sub:
			case OpCode.Mul:
			case OpCode.Div:
			case OpCode.Mod:
			case OpCode.Not:
			case OpCode.Len:
			case OpCode.Neg:
			case OpCode.Power:
			case OpCode.CNot:
			case OpCode.Scalar:
			case OpCode.ToBool:
			case OpCode.IterPrep:
			case OpCode.IterUpd:
				return InstructionFieldUsage.None;
			case OpCode.StoreLcl:
			case OpCode.StoreUpv:
				return InstructionFieldUsage.Symbol | InstructionFieldUsage.NumVal | InstructionFieldUsage.NumVal2;
			case OpCode.Local:
			case OpCode.Upvalue:
				return InstructionFieldUsage.Symbol;
			case OpCode.IndexSet:
			case OpCode.IndexSetN:
			case OpCode.IndexSetL:
				return InstructionFieldUsage.Symbol | InstructionFieldUsage.Value | InstructionFieldUsage.NumVal | InstructionFieldUsage.NumVal2;
			case OpCode.Meta:
				return InstructionFieldUsage.Name | InstructionFieldUsage.Value | InstructionFieldUsage.NumVal | InstructionFieldUsage.NumVal2;
			case OpCode.BeginFn:
				return InstructionFieldUsage.SymbolList | InstructionFieldUsage.NumVal | InstructionFieldUsage.NumVal2;
			case OpCode.Args:
				return InstructionFieldUsage.SymbolList;
			case OpCode.Call:
			case OpCode.ThisCall:
				return InstructionFieldUsage.Name | InstructionFieldUsage.NumVal;
			case OpCode.Jump:
			case OpCode.Jf:
			case OpCode.JNil:
			case OpCode.JFor:
			case OpCode.JtOrPop:
			case OpCode.JfOrPop:
				return InstructionFieldUsage.NumValAsCodeAddress;
			default:
				throw new NotImplementedException(string.Format("InstructionFieldUsage for instruction {0}", (int)op));
			}
		}
	}
}
