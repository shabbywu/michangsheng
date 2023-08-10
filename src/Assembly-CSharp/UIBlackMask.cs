using UnityEngine;

public class UIBlackMask : MonoBehaviour
{
	public Animator animator;

	private void Awake()
	{
		Object.DontDestroyOnLoad((Object)(object)((Component)this).gameObject);
		PanelMamager.inst.UIBlackMaskGameObject = ((Component)this).gameObject;
		((Component)this).gameObject.SetActive(false);
	}
}
