using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.CoreLib.IO;
using MoonSharp.Interpreter.Platforms;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x02000D79 RID: 3449
	[MoonSharpModule(Namespace = "io")]
	public class IoModule
	{
		// Token: 0x060061BF RID: 25023 RVA: 0x00274F0C File Offset: 0x0027310C
		public static void MoonSharpInit(Table globalTable, Table ioTable)
		{
			UserData.RegisterType<FileUserDataBase>(InteropAccessMode.Default, "file");
			Table table = new Table(ioTable.OwnerScript);
			DynValue value = DynValue.NewCallback(new CallbackFunction(new Func<ScriptExecutionContext, CallbackArguments, DynValue>(IoModule.__index_callback), "__index_callback"));
			table.Set("__index", value);
			ioTable.MetaTable = table;
			IoModule.SetStandardFile(globalTable.OwnerScript, StandardFileType.StdIn, globalTable.OwnerScript.Options.Stdin);
			IoModule.SetStandardFile(globalTable.OwnerScript, StandardFileType.StdOut, globalTable.OwnerScript.Options.Stdout);
			IoModule.SetStandardFile(globalTable.OwnerScript, StandardFileType.StdErr, globalTable.OwnerScript.Options.Stderr);
		}

		// Token: 0x060061C0 RID: 25024 RVA: 0x00274FB4 File Offset: 0x002731B4
		private static DynValue __index_callback(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			string a = args[1].CastToString();
			if (a == "stdin")
			{
				return IoModule.GetStandardFile(executionContext.GetScript(), StandardFileType.StdIn);
			}
			if (a == "stdout")
			{
				return IoModule.GetStandardFile(executionContext.GetScript(), StandardFileType.StdOut);
			}
			if (a == "stderr")
			{
				return IoModule.GetStandardFile(executionContext.GetScript(), StandardFileType.StdErr);
			}
			return DynValue.Nil;
		}

		// Token: 0x060061C1 RID: 25025 RVA: 0x00275021 File Offset: 0x00273221
		private static DynValue GetStandardFile(Script S, StandardFileType file)
		{
			return S.Registry.Get("853BEAAF298648839E2C99D005E1DF94_STD_" + file.ToString());
		}

		// Token: 0x060061C2 RID: 25026 RVA: 0x00275048 File Offset: 0x00273248
		private static void SetStandardFile(Script S, StandardFileType file, Stream optionsStream)
		{
			Table registry = S.Registry;
			optionsStream = (optionsStream ?? Script.GlobalOptions.Platform.IO_GetStandardStream(file));
			FileUserDataBase o;
			if (file == StandardFileType.StdIn)
			{
				o = StandardIOFileUserDataBase.CreateInputStream(optionsStream);
			}
			else
			{
				o = StandardIOFileUserDataBase.CreateOutputStream(optionsStream);
			}
			registry.Set("853BEAAF298648839E2C99D005E1DF94_STD_" + file.ToString(), UserData.Create(o));
		}

		// Token: 0x060061C3 RID: 25027 RVA: 0x002750AC File Offset: 0x002732AC
		private static FileUserDataBase GetDefaultFile(ScriptExecutionContext executionContext, StandardFileType file)
		{
			DynValue dynValue = executionContext.GetScript().Registry.Get("853BEAAF298648839E2C99D005E1DF94_" + file.ToString());
			if (dynValue.IsNil())
			{
				dynValue = IoModule.GetStandardFile(executionContext.GetScript(), file);
			}
			return dynValue.CheckUserDataType<FileUserDataBase>("getdefaultfile(" + file.ToString() + ")", -1, TypeValidationFlags.AutoConvert);
		}

		// Token: 0x060061C4 RID: 25028 RVA: 0x0027511A File Offset: 0x0027331A
		private static void SetDefaultFile(ScriptExecutionContext executionContext, StandardFileType file, FileUserDataBase fileHandle)
		{
			IoModule.SetDefaultFile(executionContext.GetScript(), file, fileHandle);
		}

		// Token: 0x060061C5 RID: 25029 RVA: 0x00275129 File Offset: 0x00273329
		internal static void SetDefaultFile(Script script, StandardFileType file, FileUserDataBase fileHandle)
		{
			script.Registry.Set("853BEAAF298648839E2C99D005E1DF94_" + file.ToString(), UserData.Create(fileHandle));
		}

		// Token: 0x060061C6 RID: 25030 RVA: 0x00275153 File Offset: 0x00273353
		public static void SetDefaultFile(Script script, StandardFileType file, Stream stream)
		{
			if (file == StandardFileType.StdIn)
			{
				IoModule.SetDefaultFile(script, file, StandardIOFileUserDataBase.CreateInputStream(stream));
				return;
			}
			IoModule.SetDefaultFile(script, file, StandardIOFileUserDataBase.CreateOutputStream(stream));
		}

		// Token: 0x060061C7 RID: 25031 RVA: 0x00275173 File Offset: 0x00273373
		[MoonSharpModuleMethod]
		public static DynValue close(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return (args.AsUserData<FileUserDataBase>(0, "close", true) ?? IoModule.GetDefaultFile(executionContext, StandardFileType.StdOut)).close(executionContext, args);
		}

		// Token: 0x060061C8 RID: 25032 RVA: 0x00275194 File Offset: 0x00273394
		[MoonSharpModuleMethod]
		public static DynValue flush(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			(args.AsUserData<FileUserDataBase>(0, "close", true) ?? IoModule.GetDefaultFile(executionContext, StandardFileType.StdOut)).flush();
			return DynValue.True;
		}

		// Token: 0x060061C9 RID: 25033 RVA: 0x002751B9 File Offset: 0x002733B9
		[MoonSharpModuleMethod]
		public static DynValue input(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return IoModule.HandleDefaultStreamSetter(executionContext, args, StandardFileType.StdIn);
		}

		// Token: 0x060061CA RID: 25034 RVA: 0x002751C3 File Offset: 0x002733C3
		[MoonSharpModuleMethod]
		public static DynValue output(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return IoModule.HandleDefaultStreamSetter(executionContext, args, StandardFileType.StdOut);
		}

		// Token: 0x060061CB RID: 25035 RVA: 0x002751D0 File Offset: 0x002733D0
		private static DynValue HandleDefaultStreamSetter(ScriptExecutionContext executionContext, CallbackArguments args, StandardFileType defaultFiles)
		{
			if (args.Count == 0 || args[0].IsNil())
			{
				return UserData.Create(IoModule.GetDefaultFile(executionContext, defaultFiles));
			}
			FileUserDataBase fileUserDataBase;
			if (args[0].Type == DataType.String || args[0].Type == DataType.Number)
			{
				string filename = args[0].CastToString();
				fileUserDataBase = IoModule.Open(executionContext, filename, IoModule.GetUTF8Encoding(), (defaultFiles == StandardFileType.StdIn) ? "r" : "w");
			}
			else
			{
				fileUserDataBase = args.AsUserData<FileUserDataBase>(0, (defaultFiles == StandardFileType.StdIn) ? "input" : "output", false);
			}
			IoModule.SetDefaultFile(executionContext, defaultFiles, fileUserDataBase);
			return UserData.Create(fileUserDataBase);
		}

		// Token: 0x060061CC RID: 25036 RVA: 0x00275271 File Offset: 0x00273471
		private static Encoding GetUTF8Encoding()
		{
			return new UTF8Encoding(false);
		}

		// Token: 0x060061CD RID: 25037 RVA: 0x0027527C File Offset: 0x0027347C
		[MoonSharpModuleMethod]
		public static DynValue lines(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			string @string = args.AsType(0, "lines", DataType.String, false).String;
			DynValue result;
			try
			{
				List<DynValue> list = new List<DynValue>();
				using (Stream stream = Script.GlobalOptions.Platform.IO_OpenFile(executionContext.GetScript(), @string, null, "r"))
				{
					using (StreamReader streamReader = new StreamReader(stream))
					{
						while (!streamReader.EndOfStream)
						{
							string str = streamReader.ReadLine();
							list.Add(DynValue.NewString(str));
						}
					}
				}
				list.Add(DynValue.Nil);
				result = DynValue.FromObject(executionContext.GetScript(), from s in list
				select s);
			}
			catch (Exception ex)
			{
				throw new ScriptRuntimeException(IoModule.IoExceptionToLuaMessage(ex, @string));
			}
			return result;
		}

		// Token: 0x060061CE RID: 25038 RVA: 0x00275374 File Offset: 0x00273574
		[MoonSharpModuleMethod]
		public static DynValue open(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			string @string = args.AsType(0, "open", DataType.String, false).String;
			DynValue dynValue = args.AsType(1, "open", DataType.String, true);
			DynValue dynValue2 = args.AsType(2, "open", DataType.String, true);
			string text = dynValue.IsNil() ? "r" : dynValue.String;
			if (text.Replace("+", "").Replace("r", "").Replace("a", "").Replace("w", "").Replace("b", "").Replace("t", "").Length > 0)
			{
				throw ScriptRuntimeException.BadArgument(1, "open", "invalid mode");
			}
			DynValue result;
			try
			{
				string text2 = dynValue2.IsNil() ? null : dynValue2.String;
				bool flag = Framework.Do.StringContainsChar(text, 'b');
				Encoding encoding;
				if (text2 == "binary")
				{
					encoding = new BinaryEncoding();
				}
				else if (text2 == null)
				{
					if (!flag)
					{
						encoding = IoModule.GetUTF8Encoding();
					}
					else
					{
						encoding = new BinaryEncoding();
					}
				}
				else
				{
					if (flag)
					{
						throw new ScriptRuntimeException("Can't specify encodings other than nil or 'binary' for binary streams.");
					}
					encoding = Encoding.GetEncoding(text2);
				}
				result = UserData.Create(IoModule.Open(executionContext, @string, encoding, text));
			}
			catch (Exception ex)
			{
				result = DynValue.NewTuple(new DynValue[]
				{
					DynValue.Nil,
					DynValue.NewString(IoModule.IoExceptionToLuaMessage(ex, @string))
				});
			}
			return result;
		}

		// Token: 0x060061CF RID: 25039 RVA: 0x00275500 File Offset: 0x00273700
		public static string IoExceptionToLuaMessage(Exception ex, string filename)
		{
			if (ex is FileNotFoundException)
			{
				return string.Format("{0}: No such file or directory", filename);
			}
			return ex.Message;
		}

		// Token: 0x060061D0 RID: 25040 RVA: 0x0027551C File Offset: 0x0027371C
		[MoonSharpModuleMethod]
		public static DynValue type(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			if (args[0].Type != DataType.UserData)
			{
				return DynValue.Nil;
			}
			FileUserDataBase fileUserDataBase = args[0].UserData.Object as FileUserDataBase;
			if (fileUserDataBase == null)
			{
				return DynValue.Nil;
			}
			if (fileUserDataBase.isopen())
			{
				return DynValue.NewString("file");
			}
			return DynValue.NewString("closed file");
		}

		// Token: 0x060061D1 RID: 25041 RVA: 0x0027557B File Offset: 0x0027377B
		[MoonSharpModuleMethod]
		public static DynValue read(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return IoModule.GetDefaultFile(executionContext, StandardFileType.StdIn).read(executionContext, args);
		}

		// Token: 0x060061D2 RID: 25042 RVA: 0x0027558B File Offset: 0x0027378B
		[MoonSharpModuleMethod]
		public static DynValue write(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return IoModule.GetDefaultFile(executionContext, StandardFileType.StdOut).write(executionContext, args);
		}

		// Token: 0x060061D3 RID: 25043 RVA: 0x0027559C File Offset: 0x0027379C
		[MoonSharpModuleMethod]
		public static DynValue tmpfile(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			string filename = Script.GlobalOptions.Platform.IO_OS_GetTempFilename();
			return UserData.Create(IoModule.Open(executionContext, filename, IoModule.GetUTF8Encoding(), "w"));
		}

		// Token: 0x060061D4 RID: 25044 RVA: 0x002755CF File Offset: 0x002737CF
		private static FileUserDataBase Open(ScriptExecutionContext executionContext, string filename, Encoding encoding, string mode)
		{
			return new FileUserData(executionContext.GetScript(), filename, encoding, mode);
		}
	}
}
