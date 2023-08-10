using UnityEngine;

public class CloseUIScene : MonoBehaviour
{
	private void Start()
	{
		if ((Object)(object)PanelMamager.inst != (Object)null)
		{
			if ((Object)(object)PanelMamager.inst.UISceneGameObject != (Object)null)
			{
				Object.Destroy((Object)(object)PanelMamager.inst.UISceneGameObject);
			}
			if ((Object)(object)PanelMamager.inst.UIBlackMaskGameObject != (Object)null)
			{
				Object.Destroy((Object)(object)PanelMamager.inst.UIBlackMaskGameObject);
			}
		}
		if ((Object)(object)FpUIMag.inst != (Object)null)
		{
			Object.Destroy((Object)(object)((Component)FpUIMag.inst).gameObject);
		}
	}
}
