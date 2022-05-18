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
	// Token: 0x02001193 RID: 4499
	[MoonSharpModule(Namespace = "io")]
	public class IoModule
	{
		// Token: 0x06006DC6 RID: 28102 RVA: 0x0029B69C File Offset: 0x0029989C
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

		// Token: 0x06006DC7 RID: 28103 RVA: 0x0029B744 File Offset: 0x00299944
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

		// Token: 0x06006DC8 RID: 28104 RVA: 0x0004ABDD File Offset: 0x00048DDD
		private static DynValue GetStandardFile(Script S, StandardFileType file)
		{
			return S.Registry.Get("853BEAAF298648839E2C99D005E1DF94_STD_" + file.ToString());
		}

		// Token: 0x06006DC9 RID: 28105 RVA: 0x0029B7B4 File Offset: 0x002999B4
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

		// Token: 0x06006DCA RID: 28106 RVA: 0x0029B818 File Offset: 0x00299A18
		private static FileUserDataBase GetDefaultFile(ScriptExecutionContext executionContext, StandardFileType file)
		{
			DynValue dynValue = executionContext.GetScript().Registry.Get("853BEAAF298648839E2C99D005E1DF94_" + file.ToString());
			if (dynValue.IsNil())
			{
				dynValue = IoModule.GetStandardFile(executionContext.GetScript(), file);
			}
			return dynValue.CheckUserDataType<FileUserDataBase>("getdefaultfile(" + file.ToString() + ")", -1, TypeValidationFlags.AutoConvert);
		}

		// Token: 0x06006DCB RID: 28107 RVA: 0x0004AC01 File Offset: 0x00048E01
		private static void SetDefaultFile(ScriptExecutionContext executionContext, StandardFileType file, FileUserDataBase fileHandle)
		{
			IoModule.SetDefaultFile(executionContext.GetScript(), file, fileHandle);
		}

		// Token: 0x06006DCC RID: 28108 RVA: 0x0004AC10 File Offset: 0x00048E10
		internal static void SetDefaultFile(Script script, StandardFileType file, FileUserDataBase fileHandle)
		{
			script.Registry.Set("853BEAAF298648839E2C99D005E1DF94_" + file.ToString(), UserData.Create(fileHandle));
		}

		// Token: 0x06006DCD RID: 28109 RVA: 0x0004AC3A File Offset: 0x00048E3A
		public static void SetDefaultFile(Script script, StandardFileType file, Stream stream)
		{
			if (file == StandardFileType.StdIn)
			{
				IoModule.SetDefaultFile(script, file, StandardIOFileUserDataBase.CreateInputStream(stream));
				return;
			}
			IoModule.SetDefaultFile(script, file, StandardIOFileUserDataBase.CreateOutputStream(stream));
		}

		// Token: 0x06006DCE RID: 28110 RVA: 0x0004AC5A File Offset: 0x00048E5A
		[MoonSharpModuleMethod]
		public static DynValue close(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return (args.AsUserData<FileUserDataBase>(0, "close", true) ?? IoModule.GetDefaultFile(executionContext, StandardFileType.StdOut)).close(executionContext, args);
		}

		// Token: 0x06006DCF RID: 28111 RVA: 0x0004AC7B File Offset: 0x00048E7B
		[MoonSharpModuleMethod]
		public static DynValue flush(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			(args.AsUserData<FileUserDataBase>(0, "close", true) ?? IoModule.GetDefaultFile(executionContext, StandardFileType.StdOut)).flush();
			return DynValue.True;
		}

		// Token: 0x06006DD0 RID: 28112 RVA: 0x0004ACA0 File Offset: 0x00048EA0
		[MoonSharpModuleMethod]
		public static DynValue input(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return IoModule.HandleDefaultStreamSetter(executionContext, args, StandardFileType.StdIn);
		}

		// Token: 0x06006DD1 RID: 28113 RVA: 0x0004ACAA File Offset: 0x00048EAA
		[MoonSharpModuleMethod]
		public static DynValue output(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return IoModule.HandleDefaultStreamSetter(executionContext, args, StandardFileType.StdOut);
		}

		// Token: 0x06006DD2 RID: 28114 RVA: 0x0029B888 File Offset: 0x00299A88
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

		// Token: 0x06006DD3 RID: 28115 RVA: 0x0004ACB4 File Offset: 0x00048EB4
		private static Encoding GetUTF8Encoding()
		{
			return new UTF8Encoding(false);
		}

		// Token: 0x06006DD4 RID: 28116 RVA: 0x0029B92C File Offset: 0x00299B2C
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

		// Token: 0x06006DD5 RID: 28117 RVA: 0x0029BA24 File Offset: 0x00299C24
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

		// Token: 0x06006DD6 RID: 28118 RVA: 0x0004ACBC File Offset: 0x00048EBC
		public static string IoExceptionToLuaMessage(Exception ex, string filename)
		{
			if (ex is FileNotFoundException)
			{
				return string.Format("{0}: No such file or directory", filename);
			}
			return ex.Message;
		}

		// Token: 0x06006DD7 RID: 28119 RVA: 0x0029BBB0 File Offset: 0x00299DB0
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

		// Token: 0x06006DD8 RID: 28120 RVA: 0x0004ACD8 File Offset: 0x00048ED8
		[MoonSharpModuleMethod]
		public static DynValue read(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return IoModule.GetDefaultFile(executionContext, StandardFileType.StdIn).read(executionContext, args);
		}

		// Token: 0x06006DD9 RID: 28121 RVA: 0x0004ACE8 File Offset: 0x00048EE8
		[MoonSharpModuleMethod]
		public static DynValue write(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return IoModule.GetDefaultFile(executionContext, StandardFileType.StdOut).write(executionContext, args);
		}

		// Token: 0x06006DDA RID: 28122 RVA: 0x0029BC10 File Offset: 0x00299E10
		[MoonSharpModuleMethod]
		public static DynValue tmpfile(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			string filename = Script.GlobalOptions.Platform.IO_OS_GetTempFilename();
			return UserData.Create(IoModule.Open(executionContext, filename, IoModule.GetUTF8Encoding(), "w"));
		}

		// Token: 0x06006DDB RID: 28123 RVA: 0x0004ACF8 File Offset: 0x00048EF8
		private static FileUserDataBase Open(ScriptExecutionContext executionContext, string filename, Encoding encoding, string mode)
		{
			return new FileUserData(executionContext.GetScript(), filename, encoding, mode);
		}
	}
}
