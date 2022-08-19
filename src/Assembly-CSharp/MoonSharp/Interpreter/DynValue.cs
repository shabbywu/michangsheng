using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using MoonSharp.Interpreter.Interop.Converters;

namespace MoonSharp.Interpreter
{
	// Token: 0x02000C9D RID: 3229
	public sealed class DynValue
	{
		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06005A1C RID: 23068 RVA: 0x002574FC File Offset: 0x002556FC
		public int ReferenceID
		{
			get
			{
				return this.m_RefID;
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06005A1D RID: 23069 RVA: 0x00257504 File Offset: 0x00255704
		public DataType Type
		{
			get
			{
				return this.m_Type;
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06005A1E RID: 23070 RVA: 0x0025750C File Offset: 0x0025570C
		public Closure Function
		{
			get
			{
				return this.m_Object as Closure;
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06005A1F RID: 23071 RVA: 0x00257519 File Offset: 0x00255719
		public double Number
		{
			get
			{
				return this.m_Number;
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06005A20 RID: 23072 RVA: 0x00257521 File Offset: 0x00255721
		public DynValue[] Tuple
		{
			get
			{
				return this.m_Object as DynValue[];
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06005A21 RID: 23073 RVA: 0x0025752E File Offset: 0x0025572E
		public Coroutine Coroutine
		{
			get
			{
				return this.m_Object as Coroutine;
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06005A22 RID: 23074 RVA: 0x0025753B File Offset: 0x0025573B
		public Table Table
		{
			get
			{
				return this.m_Object as Table;
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06005A23 RID: 23075 RVA: 0x00257548 File Offset: 0x00255748
		public bool Boolean
		{
			get
			{
				return this.Number != 0.0;
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06005A24 RID: 23076 RVA: 0x0025755E File Offset: 0x0025575E
		public string String
		{
			get
			{
				return this.m_Object as string;
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06005A25 RID: 23077 RVA: 0x0025756B File Offset: 0x0025576B
		public CallbackFunction Callback
		{
			get
			{
				return this.m_Object as CallbackFunction;
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06005A26 RID: 23078 RVA: 0x00257578 File Offset: 0x00255778
		public TailCallData TailCallData
		{
			get
			{
				return this.m_Object as TailCallData;
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06005A27 RID: 23079 RVA: 0x00257585 File Offset: 0x00255785
		public YieldRequest YieldRequest
		{
			get
			{
				return this.m_Object as YieldRequest;
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06005A28 RID: 23080 RVA: 0x00257592 File Offset: 0x00255792
		public UserData UserData
		{
			get
			{
				return this.m_Object as UserData;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06005A29 RID: 23081 RVA: 0x0025759F File Offset: 0x0025579F
		public bool ReadOnly
		{
			get
			{
				return this.m_ReadOnly;
			}
		}

		// Token: 0x06005A2A RID: 23082 RVA: 0x002575A7 File Offset: 0x002557A7
		public static DynValue NewNil()
		{
			return new DynValue();
		}

		// Token: 0x06005A2B RID: 23083 RVA: 0x002575AE File Offset: 0x002557AE
		public static DynValue NewBoolean(bool v)
		{
			return new DynValue
			{
				m_Number = (double)(v ? 1 : 0),
				m_Type = DataType.Boolean
			};
		}

		// Token: 0x06005A2C RID: 23084 RVA: 0x002575CA File Offset: 0x002557CA
		public static DynValue NewNumber(double num)
		{
			return new DynValue
			{
				m_Number = num,
				m_Type = DataType.Number,
				m_HashCode = -1
			};
		}

		// Token: 0x06005A2D RID: 23085 RVA: 0x002575E6 File Offset: 0x002557E6
		public static DynValue NewString(string str)
		{
			return new DynValue
			{
				m_Object = str,
				m_Type = DataType.String
			};
		}

		// Token: 0x06005A2E RID: 23086 RVA: 0x002575FB File Offset: 0x002557FB
		public static DynValue NewString(StringBuilder sb)
		{
			return new DynValue
			{
				m_Object = sb.ToString(),
				m_Type = DataType.String
			};
		}

		// Token: 0x06005A2F RID: 23087 RVA: 0x00257615 File Offset: 0x00255815
		public static DynValue NewString(string format, params object[] args)
		{
			return new DynValue
			{
				m_Object = string.Format(format, args),
				m_Type = DataType.String
			};
		}

		// Token: 0x06005A30 RID: 23088 RVA: 0x00257630 File Offset: 0x00255830
		public static DynValue NewCoroutine(Coroutine coroutine)
		{
			return new DynValue
			{
				m_Object = coroutine,
				m_Type = DataType.Thread
			};
		}

		// Token: 0x06005A31 RID: 23089 RVA: 0x00257646 File Offset: 0x00255846
		public static DynValue NewClosure(Closure function)
		{
			return new DynValue
			{
				m_Object = function,
				m_Type = DataType.Function
			};
		}

		// Token: 0x06005A32 RID: 23090 RVA: 0x0025765B File Offset: 0x0025585B
		public static DynValue NewCallback(Func<ScriptExecutionContext, CallbackArguments, DynValue> callBack, string name = null)
		{
			return new DynValue
			{
				m_Object = new CallbackFunction(callBack, name),
				m_Type = DataType.ClrFunction
			};
		}

		// Token: 0x06005A33 RID: 23091 RVA: 0x00257677 File Offset: 0x00255877
		public static DynValue NewCallback(CallbackFunction function)
		{
			return new DynValue
			{
				m_Object = function,
				m_Type = DataType.ClrFunction
			};
		}

		// Token: 0x06005A34 RID: 23092 RVA: 0x0025768D File Offset: 0x0025588D
		public static DynValue NewTable(Table table)
		{
			return new DynValue
			{
				m_Object = table,
				m_Type = DataType.Table
			};
		}

		// Token: 0x06005A35 RID: 23093 RVA: 0x002576A2 File Offset: 0x002558A2
		public static DynValue NewPrimeTable()
		{
			return DynValue.NewTable(new Table(null));
		}

		// Token: 0x06005A36 RID: 23094 RVA: 0x002576AF File Offset: 0x002558AF
		public static DynValue NewTable(Script script)
		{
			return DynValue.NewTable(new Table(script));
		}

		// Token: 0x06005A37 RID: 23095 RVA: 0x002576BC File Offset: 0x002558BC
		public static DynValue NewTable(Script script, params DynValue[] arrayValues)
		{
			return DynValue.NewTable(new Table(script, arrayValues));
		}

		// Token: 0x06005A38 RID: 23096 RVA: 0x002576CA File Offset: 0x002558CA
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

		// Token: 0x06005A39 RID: 23097 RVA: 0x002576F2 File Offset: 0x002558F2
		public static DynValue NewTailCallReq(TailCallData tailCallData)
		{
			return new DynValue
			{
				m_Object = tailCallData,
				m_Type = DataType.TailCallRequest
			};
		}

		// Token: 0x06005A3A RID: 23098 RVA: 0x00257708 File Offset: 0x00255908
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

		// Token: 0x06005A3B RID: 23099 RVA: 0x00257729 File Offset: 0x00255929
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

		// Token: 0x06005A3C RID: 23100 RVA: 0x0025774A File Offset: 0x0025594A
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

		// Token: 0x06005A3D RID: 23101 RVA: 0x00257774 File Offset: 0x00255974
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

		// Token: 0x06005A3E RID: 23102 RVA: 0x00257809 File Offset: 0x00255A09
		public static DynValue NewUserData(UserData userData)
		{
			return new DynValue
			{
				m_Object = userData,
				m_Type = DataType.UserData
			};
		}

		// Token: 0x06005A3F RID: 23103 RVA: 0x0025781E File Offset: 0x00255A1E
		public DynValue AsReadOnly()
		{
			if (this.ReadOnly)
			{
				return this;
			}
			return this.Clone(true);
		}

		// Token: 0x06005A40 RID: 23104 RVA: 0x00257831 File Offset: 0x00255A31
		public DynValue Clone()
		{
			return this.Clone(this.ReadOnly);
		}

		// Token: 0x06005A41 RID: 23105 RVA: 0x0025783F File Offset: 0x00255A3F
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

		// Token: 0x06005A42 RID: 23106 RVA: 0x0025787D File Offset: 0x00255A7D
		public DynValue CloneAsWritable()
		{
			return this.Clone(false);
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06005A43 RID: 23107 RVA: 0x00257886 File Offset: 0x00255A86
		// (set) Token: 0x06005A44 RID: 23108 RVA: 0x0025788D File Offset: 0x00255A8D
		public static DynValue Void { get; private set; }

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06005A45 RID: 23109 RVA: 0x00257895 File Offset: 0x00255A95
		// (set) Token: 0x06005A46 RID: 23110 RVA: 0x0025789C File Offset: 0x00255A9C
		public static DynValue Nil { get; private set; } = new DynValue
		{
			m_Type = DataType.Nil
		}.AsReadOnly();

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06005A47 RID: 23111 RVA: 0x002578A4 File Offset: 0x00255AA4
		// (set) Token: 0x06005A48 RID: 23112 RVA: 0x002578AB File Offset: 0x00255AAB
		public static DynValue True { get; private set; }

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06005A49 RID: 23113 RVA: 0x002578B3 File Offset: 0x00255AB3
		// (set) Token: 0x06005A4A RID: 23114 RVA: 0x002578BA File Offset: 0x00255ABA
		public static DynValue False { get; private set; }

		// Token: 0x06005A4B RID: 23115 RVA: 0x002578C4 File Offset: 0x00255AC4
		static DynValue()
		{
			DynValue.Void = new DynValue
			{
				m_Type = DataType.Void
			}.AsReadOnly();
			DynValue.True = DynValue.NewBoolean(true).AsReadOnly();
			DynValue.False = DynValue.NewBoolean(false).AsReadOnly();
		}

		// Token: 0x06005A4C RID: 23116 RVA: 0x00257920 File Offset: 0x00255B20
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

		// Token: 0x06005A4D RID: 23117 RVA: 0x00257A0C File Offset: 0x00255C0C
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

		// Token: 0x06005A4E RID: 23118 RVA: 0x00257AE8 File Offset: 0x00255CE8
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

		// Token: 0x06005A4F RID: 23119 RVA: 0x00257C5C File Offset: 0x00255E5C
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

		// Token: 0x06005A50 RID: 23120 RVA: 0x00257D7C File Offset: 0x00255F7C
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

		// Token: 0x06005A51 RID: 23121 RVA: 0x00257EE8 File Offset: 0x002560E8
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

		// Token: 0x06005A52 RID: 23122 RVA: 0x00257F28 File Offset: 0x00256128
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

		// Token: 0x06005A53 RID: 23123 RVA: 0x00257F84 File Offset: 0x00256184
		public bool CastToBool()
		{
			DynValue dynValue = this.ToScalar();
			if (dynValue.Type == DataType.Boolean)
			{
				return dynValue.Boolean;
			}
			return dynValue.Type != DataType.Nil && dynValue.Type != DataType.Void;
		}

		// Token: 0x06005A54 RID: 23124 RVA: 0x00257FBE File Offset: 0x002561BE
		public IScriptPrivateResource GetAsPrivateResource()
		{
			return this.m_Object as IScriptPrivateResource;
		}

		// Token: 0x06005A55 RID: 23125 RVA: 0x00257FCB File Offset: 0x002561CB
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

		// Token: 0x06005A56 RID: 23126 RVA: 0x00257FF4 File Offset: 0x002561F4
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

		// Token: 0x06005A57 RID: 23127 RVA: 0x00258034 File Offset: 0x00256234
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

		// Token: 0x06005A58 RID: 23128 RVA: 0x00258095 File Offset: 0x00256295
		public bool IsNil()
		{
			return this.Type == DataType.Nil || this.Type == DataType.Void;
		}

		// Token: 0x06005A59 RID: 23129 RVA: 0x002580AA File Offset: 0x002562AA
		public bool IsNotNil()
		{
			return this.Type != DataType.Nil && this.Type != DataType.Void;
		}

		// Token: 0x06005A5A RID: 23130 RVA: 0x002580C2 File Offset: 0x002562C2
		public bool IsVoid()
		{
			return this.Type == DataType.Void;
		}

		// Token: 0x06005A5B RID: 23131 RVA: 0x002580CD File Offset: 0x002562CD
		public bool IsNotVoid()
		{
			return this.Type != DataType.Void;
		}

		// Token: 0x06005A5C RID: 23132 RVA: 0x002580DB File Offset: 0x002562DB
		public bool IsNilOrNan()
		{
			return this.Type == DataType.Nil || this.Type == DataType.Void || (this.Type == DataType.Number && double.IsNaN(this.Number));
		}

		// Token: 0x06005A5D RID: 23133 RVA: 0x00258108 File Offset: 0x00256308
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

		// Token: 0x06005A5E RID: 23134 RVA: 0x00258161 File Offset: 0x00256361
		public static DynValue FromObject(Script script, object obj)
		{
			return ClrToScriptConversions.ObjectToDynValue(script, obj);
		}

		// Token: 0x06005A5F RID: 23135 RVA: 0x0025816A File Offset: 0x0025636A
		public object ToObject()
		{
			return ScriptToClrConversions.DynValueToObject(this);
		}

		// Token: 0x06005A60 RID: 23136 RVA: 0x00258172 File Offset: 0x00256372
		public object ToObject(Type desiredType)
		{
			return ScriptToClrConversions.DynValueToObjectOfType(this, desiredType, null, false);
		}

		// Token: 0x06005A61 RID: 23137 RVA: 0x0025817D File Offset: 0x0025637D
		public T ToObject<T>()
		{
			return (T)((object)this.ToObject(typeof(T)));
		}

		// Token: 0x06005A62 RID: 23138 RVA: 0x00258194 File Offset: 0x00256394
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

		// Token: 0x06005A63 RID: 23139 RVA: 0x00258230 File Offset: 0x00256430
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

		// Token: 0x04005273 RID: 21107
		private static int s_RefIDCounter;

		// Token: 0x04005274 RID: 21108
		private int m_RefID = ++DynValue.s_RefIDCounter;

		// Token: 0x04005275 RID: 21109
		private int m_HashCode = -1;

		// Token: 0x04005276 RID: 21110
		private bool m_ReadOnly;

		// Token: 0x04005277 RID: 21111
		private double m_Number;

		// Token: 0x04005278 RID: 21112
		private object m_Object;

		// Token: 0x04005279 RID: 21113
		private DataType m_Type;
	}
}
