using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using MoonSharp.Interpreter.Debugging;

namespace MoonSharp.Interpreter.Execution.VM
{
	// Token: 0x02001160 RID: 4448
	internal class ByteCode : RefIdObject
	{
		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06006BEB RID: 27627 RVA: 0x00049836 File Offset: 0x00047A36
		// (set) Token: 0x06006BEC RID: 27628 RVA: 0x0004983E File Offset: 0x00047A3E
		public Script Script { get; private set; }

		// Token: 0x06006BED RID: 27629 RVA: 0x00049847 File Offset: 0x00047A47
		public ByteCode(Script script)
		{
			this.Script = script;
		}

		// Token: 0x06006BEE RID: 27630 RVA: 0x00049877 File Offset: 0x00047A77
		public IDisposable EnterSource(SourceRef sref)
		{
			return new ByteCode.SourceCodeStackGuard(sref, this);
		}

		// Token: 0x06006BEF RID: 27631 RVA: 0x00049880 File Offset: 0x00047A80
		public void PushSourceRef(SourceRef sref)
		{
			this.m_SourceRefStack.Add(sref);
			this.m_CurrentSourceRef = sref;
		}

		// Token: 0x06006BF0 RID: 27632 RVA: 0x00294B70 File Offset: 0x00292D70
		public void PopSourceRef()
		{
			this.m_SourceRefStack.RemoveAt(this.m_SourceRefStack.Count - 1);
			this.m_CurrentSourceRef = ((this.m_SourceRefStack.Count > 0) ? this.m_SourceRefStack[this.m_SourceRefStack.Count - 1] : null);
		}

		// Token: 0x06006BF1 RID: 27633 RVA: 0x00294BC4 File Offset: 0x00292DC4
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

		// Token: 0x06006BF2 RID: 27634 RVA: 0x00049895 File Offset: 0x00047A95
		public int GetJumpPointForNextInstruction()
		{
			return this.Code.Count;
		}

		// Token: 0x06006BF3 RID: 27635 RVA: 0x000498A2 File Offset: 0x00047AA2
		public int GetJumpPointForLastInstruction()
		{
			return this.Code.Count - 1;
		}

		// Token: 0x06006BF4 RID: 27636 RVA: 0x000498B1 File Offset: 0x00047AB1
		public Instruction GetLastInstruction()
		{
			return this.Code[this.Code.Count - 1];
		}

		// Token: 0x06006BF5 RID: 27637 RVA: 0x000498CB File Offset: 0x00047ACB
		private Instruction AppendInstruction(Instruction c)
		{
			this.Code.Add(c);
			return c;
		}

		// Token: 0x06006BF6 RID: 27638 RVA: 0x000498DA File Offset: 0x00047ADA
		public Instruction Emit_Nop(string comment)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Nop,
				Name = comment
			});
		}

		// Token: 0x06006BF7 RID: 27639 RVA: 0x000498FB File Offset: 0x00047AFB
		public Instruction Emit_Invalid(string type)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Invalid,
				Name = type
			});
		}

		// Token: 0x06006BF8 RID: 27640 RVA: 0x0004991D File Offset: 0x00047B1D
		public Instruction Emit_Pop(int num = 1)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Pop,
				NumVal = num
			});
		}

		// Token: 0x06006BF9 RID: 27641 RVA: 0x0004993E File Offset: 0x00047B3E
		public void Emit_Call(int argCount, string debugName)
		{
			this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Call,
				NumVal = argCount,
				Name = debugName
			});
		}

		// Token: 0x06006BFA RID: 27642 RVA: 0x00049968 File Offset: 0x00047B68
		public void Emit_ThisCall(int argCount, string debugName)
		{
			this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.ThisCall,
				NumVal = argCount,
				Name = debugName
			});
		}

		// Token: 0x06006BFB RID: 27643 RVA: 0x00049992 File Offset: 0x00047B92
		public Instruction Emit_Literal(DynValue value)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Literal,
				Value = value
			});
		}

		// Token: 0x06006BFC RID: 27644 RVA: 0x000499B3 File Offset: 0x00047BB3
		public Instruction Emit_Jump(OpCode jumpOpCode, int idx, int optPar = 0)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = jumpOpCode,
				NumVal = idx,
				NumVal2 = optPar
			});
		}

		// Token: 0x06006BFD RID: 27645 RVA: 0x000499DB File Offset: 0x00047BDB
		public Instruction Emit_MkTuple(int cnt)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.MkTuple,
				NumVal = cnt
			});
		}

		// Token: 0x06006BFE RID: 27646 RVA: 0x00294C48 File Offset: 0x00292E48
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

		// Token: 0x06006BFF RID: 27647 RVA: 0x000499FD File Offset: 0x00047BFD
		[Conditional("EMIT_DEBUG_OPS")]
		public void Emit_Debug(string str)
		{
			this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Debug,
				Name = str.Substring(0, Math.Min(32, str.Length))
			});
		}

		// Token: 0x06006C00 RID: 27648 RVA: 0x00049A32 File Offset: 0x00047C32
		public Instruction Emit_Enter(RuntimeScopeBlock runtimeScopeBlock)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Clean,
				NumVal = runtimeScopeBlock.From,
				NumVal2 = runtimeScopeBlock.ToInclusive
			});
		}

		// Token: 0x06006C01 RID: 27649 RVA: 0x00049A65 File Offset: 0x00047C65
		public Instruction Emit_Leave(RuntimeScopeBlock runtimeScopeBlock)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Clean,
				NumVal = runtimeScopeBlock.From,
				NumVal2 = runtimeScopeBlock.To
			});
		}

		// Token: 0x06006C02 RID: 27650 RVA: 0x00049A32 File Offset: 0x00047C32
		public Instruction Emit_Exit(RuntimeScopeBlock runtimeScopeBlock)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Clean,
				NumVal = runtimeScopeBlock.From,
				NumVal2 = runtimeScopeBlock.ToInclusive
			});
		}

		// Token: 0x06006C03 RID: 27651 RVA: 0x00049A98 File Offset: 0x00047C98
		public Instruction Emit_Clean(RuntimeScopeBlock runtimeScopeBlock)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Clean,
				NumVal = runtimeScopeBlock.To + 1,
				NumVal2 = runtimeScopeBlock.ToInclusive
			});
		}

		// Token: 0x06006C04 RID: 27652 RVA: 0x00049ACD File Offset: 0x00047CCD
		public Instruction Emit_Closure(SymbolRef[] symbols, int jmpnum)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Closure,
				SymbolList = symbols,
				NumVal = jmpnum
			});
		}

		// Token: 0x06006C05 RID: 27653 RVA: 0x00049AF5 File Offset: 0x00047CF5
		public Instruction Emit_Args(params SymbolRef[] symbols)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Args,
				SymbolList = symbols
			});
		}

		// Token: 0x06006C06 RID: 27654 RVA: 0x00049B17 File Offset: 0x00047D17
		public Instruction Emit_Ret(int retvals)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Ret,
				NumVal = retvals
			});
		}

		// Token: 0x06006C07 RID: 27655 RVA: 0x00049B39 File Offset: 0x00047D39
		public Instruction Emit_ToNum(int stage = 0)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.ToNum,
				NumVal = stage
			});
		}

		// Token: 0x06006C08 RID: 27656 RVA: 0x00049B5B File Offset: 0x00047D5B
		public Instruction Emit_Incr(int i)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Incr,
				NumVal = i
			});
		}

		// Token: 0x06006C09 RID: 27657 RVA: 0x00049B7D File Offset: 0x00047D7D
		public Instruction Emit_NewTable(bool shared)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.NewTable,
				NumVal = (shared ? 1 : 0)
			});
		}

		// Token: 0x06006C0A RID: 27658 RVA: 0x00049BA4 File Offset: 0x00047DA4
		public Instruction Emit_IterPrep()
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.IterPrep
			});
		}

		// Token: 0x06006C0B RID: 27659 RVA: 0x00049BBF File Offset: 0x00047DBF
		public Instruction Emit_ExpTuple(int stackOffset)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.ExpTuple,
				NumVal = stackOffset
			});
		}

		// Token: 0x06006C0C RID: 27660 RVA: 0x00049BE1 File Offset: 0x00047DE1
		public Instruction Emit_IterUpd()
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.IterUpd
			});
		}

		// Token: 0x06006C0D RID: 27661 RVA: 0x00049BFC File Offset: 0x00047DFC
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

		// Token: 0x06006C0E RID: 27662 RVA: 0x00294CB0 File Offset: 0x00292EB0
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

		// Token: 0x06006C0F RID: 27663 RVA: 0x00049C2C File Offset: 0x00047E2C
		public Instruction Emit_Scalar()
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Scalar
			});
		}

		// Token: 0x06006C10 RID: 27664 RVA: 0x00294D00 File Offset: 0x00292F00
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

		// Token: 0x06006C11 RID: 27665 RVA: 0x00294DC0 File Offset: 0x00292FC0
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

		// Token: 0x06006C12 RID: 27666 RVA: 0x00049C47 File Offset: 0x00047E47
		public Instruction Emit_TblInitN()
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.TblInitN
			});
		}

		// Token: 0x06006C13 RID: 27667 RVA: 0x00049C61 File Offset: 0x00047E61
		public Instruction Emit_TblInitI(bool lastpos)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.TblInitI,
				NumVal = (lastpos ? 1 : 0)
			});
		}

		// Token: 0x06006C14 RID: 27668 RVA: 0x00294EB0 File Offset: 0x002930B0
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

		// Token: 0x06006C15 RID: 27669 RVA: 0x00294EF0 File Offset: 0x002930F0
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

		// Token: 0x06006C16 RID: 27670 RVA: 0x00049C89 File Offset: 0x00047E89
		public Instruction Emit_Copy(int numval)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Copy,
				NumVal = numval
			});
		}

		// Token: 0x06006C17 RID: 27671 RVA: 0x00049CAA File Offset: 0x00047EAA
		public Instruction Emit_Swap(int p1, int p2)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Swap,
				NumVal = p1,
				NumVal2 = p2
			});
		}

		// Token: 0x0400613C RID: 24892
		public List<Instruction> Code = new List<Instruction>();

		// Token: 0x0400613E RID: 24894
		private List<SourceRef> m_SourceRefStack = new List<SourceRef>();

		// Token: 0x0400613F RID: 24895
		private SourceRef m_CurrentSourceRef;

		// Token: 0x04006140 RID: 24896
		internal LoopTracker LoopTracker = new LoopTracker();

		// Token: 0x02001161 RID: 4449
		private class SourceCodeStackGuard : IDisposable
		{
			// Token: 0x06006C18 RID: 27672 RVA: 0x00049CD2 File Offset: 0x00047ED2
			public SourceCodeStackGuard(SourceRef sref, ByteCode bc)
			{
				this.m_Bc = bc;
				this.m_Bc.PushSourceRef(sref);
			}

			// Token: 0x06006C19 RID: 27673 RVA: 0x00049CED File Offset: 0x00047EED
			public void Dispose()
			{
				this.m_Bc.PopSourceRef();
			}

			// Token: 0x04006141 RID: 24897
			private ByteCode m_Bc;
		}
	}
}
