using System;
using MoonSharp.Interpreter.Interop;
using MoonSharp.Interpreter.Interop.BasicDescriptors;

namespace MoonSharp.Interpreter
{
	// Token: 0x02000CAF RID: 3247
	[Serializable]
	public class ScriptRuntimeException : InterpreterException
	{
		// Token: 0x06005AFD RID: 23293 RVA: 0x00259345 File Offset: 0x00257545
		public ScriptRuntimeException(Exception ex) : base(ex)
		{
		}

		// Token: 0x06005AFE RID: 23294 RVA: 0x0025934E File Offset: 0x0025754E
		public ScriptRuntimeException(ScriptRuntimeException ex) : base(ex, ex.DecoratedMessage)
		{
			base.DecoratedMessage = this.Message;
			base.DoNotDecorateMessage = true;
		}

		// Token: 0x06005AFF RID: 23295 RVA: 0x00259259 File Offset: 0x00257459
		public ScriptRuntimeException(string message) : base(message)
		{
		}

		// Token: 0x06005B00 RID: 23296 RVA: 0x00259262 File Offset: 0x00257462
		public ScriptRuntimeException(string format, params object[] args) : base(format, args)
		{
		}

		// Token: 0x06005B01 RID: 23297 RVA: 0x00259370 File Offset: 0x00257570
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

		// Token: 0x06005B02 RID: 23298 RVA: 0x0025940C File Offset: 0x0025760C
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

		// Token: 0x06005B03 RID: 23299 RVA: 0x00259488 File Offset: 0x00257688
		public static ScriptRuntimeException LenOnInvalidType(DynValue r)
		{
			return new ScriptRuntimeException("attempt to get length of a {0} value", new object[]
			{
				r.Type.ToLuaTypeString()
			});
		}

		// Token: 0x06005B04 RID: 23300 RVA: 0x002594A8 File Offset: 0x002576A8
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

		// Token: 0x06005B05 RID: 23301 RVA: 0x0025951D File Offset: 0x0025771D
		public static ScriptRuntimeException BadArgument(int argNum, string funcName, string message)
		{
			return new ScriptRuntimeException("bad argument #{0} to '{1}' ({2})", new object[]
			{
				argNum + 1,
				funcName,
				message
			});
		}

		// Token: 0x06005B06 RID: 23302 RVA: 0x00259544 File Offset: 0x00257744
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

		// Token: 0x06005B07 RID: 23303 RVA: 0x002595B3 File Offset: 0x002577B3
		public static ScriptRuntimeException BadArgument(int argNum, string funcName, DataType expected, DataType got, bool allowNil)
		{
			return ScriptRuntimeException.BadArgument(argNum, funcName, expected.ToErrorTypeString(), got.ToErrorTypeString(), allowNil);
		}

		// Token: 0x06005B08 RID: 23304 RVA: 0x002595CA File Offset: 0x002577CA
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

		// Token: 0x06005B09 RID: 23305 RVA: 0x00259606 File Offset: 0x00257806
		public static ScriptRuntimeException BadArgumentNoValue(int argNum, string funcName, DataType expected)
		{
			return new ScriptRuntimeException("bad argument #{0} to '{1}' ({2} expected, got no value)", new object[]
			{
				argNum + 1,
				funcName,
				expected.ToErrorTypeString()
			});
		}

		// Token: 0x06005B0A RID: 23306 RVA: 0x00259630 File Offset: 0x00257830
		public static ScriptRuntimeException BadArgumentIndexOutOfRange(string funcName, int argNum)
		{
			return new ScriptRuntimeException("bad argument #{0} to '{1}' (index out of range)", new object[]
			{
				argNum + 1,
				funcName
			});
		}

		// Token: 0x06005B0B RID: 23307 RVA: 0x00259651 File Offset: 0x00257851
		public static ScriptRuntimeException BadArgumentNoNegativeNumbers(int argNum, string funcName)
		{
			return new ScriptRuntimeException("bad argument #{0} to '{1}' (not a non-negative number in proper range)", new object[]
			{
				argNum + 1,
				funcName
			});
		}

		// Token: 0x06005B0C RID: 23308 RVA: 0x00259672 File Offset: 0x00257872
		public static ScriptRuntimeException BadArgumentValueExpected(int argNum, string funcName)
		{
			return new ScriptRuntimeException("bad argument #{0} to '{1}' (value expected)", new object[]
			{
				argNum + 1,
				funcName
			});
		}

		// Token: 0x06005B0D RID: 23309 RVA: 0x00259693 File Offset: 0x00257893
		public static ScriptRuntimeException IndexType(DynValue obj)
		{
			return new ScriptRuntimeException("attempt to index a {0} value", new object[]
			{
				obj.Type.ToLuaTypeString()
			});
		}

		// Token: 0x06005B0E RID: 23310 RVA: 0x002596B3 File Offset: 0x002578B3
		public static ScriptRuntimeException LoopInIndex()
		{
			return new ScriptRuntimeException("loop in gettable");
		}

		// Token: 0x06005B0F RID: 23311 RVA: 0x002596BF File Offset: 0x002578BF
		public static ScriptRuntimeException LoopInNewIndex()
		{
			return new ScriptRuntimeException("loop in settable");
		}

		// Token: 0x06005B10 RID: 23312 RVA: 0x002596CB File Offset: 0x002578CB
		public static ScriptRuntimeException LoopInCall()
		{
			return new ScriptRuntimeException("loop in call");
		}

		// Token: 0x06005B11 RID: 23313 RVA: 0x002596D7 File Offset: 0x002578D7
		public static ScriptRuntimeException TableIndexIsNil()
		{
			return new ScriptRuntimeException("table index is nil");
		}

		// Token: 0x06005B12 RID: 23314 RVA: 0x002596E3 File Offset: 0x002578E3
		public static ScriptRuntimeException TableIndexIsNaN()
		{
			return new ScriptRuntimeException("table index is NaN");
		}

		// Token: 0x06005B13 RID: 23315 RVA: 0x002596F0 File Offset: 0x002578F0
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

		// Token: 0x06005B14 RID: 23316 RVA: 0x0025973E File Offset: 0x0025793E
		public static ScriptRuntimeException ConvertObjectFailed(object obj)
		{
			return new ScriptRuntimeException("cannot convert clr type {0}", new object[]
			{
				obj.GetType()
			});
		}

		// Token: 0x06005B15 RID: 23317 RVA: 0x00259759 File Offset: 0x00257959
		public static ScriptRuntimeException ConvertObjectFailed(DataType t)
		{
			return new ScriptRuntimeException("cannot convert a {0} to a clr type", new object[]
			{
				t.ToString().ToLowerInvariant()
			});
		}

		// Token: 0x06005B16 RID: 23318 RVA: 0x00259780 File Offset: 0x00257980
		public static ScriptRuntimeException ConvertObjectFailed(DataType t, Type t2)
		{
			return new ScriptRuntimeException("cannot convert a {0} to a clr type {1}", new object[]
			{
				t.ToString().ToLowerInvariant(),
				t2.FullName
			});
		}

		// Token: 0x06005B17 RID: 23319 RVA: 0x002597B0 File Offset: 0x002579B0
		public static ScriptRuntimeException UserDataArgumentTypeMismatch(DataType t, Type clrType)
		{
			return new ScriptRuntimeException("cannot find a conversion from a MoonSharp {0} to a clr {1}", new object[]
			{
				t.ToString().ToLowerInvariant(),
				clrType.FullName
			});
		}

		// Token: 0x06005B18 RID: 23320 RVA: 0x002597E0 File Offset: 0x002579E0
		public static ScriptRuntimeException UserDataMissingField(string typename, string fieldname)
		{
			return new ScriptRuntimeException("cannot access field {0} of userdata<{1}>", new object[]
			{
				fieldname,
				typename
			});
		}

		// Token: 0x06005B19 RID: 23321 RVA: 0x002597FA File Offset: 0x002579FA
		public static ScriptRuntimeException CannotResumeNotSuspended(CoroutineState state)
		{
			if (state == CoroutineState.Dead)
			{
				return new ScriptRuntimeException("cannot resume dead coroutine");
			}
			return new ScriptRuntimeException("cannot resume non-suspended coroutine");
		}

		// Token: 0x06005B1A RID: 23322 RVA: 0x00259815 File Offset: 0x00257A15
		public static ScriptRuntimeException CannotYield()
		{
			return new ScriptRuntimeException("attempt to yield across a CLR-call boundary");
		}

		// Token: 0x06005B1B RID: 23323 RVA: 0x00259821 File Offset: 0x00257A21
		public static ScriptRuntimeException CannotYieldMain()
		{
			return new ScriptRuntimeException("attempt to yield from outside a coroutine");
		}

		// Token: 0x06005B1C RID: 23324 RVA: 0x00259830 File Offset: 0x00257A30
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

		// Token: 0x06005B1D RID: 23325 RVA: 0x00259874 File Offset: 0x00257A74
		public static ScriptRuntimeException AccessInstanceMemberOnStatics(IMemberDescriptor desc)
		{
			return new ScriptRuntimeException("attempt to access instance member {0} from a static userdata", new object[]
			{
				desc.Name
			});
		}

		// Token: 0x06005B1E RID: 23326 RVA: 0x0025988F File Offset: 0x00257A8F
		public static ScriptRuntimeException AccessInstanceMemberOnStatics(IUserDataDescriptor typeDescr, IMemberDescriptor desc)
		{
			return new ScriptRuntimeException("attempt to access instance member {0}.{1} from a static userdata", new object[]
			{
				typeDescr.Name,
				desc.Name
			});
		}

		// Token: 0x06005B1F RID: 23327 RVA: 0x002598B3 File Offset: 0x00257AB3
		public override void Rethrow()
		{
			if (Script.GlobalOptions.RethrowExceptionNested)
			{
				throw new ScriptRuntimeException(this);
			}
		}
	}
}
