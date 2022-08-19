using System;
using System.Collections.Generic;
using System.Linq;
using MoonSharp.Interpreter.Compatibility;

namespace MoonSharp.Interpreter.CoreLib.IO
{
	// Token: 0x02000D89 RID: 3465
	internal abstract class FileUserDataBase : RefIdObject
	{
		// Token: 0x0600627F RID: 25215 RVA: 0x002794D0 File Offset: 0x002776D0
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

		// Token: 0x06006280 RID: 25216 RVA: 0x00279530 File Offset: 0x00277730
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

		// Token: 0x06006281 RID: 25217 RVA: 0x002796F8 File Offset: 0x002778F8
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

		// Token: 0x06006282 RID: 25218 RVA: 0x00279780 File Offset: 0x00277980
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

		// Token: 0x06006283 RID: 25219 RVA: 0x00279804 File Offset: 0x00277A04
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

		// Token: 0x06006284 RID: 25220 RVA: 0x00279874 File Offset: 0x00277A74
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

		// Token: 0x06006285 RID: 25221
		protected abstract bool Eof();

		// Token: 0x06006286 RID: 25222
		protected abstract string ReadLine();

		// Token: 0x06006287 RID: 25223
		protected abstract string ReadBuffer(int p);

		// Token: 0x06006288 RID: 25224
		protected abstract string ReadToEnd();

		// Token: 0x06006289 RID: 25225
		protected abstract char Peek();

		// Token: 0x0600628A RID: 25226
		protected abstract void Write(string value);

		// Token: 0x0600628B RID: 25227
		protected internal abstract bool isopen();

		// Token: 0x0600628C RID: 25228
		protected abstract string Close();

		// Token: 0x0600628D RID: 25229
		public abstract bool flush();

		// Token: 0x0600628E RID: 25230
		public abstract long seek(string whence, long offset);

		// Token: 0x0600628F RID: 25231
		public abstract bool setvbuf(string mode);

		// Token: 0x06006290 RID: 25232 RVA: 0x002798DD File Offset: 0x00277ADD
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
