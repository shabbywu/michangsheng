using System;
using System.Collections.Generic;
using System.Reflection;

namespace KBEngine;

public class ScriptModule
{
	public string name;

	public bool usePropertyDescrAlias;

	public bool useMethodDescrAlias;

	public Dictionary<string, Property> propertys = new Dictionary<string, Property>();

	public Dictionary<ushort, Property> idpropertys = new Dictionary<ushort, Property>();

	public Dictionary<string, Method> methods = new Dictionary<string, Method>();

	public Dictionary<string, Method> base_methods = new Dictionary<string, Method>();

	public Dictionary<string, Method> cell_methods = new Dictionary<string, Method>();

	public Dictionary<ushort, Method> idmethods = new Dictionary<ushort, Method>();

	public Dictionary<ushort, Method> idbase_methods = new Dictionary<ushort, Method>();

	public Dictionary<ushort, Method> idcell_methods = new Dictionary<ushort, Method>();

	public Type entityScript;

	public ScriptModule(string modulename)
	{
		name = modulename;
		Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
		foreach (Assembly assembly in assemblies)
		{
			entityScript = assembly.GetType("KBEngine." + modulename);
			if (entityScript == null)
			{
				entityScript = assembly.GetType(modulename);
			}
			if (entityScript != null)
			{
				break;
			}
		}
		usePropertyDescrAlias = false;
		useMethodDescrAlias = false;
		if (entityScript == null)
		{
			Dbg.ERROR_MSG("can't load(KBEngine." + modulename + ")!");
		}
	}
}
