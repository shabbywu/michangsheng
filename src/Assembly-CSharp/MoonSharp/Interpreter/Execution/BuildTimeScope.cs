using System;
using System.Collections.Generic;
using System.Linq;
using MoonSharp.Interpreter.Execution.Scopes;
using MoonSharp.Interpreter.Tree.Statements;

namespace MoonSharp.Interpreter.Execution
{
	// Token: 0x02001157 RID: 4439
	internal class BuildTimeScope
	{
		// Token: 0x06006BB6 RID: 27574 RVA: 0x000495AB File Offset: 0x000477AB
		public void PushFunction(IClosureBuilder closureBuilder, bool hasVarArgs)
		{
			this.m_ClosureBuilders.Add(closureBuilder);
			this.m_Frames.Add(new BuildTimeScopeFrame(hasVarArgs));
		}

		// Token: 0x06006BB7 RID: 27575 RVA: 0x000495CA File Offset: 0x000477CA
		public void PushBlock()
		{
			this.m_Frames.Last<BuildTimeScopeFrame>().PushBlock();
		}

		// Token: 0x06006BB8 RID: 27576 RVA: 0x000495DC File Offset: 0x000477DC
		public RuntimeScopeBlock PopBlock()
		{
			return this.m_Frames.Last<BuildTimeScopeFrame>().PopBlock();
		}

		// Token: 0x06006BB9 RID: 27577 RVA: 0x00294A1C File Offset: 0x00292C1C
		public RuntimeScopeFrame PopFunction()
		{
			BuildTimeScopeFrame buildTimeScopeFrame = this.m_Frames.Last<BuildTimeScopeFrame>();
			buildTimeScopeFrame.ResolveLRefs();
			this.m_Frames.RemoveAt(this.m_Frames.Count - 1);
			this.m_ClosureBuilders.RemoveAt(this.m_ClosureBuilders.Count - 1);
			return buildTimeScopeFrame.GetRuntimeFrameData();
		}

		// Token: 0x06006BBA RID: 27578 RVA: 0x00294A70 File Offset: 0x00292C70
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

		// Token: 0x06006BBB RID: 27579 RVA: 0x00294AE8 File Offset: 0x00292CE8
		public SymbolRef CreateGlobalReference(string name)
		{
			if (name == "_ENV")
			{
				throw new InternalErrorException("_ENV passed in CreateGlobalReference");
			}
			SymbolRef envSymbol = this.Find("_ENV");
			return SymbolRef.Global(name, envSymbol);
		}

		// Token: 0x06006BBC RID: 27580 RVA: 0x000495EE File Offset: 0x000477EE
		public void ForceEnvUpValue()
		{
			this.Find("_ENV");
		}

		// Token: 0x06006BBD RID: 27581 RVA: 0x00294B20 File Offset: 0x00292D20
		private SymbolRef CreateUpValue(BuildTimeScope buildTimeScope, SymbolRef symb, int closuredFrame, int currentFrame)
		{
			if (closuredFrame == currentFrame)
			{
				return this.m_ClosureBuilders[currentFrame + 1].CreateUpvalue(this, symb);
			}
			SymbolRef symbol = this.CreateUpValue(buildTimeScope, symb, closuredFrame, currentFrame - 1);
			return this.m_ClosureBuilders[currentFrame + 1].CreateUpvalue(this, symbol);
		}

		// Token: 0x06006BBE RID: 27582 RVA: 0x000495FC File Offset: 0x000477FC
		public SymbolRef DefineLocal(string name)
		{
			return this.m_Frames.Last<BuildTimeScopeFrame>().DefineLocal(name);
		}

		// Token: 0x06006BBF RID: 27583 RVA: 0x0004960F File Offset: 0x0004780F
		public SymbolRef TryDefineLocal(string name)
		{
			return this.m_Frames.Last<BuildTimeScopeFrame>().TryDefineLocal(name);
		}

		// Token: 0x06006BC0 RID: 27584 RVA: 0x00049622 File Offset: 0x00047822
		public bool CurrentFunctionHasVarArgs()
		{
			return this.m_Frames.Last<BuildTimeScopeFrame>().HasVarArgs;
		}

		// Token: 0x06006BC1 RID: 27585 RVA: 0x00049634 File Offset: 0x00047834
		internal void DefineLabel(LabelStatement label)
		{
			this.m_Frames.Last<BuildTimeScopeFrame>().DefineLabel(label);
		}

		// Token: 0x06006BC2 RID: 27586 RVA: 0x00049647 File Offset: 0x00047847
		internal void RegisterGoto(GotoStatement gotostat)
		{
			this.m_Frames.Last<BuildTimeScopeFrame>().RegisterGoto(gotostat);
		}

		// Token: 0x0400612B RID: 24875
		private List<BuildTimeScopeFrame> m_Frames = new List<BuildTimeScopeFrame>();

		// Token: 0x0400612C RID: 24876
		private List<IClosureBuilder> m_ClosureBuilders = new List<IClosureBuilder>();
	}
}
