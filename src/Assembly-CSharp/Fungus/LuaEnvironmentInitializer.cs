using UnityEngine;

namespace Fungus;

public abstract class LuaEnvironmentInitializer : MonoBehaviour
{
	public abstract void Initialize();

	public abstract string PreprocessScript(string input);
}
