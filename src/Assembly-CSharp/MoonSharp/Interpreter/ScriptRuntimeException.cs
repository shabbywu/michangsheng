using System;
using MoonSharp.Interpreter.Interop;
using MoonSharp.Interpreter.Interop.BasicDescriptors;

namespace MoonSharp.Interpreter
{
	// Token: 0x0200107E RID: 4222
	[Serializable]
	public class ScriptRuntimeException : InterpreterException
	{
		// Token: 0x060065EF RID: 26095 RVA: 0x0004632F File Offset: 0x0004452F
		public ScriptRuntimeException(Exception ex) : base(ex)
		{
		}

		// Token: 0x060065F0 RID: 26096 RVA: 0x00046338 File Offset: 0x00044538
		public ScriptRuntimeException(ScriptRuntimeException ex) : base(ex, ex.DecoratedMessage)
		{
			base.DecoratedMessage = this.Message;
			base.DoNotDecorateMessage = true;
		}

		// Token: 0x060065F1 RID: 26097 RVA: 0x000462B0 File Offset: 0x000444B0
		public ScriptRuntimeException(string message) : base(message)
		{
		}

		// Token: 0x060065F2 RID: 26098 RVA: 0x000462B9 File Offset: 0x000444B9
		public ScriptRuntimeException(string format, params object[] args) : base(format, args)
		{
		}

		// Token: 0x060065F3 RID: 26099 RVA: 0x00283710 File Offset: 0x00281910
		public static ScriptRuntimeException ArithmeticOnNonNumber(DynValue l, DynValue r = null)
		{
			if (l.Type != DataType.Number && l.Type != DataType.String)
			{
				return new ScriptRuntimeException("attempt to perform arithmetic on a {0} value", new object[]
				{
					l.Type.ToLuaTypeString()
				});
			}
			if (r != null && r.Type != DataType.Number && r.Type != DataType.String)
			{
				return new ScriptRuntimeException("attempt to perform arithmetic on a {0} value", new object[]
				{
					r.Type.ToLuaTypeString()
				});
			}
			if (l.Type == DataType.String || (r != null && r.Type == DataType.String))
			{
				return new ScriptRuntimeException("attempt to perform arithmetic on a string value");
			}
			throw new InternalErrorException("ArithmeticOnNonNumber - both are numbers");
		}

		// Token: 0x060065F4 RID: 26100 RVA: 0x002837AC File Offset: 0x002819AC
		public static ScriptRuntimeException ConcatOnNonString(DynValue l, DynValue r)
		{
			if (l.Type != DataType.Number && l.Type != DataType.String)
			{
				return new ScriptRuntimeException("attempt to concatenate a {0} value", new object[]
				{
					l.Type.ToLuaTypeString()
				});
			}
			if (r != null && r.Type != DataType.Number && r.Type != DataType.String)
			{
				return new ScriptRuntimeException("attempt to concatenate a {0} value", new object[]
				{
					r.Type.ToLuaTypeString()
				});
			}
			throw new InternalErrorException("ConcatOnNonString - both are numbers/strings");
		}

		// Token: 0x060065F5 RID: 26101 RVA: 0x0004635A File Offset: 0x0004455A
		public static ScriptRuntimeException LenOnInvalidType(DynValue r)
		{
			return new ScriptRuntimeException("attempt to get length of a {0} value", new object[]
			{
				r.Type.ToLuaTypeString()
			});
		}

		// Token: 0x060065F6 RID: 26102 RVA: 0x00283828 File Offset: 0x00281A28
		public static ScriptRuntimeException CompareInvalidType(DynValue l, DynValue r)
		{
			if (l.Type.ToLuaTypeString() == r.Type.ToLuaTypeString())
			{
				return new ScriptRuntimeException("attempt to compare two {0} values", new object[]
				{
					l.Type.ToLuaTypeString()
				});
			}
			return new ScriptRuntimeException("attempt to compare {0} with {1}", new object[]
			{
				l.Type.ToLuaTypeString(),
				r.Type.ToLuaTypeString()
			});
		}

		// Token: 0x060065F7 RID: 26103 RVA: 0x0004637A File Offset: 0x0004457A
		public static ScriptRuntimeException BadArgument(int argNum, string funcName, string message)
		{
			return new ScriptRuntimeException("bad argument #{0} to '{1}' ({2})", new object[]
			{
				argNum + 1,
				funcName,
				message
			});
		}

		// Token: 0x060065F8 RID: 26104 RVA: 0x002838A0 File Offset: 0x00281AA0
		public static ScriptRuntimeException BadArgumentUserData(int argNum, string funcName, Type expected, object got, bool allowNil)
		{
			return new ScriptRuntimeException("bad argument #{0} to '{1}' (userdata<{2}>{3} expected, got {4})", new object[]
			{
				argNum + 1,
				funcName,
				expected.Name,
				allowNil ? "nil or " : "",
				(got != null) ? ("userdata<" + got.GetType().Name + ">") : "null"
			});
		}

		// Token: 0x060065F9 RID: 26105 RVA: 0x0004639F File Offset: 0x0004459F
		public static ScriptRuntimeException BadArgument(int argNum, string funcName, DataType expected, DataType got, bool allowNil)
		{
			return ScriptRuntimeException.BadArgument(argNum, funcName, expected.ToErrorTypeString(), got.ToErrorTypeString(), allowNil);
		}

		// Token: 0x060065FA RID: 26106 RVA: 0x000463B6 File Offset: 0x000445B6
		public static ScriptRuntimeException BadArgument(int argNum, string funcName, string expected, string got, bool allowNil)
		{
			return new ScriptRuntimeException("bad argument #{0} to '{1}' ({2}{3} expected, got {4})", new object[]
			{
				argNum + 1,
				funcName,
				allowNil ? "nil or " : "",
				expected,
				got
			});
		}

		// Token: 0x060065FB RID: 26107 RVA: 0x000463F2 File Offset: 0x000445F2
		public static ScriptRuntimeException BadArgumentNoValue(int argNum, string funcName, DataType expected)
		{
			return new ScriptRuntimeException("bad argument #{0} to '{1}' ({2} expected, got no value)", new object[]
			{
				argNum + 1,
				funcName,
				expected.ToErrorTypeString()
			});
		}

		// Token: 0x060065FC RID: 26108 RVA: 0x0004641C File Offset: 0x0004461C
		public static ScriptRuntimeException BadArgumentIndexOutOfRange(string funcName, int argNum)
		{
			return new ScriptRuntimeException("bad argument #{0} to '{1}' (index out of range)", new object[]
			{
				argNum + 1,
				funcName
			});
		}

		// Token: 0x060065FD RID: 26109 RVA: 0x0004643D File Offset: 0x0004463D
		public static ScriptRuntimeException BadArgumentNoNegativeNumbers(int argNum, string funcName)
		{
			return new ScriptRuntimeException("bad argument #{0} to '{1}' (not a non-negative number in proper range)", new object[]
			{
				argNum + 1,
				funcName
			});
		}

		// Token: 0x060065FE RID: 26110 RVA: 0x0004645E File Offset: 0x0004465E
		public static ScriptRuntimeException BadArgumentValueExpected(int argNum, string funcName)
		{
			return new ScriptRuntimeException("bad argument #{0} to '{1}' (value expected)", new object[]
			{
				argNum + 1,
				funcName
			});
		}

		// Token: 0x060065FF RID: 26111 RVA: 0x0004647F File Offset: 0x0004467F
		public static ScriptRuntimeException IndexType(DynValue obj)
		{
			return new ScriptRuntimeException("attempt to index a {0} value", new object[]
			{
				obj.Type.ToLuaTypeString()
			});
		}

		// Token: 0x06006600 RID: 26112 RVA: 0x0004649F File Offset: 0x0004469F
		public static ScriptRuntimeException LoopInIndex()
		{
			return new ScriptRuntimeException("loop in gettable");
		}

		// Token: 0x06006601 RID: 26113 RVA: 0x000464AB File Offset: 0x000446AB
		public static ScriptRuntimeException LoopInNewIndex()
		{
			return new ScriptRuntimeException("loop in settable");
		}

		// Token: 0x06006602 RID: 26114 RVA: 0x000464B7 File Offset: 0x000446B7
		public static ScriptRuntimeException LoopInCall()
		{
			return new ScriptRuntimeException("loop in call");
		}

		// Token: 0x06006603 RID: 26115 RVA: 0x000464C3 File Offset: 0x000446C3
		public static ScriptRuntimeException TableIndexIsNil()
		{
			return new ScriptRuntimeException("table index is nil");
		}

		// Token: 0x06006604 RID: 26116 RVA: 0x000464CF File Offset: 0x000446CF
		public static ScriptRuntimeException TableIndexIsNaN()
		{
			return new ScriptRuntimeException("table index is NaN");
		}

		// Token: 0x06006605 RID: 26117 RVA: 0x00283910 File Offset: 0x00281B10
		public static ScriptRuntimeException ConvertToNumberFailed(int stage)
		{
			switch (stage)
			{
			case 1:
				return new ScriptRuntimeException("'for' initial value must be a number");
			case 2:
				return new ScriptRuntimeException("'for' step must be a number");
			case 3:
				return new ScriptRuntimeException("'for' limit must be a number");
			default:
				return new ScriptRuntimeException("value must be a number");
			}
		}

		// Token: 0x06006606 RID: 26118 RVA: 0x000464DB File Offset: 0x000446DB
		public static ScriptRuntimeException ConvertObjectFailed(object obj)
		{
			return new ScriptRuntimeException("cannot convert clr type {0}", new object[]
			{
				obj.GetType()
			});
		}

		// Token: 0x06006607 RID: 26119 RVA: 0x000464F6 File Offset: 0x000446F6
		public static ScriptRuntimeException ConvertObjectFailed(DataType t)
		{
			return new ScriptRuntimeException("cannot convert a {0} to a clr type", new object[]
			{
				t.ToString().ToLowerInvariant()
			});
		}

		// Token: 0x06006608 RID: 26120 RVA: 0x0004651D File Offset: 0x0004471D
		public static ScriptRuntimeException ConvertObjectFailed(DataType t, Type t2)
		{
			return new ScriptRuntimeException("cannot convert a {0} to a clr type {1}", new object[]
			{
				t.ToString().ToLowerInvariant(),
				t2.FullName
			});
		}

		// Token: 0x06006609 RID: 26121 RVA: 0x0004654D File Offset: 0x0004474D
		public static ScriptRuntimeException UserDataArgumentTypeMismatch(DataType t, Type clrType)
		{
			return new ScriptRuntimeException("cannot find a conversion from a MoonSharp {0} to a clr {1}", new object[]
			{
				t.ToString().ToLowerInvariant(),
				clrType.FullName
			});
		}

		// Token: 0x0600660A RID: 26122 RVA: 0x0004657D File Offset: 0x0004477D
		public static ScriptRuntimeException UserDataMissingField(string typename, string fieldname)
		{
			return new ScriptRuntimeException("cannot access field {0} of userdata<{1}>", new object[]
			{
				fieldname,
				typename
			});
		}

		// Token: 0x0600660B RID: 26123 RVA: 0x00046597 File Offset: 0x00044797
		public static ScriptRuntimeException CannotResumeNotSuspended(CoroutineState state)
		{
			if (state == CoroutineState.Dead)
			{
				return new ScriptRuntimeException("cannot resume dead coroutine");
			}
			return new ScriptRuntimeException("cannot resume non-suspended coroutine");
		}

		// Token: 0x0600660C RID: 26124 RVA: 0x000465B2 File Offset: 0x000447B2
		public static ScriptRuntimeException CannotYield()
		{
			return new ScriptRuntimeException("attempt to yield across a CLR-call boundary");
		}

		// Token: 0x0600660D RID: 26125 RVA: 0x000465BE File Offset: 0x000447BE
		public static ScriptRuntimeException CannotYieldMain()
		{
			return new ScriptRuntimeException("attempt to yield from outside a coroutine");
		}

		// Token: 0x0600660E RID: 26126 RVA: 0x00283960 File Offset: 0x00281B60
		public static ScriptRuntimeException AttemptToCallNonFunc(DataType type, string debugText = null)
		{
			string text = type.ToErrorTypeString();
			if (debugText != null)
			{
				return new ScriptRuntimeException("attempt to call a {0} value near '{1}'", new object[]
				{
					text,
					debugText
				});
			}
			return new ScriptRuntimeException("attempt to call a {0} value", new object[]
			{
				text
			});
		}

		// Token: 0x0600660F RID: 26127 RVA: 0x000465CA File Offset: 0x000447CA
		public static ScriptRuntimeException AccessInstanceMemberOnStatics(IMemberDescriptor desc)
		{
			return new ScriptRuntimeException("attempt to access instance member {0} from a static userdata", new object[]
			{
				desc.Name
			});
		}

		// Token: 0x06006610 RID: 26128 RVA: 0x000465E5 File Offset: 0x000447E5
		public static ScriptRuntimeException AccessInstanceMemberOnStatics(IUserDataDescriptor typeDescr, IMemberDescriptor desc)
		{
			return new ScriptRuntimeException("attempt to access instance member {0}.{1} from a static userdata", new object[]
			{
				typeDescr.Name,
				desc.Name
			});
		}

		// Token: 0x06006611 RID: 26129 RVA: 0x00046609 File Offset: 0x00044809
		public override void Rethrow()
		{
			if (Script.GlobalOptions.RethrowExceptionNested)
			{
				throw new ScriptRuntimeException(this);
			}
		}
	}
}
