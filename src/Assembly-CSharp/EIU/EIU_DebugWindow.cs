using System.Collections.Generic;
using UnityEngine;

namespace EIU;

public class EIU_DebugWindow : MonoBehaviour
{
	public bool debug = true;

	public List<EIU_AxisBase> Axes = new List<EIU_AxisBase>();

	[Header("UI References")]
	public Transform DebugWindow;

	public GameObject DebugItemPrefab;

	private void Start()
	{
		if (debug && Object.op_Implicit((Object)(object)EasyInputUtility.instance))
		{
			populateDebugWindow();
		}
		if ((Object)(object)EasyInputUtility.instance == (Object)null)
		{
			((Component)this).gameObject.SetActive(false);
		}
	}

	private void populateDebugWindow()
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		Axes = EasyInputUtility.instance.Axes;
		foreach (EIU_AxisBase axis in Axes)
		{
			GameObject obj = Object.Instantiate<GameObject>(DebugItemPrefab);
			((Object)obj).name = "positiveAxis";
			obj.transform.SetParent(DebugWindow);
			obj.transform.localScale = Vector3.one;
			obj.GetComponent<EIU_DebugItem>().Init(axis.pKeyDescription, ((object)(KeyCode)(ref axis.positiveKey)).ToString());
			if (axis.nKeyDescription != "")
			{
				GameObject obj2 = Object.Instantiate<GameObject>(DebugItemPrefab);
				((Object)obj2).name = "negativeAxis";
				obj2.transform.SetParent(DebugWindow);
				obj2.transform.localScale = Vector3.one;
				obj2.GetComponent<EIU_DebugItem>().Init(axis.nKeyDescription, ((object)(KeyCode)(ref axis.negativeKey)).ToString());
			}
		}
	}
}
