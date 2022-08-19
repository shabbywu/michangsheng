using System;

namespace MoonSharp.Interpreter.REPL
{
	// Token: 0x02000CF2 RID: 3314
	public class ReplInterpreter
	{
		// Token: 0x06005CB6 RID: 23734 RVA: 0x002618EC File Offset: 0x0025FAEC
		public ReplInterpreter(Script script)
		{
			this.m_Script = script;
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06005CB7 RID: 23735 RVA: 0x00261906 File Offset: 0x0025FB06
		// (set) Token: 0x06005CB8 RID: 23736 RVA: 0x0026190E File Offset: 0x0025FB0E
		public bool HandleDynamicExprs { get; set; }

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06005CB9 RID: 23737 RVA: 0x00261917 File Offset: 0x0025FB17
		// (set) Token: 0x06005CBA RID: 23738 RVA: 0x0026191F File Offset: 0x0025FB1F
		public bool HandleClassicExprsSyntax { get; set; }

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06005CBB RID: 23739 RVA: 0x00261928 File Offset: 0x0025FB28
		public virtual bool HasPendingCommand
		{
			get
			{
				return this.m_CurrentCommand.Length > 0;
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06005CBC RID: 23740 RVA: 0x00261938 File Offset: 0x0025FB38
		public virtual string CurrentPendingCommand
		{
			get
			{
				return this.m_CurrentCommand;
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06005CBD RID: 23741 RVA: 0x00261940 File Offset: 0x0025FB40
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

		// Token: 0x06005CBE RID: 23742 RVA: 0x00261958 File Offset: 0x0025FB58
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

		// Token: 0x040053BF RID: 21439
		private Script m_Script;

		// Token: 0x040053C0 RID: 21440
		private string m_CurrentCommand = string.Empty;
	}
}
