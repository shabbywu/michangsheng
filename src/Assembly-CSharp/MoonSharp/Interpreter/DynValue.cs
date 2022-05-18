using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using MoonSharp.Interpreter.Interop.Converters;

namespace MoonSharp.Interpreter
{
	// Token: 0x02001069 RID: 4201
	public sealed class DynValue
	{
		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x060064FF RID: 25855 RVA: 0x000456DD File Offset: 0x000438DD
		public int ReferenceID
		{
			get
			{
				return this.m_RefID;
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06006500 RID: 25856 RVA: 0x000456E5 File Offset: 0x000438E5
		public DataType Type
		{
			get
			{
				return this.m_Type;
			}
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06006501 RID: 25857 RVA: 0x000456ED File Offset: 0x000438ED
		public Closure Function
		{
			get
			{
				return this.m_Object as Closure;
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06006502 RID: 25858 RVA: 0x000456FA File Offset: 0x000438FA
		public double Number
		{
			get
			{
				return this.m_Number;
			}
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06006503 RID: 25859 RVA: 0x00045702 File Offset: 0x00043902
		public DynValue[] Tuple
		{
			get
			{
				return this.m_Object as DynValue[];
			}
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06006504 RID: 25860 RVA: 0x0004570F File Offset: 0x0004390F
		public Coroutine Coroutine
		{
			get
			{
				return this.m_Object as Coroutine;
			}
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06006505 RID: 25861 RVA: 0x0004571C File Offset: 0x0004391C
		public Table Table
		{
			get
			{
				return this.m_Object as Table;
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06006506 RID: 25862 RVA: 0x00045729 File Offset: 0x00043929
		public bool Boolean
		{
			get
			{
				return this.Number != 0.0;
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06006507 RID: 25863 RVA: 0x0004573F File Offset: 0x0004393F
		public string String
		{
			get
			{
				return this.m_Object as string;
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06006508 RID: 25864 RVA: 0x0004574C File Offset: 0x0004394C
		public CallbackFunction Callback
		{
			get
			{
				return this.m_Object as CallbackFunction;
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06006509 RID: 25865 RVA: 0x00045759 File Offset: 0x00043959
		public TailCallData TailCallData
		{
			get
			{
				return this.m_Object as TailCallData;
			}
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x0600650A RID: 25866 RVA: 0x00045766 File Offset: 0x00043966
		public YieldRequest YieldRequest
		{
			get
			{
				return this.m_Object as YieldRequest;
			}
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x0600650B RID: 25867 RVA: 0x00045773 File Offset: 0x00043973
		public UserData UserData
		{
			get
			{
				return this.m_Object as UserData;
			}
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x0600650C RID: 25868 RVA: 0x00045780 File Offset: 0x00043980
		public bool ReadOnly
		{
			get
			{
				return this.m_ReadOnly;
			}
		}

		// Token: 0x0600650D RID: 25869 RVA: 0x00045788 File Offset: 0x00043988
		public static DynValue NewNil()
		{
			return new DynValue();
		}

		// Token: 0x0600650E RID: 25870 RVA: 0x0004578F File Offset: 0x0004398F
		public static DynValue NewBoolean(bool v)
		{
			return new DynValue
			{
				m_Number = (double)(v ? 1 : 0),
				m_Type = DataType.Boolean
			};
		}

		// Token: 0x0600650F RID: 25871 RVA: 0x000457AB File Offset: 0x000439AB
		public static DynValue NewNumber(double num)
		{
			return new DynValue
			{
				m_Number = num,
				m_Type = DataType.Number,
				m_HashCode = -1
			};
		}

		// Token: 0x06006510 RID: 25872 RVA: 0x000457C7 File Offset: 0x000439C7
		public static DynValue NewString(string str)
		{
			return new DynValue
			{
				m_Object = str,
				m_Type = DataType.String
			};
		}

		// Token: 0x06006511 RID: 25873 RVA: 0x000457DC File Offset: 0x000439DC
		public static DynValue NewString(StringBuilder sb)
		{
			return new DynValue
			{
				m_Object = sb.ToString(),
				m_Type = DataType.String
			};
		}

		// Token: 0x06006512 RID: 25874 RVA: 0x000457F6 File Offset: 0x000439F6
		public static DynValue NewString(string format, params object[] args)
		{
			return new DynValue
			{
				m_Object = string.Format(format, args),
				m_Type = DataType.String
			};
		}

		// Token: 0x06006513 RID: 25875 RVA: 0x00045811 File Offset: 0x00043A11
		public static DynValue NewCoroutine(Coroutine coroutine)
		{
			return new DynValue
			{
				m_Object = coroutine,
				m_Type = DataType.Thread
			};
		}

		// Token: 0x06006514 RID: 25876 RVA: 0x00045827 File Offset: 0x00043A27
		public static DynValue NewClosure(Closure function)
		{
			return new DynValue
			{
				m_Object = function,
				m_Type = DataType.Function
			};
		}

		// Token: 0x06006515 RID: 25877 RVA: 0x0004583C File Offset: 0x00043A3C
		public static DynValue NewCallback(Func<ScriptExecutionContext, CallbackArguments, DynValue> callBack, string name = null)
		{
			return new DynValue
			{
				m_Object = new CallbackFunction(callBack, name),
				m_Type = DataType.ClrFunction
			};
		}

		// Token: 0x06006516 RID: 25878 RVA: 0x00045858 File Offset: 0x00043A58
		public static DynValue NewCallback(CallbackFunction function)
		{
			return new DynValue
			{
				m_Object = function,
				m_Type = DataType.ClrFunction
			};
		}

		// Token: 0x06006517 RID: 25879 RVA: 0x0004586E File Offset: 0x00043A6E
		public static DynValue NewTable(Table table)
		{
			return new DynValue
			{
				m_Object = table,
				m_Type = DataType.Table
			};
		}

		// Token: 0x06006518 RID: 25880 RVA: 0x00045883 File Offset: 0x00043A83
		public static DynValue NewPrimeTable()
		{
			return DynValue.NewTable(new Table(null));
		}

		// Token: 0x06006519 RID: 25881 RVA: 0x00045890 File Offset: 0x00043A90
		public static DynValue NewTable(Script script)
		{
			return DynValue.NewTable(new Table(script));
		}

		// Token: 0x0600651A RID: 25882 RVA: 0x0004589D File Offset: 0x00043A9D
		public static DynValue NewTable(Script script, params DynValue[] arrayValues)
		{
			return DynValue.NewTable(new Table(script, arrayValues));
		}

		// Token: 0x0600651B RID: 25883 RVA: 0x000458AB File Offset: 0x00043AAB
		public static DynValue NewTailCallReq(DynValue tailFn, params DynValue[] args)
		{
			return new DynValue
			{
				m_Object = new TailCallData
				{
					Args = args,
					Function = tailFn
				},
				m_Type = DataType.TailCallRequest
			};
		}

		// Token: 0x0600651C RID: 25884 RVA: 0x000458D3 File Offset: 0x00043AD3
		public static DynValue NewTailCallReq(TailCallData tailCallData)
		{
			return new DynValue
			{
				m_Object = tailCallData,
				m_Type = DataType.TailCallRequest
			};
		}

		// Token: 0x0600651D RID: 25885 RVA: 0x000458E9 File Offset: 0x00043AE9
		public static DynValue NewYieldReq(DynValue[] args)
		{
			return new DynValue
			{
				m_Object = new YieldRequest
				{
					ReturnValues = args
				},
				m_Type = DataType.YieldRequest
			};
		}

		// Token: 0x0600651E RID: 25886 RVA: 0x0004590A File Offset: 0x00043B0A
		internal static DynValue NewForcedYieldReq()
		{
			return new DynValue
			{
				m_Object = new YieldRequest
				{
					Forced = true
				},
				m_Type = DataType.YieldRequest
			};
		}

		// Token: 0x0600651F RID: 25887 RVA: 0x0004592B File Offset: 0x00043B2B
		public static DynValue NewTuple(params DynValue[] values)
		{
			if (values.Length == 0)
			{
				return DynValue.NewNil();
			}
			if (values.Length == 1)
			{
				return values[0];
			}
			return new DynValue
			{
				m_Object = values,
				m_Type = DataType.Tuple
			};
		}

		// Token: 0x06006520 RID: 25888 RVA: 0x002824A4 File Offset: 0x002806A4
		public static DynValue NewTupleNested(params DynValue[] values)
		{
			if (!values.Any((DynValue v) => v.Type == DataType.Tuple))
			{
				return DynValue.NewTuple(values);
			}
			if (values.Length == 1)
			{
				return values[0];
			}
			List<DynValue> list = new List<DynValue>();
			foreach (DynValue dynValue in values)
			{
				if (dynValue.Type == DataType.Tuple)
				{
					list.AddRange(dynValue.Tuple);
				}
				else
				{
					list.Add(dynValue);
				}
			}
			return new DynValue
			{
				m_Object = list.ToArray(),
				m_Type = DataType.Tuple
			};
		}

		// Token: 0x06006521 RID: 25889 RVA: 0x00045954 File Offset: 0x00043B54
		public static DynValue NewUserData(UserData userData)
		{
			return new DynValue
			{
				m_Object = userData,
				m_Type = DataType.UserData
			};
		}

		// Token: 0x06006522 RID: 25890 RVA: 0x00045969 File Offset: 0x00043B69
		public DynValue AsReadOnly()
		{
			if (this.ReadOnly)
			{
				return this;
			}
			return this.Clone(true);
		}

		// Token: 0x06006523 RID: 25891 RVA: 0x0004597C File Offset: 0x00043B7C
		public DynValue Clone()
		{
			return this.Clone(this.ReadOnly);
		}

		// Token: 0x06006524 RID: 25892 RVA: 0x0004598A File Offset: 0x00043B8A
		public DynValue Clone(bool readOnly)
		{
			return new DynValue
			{
				m_Object = this.m_Object,
				m_Number = this.m_Number,
				m_HashCode = this.m_HashCode,
				m_Type = this.m_Type,
				m_ReadOnly = readOnly
			};
		}

		// Token: 0x06006525 RID: 25893 RVA: 0x000459C8 File Offset: 0x00043BC8
		public DynValue CloneAsWritable()
		{
			return this.Clone(false);
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06006526 RID: 25894 RVA: 0x000459D1 File Offset: 0x00043BD1
		// (set) Token: 0x06006527 RID: 25895 RVA: 0x000459D8 File Offset: 0x00043BD8
		public static DynValue Void { get; private set; }

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06006528 RID: 25896 RVA: 0x000459E0 File Offset: 0x00043BE0
		// (set) Token: 0x06006529 RID: 25897 RVA: 0x000459E7 File Offset: 0x00043BE7
		public static DynValue Nil { get; private set; } = new DynValue
		{
			m_Type = DataType.Nil
		}.AsReadOnly();

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x0600652A RID: 25898 RVA: 0x000459EF File Offset: 0x00043BEF
		// (set) Token: 0x0600652B RID: 25899 RVA: 0x000459F6 File Offset: 0x00043BF6
		public static DynValue True { get; private set; }

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x0600652C RID: 25900 RVA: 0x000459FE File Offset: 0x00043BFE
		// (set) Token: 0x0600652D RID: 25901 RVA: 0x00045A05 File Offset: 0x00043C05
		public static DynValue False { get; private set; }

		// Token: 0x0600652E RID: 25902 RVA: 0x0028253C File Offset: 0x0028073C
		static DynValue()
		{
			DynValue.Void = new DynValue
			{
				m_Type = DataType.Void
			}.AsReadOnly();
			DynValue.True = DynValue.NewBoolean(true).AsReadOnly();
			DynValue.False = DynValue.NewBoolean(false).AsReadOnly();
		}

		// Token: 0x0600652F RID: 25903 RVA: 0x00282598 File Offset: 0x00280798
		public string ToPrintString()
		{
			if (this.m_Object != null && this.m_Object is RefIdObject)
			{
				RefIdObject refIdObject = (RefIdObject)this.m_Object;
				string typeString = this.Type.ToLuaTypeString();
				if (this.m_Object is UserData)
				{
					UserData userData = (UserData)this.m_Object;
					string text = userData.Descriptor.AsString(userData.Object);
					if (text != null)
					{
						return text;
					}
				}
				return refIdObject.FormatTypeString(typeString);
			}
			DataType type = this.Type;
			if (type <= DataType.Tuple)
			{
				if (type == DataType.String)
				{
					return this.String;
				}
				if (type == DataType.Tuple)
				{
					return string.Join("\t", (from t in this.Tuple
					select t.ToPrintString()).ToArray<string>());
				}
			}
			else
			{
				if (type == DataType.TailCallRequest)
				{
					return "(TailCallRequest -- INTERNAL!)";
				}
				if (type == DataType.YieldRequest)
				{
					return "(YieldRequest -- INTERNAL!)";
				}
			}
			return this.ToString();
		}

		// Token: 0x06006530 RID: 25904 RVA: 0x00282684 File Offset: 0x00280884
		public string ToDebugPrintString()
		{
			if (this.m_Object != null && this.m_Object is RefIdObject)
			{
				RefIdObject refIdObject = (RefIdObject)this.m_Object;
				string typeString = this.Type.ToLuaTypeString();
				if (this.m_Object is UserData)
				{
					UserData userData = (UserData)this.m_Object;
					string text = userData.Descriptor.AsString(userData.Object);
					if (text != null)
					{
						return text;
					}
				}
				return refIdObject.FormatTypeString(typeString);
			}
			DataType type = this.Type;
			if (type == DataType.Tuple)
			{
				return string.Join("\t", (from t in this.Tuple
				select t.ToPrintString()).ToArray<string>());
			}
			if (type == DataType.TailCallRequest)
			{
				return "(TailCallRequest)";
			}
			if (type != DataType.YieldRequest)
			{
				return this.ToString();
			}
			return "(YieldRequest)";
		}

		// Token: 0x06006531 RID: 25905 RVA: 0x00282760 File Offset: 0x00280960
		public override string ToString()
		{
			switch (this.Type)
			{
			case DataType.Nil:
				return "nil";
			case DataType.Void:
				return "void";
			case DataType.Boolean:
				return this.Boolean.ToString().ToLower();
			case DataType.Number:
				return this.Number.ToString(CultureInfo.InvariantCulture);
			case DataType.String:
				return "\"" + this.String + "\"";
			case DataType.Function:
				return string.Format("(Function {0:X8})", this.Function.EntryPointByteCodeLocation);
			case DataType.Table:
				return "(Table)";
			case DataType.Tuple:
				return string.Join(", ", (from t in this.Tuple
				select t.ToString()).ToArray<string>());
			case DataType.UserData:
				return "(UserData)";
			case DataType.Thread:
				return string.Format("(Coroutine {0:X8})", this.Coroutine.ReferenceID);
			case DataType.ClrFunction:
				return string.Format("(Function CLR)", this.Function);
			case DataType.TailCallRequest:
				return "Tail:(" + string.Join(", ", (from t in this.Tuple
				select t.ToString()).ToArray<string>()) + ")";
			default:
				return "(???)";
			}
		}

		// Token: 0x06006532 RID: 25906 RVA: 0x002828D4 File Offset: 0x00280AD4
		public override int GetHashCode()
		{
			if (this.m_HashCode != -1)
			{
				return this.m_HashCode;
			}
			int num = (int)((int)this.Type << 27);
			switch (this.Type)
			{
			case DataType.Nil:
			case DataType.Void:
				this.m_HashCode = 0;
				goto IL_10B;
			case DataType.Boolean:
				this.m_HashCode = (this.Boolean ? 1 : 2);
				goto IL_10B;
			case DataType.Number:
				this.m_HashCode = (num ^ this.Number.GetHashCode());
				goto IL_10B;
			case DataType.String:
				this.m_HashCode = (num ^ this.String.GetHashCode());
				goto IL_10B;
			case DataType.Function:
				this.m_HashCode = (num ^ this.Function.GetHashCode());
				goto IL_10B;
			case DataType.Table:
				this.m_HashCode = (num ^ this.Table.GetHashCode());
				goto IL_10B;
			case DataType.Tuple:
			case DataType.TailCallRequest:
				this.m_HashCode = (num ^ this.Tuple.GetHashCode());
				goto IL_10B;
			case DataType.ClrFunction:
				this.m_HashCode = (num ^ this.Callback.GetHashCode());
				goto IL_10B;
			}
			this.m_HashCode = 999;
			IL_10B:
			return this.m_HashCode;
		}

		// Token: 0x06006533 RID: 25907 RVA: 0x002829F4 File Offset: 0x00280BF4
		public override bool Equals(object obj)
		{
			DynValue dynValue = obj as DynValue;
			if (dynValue == null)
			{
				return false;
			}
			if ((dynValue.Type == DataType.Nil && this.Type == DataType.Void) || (dynValue.Type == DataType.Void && this.Type == DataType.Nil))
			{
				return true;
			}
			if (dynValue.Type != this.Type)
			{
				return false;
			}
			switch (this.Type)
			{
			case DataType.Nil:
			case DataType.Void:
				return true;
			case DataType.Boolean:
				return this.Boolean == dynValue.Boolean;
			case DataType.Number:
				return this.Number == dynValue.Number;
			case DataType.String:
				return this.String == dynValue.String;
			case DataType.Function:
				return this.Function == dynValue.Function;
			case DataType.Table:
				return this.Table == dynValue.Table;
			case DataType.Tuple:
			case DataType.TailCallRequest:
				return this.Tuple == dynValue.Tuple;
			case DataType.UserData:
			{
				UserData userData = this.UserData;
				UserData userData2 = dynValue.UserData;
				return userData != null && userData2 != null && userData.Descriptor == userData2.Descriptor && ((userData.Object == null && userData2.Object == null) || (userData.Object != null && userData2.Object != null && userData.Object.Equals(userData2.Object)));
			}
			case DataType.Thread:
				return this.Coroutine == dynValue.Coroutine;
			case DataType.ClrFunction:
				return this.Callback == dynValue.Callback;
			default:
				return this == dynValue;
			}
		}

		// Token: 0x06006534 RID: 25908 RVA: 0x00282B60 File Offset: 0x00280D60
		public string CastToString()
		{
			DynValue dynValue = this.ToScalar();
			if (dynValue.Type == DataType.Number)
			{
				return dynValue.Number.ToString();
			}
			if (dynValue.Type == DataType.String)
			{
				return dynValue.String;
			}
			return null;
		}

		// Token: 0x06006535 RID: 25909 RVA: 0x00282BA0 File Offset: 0x00280DA0
		public double? CastToNumber()
		{
			DynValue dynValue = this.ToScalar();
			if (dynValue.Type == DataType.Number)
			{
				return new double?(dynValue.Number);
			}
			double value;
			if (dynValue.Type == DataType.String && double.TryParse(dynValue.String, NumberStyles.Any, CultureInfo.InvariantCulture, out value))
			{
				return new double?(value);
			}
			return null;
		}

		// Token: 0x06006536 RID: 25910 RVA: 0x00282BFC File Offset: 0x00280DFC
		public bool CastToBool()
		{
			DynValue dynValue = this.ToScalar();
			if (dynValue.Type == DataType.Boolean)
			{
				return dynValue.Boolean;
			}
			return dynValue.Type != DataType.Nil && dynValue.Type != DataType.Void;
		}

		// Token: 0x06006537 RID: 25911 RVA: 0x00045A0D File Offset: 0x00043C0D
		public IScriptPrivateResource GetAsPrivateResource()
		{
			return this.m_Object as IScriptPrivateResource;
		}

		// Token: 0x06006538 RID: 25912 RVA: 0x00045A1A File Offset: 0x00043C1A
		public DynValue ToScalar()
		{
			if (this.Type != DataType.Tuple)
			{
				return this;
			}
			if (this.Tuple.Length == 0)
			{
				return DynValue.Void;
			}
			return this.Tuple[0].ToScalar();
		}

		// Token: 0x06006539 RID: 25913 RVA: 0x00045A43 File Offset: 0x00043C43
		public void Assign(DynValue value)
		{
			if (this.ReadOnly)
			{
				throw new ScriptRuntimeException("Assigning on r-value");
			}
			this.m_Number = value.m_Number;
			this.m_Object = value.m_Object;
			this.m_Type = value.Type;
			this.m_HashCode = -1;
		}

		// Token: 0x0600653A RID: 25914 RVA: 0x00282C38 File Offset: 0x00280E38
		public DynValue GetLength()
		{
			if (this.Type == DataType.Table)
			{
				return DynValue.NewNumber((double)this.Table.Length);
			}
			if (this.Type == DataType.String)
			{
				return DynValue.NewNumber((double)this.String.Length);
			}
			throw new ScriptRuntimeException("Can't get length of type {0}", new object[]
			{
				this.Type
			});
		}

		// Token: 0x0600653B RID: 25915 RVA: 0x00045A83 File Offset: 0x00043C83
		public bool IsNil()
		{
			return this.Type == DataType.Nil || this.Type == DataType.Void;
		}

		// Token: 0x0600653C RID: 25916 RVA: 0x00045A98 File Offset: 0x00043C98
		public bool IsNotNil()
		{
			return this.Type != DataType.Nil && this.Type != DataType.Void;
		}

		// Token: 0x0600653D RID: 25917 RVA: 0x00045AB0 File Offset: 0x00043CB0
		public bool IsVoid()
		{
			return this.Type == DataType.Void;
		}

		// Token: 0x0600653E RID: 25918 RVA: 0x00045ABB File Offset: 0x00043CBB
		public bool IsNotVoid()
		{
			return this.Type != DataType.Void;
		}

		// Token: 0x0600653F RID: 25919 RVA: 0x00045AC9 File Offset: 0x00043CC9
		public bool IsNilOrNan()
		{
			return this.Type == DataType.Nil || this.Type == DataType.Void || (this.Type == DataType.Number && double.IsNaN(this.Number));
		}

		// Token: 0x06006540 RID: 25920 RVA: 0x00282C9C File Offset: 0x00280E9C
		internal void AssignNumber(double num)
		{
			if (this.ReadOnly)
			{
				throw new InternalErrorException(null, new object[]
				{
					"Writing on r-value"
				});
			}
			if (this.Type != DataType.Number)
			{
				throw new InternalErrorException("Can't assign number to type {0}", new object[]
				{
					this.Type
				});
			}
			this.m_Number = num;
		}

		// Token: 0x06006541 RID: 25921 RVA: 0x00045AF4 File Offset: 0x00043CF4
		public static DynValue FromObject(Script script, object obj)
		{
			return ClrToScriptConversions.ObjectToDynValue(script, obj);
		}

		// Token: 0x06006542 RID: 25922 RVA: 0x00045AFD File Offset: 0x00043CFD
		public object ToObject()
		{
			return ScriptToClrConversions.DynValueToObject(this);
		}

		// Token: 0x06006543 RID: 25923 RVA: 0x00045B05 File Offset: 0x00043D05
		public object ToObject(Type desiredType)
		{
			return ScriptToClrConversions.DynValueToObjectOfType(this, desiredType, null, false);
		}

		// Token: 0x06006544 RID: 25924 RVA: 0x00045B10 File Offset: 0x00043D10
		public T ToObject<T>()
		{
			return (T)((object)this.ToObject(typeof(T)));
		}

		// Token: 0x06006545 RID: 25925 RVA: 0x00282CF8 File Offset: 0x00280EF8
		public DynValue CheckType(string funcName, DataType desiredType, int argNum = -1, TypeValidationFlags flags = TypeValidationFlags.AutoConvert)
		{
			if (this.Type == desiredType)
			{
				return this;
			}
			bool flag = (flags & TypeValidationFlags.AllowNil) > TypeValidationFlags.None;
			if (flag && this.IsNil())
			{
				return this;
			}
			if ((flags & TypeValidationFlags.AutoConvert) > TypeValidationFlags.None)
			{
				if (desiredType == DataType.Boolean)
				{
					return DynValue.NewBoolean(this.CastToBool());
				}
				if (desiredType == DataType.Number)
				{
					double? num = this.CastToNumber();
					if (num != null)
					{
						return DynValue.NewNumber(num.Value);
					}
				}
				if (desiredType == DataType.String)
				{
					string text = this.CastToString();
					if (text != null)
					{
						return DynValue.NewString(text);
					}
				}
			}
			if (this.IsVoid())
			{
				throw ScriptRuntimeException.BadArgumentNoValue(argNum, funcName, desiredType);
			}
			throw ScriptRuntimeException.BadArgument(argNum, funcName, desiredType, this.Type, flag);
		}

		// Token: 0x06006546 RID: 25926 RVA: 0x00282D94 File Offset: 0x00280F94
		public T CheckUserDataType<T>(string funcName, int argNum = -1, TypeValidationFlags flags = TypeValidationFlags.AutoConvert)
		{
			DynValue dynValue = this.CheckType(funcName, DataType.UserData, argNum, flags);
			bool allowNil = (flags & TypeValidationFlags.AllowNil) > TypeValidationFlags.None;
			if (dynValue.IsNil())
			{
				return default(T);
			}
			object @object = dynValue.UserData.Object;
			if (@object != null && @object is T)
			{
				return (T)((object)@object);
			}
			throw ScriptRuntimeException.BadArgumentUserData(argNum, funcName, typeof(T), @object, allowNil);
		}

		// Token: 0x04005E3C RID: 24124
		private static int s_RefIDCounter;

		// Token: 0x04005E3D RID: 24125
		private int m_RefID = ++DynValue.s_RefIDCounter;

		// Token: 0x04005E3E RID: 24126
		private int m_HashCode = -1;

		// Token: 0x04005E3F RID: 24127
		private bool m_ReadOnly;

		// Token: 0x04005E40 RID: 24128
		private double m_Number;

		// Token: 0x04005E41 RID: 24129
		private object m_Object;

		// Token: 0x04005E42 RID: 24130
		private DataType m_Type;
	}
}
