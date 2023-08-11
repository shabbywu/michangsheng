using GUIPackage;
using UnityEngine;

public class ThreeSceneMagFab : MonoBehaviour
{
	public static ThreeSceneMagFab inst;

	private void Start()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		inst = this;
		if ((Object)(object)UI_Manager.inst != (Object)null)
		{
			((Component)this).transform.localPosition = new Vector3(0f, 0f, 0f);
			((Component)this).transform.localScale = Vector3.one;
			ThreeSceneMag component = ((Component)this).gameObject.GetComponent<ThreeSceneMag>();
			component.CraftingList = UI_Manager.inst.CraftingList;
			component.xiala = UI_Manager.inst.xialian;
			component.exchangeUI = UI_Manager.inst.exchangeUI;
			component.init();
		}
	}
}
