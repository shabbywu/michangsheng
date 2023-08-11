using System.Collections.Generic;
using UnityEngine;

namespace Fungus;

public abstract class LuaBindingsBase : MonoBehaviour
{
	public abstract List<BoundObject> BoundObjects { get; }

	public abstract void AddBindings(LuaEnvironment luaEnv);
}
