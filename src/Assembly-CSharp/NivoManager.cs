using UnityEngine;

public class NivoManager : MonoBehaviour
{
	public int currentLevel;

	private void Awake()
	{
		Object.DontDestroyOnLoad((Object)(object)((Component)this).gameObject);
	}
}
