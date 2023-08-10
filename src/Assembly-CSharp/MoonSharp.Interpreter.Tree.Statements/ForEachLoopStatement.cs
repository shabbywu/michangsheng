using System.Collections.Generic;
using System.Linq;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;
using MoonSharp.Interpreter.Tree.Expressions;

namespace MoonSharp.Interpreter.Tree.Statements;

internal class ForEachLoopStatement : Statement
{
	private RuntimeScopeBlock m_StackFrame;

	private SymbolRef[] m_Names;

	private IVariable[] m_NameExps;

	private Expression m_RValues;

	private Statement m_Block;

	private SourceRef m_RefFor;

	private SourceRef m_RefEnd;

	public ForEachLoopStatement(ScriptLoadingContext lcontext, Token firstNameToken, Token forToken)
		: base(lcontext)
	{
		List<string> list = new List<string> { firstNameToken.Text };
		while (lcontext.Lexer.Current.Type == TokenType.Comma)
		{
			lcontext.Lexer.Next();
			Token token = NodeBase.CheckTokenType(lcontext, TokenType.Name);
			list.Add(token.Text);
		}
		NodeBase.CheckTokenType(lcontext, TokenType.In);
		m_RValues = new ExprListExpression(Expression.ExprList(lcontext), lcontext);
		lcontext.Scope.PushBlock();
		m_Names = list.Select((string n) => lcontext.Scope.TryDefineLocal(n)).ToArray();
		m_NameExps = m_Names.Select((SymbolRef s) => new SymbolRefExpression(lcontext, s)).Cast<IVariable>().ToArray();
		m_RefFor = forToken.GetSourceRef(NodeBase.CheckTokenType(lcontext, TokenType.Do));
		m_Block = new CompositeStatement(lcontext);
		m_RefEnd = NodeBase.CheckTokenType(lcontext, TokenType.End).GetSourceRef();
		m_StackFrame = lcontext.Scope.PopBlock();
		lcontext.Source.Refs.Add(m_RefFor);
		lcontext.Source.Refs.Add(m_RefEnd);
	}

	public override void Compile(ByteCode bc)
	{
		bc.PushSourceRef(m_RefFor);
		Loop loop = new Loop
		{
			Scope = m_StackFrame
		};
		bc.LoopTracker.Loops.Push(loop);
		m_RValues.Compile(bc);
		bc.Emit_IterPrep();
		int jumpPointForNextInstruction = bc.GetJumpPointForNextInstruction();
		bc.Emit_Enter(m_StackFrame);
		bc.Emit_ExpTuple(0);
		bc.Emit_Call(2, "for..in");
		for (int i = 0; i < m_NameExps.Length; i++)
		{
			m_NameExps[i].CompileAssignment(bc, 0, i);
		}
		bc.Emit_Pop();
		bc.Emit_Load(m_Names[0]);
		bc.Emit_IterUpd();
		Instruction instruction = bc.Emit_Jump(OpCode.JNil, -1);
		m_Block.Compile(bc);
		bc.PopSourceRef();
		bc.PushSourceRef(m_RefEnd);
		bc.Emit_Leave(m_StackFrame);
		bc.Emit_Jump(OpCode.Jump, jumpPointForNextInstruction);
		bc.LoopTracker.Loops.Pop();
		int jumpPointForNextInstruction2 = bc.GetJumpPointForNextInstruction();
		bc.Emit_Leave(m_StackFrame);
		int jumpPointForNextInstruction3 = bc.GetJumpPointForNextInstruction();
		bc.Emit_Pop();
		foreach (Instruction breakJump in loop.BreakJumps)
		{
			breakJump.NumVal = jumpPointForNextInstruction3;
		}
		instruction.NumVal = jumpPointForNextInstruction2;
		bc.PopSourceRef();
	}
}
