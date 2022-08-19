using System;
using System.Collections;
using MoonSharp.Interpreter.Interop.Converters;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000D0F RID: 3343
	internal class EnumerableWrapper : IUserDataType
	{
		// Token: 0x06005D8B RID: 23947 RVA: 0x00262EFD File Offset: 0x002610FD
		private EnumerableWrapper(Script script, IEnumerator enumerator)
		{
			this.m_Script = script;
			this.m_Enumerator = enumerator;
		}

		// Token: 0x06005D8C RID: 23948 RVA: 0x00262F1E File Offset: 0x0026111E
		public void Reset()
		{
			if (this.m_HasTurnOnce)
			{
				this.m_Enumerator.Reset();
			}
			this.m_HasTurnOnce = true;
		}

		// Token: 0x06005D8D RID: 23949 RVA: 0x00262F3C File Offset: 0x0026113C
		private DynValue GetNext(DynValue prev)
		{
			if (prev.IsNil())
			{
				this.Reset();
			}
			while (this.m_Enumerator.MoveNext())
			{
				DynValue dynValue = ClrToScriptConversions.ObjectToDynValue(this.m_Script, this.m_Enumerator.Current);
				if (!dynValue.IsNil())
				{
					return dynValue;
				}
			}
			return DynValue.Nil;
		}

		// Token: 0x06005D8E RID: 23950 RVA: 0x00262F8C File Offset: 0x0026118C
		private DynValue LuaIteratorCallback(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			this.m_Prev = this.GetNext(this.m_Prev);
			return this.m_Prev;
		}

		// Token: 0x06005D8F RID: 23951 RVA: 0x00262FA8 File Offset: 0x002611A8
		internal static DynValue ConvertIterator(Script script, IEnumerator enumerator)
		{
			EnumerableWrapper o = new EnumerableWrapper(script, enumerator);
			return DynValue.NewTuple(new DynValue[]
			{
				UserData.Create(o),
				DynValue.Nil,
				DynValue.Nil
			});
		}

		// Token: 0x06005D90 RID: 23952 RVA: 0x00262FE1 File Offset: 0x002611E1
		internal static DynValue ConvertTable(Table table)
		{
			return EnumerableWrapper.ConvertIterator(table.OwnerScript, table.Values.GetEnumerator());
		}

		// Token: 0x06005D91 RID: 23953 RVA: 0x00262FFC File Offset: 0x002611FC
		public DynValue Index(Script script, DynValue index, bool isDirectIndexing)
		{
			if (index.Type == DataType.String)
			{
				string @string = index.String;
				if (@string == "Current" || @string == "current")
				{
					return DynValue.FromObject(script, this.m_Enumerator.Current);
				}
				if (@string == "MoveNext" || @string == "moveNext" || @string == "move_next")
				{
					return DynValue.NewCallback((ScriptExecutionContext ctx, CallbackArguments args) => DynValue.NewBoolean(this.m_Enumerator.MoveNext()), null);
				}
				if (@string == "Reset" || @string == "reset")
				{
					return DynValue.NewCallback(delegate(ScriptExecutionContext ctx, CallbackArguments args)
					{
						this.Reset();
						return DynValue.Nil;
					}, null);
				}
			}
			return null;
		}

		// Token: 0x06005D92 RID: 23954 RVA: 0x0000280F File Offset: 0x00000A0F
		public bool SetIndex(Script script, DynValue index, DynValue value, bool isDirectIndexing)
		{
			return false;
		}

		// Token: 0x06005D93 RID: 23955 RVA: 0x002630B0 File Offset: 0x002612B0
		public DynValue MetaIndex(Script script, string metaname)
		{
			if (metaname == "__call")
			{
				return DynValue.NewCallback(new Func<ScriptExecutionContext, CallbackArguments, DynValue>(this.LuaIteratorCallback), null);
			}
			return null;
		}

		// Token: 0x040053DE RID: 21470
		private IEnumerator m_Enumerator;

		// Token: 0x040053DF RID: 21471
		private Script m_Script;

		// Token: 0x040053E0 RID: 21472
		private DynValue m_Prev = DynValue.Nil;

		// Token: 0x040053E1 RID: 21473
		private bool m_HasTurnOnce;
	}
}
