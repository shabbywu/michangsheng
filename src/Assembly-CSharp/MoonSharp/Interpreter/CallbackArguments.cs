using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.DataStructs;

namespace MoonSharp.Interpreter
{
	// Token: 0x0200105C RID: 4188
	public class CallbackArguments
	{
		// Token: 0x06006497 RID: 25751 RVA: 0x00281B28 File Offset: 0x0027FD28
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

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06006498 RID: 25752 RVA: 0x000451FD File Offset: 0x000433FD
		public int Count
		{
			get
			{
				return this.m_Count;
			}
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06006499 RID: 25753 RVA: 0x00045205 File Offset: 0x00043405
		// (set) Token: 0x0600649A RID: 25754 RVA: 0x0004520D File Offset: 0x0004340D
		public bool IsMethodCall { get; private set; }

		// Token: 0x170008CC RID: 2252
		public DynValue this[int index]
		{
			get
			{
				return this.RawGet(index, true) ?? DynValue.Void;
			}
		}

		// Token: 0x0600649C RID: 25756 RVA: 0x00281BD8 File Offset: 0x0027FDD8
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

		// Token: 0x0600649D RID: 25757 RVA: 0x00281C7C File Offset: 0x0027FE7C
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

		// Token: 0x0600649E RID: 25758 RVA: 0x00045229 File Offset: 0x00043429
		public DynValue AsType(int argNum, string funcName, DataType type, bool allowNil = false)
		{
			return this[argNum].CheckType(funcName, type, argNum, allowNil ? (TypeValidationFlags.AllowNil | TypeValidationFlags.AutoConvert) : TypeValidationFlags.AutoConvert);
		}

		// Token: 0x0600649F RID: 25759 RVA: 0x00045242 File Offset: 0x00043442
		public T AsUserData<T>(int argNum, string funcName, bool allowNil = false)
		{
			return this[argNum].CheckUserDataType<T>(funcName, argNum, allowNil ? TypeValidationFlags.AllowNil : TypeValidationFlags.None);
		}

		// Token: 0x060064A0 RID: 25760 RVA: 0x00045259 File Offset: 0x00043459
		public int AsInt(int argNum, string funcName)
		{
			return (int)this.AsType(argNum, funcName, DataType.Number, false).Number;
		}

		// Token: 0x060064A1 RID: 25761 RVA: 0x0004526B File Offset: 0x0004346B
		public long AsLong(int argNum, string funcName)
		{
			return (long)this.AsType(argNum, funcName, DataType.Number, false).Number;
		}

		// Token: 0x060064A2 RID: 25762 RVA: 0x00281CC8 File Offset: 0x0027FEC8
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

		// Token: 0x060064A3 RID: 25763 RVA: 0x0004527D File Offset: 0x0004347D
		public CallbackArguments SkipMethodCall()
		{
			if (this.IsMethodCall)
			{
				return new CallbackArguments(new Slice<DynValue>(this.m_Args, 1, this.m_Args.Count - 1, false), false);
			}
			return this;
		}

		// Token: 0x04005DFB RID: 24059
		private IList<DynValue> m_Args;

		// Token: 0x04005DFC RID: 24060
		private int m_Count;

		// Token: 0x04005DFD RID: 24061
		private bool m_LastIsTuple;
	}
}
