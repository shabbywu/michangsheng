using UnityEngine;

namespace Fungus;

[AddComponentMenu("")]
public class CommandCopyBuffer : Block
{
	protected static CommandCopyBuffer instance;

	protected virtual void Start()
	{
		if (Application.isPlaying)
		{
			Object.Destroy((Object)(object)((Component)this).gameObject);
		}
	}

	public static CommandCopyBuffer GetInstance()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		if ((Object)(object)instance == (Object)null)
		{
			GameObject val = GameObject.Find("_CommandCopyBuffer");
			if ((Object)(object)val == (Object)null)
			{
				val = new GameObject("_CommandCopyBuffer");
				((Object)val).hideFlags = (HideFlags)61;
			}
			instance = val.GetComponent<CommandCopyBuffer>();
			if ((Object)(object)instance == (Object)null)
			{
				instance = val.AddComponent<CommandCopyBuffer>();
			}
		}
		return instance;
	}

	public virtual bool HasCommands()
	{
		return GetCommands().Length != 0;
	}

	public virtual Command[] GetCommands()
	{
		return ((Component)this).GetComponents<Command>();
	}

	public virtual void Clear()
	{
		Command[] commands = GetCommands();
		for (int i = 0; i < commands.Length; i++)
		{
			Object.DestroyImmediate((Object)(object)commands[i]);
		}
	}
}
