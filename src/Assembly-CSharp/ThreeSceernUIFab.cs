using GUIPackage;
using UnityEngine;

public class ThreeSceernUIFab : MonoBehaviour
{
	[SerializeField]
	private UIWidget uIWidget;

	public static ThreeSceernUIFab inst;

	private void Start()
	{
		inst = this;
	}

	public void FixPostion()
	{
		if ((Object)(object)uIWidget == (Object)null)
		{
			uIWidget = ((Component)this).GetComponent<UIWidget>();
		}
		((Behaviour)uIWidget).enabled = true;
		uIWidget.SetAnchor(((Component)UI_Manager.inst).gameObject);
		uIWidget.updateAnchors = UIRect.AnchorUpdate.OnUpdate;
		uIWidget.leftAnchor.relative = 1f;
		uIWidget.rightAnchor.relative = 1f;
		uIWidget.bottomAnchor.relative = 0f;
		uIWidget.topAnchor.relative = 0f;
		uIWidget.leftAnchor.absolute = 50;
		uIWidget.rightAnchor.absolute = 100;
		uIWidget.bottomAnchor.absolute = 66;
		uIWidget.topAnchor.absolute = 116;
		((MonoBehaviour)this).Invoke("lateAction", 1f);
	}

	public void lateAction()
	{
		uIWidget.updateAnchors = UIRect.AnchorUpdate.OnEnable;
	}
}
