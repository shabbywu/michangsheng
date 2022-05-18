using System;
using System.Collections.Generic;
using System.Linq;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;
using MoonSharp.Interpreter.Tree.Expressions;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x020010AB RID: 4267
	internal class ForEachLoopStatement : Statement
	{
		// Token: 0x06006725 RID: 26405 RVA: 0x00287290 File Offset: 0x00285490
		public ForEachLoopStatement(ScriptLoadingContext lcontext, Token firstNameToken, Token forToken) : base(lcontext)
		{
			List<string> list = new List<string>();
			list.Add(firstNameToken.Text);
			while (lcontext.Lexer.Current.Type == TokenType.Comma)
			{
				lcontext.Lexer.Next();
				Token token = NodeBase.CheckTokenType(lcontext, TokenType.Name);
				list.Add(token.Text);
			}
			NodeBase.CheckTokenType(lcontext, TokenType.In);
			this.m_RValues = new ExprListExpression(Expression.ExprList(lcontext), lcontext);
			lcontext.Scope.PushBlock();
			this.m_Names = (from n in list
			select lcontext.Scope.TryDefineLocal(n)).ToArray<SymbolRef>();
			this.m_NameExps = (from s in this.m_Names
			select new SymbolRefExpression(lcontext, s)).Cast<IVariable>().ToArray<IVariable>();
			this.m_RefFor = forToken.GetSourceRef(NodeBase.CheckTokenType(lcontext, TokenType.Do), true);
			this.m_Block = new CompositeStatement(lcontext);
			this.m_RefEnd = NodeBase.CheckTokenType(lcontext, TokenType.End).GetSourceRef(true);
			this.m_StackFrame = lcontext.Scope.PopBlock();
			lcontext.Source.Refs.Add(this.m_RefFor);
			lcontext.Source.Refs.Add(this.m_RefEnd);
		}

		// Token: 0x06006726 RID: 26406 RVA: 0x00287418 File Offset: 0x00285618
		public override void Compile(ByteCode bc)
		{
			bc.PushSourceRef(this.m_RefFor);
			Loop loop = new Loop
			{
				Scope = this.m_StackFrame
			};
			bc.LoopTracker.Loops.Push(loop);
			this.m_RValues.Compile(bc);
			bc.Emit_IterPrep();
			int jumpPointForNextInstruction = bc.GetJumpPointForNextInstruction();
			bc.Emit_Enter(this.m_StackFrame);
			bc.Emit_ExpTuple(0);
			bc.Emit_Call(2, "for..in");
			for (int i = 0; i < this.m_NameExps.Length; i++)
			{
				this.m_NameExps[i].CompileAssignment(bc, 0, i);
			}
			bc.Emit_Pop(1);
			bc.Emit_Load(this.m_Names[0]);
			bc.Emit_IterUpd();
			Instruction instruction = bc.Emit_Jump(OpCode.JNil, -1, 0);
			this.m_Block.Compile(bc);
			bc.PopSourceRef();
			bc.PushSourceRef(this.m_RefEnd);
			bc.Emit_Leave(this.m_StackFrame);
			bc.Emit_Jump(OpCode.Jump, jumpPointForNextInstruction, 0);
			bc.LoopTracker.Loops.Pop();
			int jumpPointForNextInstruction2 = bc.GetJumpPointForNextInstruction();
			bc.Emit_Leave(this.m_StackFrame);
			int jumpPointForNextInstruction3 = bc.GetJumpPointForNextInstruction();
			bc.Emit_Pop(1);
			foreach (Instruction instruction2 in loop.BreakJumps)
			{
				instruction2.NumVal = jumpPointForNextInstruction3;
			}
			instruction.NumVal = jumpPointForNextInstruction2;
			bc.PopSourceRef();
		}

		// Token: 0x04005F3D RID: 24381
		private RuntimeScopeBlock m_StackFrame;

		// Token: 0x04005F3E RID: 24382
		private SymbolRef[] m_Names;

		// Token: 0x04005F3F RID: 24383
		private IVariable[] m_NameExps;

		// Token: 0x04005F40 RID: 24384
		private Expression m_RValues;

		// Token: 0x04005F41 RID: 24385
		private Statement m_Block;

		// Token: 0x04005F42 RID: 24386
		private SourceRef m_RefFor;

		// Token: 0x04005F43 RID: 24387
		private SourceRef m_RefEnd;
	}
}
