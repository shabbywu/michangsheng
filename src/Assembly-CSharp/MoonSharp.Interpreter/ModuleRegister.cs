using System;
using System.Linq;
using System.Reflection;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.CoreLib;
using MoonSharp.Interpreter.Platforms;

namespace MoonSharp.Interpreter;

public static class ModuleRegister
{
	public static Table RegisterCoreModules(this Table table, CoreModules modules)
	{
		modules = Script.GlobalOptions.Platform.FilterSupportedCoreModules(modules);
		if (modules.Has(CoreModules.GlobalConsts))
		{
			table.RegisterConstants();
		}
		if (modules.Has(CoreModules.TableIterators))
		{
			table.RegisterModuleType<TableIteratorsModule>();
		}
		if (modules.Has(CoreModules.Basic))
		{
			table.RegisterModuleType<BasicModule>();
		}
		if (modules.Has(CoreModules.Metatables))
		{
			table.RegisterModuleType<MetaTableModule>();
		}
		if (modules.Has(CoreModules.String))
		{
			table.RegisterModuleType<StringModule>();
		}
		if (modules.Has(CoreModules.LoadMethods))
		{
			table.RegisterModuleType<LoadModule>();
		}
		if (modules.Has(CoreModules.Table))
		{
			table.RegisterModuleType<TableModule>();
		}
		if (modules.Has(CoreModules.Table))
		{
			table.RegisterModuleType<TableModule_Globals>();
		}
		if (modules.Has(CoreModules.ErrorHandling))
		{
			table.RegisterModuleType<ErrorHandlingModule>();
		}
		if (modules.Has(CoreModules.Math))
		{
			table.RegisterModuleType<MathModule>();
		}
		if (modules.Has(CoreModules.Coroutine))
		{
			table.RegisterModuleType<CoroutineModule>();
		}
		if (modules.Has(CoreModules.Bit32))
		{
			table.RegisterModuleType<Bit32Module>();
		}
		if (modules.Has(CoreModules.Dynamic))
		{
			table.RegisterModuleType<DynamicModule>();
		}
		if (modules.Has(CoreModules.OS_System))
		{
			table.RegisterModuleType<OsSystemModule>();
		}
		if (modules.Has(CoreModules.OS_Time))
		{
			table.RegisterModuleType<OsTimeModule>();
		}
		if (modules.Has(CoreModules.IO))
		{
			table.RegisterModuleType<IoModule>();
		}
		if (modules.Has(CoreModules.Debug))
		{
			table.RegisterModuleType<DebugModule>();
		}
		if (modules.Has(CoreModules.Json))
		{
			table.RegisterModuleType<JsonModule>();
		}
		return table;
	}

	public static Table RegisterConstants(this Table table)
	{
		DynValue dynValue = DynValue.NewTable(table.OwnerScript);
		Table table2 = dynValue.Table;
		table.Set("_G", DynValue.NewTable(table));
		table.Set("_VERSION", DynValue.NewString(string.Format("MoonSharp {0}", "2.0.0.0")));
		table.Set("_MOONSHARP", dynValue);
		table2.Set("version", DynValue.NewString("2.0.0.0"));
		table2.Set("luacompat", DynValue.NewString("5.2"));
		table2.Set("platform", DynValue.NewString(Script.GlobalOptions.Platform.GetPlatformName()));
		table2.Set("is_aot", DynValue.NewBoolean(Script.GlobalOptions.Platform.IsRunningOnAOT()));
		table2.Set("is_unity", DynValue.NewBoolean(PlatformAutoDetector.IsRunningOnUnity));
		table2.Set("is_mono", DynValue.NewBoolean(PlatformAutoDetector.IsRunningOnMono));
		table2.Set("is_clr4", DynValue.NewBoolean(PlatformAutoDetector.IsRunningOnClr4));
		table2.Set("is_pcl", DynValue.NewBoolean(PlatformAutoDetector.IsPortableFramework));
		table2.Set("banner", DynValue.NewString(Script.GetBanner()));
		return table;
	}

	public static Table RegisterModuleType(this Table gtable, Type t)
	{
		Table table = CreateModuleNamespace(gtable, t);
		foreach (MethodInfo item in from __mi in Framework.Do.GetMethods(t)
			where __mi.IsStatic
			select __mi)
		{
			if (item.GetCustomAttributes(typeof(MoonSharpModuleMethodAttribute), inherit: false).ToArray().Length != 0)
			{
				MoonSharpModuleMethodAttribute moonSharpModuleMethodAttribute = (MoonSharpModuleMethodAttribute)item.GetCustomAttributes(typeof(MoonSharpModuleMethodAttribute), inherit: false).First();
				if (!CallbackFunction.CheckCallbackSignature(item, requirePublicVisibility: true))
				{
					throw new ArgumentException($"Method {item.Name} does not have the right signature.");
				}
				Func<ScriptExecutionContext, CallbackArguments, DynValue> callBack = (Func<ScriptExecutionContext, CallbackArguments, DynValue>)Delegate.CreateDelegate(typeof(Func<ScriptExecutionContext, CallbackArguments, DynValue>), item);
				string text = ((!string.IsNullOrEmpty(moonSharpModuleMethodAttribute.Name)) ? moonSharpModuleMethodAttribute.Name : item.Name);
				table.Set(text, DynValue.NewCallback(callBack, text));
			}
			else if (item.Name == "MoonSharpInit")
			{
				object[] parameters = new object[2] { gtable, table };
				item.Invoke(null, parameters);
			}
		}
		foreach (FieldInfo item2 in from _mi in Framework.Do.GetFields(t)
			where _mi.IsStatic && _mi.GetCustomAttributes(typeof(MoonSharpModuleMethodAttribute), inherit: false).ToArray().Length != 0
			select _mi)
		{
			MoonSharpModuleMethodAttribute moonSharpModuleMethodAttribute2 = (MoonSharpModuleMethodAttribute)item2.GetCustomAttributes(typeof(MoonSharpModuleMethodAttribute), inherit: false).First();
			string name = ((!string.IsNullOrEmpty(moonSharpModuleMethodAttribute2.Name)) ? moonSharpModuleMethodAttribute2.Name : item2.Name);
			RegisterScriptField(item2, null, table, t, name);
		}
		foreach (FieldInfo item3 in from _mi in Framework.Do.GetFields(t)
			where _mi.IsStatic && _mi.GetCustomAttributes(typeof(MoonSharpModuleConstantAttribute), inherit: false).ToArray().Length != 0
			select _mi)
		{
			MoonSharpModuleConstantAttribute moonSharpModuleConstantAttribute = (MoonSharpModuleConstantAttribute)item3.GetCustomAttributes(typeof(MoonSharpModuleConstantAttribute), inherit: false).First();
			string name2 = ((!string.IsNullOrEmpty(moonSharpModuleConstantAttribute.Name)) ? moonSharpModuleConstantAttribute.Name : item3.Name);
			RegisterScriptFieldAsConst(item3, null, table, t, name2);
		}
		return gtable;
	}

	private static void RegisterScriptFieldAsConst(FieldInfo fi, object o, Table table, Type t, string name)
	{
		if (fi.FieldType == typeof(string))
		{
			string str = fi.GetValue(o) as string;
			table.Set(name, DynValue.NewString(str));
			return;
		}
		if (fi.FieldType == typeof(double))
		{
			double num = (double)fi.GetValue(o);
			table.Set(name, DynValue.NewNumber(num));
			return;
		}
		throw new ArgumentException($"Field {name} does not have the right type - it must be string or double.");
	}

	private static void RegisterScriptField(FieldInfo fi, object o, Table table, Type t, string name)
	{
		if (fi.FieldType != typeof(string))
		{
			throw new ArgumentException($"Field {name} does not have the right type - it must be string.");
		}
		string code = fi.GetValue(o) as string;
		DynValue value = table.OwnerScript.LoadFunction(code, table, name);
		table.Set(name, value);
	}

	private static Table CreateModuleNamespace(Table gtable, Type t)
	{
		MoonSharpModuleAttribute moonSharpModuleAttribute = (MoonSharpModuleAttribute)Framework.Do.GetCustomAttributes(t, typeof(MoonSharpModuleAttribute), inherit: false).First();
		if (string.IsNullOrEmpty(moonSharpModuleAttribute.Namespace))
		{
			return gtable;
		}
		Table table = null;
		DynValue dynValue = gtable.Get(moonSharpModuleAttribute.Namespace);
		if (dynValue.Type == DataType.Table)
		{
			table = dynValue.Table;
		}
		else
		{
			table = new Table(gtable.OwnerScript);
			gtable.Set(moonSharpModuleAttribute.Namespace, DynValue.NewTable(table));
		}
		DynValue dynValue2 = gtable.RawGet("package");
		if (dynValue2 == null || dynValue2.Type != DataType.Table)
		{
			gtable.Set("package", dynValue2 = DynValue.NewTable(gtable.OwnerScript));
		}
		DynValue dynValue3 = dynValue2.Table.RawGet("loaded");
		if (dynValue3 == null || dynValue3.Type != DataType.Table)
		{
			dynValue2.Table.Set("loaded", dynValue3 = DynValue.NewTable(gtable.OwnerScript));
		}
		dynValue3.Table.Set(moonSharpModuleAttribute.Namespace, DynValue.NewTable(table));
		return table;
	}

	public static Table RegisterModuleType<T>(this Table table)
	{
		return table.RegisterModuleType(typeof(T));
	}
}
