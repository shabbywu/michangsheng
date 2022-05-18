using System;

namespace MoonSharp.Interpreter.REPL
{
	// Token: 0x020010CF RID: 4303
	public class ReplInterpreter
	{
		// Token: 0x060067CC RID: 26572 RVA: 0x000475F2 File Offset: 0x000457F2
		public ReplInterpreter(Script script)
		{
			this.m_Script = script;
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x060067CD RID: 26573 RVA: 0x0004760C File Offset: 0x0004580C
		// (set) Token: 0x060067CE RID: 26574 RVA: 0x00047614 File Offset: 0x00045814
		public bool HandleDynamicExprs { get; set; }

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x060067CF RID: 26575 RVA: 0x0004761D File Offset: 0x0004581D
		// (set) Token: 0x060067D0 RID: 26576 RVA: 0x00047625 File Offset: 0x00045825
		public bool HandleClassicExprsSyntax { get; set; }

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x060067D1 RID: 26577 RVA: 0x0004762E File Offset: 0x0004582E
		public virtual bool HasPendingCommand
		{
			get
			{
				return this.m_CurrentCommand.Length > 0;
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x060067D2 RID: 26578 RVA: 0x0004763E File Offset: 0x0004583E
		public virtual string CurrentPendingCommand
		{
			get
			{
				return this.m_CurrentCommand;
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x060067D3 RID: 26579 RVA: 0x00047646 File Offset: 0x00045846
		public virtual string ClassicPrompt
		{
			get
			{
				if (!this.HasPendingCommand)
				{
					return ">";
				}
				return ">>";
			}
		}

		// Token: 0x060067D4 RID: 26580 RVA: 0x0028AB2C File Offset: 0x00288D2C
		public virtual DynValue Evaluate(string input)
		{
			bool flag = !this.HasPendingCommand;
			bool flag2 = input == "";
			this.m_CurrentCommand += input;
			if (this.m_CurrentCommand.Length == 0)
			{
				return DynValue.Void;
			}
			this.m_CurrentCommand += "\n";
			DynValue result;
			try
			{
				if (flag && this.HandleClassicExprsSyntax && this.m_CurrentCommand.StartsWith("="))
				{
					this.m_CurrentCommand = "return " + this.m_CurrentCommand.Substring(1);
				}
				DynValue dynValue;
				if (flag && this.HandleDynamicExprs && this.m_CurrentCommand.StartsWith("?"))
				{
					string code = this.m_CurrentCommand.Substring(1);
					dynValue = this.m_Script.CreateDynamicExpression(code).Evaluate(null);
				}
				else
				{
					DynValue function = this.m_Script.LoadString(this.m_CurrentCommand, null, "stdin");
					dynValue = this.m_Script.Call(function);
				}
				this.m_CurrentCommand = "";
				result = dynValue;
			}
			catch (SyntaxErrorException ex)
			{
				if (flag2 || !ex.IsPrematureStreamTermination)
				{
					this.m_CurrentCommand = "";
					ex.Rethrow();
					throw;
				}
				result = null;
			}
			catch (ScriptRuntimeException ex2)
			{
				this.m_CurrentCommand = "";
				ex2.Rethrow();
				throw;
			}
			catch (Exception)
			{
				this.m_CurrentCommand = "";
				throw;
			}
			return result;
		}

		// Token: 0x04005FC3 RID: 24515
		private Script m_Script;

		// Token: 0x04005FC4 RID: 24516
		private string m_CurrentCommand = string.Empty;
	}
}
