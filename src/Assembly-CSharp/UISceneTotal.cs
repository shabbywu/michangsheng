using UnityEngine;

public class UISceneTotal : MonoBehaviour
{
	private void Start()
	{
		Object.DontDestroyOnLoad((Object)(object)((Component)this).gameObject);
		PanelMamager.inst.UISceneGameObject = ((Component)this).gameObject;
	}
}
