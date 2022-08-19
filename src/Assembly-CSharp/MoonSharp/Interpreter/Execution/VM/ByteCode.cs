using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using MoonSharp.Interpreter.Debugging;

namespace MoonSharp.Interpreter.Execution.VM
{
	// Token: 0x02000D51 RID: 3409
	internal class ByteCode : RefIdObject
	{
		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06006004 RID: 24580 RVA: 0x0026D277 File Offset: 0x0026B477
		// (set) Token: 0x06006005 RID: 24581 RVA: 0x0026D27F File Offset: 0x0026B47F
		public Script Script { get; private set; }

		// Token: 0x06006006 RID: 24582 RVA: 0x0026D288 File Offset: 0x0026B488
		public ByteCode(Script script)
		{
			this.Script = script;
		}

		// Token: 0x06006007 RID: 24583 RVA: 0x0026D2B8 File Offset: 0x0026B4B8
		public IDisposable EnterSource(SourceRef sref)
		{
			return new ByteCode.SourceCodeStackGuard(sref, this);
		}

		// Token: 0x06006008 RID: 24584 RVA: 0x0026D2C1 File Offset: 0x0026B4C1
		public void PushSourceRef(SourceRef sref)
		{
			this.m_SourceRefStack.Add(sref);
			this.m_CurrentSourceRef = sref;
		}

		// Token: 0x06006009 RID: 24585 RVA: 0x0026D2D8 File Offset: 0x0026B4D8
		public void PopSourceRef()
		{
			this.m_SourceRefStack.RemoveAt(this.m_SourceRefStack.Count - 1);
			this.m_CurrentSourceRef = ((this.m_SourceRefStack.Count > 0) ? this.m_SourceRefStack[this.m_SourceRefStack.Count - 1] : null);
		}

		// Token: 0x0600600A RID: 24586 RVA: 0x0026D32C File Offset: 0x0026B52C
		public void Dump(string file)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.Code.Count; i++)
			{
				if (this.Code[i].OpCode == OpCode.Debug)
				{
					stringBuilder.AppendFormat("    {0}\n", this.Code[i]);
				}
				else
				{
					stringBuilder.AppendFormat("{0:X8}  {1}\n", i, this.Code[i]);
				}
			}
			File.WriteAllText(file, stringBuilder.ToString());
		}

		// Token: 0x0600600B RID: 24587 RVA: 0x0026D3AD File Offset: 0x0026B5AD
		public int GetJumpPointForNextInstruction()
		{
			return this.Code.Count;
		}

		// Token: 0x0600600C RID: 24588 RVA: 0x0026D3BA File Offset: 0x0026B5BA
		public int GetJumpPointForLastInstruction()
		{
			return this.Code.Count - 1;
		}

		// Token: 0x0600600D RID: 24589 RVA: 0x0026D3C9 File Offset: 0x0026B5C9
		public Instruction GetLastInstruction()
		{
			return this.Code[this.Code.Count - 1];
		}

		// Token: 0x0600600E RID: 24590 RVA: 0x0026D3E3 File Offset: 0x0026B5E3
		private Instruction AppendInstruction(Instruction c)
		{
			this.Code.Add(c);
			return c;
		}

		// Token: 0x0600600F RID: 24591 RVA: 0x0026D3F2 File Offset: 0x0026B5F2
		public Instruction Emit_Nop(string comment)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Nop,
				Name = comment
			});
		}

		// Token: 0x06006010 RID: 24592 RVA: 0x0026D413 File Offset: 0x0026B613
		public Instruction Emit_Invalid(string type)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Invalid,
				Name = type
			});
		}

		// Token: 0x06006011 RID: 24593 RVA: 0x0026D435 File Offset: 0x0026B635
		public Instruction Emit_Pop(int num = 1)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Pop,
				NumVal = num
			});
		}

		// Token: 0x06006012 RID: 24594 RVA: 0x0026D456 File Offset: 0x0026B656
		public void Emit_Call(int argCount, string debugName)
		{
			this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Call,
				NumVal = argCount,
				Name = debugName
			});
		}

		// Token: 0x06006013 RID: 24595 RVA: 0x0026D480 File Offset: 0x0026B680
		public void Emit_ThisCall(int argCount, string debugName)
		{
			this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.ThisCall,
				NumVal = argCount,
				Name = debugName
			});
		}

		// Token: 0x06006014 RID: 24596 RVA: 0x0026D4AA File Offset: 0x0026B6AA
		public Instruction Emit_Literal(DynValue value)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Literal,
				Value = value
			});
		}

		// Token: 0x06006015 RID: 24597 RVA: 0x0026D4CB File Offset: 0x0026B6CB
		public Instruction Emit_Jump(OpCode jumpOpCode, int idx, int optPar = 0)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = jumpOpCode,
				NumVal = idx,
				NumVal2 = optPar
			});
		}

		// Token: 0x06006016 RID: 24598 RVA: 0x0026D4F3 File Offset: 0x0026B6F3
		public Instruction Emit_MkTuple(int cnt)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.MkTuple,
				NumVal = cnt
			});
		}

		// Token: 0x06006017 RID: 24599 RVA: 0x0026D518 File Offset: 0x0026B718
		public Instruction Emit_Operator(OpCode opcode)
		{
			Instruction result = this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = opcode
			});
			if (opcode == OpCode.LessEq)
			{
				this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
				{
					OpCode = OpCode.CNot
				});
			}
			if (opcode == OpCode.Eq || opcode == OpCode.Less)
			{
				this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
				{
					OpCode = OpCode.ToBool
				});
			}
			return result;
		}

		// Token: 0x06006018 RID: 24600 RVA: 0x0026D580 File Offset: 0x0026B780
		[Conditional("EMIT_DEBUG_OPS")]
		public void Emit_Debug(string str)
		{
			this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Debug,
				Name = str.Substring(0, Math.Min(32, str.Length))
			});
		}

		// Token: 0x06006019 RID: 24601 RVA: 0x0026D5B5 File Offset: 0x0026B7B5
		public Instruction Emit_Enter(RuntimeScopeBlock runtimeScopeBlock)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Clean,
				NumVal = runtimeScopeBlock.From,
				NumVal2 = runtimeScopeBlock.ToInclusive
			});
		}

		// Token: 0x0600601A RID: 24602 RVA: 0x0026D5E8 File Offset: 0x0026B7E8
		public Instruction Emit_Leave(RuntimeScopeBlock runtimeScopeBlock)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Clean,
				NumVal = runtimeScopeBlock.From,
				NumVal2 = runtimeScopeBlock.To
			});
		}

		// Token: 0x0600601B RID: 24603 RVA: 0x0026D5B5 File Offset: 0x0026B7B5
		public Instruction Emit_Exit(RuntimeScopeBlock runtimeScopeBlock)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Clean,
				NumVal = runtimeScopeBlock.From,
				NumVal2 = runtimeScopeBlock.ToInclusive
			});
		}

		// Token: 0x0600601C RID: 24604 RVA: 0x0026D61B File Offset: 0x0026B81B
		public Instruction Emit_Clean(RuntimeScopeBlock runtimeScopeBlock)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Clean,
				NumVal = runtimeScopeBlock.To + 1,
				NumVal2 = runtimeScopeBlock.ToInclusive
			});
		}

		// Token: 0x0600601D RID: 24605 RVA: 0x0026D650 File Offset: 0x0026B850
		public Instruction Emit_Closure(SymbolRef[] symbols, int jmpnum)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Closure,
				SymbolList = symbols,
				NumVal = jmpnum
			});
		}

		// Token: 0x0600601E RID: 24606 RVA: 0x0026D678 File Offset: 0x0026B878
		public Instruction Emit_Args(params SymbolRef[] symbols)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Args,
				SymbolList = symbols
			});
		}

		// Token: 0x0600601F RID: 24607 RVA: 0x0026D69A File Offset: 0x0026B89A
		public Instruction Emit_Ret(int retvals)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Ret,
				NumVal = retvals
			});
		}

		// Token: 0x06006020 RID: 24608 RVA: 0x0026D6BC File Offset: 0x0026B8BC
		public Instruction Emit_ToNum(int stage = 0)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.ToNum,
				NumVal = stage
			});
		}

		// Token: 0x06006021 RID: 24609 RVA: 0x0026D6DE File Offset: 0x0026B8DE
		public Instruction Emit_Incr(int i)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Incr,
				NumVal = i
			});
		}

		// Token: 0x06006022 RID: 24610 RVA: 0x0026D700 File Offset: 0x0026B900
		public Instruction Emit_NewTable(bool shared)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.NewTable,
				NumVal = (shared ? 1 : 0)
			});
		}

		// Token: 0x06006023 RID: 24611 RVA: 0x0026D727 File Offset: 0x0026B927
		public Instruction Emit_IterPrep()
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.IterPrep
			});
		}

		// Token: 0x06006024 RID: 24612 RVA: 0x0026D742 File Offset: 0x0026B942
		public Instruction Emit_ExpTuple(int stackOffset)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.ExpTuple,
				NumVal = stackOffset
			});
		}

		// Token: 0x06006025 RID: 24613 RVA: 0x0026D764 File Offset: 0x0026B964
		public Instruction Emit_IterUpd()
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.IterUpd
			});
		}

		// Token: 0x06006026 RID: 24614 RVA: 0x0026D77F File Offset: 0x0026B97F
		public Instruction Emit_Meta(string funcName, OpCodeMetadataType metaType, DynValue value = null)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Meta,
				Name = funcName,
				NumVal2 = (int)metaType,
				Value = value
			});
		}

		// Token: 0x06006027 RID: 24615 RVA: 0x0026D7B0 File Offset: 0x0026B9B0
		public Instruction Emit_BeginFn(RuntimeScopeFrame stackFrame)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.BeginFn,
				SymbolList = stackFrame.DebugSymbols.ToArray(),
				NumVal = stackFrame.Count,
				NumVal2 = stackFrame.ToFirstBlock
			});
		}

		// Token: 0x06006028 RID: 24616 RVA: 0x0026D7FF File Offset: 0x0026B9FF
		public Instruction Emit_Scalar()
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Scalar
			});
		}

		// Token: 0x06006029 RID: 24617 RVA: 0x0026D81C File Offset: 0x0026BA1C
		public int Emit_Load(SymbolRef sym)
		{
			switch (sym.Type)
			{
			case SymbolRefType.Local:
				this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
				{
					OpCode = OpCode.Local,
					Symbol = sym
				});
				return 1;
			case SymbolRefType.Upvalue:
				this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
				{
					OpCode = OpCode.Upvalue,
					Symbol = sym
				});
				return 1;
			case SymbolRefType.Global:
				this.Emit_Load(sym.i_Env);
				this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
				{
					OpCode = OpCode.Index,
					Value = DynValue.NewString(sym.i_Name)
				});
				return 2;
			default:
				throw new InternalErrorException("Unexpected symbol type : {0}", new object[]
				{
					sym
				});
			}
		}

		// Token: 0x0600602A RID: 24618 RVA: 0x0026D8DC File Offset: 0x0026BADC
		public int Emit_Store(SymbolRef sym, int stackofs, int tupleidx)
		{
			switch (sym.Type)
			{
			case SymbolRefType.Local:
				this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
				{
					OpCode = OpCode.StoreLcl,
					Symbol = sym,
					NumVal = stackofs,
					NumVal2 = tupleidx
				});
				return 1;
			case SymbolRefType.Upvalue:
				this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
				{
					OpCode = OpCode.StoreUpv,
					Symbol = sym,
					NumVal = stackofs,
					NumVal2 = tupleidx
				});
				return 1;
			case SymbolRefType.Global:
				this.Emit_Load(sym.i_Env);
				this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
				{
					OpCode = OpCode.IndexSet,
					Symbol = sym,
					NumVal = stackofs,
					NumVal2 = tupleidx,
					Value = DynValue.NewString(sym.i_Name)
				});
				return 2;
			default:
				throw new InternalErrorException("Unexpected symbol type : {0}", new object[]
				{
					sym
				});
			}
		}

		// Token: 0x0600602B RID: 24619 RVA: 0x0026D9CC File Offset: 0x0026BBCC
		public Instruction Emit_TblInitN()
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.TblInitN
			});
		}

		// Token: 0x0600602C RID: 24620 RVA: 0x0026D9E6 File Offset: 0x0026BBE6
		public Instruction Emit_TblInitI(bool lastpos)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.TblInitI,
				NumVal = (lastpos ? 1 : 0)
			});
		}

		// Token: 0x0600602D RID: 24621 RVA: 0x0026DA10 File Offset: 0x0026BC10
		public Instruction Emit_Index(DynValue index = null, bool isNameIndex = false, bool isExpList = false)
		{
			OpCode opCode;
			if (isNameIndex)
			{
				opCode = OpCode.IndexN;
			}
			else if (isExpList)
			{
				opCode = OpCode.IndexL;
			}
			else
			{
				opCode = OpCode.Index;
			}
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = opCode,
				Value = index
			});
		}

		// Token: 0x0600602E RID: 24622 RVA: 0x0026DA50 File Offset: 0x0026BC50
		public Instruction Emit_IndexSet(int stackofs, int tupleidx, DynValue index = null, bool isNameIndex = false, bool isExpList = false)
		{
			OpCode opCode;
			if (isNameIndex)
			{
				opCode = OpCode.IndexSetN;
			}
			else if (isExpList)
			{
				opCode = OpCode.IndexSetL;
			}
			else
			{
				opCode = OpCode.IndexSet;
			}
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = opCode,
				NumVal = stackofs,
				NumVal2 = tupleidx,
				Value = index
			});
		}

		// Token: 0x0600602F RID: 24623 RVA: 0x0026DA9F File Offset: 0x0026BC9F
		public Instruction Emit_Copy(int numval)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Copy,
				NumVal = numval
			});
		}

		// Token: 0x06006030 RID: 24624 RVA: 0x0026DAC0 File Offset: 0x0026BCC0
		public Instruction Emit_Swap(int p1, int p2)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Swap,
				NumVal = p1,
				NumVal2 = p2
			});
		}

		// Token: 0x040054C1 RID: 21697
		public List<Instruction> Code = new List<Instruction>();

		// Token: 0x040054C3 RID: 21699
		private List<SourceRef> m_SourceRefStack = new List<SourceRef>();

		// Token: 0x040054C4 RID: 21700
		private SourceRef m_CurrentSourceRef;

		// Token: 0x040054C5 RID: 21701
		internal LoopTracker LoopTracker = new LoopTracker();

		// Token: 0x02001684 RID: 5764
		private class SourceCodeStackGuard : IDisposable
		{
			// Token: 0x06008752 RID: 34642 RVA: 0x002E6E44 File Offset: 0x002E5044
			public SourceCodeStackGuard(SourceRef sref, ByteCode bc)
			{
				this.m_Bc = bc;
				this.m_Bc.PushSourceRef(sref);
			}

			// Token: 0x06008753 RID: 34643 RVA: 0x002E6E5F File Offset: 0x002E505F
			public void Dispose()
			{
				this.m_Bc.PopSourceRef();
			}

			// Token: 0x040072D8 RID: 29400
			private ByteCode m_Bc;
		}
	}
}
