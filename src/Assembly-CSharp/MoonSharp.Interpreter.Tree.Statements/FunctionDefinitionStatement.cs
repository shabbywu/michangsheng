using System.Collections.Generic;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;
using MoonSharp.Interpreter.Tree.Expressions;

namespace MoonSharp.Interpreter.Tree.Statements;

internal class FunctionDefinitionStatement : Statement
{
	private SymbolRef m_FuncSymbol;

	private SourceRef m_SourceRef;

	private bool m_Local;

	private bool m_IsMethodCallingConvention;

	private string m_MethodName;

	private string m_FriendlyName;

	private List<string> m_TableAccessors;

	private FunctionDefinitionExpression m_FuncDef;

	public FunctionDefinitionStatement(ScriptLoadingContext lcontext, bool local, Token localToken)
		: base(lcontext)
	{
		Token token = NodeBase.CheckTokenType(lcontext, TokenType.Function);
		token = localToken ?? token;
		m_Local = local;
		if (m_Local)
		{
			Token token2 = NodeBase.CheckTokenType(lcontext, TokenType.Name);
			m_FuncSymbol = lcontext.Scope.TryDefineLocal(token2.Text);
			m_FriendlyName = $"{token2.Text} (local)";
			m_SourceRef = token.GetSourceRef(token2);
		}
		else
		{
			Token token3 = NodeBase.CheckTokenType(lcontext, TokenType.Name);
			string text = token3.Text;
			m_SourceRef = token.GetSourceRef(token3);
			m_FuncSymbol = lcontext.Scope.Find(text);
			m_FriendlyName = text;
			if (lcontext.Lexer.Current.Type != TokenType.Brk_Open_Round)
			{
				m_TableAccessors = new List<string>();
				while (lcontext.Lexer.Current.Type != TokenType.Brk_Open_Round)
				{
					Token current = lcontext.Lexer.Current;
					if (current.Type != TokenType.Colon && current.Type != TokenType.Dot)
					{
						NodeBase.UnexpectedTokenType(current);
					}
					lcontext.Lexer.Next();
					Token token4 = NodeBase.CheckTokenType(lcontext, TokenType.Name);
					m_FriendlyName = m_FriendlyName + current.Text + token4.Text;
					m_SourceRef = token.GetSourceRef(token4);
					if (current.Type == TokenType.Colon)
					{
						m_MethodName = token4.Text;
						m_IsMethodCallingConvention = true;
						break;
					}
					m_TableAccessors.Add(token4.Text);
				}
				if (m_MethodName == null && m_TableAccessors.Count > 0)
				{
					m_MethodName = m_TableAccessors[m_TableAccessors.Count - 1];
					m_TableAccessors.RemoveAt(m_TableAccessors.Count - 1);
				}
			}
		}
		m_FuncDef = new FunctionDefinitionExpression(lcontext, m_IsMethodCallingConvention, isLambda: false);
		lcontext.Source.Refs.Add(m_SourceRef);
	}

	public override void Compile(ByteCode bc)
	{
		using (bc.EnterSource(m_SourceRef))
		{
			if (m_Local)
			{
				bc.Emit_Literal(DynValue.Nil);
				bc.Emit_Store(m_FuncSymbol, 0, 0);
				m_FuncDef.Compile(bc, () => SetFunction(bc, 2), m_FriendlyName);
			}
			else if (m_MethodName == null)
			{
				m_FuncDef.Compile(bc, () => SetFunction(bc, 1), m_FriendlyName);
			}
			else
			{
				m_FuncDef.Compile(bc, () => SetMethod(bc), m_FriendlyName);
			}
		}
	}

	private int SetMethod(ByteCode bc)
	{
		int num = 0;
		num += bc.Emit_Load(m_FuncSymbol);
		foreach (string tableAccessor in m_TableAccessors)
		{
			bc.Emit_Index(DynValue.NewString(tableAccessor), isNameIndex: true);
			num++;
		}
		bc.Emit_IndexSet(0, 0, DynValue.NewString(m_MethodName), isNameIndex: true);
		return 1 + num;
	}

	private int SetFunction(ByteCode bc, int numPop)
	{
		int num = bc.Emit_Store(m_FuncSymbol, 0, 0);
		bc.Emit_Pop(numPop);
		return num + 1;
	}
}
