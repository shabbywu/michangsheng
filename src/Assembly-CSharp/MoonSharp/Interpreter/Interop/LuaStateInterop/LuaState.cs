using System;
using System.Collections.Generic;

namespace MoonSharp.Interpreter.Interop.LuaStateInterop
{
	// Token: 0x0200113F RID: 4415
	public class LuaState
	{
		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06006B1F RID: 27423 RVA: 0x0004907E File Offset: 0x0004727E
		// (set) Token: 0x06006B20 RID: 27424 RVA: 0x00049086 File Offset: 0x00047286
		public ScriptExecutionContext ExecutionContext { get; private set; }

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06006B21 RID: 27425 RVA: 0x0004908F File Offset: 0x0004728F
		// (set) Token: 0x06006B22 RID: 27426 RVA: 0x00049097 File Offset: 0x00047297
		public string FunctionName { get; private set; }

		// Token: 0x06006B23 RID: 27427 RVA: 0x0029161C File Offset: 0x0028F81C
		internal LuaState(ScriptExecutionContext executionContext, CallbackArguments args, string functionName)
		{
			this.ExecutionContext = executionContext;
			this.m_Stack = new List<DynValue>(16);
			for (int i = 0; i < args.Count; i++)
			{
				this.m_Stack.Add(args[i]);
			}
			this.FunctionName = functionName;
		}

		// Token: 0x06006B24 RID: 27428 RVA: 0x000490A0 File Offset: 0x000472A0
		public DynValue Top(int pos = 0)
		{
			return this.m_Stack[this.m_Stack.Count - 1 - pos];
		}

		// Token: 0x06006B25 RID: 27429 RVA: 0x000490BC File Offset: 0x000472BC
		public DynValue At(int pos)
		{
			if (pos < 0)
			{
				pos = this.m_Stack.Count + pos + 1;
			}
			if (pos > this.m_Stack.Count)
			{
				return DynValue.Void;
			}
			return this.m_Stack[pos - 1];
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06006B26 RID: 27430 RVA: 0x000490F5 File Offset: 0x000472F5
		public int Count
		{
			get
			{
				return this.m_Stack.Count;
			}
		}

		// Token: 0x06006B27 RID: 27431 RVA: 0x00049102 File Offset: 0x00047302
		public void Push(DynValue v)
		{
			this.m_Stack.Add(v);
		}

		// Token: 0x06006B28 RID: 27432 RVA: 0x00049110 File Offset: 0x00047310
		public DynValue Pop()
		{
			DynValue result = this.Top(0);
			this.m_Stack.RemoveAt(this.m_Stack.Count - 1);
			return result;
		}

		// Token: 0x06006B29 RID: 27433 RVA: 0x00291670 File Offset: 0x0028F870
		public DynValue[] GetTopArray(int num)
		{
			DynValue[] array = new DynValue[num];
			for (int i = 0; i < num; i++)
			{
				array[num - i - 1] = this.Top(i);
			}
			return array;
		}

		// Token: 0x06006B2A RID: 27434 RVA: 0x00049131 File Offset: 0x00047331
		public DynValue GetReturnValue(int retvals)
		{
			if (retvals == 0)
			{
				return DynValue.Nil;
			}
			if (retvals == 1)
			{
				return this.Top(0);
			}
			return DynValue.NewTupleNested(this.GetTopArray(retvals));
		}

		// Token: 0x06006B2B RID: 27435 RVA: 0x002916A0 File Offset: 0x0028F8A0
		public void Discard(int nargs)
		{
			for (int i = 0; i < nargs; i++)
			{
				this.m_Stack.RemoveAt(this.m_Stack.Count - 1);
			}
		}

		// Token: 0x040060D9 RID: 24793
		private List<DynValue> m_Stack;
	}
}
