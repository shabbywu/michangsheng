﻿using System;
using System.Collections.Generic;
using System.Linq;
using MoonSharp.Interpreter.Execution.Scopes;
using MoonSharp.Interpreter.Tree.Statements;

namespace MoonSharp.Interpreter.Execution
{
	// Token: 0x02000D49 RID: 3401
	internal class BuildTimeScope
	{
		// Token: 0x06005FD2 RID: 24530 RVA: 0x0026CEAA File Offset: 0x0026B0AA
		public void PushFunction(IClosureBuilder closureBuilder, bool hasVarArgs)
		{
			this.m_ClosureBuilders.Add(closureBuilder);
			this.m_Frames.Add(new BuildTimeScopeFrame(hasVarArgs));
		}

		// Token: 0x06005FD3 RID: 24531 RVA: 0x0026CEC9 File Offset: 0x0026B0C9
		public void PushBlock()
		{
			this.m_Frames.Last<BuildTimeScopeFrame>().PushBlock();
		}

		// Token: 0x06005FD4 RID: 24532 RVA: 0x0026CEDB File Offset: 0x0026B0DB
		public RuntimeScopeBlock PopBlock()
		{
			return this.m_Frames.Last<BuildTimeScopeFrame>().PopBlock();
		}

		// Token: 0x06005FD5 RID: 24533 RVA: 0x0026CEF0 File Offset: 0x0026B0F0
		public RuntimeScopeFrame PopFunction()
		{
			BuildTimeScopeFrame buildTimeScopeFrame = this.m_Frames.Last<BuildTimeScopeFrame>();
			buildTimeScopeFrame.ResolveLRefs();
			this.m_Frames.RemoveAt(this.m_Frames.Count - 1);
			this.m_ClosureBuilders.RemoveAt(this.m_ClosureBuilders.Count - 1);
			return buildTimeScopeFrame.GetRuntimeFrameData();
		}

		// Token: 0x06005FD6 RID: 24534 RVA: 0x0026CF44 File Offset: 0x0026B144
		public SymbolRef Find(string name)
		{
			SymbolRef symbolRef = this.m_Frames.Last<BuildTimeScopeFrame>().Find(name);
			if (symbolRef != null)
			{
				return symbolRef;
			}
			for (int i = this.m_Frames.Count - 2; i >= 0; i--)
			{
				SymbolRef symbolRef2 = this.m_Frames[i].Find(name);
				if (symbolRef2 != null)
				{
					symbolRef2 = this.CreateUpValue(this, symbolRef2, i, this.m_Frames.Count - 2);
					if (symbolRef2 != null)
					{
						return symbolRef2;
					}
				}
			}
			return this.CreateGlobalReference(name);
		}

		// Token: 0x06005FD7 RID: 24535 RVA: 0x0026CFBC File Offset: 0x0026B1BC
		public SymbolRef CreateGlobalReference(string name)
		{
			if (name == "_ENV")
			{
				throw new InternalErrorException("_ENV passed in CreateGlobalReference");
			}
			SymbolRef envSymbol = this.Find("_ENV");
			return SymbolRef.Global(name, envSymbol);
		}

		// Token: 0x06005FD8 RID: 24536 RVA: 0x0026CFF4 File Offset: 0x0026B1F4
		public void ForceEnvUpValue()
		{
			this.Find("_ENV");
		}

		// Token: 0x06005FD9 RID: 24537 RVA: 0x0026D004 File Offset: 0x0026B204
		private SymbolRef CreateUpValue(BuildTimeScope buildTimeScope, SymbolRef symb, int closuredFrame, int currentFrame)
		{
			if (closuredFrame == currentFrame)
			{
				return this.m_ClosureBuilders[currentFrame + 1].CreateUpvalue(this, symb);
			}
			SymbolRef symbol = this.CreateUpValue(buildTimeScope, symb, closuredFrame, currentFrame - 1);
			return this.m_ClosureBuilders[currentFrame + 1].CreateUpvalue(this, symbol);
		}

		// Token: 0x06005FDA RID: 24538 RVA: 0x0026D051 File Offset: 0x0026B251
		public SymbolRef DefineLocal(string name)
		{
			return this.m_Frames.Last<BuildTimeScopeFrame>().DefineLocal(name);
		}

		// Token: 0x06005FDB RID: 24539 RVA: 0x0026D064 File Offset: 0x0026B264
		public SymbolRef TryDefineLocal(string name)
		{
			return this.m_Frames.Last<BuildTimeScopeFrame>().TryDefineLocal(name);
		}

		// Token: 0x06005FDC RID: 24540 RVA: 0x0026D077 File Offset: 0x0026B277
		public bool CurrentFunctionHasVarArgs()
		{
			return this.m_Frames.Last<BuildTimeScopeFrame>().HasVarArgs;
		}

		// Token: 0x06005FDD RID: 24541 RVA: 0x0026D089 File Offset: 0x0026B289
		internal void DefineLabel(LabelStatement label)
		{
			this.m_Frames.Last<BuildTimeScopeFrame>().DefineLabel(label);
		}

		// Token: 0x06005FDE RID: 24542 RVA: 0x0026D09C File Offset: 0x0026B29C
		internal void RegisterGoto(GotoStatement gotostat)
		{
			this.m_Frames.Last<BuildTimeScopeFrame>().RegisterGoto(gotostat);
		}

		// Token: 0x040054B2 RID: 21682
		private List<BuildTimeScopeFrame> m_Frames = new List<BuildTimeScopeFrame>();

		// Token: 0x040054B3 RID: 21683
		private List<IClosureBuilder> m_ClosureBuilders = new List<IClosureBuilder>();
	}
}
