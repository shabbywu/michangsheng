using System;

namespace MoonSharp.Interpreter.Interop.StandardDescriptors
{
	// Token: 0x02000D2B RID: 3371
	internal class EventFacade : IUserDataType
	{
		// Token: 0x06005EB2 RID: 24242 RVA: 0x0026889F File Offset: 0x00266A9F
		public EventFacade(EventMemberDescriptor parent, object obj)
		{
			this.m_Object = obj;
			this.m_AddCallback = new Func<object, ScriptExecutionContext, CallbackArguments, DynValue>(parent.AddCallback);
			this.m_RemoveCallback = new Func<object, ScriptExecutionContext, CallbackArguments, DynValue>(parent.RemoveCallback);
		}

		// Token: 0x06005EB3 RID: 24243 RVA: 0x002688D2 File Offset: 0x00266AD2
		public EventFacade(Func<object, ScriptExecutionContext, CallbackArguments, DynValue> addCallback, Func<object, ScriptExecutionContext, CallbackArguments, DynValue> removeCallback, object obj)
		{
			this.m_Object = obj;
			this.m_AddCallback = addCallback;
			this.m_RemoveCallback = removeCallback;
		}

		// Token: 0x06005EB4 RID: 24244 RVA: 0x002688F0 File Offset: 0x00266AF0
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

		// Token: 0x06005EB5 RID: 24245 RVA: 0x0026895A File Offset: 0x00266B5A
		public bool SetIndex(Script script, DynValue index, DynValue value, bool isDirectIndexing)
		{
			throw new ScriptRuntimeException("Events do not have settable fields");
		}

		// Token: 0x06005EB6 RID: 24246 RVA: 0x000306E7 File Offset: 0x0002E8E7
		public DynValue MetaIndex(Script script, string metaname)
		{
			return null;
		}

		// Token: 0x0400545F RID: 21599
		private Func<object, ScriptExecutionContext, CallbackArguments, DynValue> m_AddCallback;

		// Token: 0x04005460 RID: 21600
		private Func<object, ScriptExecutionContext, CallbackArguments, DynValue> m_RemoveCallback;

		// Token: 0x04005461 RID: 21601
		private object m_Object;
	}
}
