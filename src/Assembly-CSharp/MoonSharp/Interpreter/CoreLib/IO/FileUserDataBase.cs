using System;
using System.Collections.Generic;
using System.Linq;
using MoonSharp.Interpreter.Compatibility;

namespace MoonSharp.Interpreter.CoreLib.IO
{
	// Token: 0x020011AB RID: 4523
	internal abstract class FileUserDataBase : RefIdObject
	{
		// Token: 0x06006EAD RID: 28333 RVA: 0x0029F49C File Offset: 0x0029D69C
		public DynValue lines(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			List<DynValue> list = new List<DynValue>();
			DynValue dynValue;
			do
			{
				dynValue = this.read(executionContext, args);
				list.Add(dynValue);
			}
			while (dynValue.IsNotNil());
			return DynValue.FromObject(executionContext.GetScript(), from s in list
			select s);
		}

		// Token: 0x06006EAE RID: 28334 RVA: 0x0029F4FC File Offset: 0x0029D6FC
		public DynValue read(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			if (args.Count != 0)
			{
				List<DynValue> list = new List<DynValue>();
				for (int i = 0; i < args.Count; i++)
				{
					DynValue item;
					if (args[i].Type == DataType.Number)
					{
						if (this.Eof())
						{
							return DynValue.Nil;
						}
						int p = (int)args[i].Number;
						item = DynValue.NewString(this.ReadBuffer(p));
					}
					else
					{
						string @string = args.AsType(i, "read", DataType.String, false).String;
						if (this.Eof())
						{
							item = (@string.StartsWith("*a") ? DynValue.NewString("") : DynValue.Nil);
						}
						else if (@string.StartsWith("*n"))
						{
							double? num = this.ReadNumber();
							if (num != null)
							{
								item = DynValue.NewNumber(num.Value);
							}
							else
							{
								item = DynValue.Nil;
							}
						}
						else if (@string.StartsWith("*a"))
						{
							item = DynValue.NewString(this.ReadToEnd());
						}
						else if (@string.StartsWith("*l"))
						{
							item = DynValue.NewString(this.ReadLine().TrimEnd(new char[]
							{
								'\n',
								'\r'
							}));
						}
						else
						{
							if (!@string.StartsWith("*L"))
							{
								throw ScriptRuntimeException.BadArgument(i, "read", "invalid option");
							}
							item = DynValue.NewString(this.ReadLine().TrimEnd(new char[]
							{
								'\n',
								'\r'
							}) + "\n");
						}
					}
					list.Add(item);
				}
				return DynValue.NewTuple(list.ToArray());
			}
			string text = this.ReadLine();
			if (text == null)
			{
				return DynValue.Nil;
			}
			text = text.TrimEnd(new char[]
			{
				'\n',
				'\r'
			});
			return DynValue.NewString(text);
		}

		// Token: 0x06006EAF RID: 28335 RVA: 0x0029F6C4 File Offset: 0x0029D8C4
		public DynValue write(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue result;
			try
			{
				for (int i = 0; i < args.Count; i++)
				{
					string @string = args.AsType(i, "write", DataType.String, false).String;
					this.Write(@string);
				}
				result = UserData.Create(this);
			}
			catch (ScriptRuntimeException)
			{
				throw;
			}
			catch (Exception ex)
			{
				result = DynValue.NewTuple(new DynValue[]
				{
					DynValue.Nil,
					DynValue.NewString(ex.Message)
				});
			}
			return result;
		}

		// Token: 0x06006EB0 RID: 28336 RVA: 0x0029F74C File Offset: 0x0029D94C
		public DynValue close(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue result;
			try
			{
				string text = this.Close();
				if (text == null)
				{
					result = DynValue.True;
				}
				else
				{
					result = DynValue.NewTuple(new DynValue[]
					{
						DynValue.Nil,
						DynValue.NewString(text)
					});
				}
			}
			catch (ScriptRuntimeException)
			{
				throw;
			}
			catch (Exception ex)
			{
				result = DynValue.NewTuple(new DynValue[]
				{
					DynValue.Nil,
					DynValue.NewString(ex.Message)
				});
			}
			return result;
		}

		// Token: 0x06006EB1 RID: 28337 RVA: 0x0029F7D0 File Offset: 0x0029D9D0
		private double? ReadNumber()
		{
			string text = "";
			while (!this.Eof())
			{
				char c = this.Peek();
				if (char.IsWhiteSpace(c))
				{
					this.ReadBuffer(1);
				}
				else
				{
					if (!this.IsNumericChar(c, text))
					{
						break;
					}
					this.ReadBuffer(1);
					text += c.ToString();
				}
			}
			double value;
			if (double.TryParse(text, out value))
			{
				return new double?(value);
			}
			return null;
		}

		// Token: 0x06006EB2 RID: 28338 RVA: 0x0029F840 File Offset: 0x0029DA40
		private bool IsNumericChar(char c, string numAsFar)
		{
			if (char.IsDigit(c))
			{
				return true;
			}
			if (c == '-')
			{
				return numAsFar.Length == 0;
			}
			if (c == '.')
			{
				return !Framework.Do.StringContainsChar(numAsFar, '.');
			}
			return (c == 'E' || c == 'e') && !Framework.Do.StringContainsChar(numAsFar, 'E') && !Framework.Do.StringContainsChar(numAsFar, 'e');
		}

		// Token: 0x06006EB3 RID: 28339
		protected abstract bool Eof();

		// Token: 0x06006EB4 RID: 28340
		protected abstract string ReadLine();

		// Token: 0x06006EB5 RID: 28341
		protected abstract string ReadBuffer(int p);

		// Token: 0x06006EB6 RID: 28342
		protected abstract string ReadToEnd();

		// Token: 0x06006EB7 RID: 28343
		protected abstract char Peek();

		// Token: 0x06006EB8 RID: 28344
		protected abstract void Write(string value);

		// Token: 0x06006EB9 RID: 28345
		protected internal abstract bool isopen();

		// Token: 0x06006EBA RID: 28346
		protected abstract string Close();

		// Token: 0x06006EBB RID: 28347
		public abstract bool flush();

		// Token: 0x06006EBC RID: 28348
		public abstract long seek(string whence, long offset);

		// Token: 0x06006EBD RID: 28349
		public abstract bool setvbuf(string mode);

		// Token: 0x06006EBE RID: 28350 RVA: 0x0004B521 File Offset: 0x00049721
		public override string ToString()
		{
			if (this.isopen())
			{
				return string.Format("file ({0:X8})", base.ReferenceID);
			}
			return "file (closed)";
		}
	}
}
