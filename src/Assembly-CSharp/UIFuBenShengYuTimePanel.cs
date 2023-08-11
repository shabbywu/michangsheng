using UnityEngine;
using UnityEngine.UI;

public class UIFuBenShengYuTimePanel : MonoBehaviour
{
	public static UIFuBenShengYuTimePanel Inst;

	public GameObject ScaleObj;

	public Text TimeText;

	private void Awake()
	{
		Inst = this;
	}
}
