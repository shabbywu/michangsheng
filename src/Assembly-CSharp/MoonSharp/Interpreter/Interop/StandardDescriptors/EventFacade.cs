using System;

namespace MoonSharp.Interpreter.Interop.StandardDescriptors
{
	// Token: 0x02001133 RID: 4403
	internal class EventFacade : IUserDataType
	{
		// Token: 0x06006A84 RID: 27268 RVA: 0x000489F7 File Offset: 0x00046BF7
		public EventFacade(EventMemberDescriptor parent, object obj)
		{
			this.m_Object = obj;
			this.m_AddCallback = new Func<object, ScriptExecutionContext, CallbackArguments, DynValue>(parent.AddCallback);
			this.m_RemoveCallback = new Func<object, ScriptExecutionContext, CallbackArguments, DynValue>(parent.RemoveCallback);
		}

		// Token: 0x06006A85 RID: 27269 RVA: 0x00048A2A File Offset: 0x00046C2A
		public EventFacade(Func<object, ScriptExecutionContext, CallbackArguments, DynValue> addCallback, Func<object, ScriptExecutionContext, CallbackArguments, DynValue> removeCallback, object obj)
		{
			this.m_Object = obj;
			this.m_AddCallback = addCallback;
			this.m_RemoveCallback = removeCallback;
		}

		// Token: 0x06006A86 RID: 27270 RVA: 0x00290F10 File Offset: 0x0028F110
		public DynValue Index(Script script, DynValue index, bool isDirectIndexing)
		{
			if (index.Type == DataType.String)
			{
				if (index.String == "add")
				{
					return DynValue.NewCallback((ScriptExecutionContext c, CallbackArguments a) => this.m_AddCallback(this.m_Object, c, a), null);
				}
				if (index.String == "remove")
				{
					return DynValue.NewCallback((ScriptExecutionContext c, CallbackArguments a) => this.m_RemoveCallback(this.m_Object, c, a), null);
				}
			}
			throw new ScriptRuntimeException("Events only support add and remove methods");
		}

		// Token: 0x06006A87 RID: 27271 RVA: 0x00048A47 File Offset: 0x00046C47
		public bool SetIndex(Script script, DynValue index, DynValue value, bool isDirectIndexing)
		{
			throw new ScriptRuntimeException("Events do not have settable fields");
		}

		// Token: 0x06006A88 RID: 27272 RVA: 0x0000B171 File Offset: 0x00009371
		public DynValue MetaIndex(Script script, string metaname)
		{
			return null;
		}

		// Token: 0x040060C1 RID: 24769
		private Func<object, ScriptExecutionContext, CallbackArguments, DynValue> m_AddCallback;

		// Token: 0x040060C2 RID: 24770
		private Func<object, ScriptExecutionContext, CallbackArguments, DynValue> m_RemoveCallback;

		// Token: 0x040060C3 RID: 24771
		private object m_Object;
	}
}
