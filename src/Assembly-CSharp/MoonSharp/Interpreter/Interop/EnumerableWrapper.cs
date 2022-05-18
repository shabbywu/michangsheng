using System;
using System.Collections;
using MoonSharp.Interpreter.Interop.Converters;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x020010F3 RID: 4339
	internal class EnumerableWrapper : IUserDataType
	{
		// Token: 0x060068BA RID: 26810 RVA: 0x00047C89 File Offset: 0x00045E89
		private EnumerableWrapper(Script script, IEnumerator enumerator)
		{
			this.m_Script = script;
			this.m_Enumerator = enumerator;
		}

		// Token: 0x060068BB RID: 26811 RVA: 0x00047CAA File Offset: 0x00045EAA
		public void Reset()
		{
			if (this.m_HasTurnOnce)
			{
				this.m_Enumerator.Reset();
			}
			this.m_HasTurnOnce = true;
		}

		// Token: 0x060068BC RID: 26812 RVA: 0x0028BC8C File Offset: 0x00289E8C
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

		// Token: 0x060068BD RID: 26813 RVA: 0x00047CC6 File Offset: 0x00045EC6
		private DynValue LuaIteratorCallback(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			this.m_Prev = this.GetNext(this.m_Prev);
			return this.m_Prev;
		}

		// Token: 0x060068BE RID: 26814 RVA: 0x0028BCDC File Offset: 0x00289EDC
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

		// Token: 0x060068BF RID: 26815 RVA: 0x00047CE0 File Offset: 0x00045EE0
		internal static DynValue ConvertTable(Table table)
		{
			return EnumerableWrapper.ConvertIterator(table.OwnerScript, table.Values.GetEnumerator());
		}

		// Token: 0x060068C0 RID: 26816 RVA: 0x0028BD18 File Offset: 0x00289F18
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

		// Token: 0x060068C1 RID: 26817 RVA: 0x00004050 File Offset: 0x00002250
		public bool SetIndex(Script script, DynValue index, DynValue value, bool isDirectIndexing)
		{
			return false;
		}

		// Token: 0x060068C2 RID: 26818 RVA: 0x00047CF8 File Offset: 0x00045EF8
		public DynValue MetaIndex(Script script, string metaname)
		{
			if (metaname == "__call")
			{
				return DynValue.NewCallback(new Func<ScriptExecutionContext, CallbackArguments, DynValue>(this.LuaIteratorCallback), null);
			}
			return null;
		}

		// Token: 0x04005FF5 RID: 24565
		private IEnumerator m_Enumerator;

		// Token: 0x04005FF6 RID: 24566
		private Script m_Script;

		// Token: 0x04005FF7 RID: 24567
		private DynValue m_Prev = DynValue.Nil;

		// Token: 0x04005FF8 RID: 24568
		private bool m_HasTurnOnce;
	}
}
