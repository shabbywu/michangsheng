using System;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000D17 RID: 3351
	public struct ReflectionSpecialName
	{
		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06005DAE RID: 23982 RVA: 0x00263547 File Offset: 0x00261747
		// (set) Token: 0x06005DAF RID: 23983 RVA: 0x0026354F File Offset: 0x0026174F
		public ReflectionSpecialNameType Type { get; private set; }

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06005DB0 RID: 23984 RVA: 0x00263558 File Offset: 0x00261758
		// (set) Token: 0x06005DB1 RID: 23985 RVA: 0x00263560 File Offset: 0x00261760
		public string Argument { get; private set; }

		// Token: 0x06005DB2 RID: 23986 RVA: 0x00263569 File Offset: 0x00261769
		public ReflectionSpecialName(ReflectionSpecialNameType type, string argument = null)
		{
			this = default(ReflectionSpecialName);
			this.Type = type;
			this.Argument = argument;
		}

		// Token: 0x06005DB3 RID: 23987 RVA: 0x00263580 File Offset: 0x00261780
		public ReflectionSpecialName(string name)
		{
			this = default(ReflectionSpecialName);
			if (name.Contains("."))
			{
				string[] array = name.Split(new char[]
				{
					'.'
				});
				name = array[array.Length - 1];
			}
			uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
			if (num <= 2366795836U)
			{
				if (num <= 1195761148U)
				{
					if (num <= 835846267U)
					{
						if (num != 90588446U)
						{
							if (num != 215197780U)
							{
								if (num == 835846267U)
								{
									if (name == "op_BitwiseAnd")
									{
										this.Type = ReflectionSpecialNameType.OperatorAnd;
										this.Argument = "&";
										return;
									}
								}
							}
							else if (name == "op_Implicit")
							{
								this.Type = ReflectionSpecialNameType.ImplicitCast;
								return;
							}
						}
						else if (name == "op_OnesComplement")
						{
							this.Type = ReflectionSpecialNameType.OperatorCompl;
							this.Argument = "~";
							return;
						}
					}
					else if (num != 906583475U)
					{
						if (num != 1034931220U)
						{
							if (num == 1195761148U)
							{
								if (name == "op_GreaterThan")
								{
									this.Type = ReflectionSpecialNameType.OperatorGt;
									this.Argument = ">";
									return;
								}
							}
						}
						else if (name == "op_Increment")
						{
							this.Type = ReflectionSpecialNameType.OperatorInc;
							this.Argument = "++";
							return;
						}
					}
					else if (name == "op_Addition")
					{
						this.Type = ReflectionSpecialNameType.OperatorAdd;
						this.Argument = "+";
						return;
					}
				}
				else if (num <= 1516143579U)
				{
					if (num != 1234170120U)
					{
						if (num != 1258540185U)
						{
							if (num == 1516143579U)
							{
								if (name == "op_Equality")
								{
									this.Type = ReflectionSpecialNameType.OperatorEq;
									this.Argument = "==";
									return;
								}
							}
						}
						else if (name == "op_LessThan")
						{
							this.Type = ReflectionSpecialNameType.OperatorLt;
							this.Argument = "<";
							return;
						}
					}
					else if (name == "op_LessThanOrEqual")
					{
						this.Type = ReflectionSpecialNameType.OperatorLte;
						this.Argument = "<=";
						return;
					}
				}
				else if (num <= 1850069070U)
				{
					if (num != 1587019679U)
					{
						if (num == 1850069070U)
						{
							if (name == "op_False")
							{
								this.Type = ReflectionSpecialNameType.OperatorFalse;
								return;
							}
						}
					}
					else if (name == "op_Explicit")
					{
						this.Type = ReflectionSpecialNameType.ExplicitCast;
						return;
					}
				}
				else if (num != 1915672496U)
				{
					if (num == 2366795836U)
					{
						if (name == "op_ExclusiveOr")
						{
							this.Type = ReflectionSpecialNameType.OperatorXor;
							this.Argument = "^";
							return;
						}
					}
				}
				else if (name == "op_Division")
				{
					this.Type = ReflectionSpecialNameType.OperatorDiv;
					this.Argument = "/";
					return;
				}
			}
			else if (num <= 2958252495U)
			{
				if (num <= 2536726348U)
				{
					if (num != 2429678952U)
					{
						if (num != 2459852411U)
						{
							if (num == 2536726348U)
							{
								if (name == "op_Decrement")
								{
									this.Type = ReflectionSpecialNameType.OperatorDec;
									this.Argument = "--";
									return;
								}
							}
						}
						else if (name == "op_GreaterThanOrEqual")
						{
							this.Type = ReflectionSpecialNameType.OperatorGte;
							this.Argument = ">=";
							return;
						}
					}
					else if (name == "op_Modulus")
					{
						this.Type = ReflectionSpecialNameType.OperatorMod;
						this.Argument = "%";
						return;
					}
				}
				else if (num != 2574677899U)
				{
					if (num != 2772785643U)
					{
						if (num == 2958252495U)
						{
							if (name == "op_Multiply")
							{
								this.Type = ReflectionSpecialNameType.OperatorMul;
								this.Argument = "*";
								return;
							}
						}
					}
					else if (name == "set_Item")
					{
						this.Type = ReflectionSpecialNameType.IndexSetter;
						return;
					}
				}
				else if (name == "op_LogicalNot")
				{
					this.Type = ReflectionSpecialNameType.OperatorNot;
					this.Argument = "!";
					return;
				}
			}
			else if (num <= 3492550567U)
			{
				if (num != 3075696130U)
				{
					if (num != 3279419199U)
					{
						if (num == 3492550567U)
						{
							if (name == "op_BitwiseOr")
							{
								this.Type = ReflectionSpecialNameType.OperatorOr;
								this.Argument = "|";
								return;
							}
						}
					}
					else if (name == "op_Subtraction")
					{
						this.Type = ReflectionSpecialNameType.OperatorSub;
						this.Argument = "-";
						return;
					}
				}
				else if (name == "op_UnaryPlus")
				{
					this.Type = ReflectionSpecialNameType.OperatorUnaryPlus;
					this.Argument = "+";
					return;
				}
			}
			else if (num <= 3716665893U)
			{
				if (num != 3568900899U)
				{
					if (num == 3716665893U)
					{
						if (name == "op_UnaryNegation")
						{
							this.Type = ReflectionSpecialNameType.OperatorNeg;
							this.Argument = "-";
							return;
						}
					}
				}
				else if (name == "op_True")
				{
					this.Type = ReflectionSpecialNameType.OperatorTrue;
					return;
				}
			}
			else if (num != 3794317784U)
			{
				if (num == 4147557959U)
				{
					if (name == "get_Item")
					{
						this.Type = ReflectionSpecialNameType.IndexGetter;
						return;
					}
				}
			}
			else if (name == "op_Inequality")
			{
				this.Type = ReflectionSpecialNameType.OperatorNeq;
				this.Argument = "!=";
				return;
			}
			if (name.StartsWith("get_"))
			{
				this.Type = ReflectionSpecialNameType.PropertyGetter;
				this.Argument = name.Substring(4);
				return;
			}
			if (name.StartsWith("set_"))
			{
				this.Type = ReflectionSpecialNameType.PropertySetter;
				this.Argument = name.Substring(4);
				return;
			}
			if (name.StartsWith("add_"))
			{
				this.Type = ReflectionSpecialNameType.AddEvent;
				this.Argument = name.Substring(4);
				return;
			}
			if (name.StartsWith("remove_"))
			{
				this.Type = ReflectionSpecialNameType.RemoveEvent;
				this.Argument = name.Substring(7);
			}
		}
	}
}
