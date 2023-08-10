using UnityEngine;

public class LoadUIScence : MonoBehaviour
{
	private void Start()
	{
		Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("GameObject"));
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}
}
