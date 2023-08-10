using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
{
	protected static T _instance;

	public static T Instance
	{
		get
		{
			if ((Object)(object)_instance == (Object)null)
			{
				return null;
			}
			return _instance;
		}
	}
}
