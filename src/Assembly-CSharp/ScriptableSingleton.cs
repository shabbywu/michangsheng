using UnityEngine;

public class ScriptableSingleton<T> : ScriptableObject where T : ScriptableObject
{
	private static T m_Instance;

	public static T Instance
	{
		get
		{
			if (!Object.op_Implicit((Object)(object)m_Instance))
			{
				T[] array = Resources.LoadAll<T>("");
				if (array.Length != 0)
				{
					m_Instance = array[0];
				}
				else
				{
					Debug.LogErrorFormat("[ScriptableSingleton] - No object of type '{0}' was found in the project!", new object[1] { typeof(T).Name });
				}
			}
			return m_Instance;
		}
	}
}
