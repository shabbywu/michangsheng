using System;
using System.Collections.Generic;

namespace MoonSharp.Interpreter.Interop.LuaStateInterop
{
	// Token: 0x02000D37 RID: 3383
	public class LuaState
	{
		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06005F4D RID: 24397 RVA: 0x00269643 File Offset: 0x00267843
		// (set) Token: 0x06005F4E RID: 24398 RVA: 0x0026964B File Offset: 0x0026784B
		public ScriptExecutionContext ExecutionContext { get; private set; }

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06005F4F RID: 24399 RVA: 0x00269654 File Offset: 0x00267854
		// (set) Token: 0x06005F50 RID: 24400 RVA: 0x0026965C File Offset: 0x0026785C
		public string FunctionName { get; private set; }

		// Token: 0x06005F51 RID: 24401 RVA: 0x00269668 File Offset: 0x00267868
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

		// Token: 0x06005F52 RID: 24402 RVA: 0x002696B9 File Offset: 0x002678B9
		public DynValue Top(int pos = 0)
		{
			return this.m_Stack[this.m_Stack.Count - 1 - pos];
		}

		// Token: 0x06005F53 RID: 24403 RVA: 0x002696D5 File Offset: 0x002678D5
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

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06005F54 RID: 24404 RVA: 0x0026970E File Offset: 0x0026790E
		public int Count
		{
			get
			{
				return this.m_Stack.Count;
			}
		}

		// Token: 0x06005F55 RID: 24405 RVA: 0x0026971B File Offset: 0x0026791B
		public void Push(DynValue v)
		{
			this.m_Stack.Add(v);
		}

		// Token: 0x06005F56 RID: 24406 RVA: 0x00269729 File Offset: 0x00267929
		public DynValue Pop()
		{
			DynValue result = this.Top(0);
			this.m_Stack.RemoveAt(this.m_Stack.Count - 1);
			return result;
		}

		// Token: 0x06005F57 RID: 24407 RVA: 0x0026974C File Offset: 0x0026794C
		public DynValue[] GetTopArray(int num)
		{
			DynValue[] array = new DynValue[num];
			for (int i = 0; i < num; i++)
			{
				array[num - i - 1] = this.Top(i);
			}
			return array;
		}

		// Token: 0x06005F58 RID: 24408 RVA: 0x0026977B File Offset: 0x0026797B
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

		// Token: 0x06005F59 RID: 24409 RVA: 0x002697A0 File Offset: 0x002679A0
		public void Discard(int nargs)
		{
			for (int i = 0; i < nargs; i++)
			{
				this.m_Stack.RemoveAt(this.m_Stack.Count - 1);
			}
		}

		// Token: 0x04005477 RID: 21623
		private List<DynValue> m_Stack;
	}
}
