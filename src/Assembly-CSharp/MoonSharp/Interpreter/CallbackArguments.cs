using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.DataStructs;

namespace MoonSharp.Interpreter
{
	// Token: 0x02000C96 RID: 3222
	public class CallbackArguments
	{
		// Token: 0x060059D5 RID: 22997 RVA: 0x00256B80 File Offset: 0x00254D80
		public CallbackArguments(IList<DynValue> args, bool isMethodCall)
		{
			this.m_Args = args;
			if (this.m_Args.Count > 0)
			{
				DynValue dynValue = this.m_Args[this.m_Args.Count - 1];
				if (dynValue.Type == DataType.Tuple)
				{
					this.m_Count = dynValue.Tuple.Length - 1 + this.m_Args.Count;
					this.m_LastIsTuple = true;
				}
				else if (dynValue.Type == DataType.Void)
				{
					this.m_Count = this.m_Args.Count - 1;
				}
				else
				{
					this.m_Count = this.m_Args.Count;
				}
			}
			else
			{
				this.m_Count = 0;
			}
			this.IsMethodCall = isMethodCall;
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x060059D6 RID: 22998 RVA: 0x00256C2E File Offset: 0x00254E2E
		public int Count
		{
			get
			{
				return this.m_Count;
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x060059D7 RID: 22999 RVA: 0x00256C36 File Offset: 0x00254E36
		// (set) Token: 0x060059D8 RID: 23000 RVA: 0x00256C3E File Offset: 0x00254E3E
		public bool IsMethodCall { get; private set; }

		// Token: 0x17000679 RID: 1657
		public DynValue this[int index]
		{
			get
			{
				return this.RawGet(index, true) ?? DynValue.Void;
			}
		}

		// Token: 0x060059DA RID: 23002 RVA: 0x00256C5C File Offset: 0x00254E5C
		public DynValue RawGet(int index, bool translateVoids)
		{
			if (index >= this.m_Count)
			{
				return null;
			}
			DynValue dynValue;
			if (!this.m_LastIsTuple || index < this.m_Args.Count - 1)
			{
				dynValue = this.m_Args[index];
			}
			else
			{
				dynValue = this.m_Args[this.m_Args.Count - 1].Tuple[index - (this.m_Args.Count - 1)];
			}
			if (dynValue.Type == DataType.Tuple)
			{
				if (dynValue.Tuple.Length != 0)
				{
					dynValue = dynValue.Tuple[0];
				}
				else
				{
					dynValue = DynValue.Nil;
				}
			}
			if (translateVoids && dynValue.Type == DataType.Void)
			{
				dynValue = DynValue.Nil;
			}
			return dynValue;
		}

		// Token: 0x060059DB RID: 23003 RVA: 0x00256D00 File Offset: 0x00254F00
		public DynValue[] GetArray(int skip = 0)
		{
			if (skip >= this.m_Count)
			{
				return new DynValue[0];
			}
			DynValue[] array = new DynValue[this.m_Count - skip];
			for (int i = skip; i < this.m_Count; i++)
			{
				array[i - skip] = this[i];
			}
			return array;
		}

		// Token: 0x060059DC RID: 23004 RVA: 0x00256D49 File Offset: 0x00254F49
		public DynValue AsType(int argNum, string funcName, DataType type, bool allowNil = false)
		{
			return this[argNum].CheckType(funcName, type, argNum, allowNil ? (TypeValidationFlags.AllowNil | TypeValidationFlags.AutoConvert) : TypeValidationFlags.AutoConvert);
		}

		// Token: 0x060059DD RID: 23005 RVA: 0x00256D62 File Offset: 0x00254F62
		public T AsUserData<T>(int argNum, string funcName, bool allowNil = false)
		{
			return this[argNum].CheckUserDataType<T>(funcName, argNum, allowNil ? TypeValidationFlags.AllowNil : TypeValidationFlags.None);
		}

		// Token: 0x060059DE RID: 23006 RVA: 0x00256D79 File Offset: 0x00254F79
		public int AsInt(int argNum, string funcName)
		{
			return (int)this.AsType(argNum, funcName, DataType.Number, false).Number;
		}

		// Token: 0x060059DF RID: 23007 RVA: 0x00256D8B File Offset: 0x00254F8B
		public long AsLong(int argNum, string funcName)
		{
			return (long)this.AsType(argNum, funcName, DataType.Number, false).Number;
		}

		// Token: 0x060059E0 RID: 23008 RVA: 0x00256DA0 File Offset: 0x00254FA0
		public string AsStringUsingMeta(ScriptExecutionContext executionContext, int argNum, string funcName)
		{
			if (this[argNum].Type != DataType.Table || this[argNum].Table.MetaTable == null || this[argNum].Table.MetaTable.RawGet("__tostring") == null)
			{
				return this[argNum].ToPrintString();
			}
			DynValue dynValue = executionContext.GetScript().Call(this[argNum].Table.MetaTable.RawGet("__tostring"), new DynValue[]
			{
				this[argNum]
			});
			if (dynValue.Type != DataType.String)
			{
				throw new ScriptRuntimeException("'tostring' must return a string to '{0}'", new object[]
				{
					funcName
				});
			}
			return dynValue.ToPrintString();
		}

		// Token: 0x060059E1 RID: 23009 RVA: 0x00256E57 File Offset: 0x00255057
		public CallbackArguments SkipMethodCall()
		{
			if (this.IsMethodCall)
			{
				return new CallbackArguments(new Slice<DynValue>(this.m_Args, 1, this.m_Args.Count - 1, false), false);
			}
			return this;
		}

		// Token: 0x0400524C RID: 21068
		private IList<DynValue> m_Args;

		// Token: 0x0400524D RID: 21069
		private int m_Count;

		// Token: 0x0400524E RID: 21070
		private bool m_LastIsTuple;
	}
}
