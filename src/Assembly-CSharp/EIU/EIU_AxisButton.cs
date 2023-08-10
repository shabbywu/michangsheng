using UnityEngine;
using UnityEngine.UI;

namespace EIU;

public class EIU_AxisButton : MonoBehaviour
{
	[Header("Child Text Objects")]
	public Text axisName_text;

	public Text keyName_text;

	[Space(10f)]
	[Header("Axis Button Info")]
	public string axisName;

	public bool negativeKey;

	public void init(string axisName, string buttonDescription, string key, bool nKey = false)
	{
		this.axisName = axisName;
		axisName_text.text = buttonDescription;
		keyName_text.text = key;
		negativeKey = nKey;
	}

	public void ChangeKeyText(string key)
	{
		keyName_text.text = key;
	}

	public void RebindAxis()
	{
		EIU_ControlsMenu.Instance().OpenRebindButtonDialog(axisName, negativeKey);
	}

	public void HoverClip()
	{
		EasyAudioUtility.instance.Play("Hover");
	}
}
