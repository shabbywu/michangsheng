using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TabButton1 : TabButton
{
	public GameObject OnToggleObject;

	public GameObject LoseToggleObject;

	public Button OnToggleButton;

	public Button LoseToggleButton;

	public TabPanelBase Panel;

	public override void Awake()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		Group.AddTab(this);
		((UnityEvent)OnToggleButton.onClick).AddListener(new UnityAction(OnButtonClick));
		((UnityEvent)LoseToggleButton.onClick).AddListener(new UnityAction(OnButtonClick));
	}

	public override void OnToggle()
	{
		base.OnToggle();
		OnToggleObject.SetActive(true);
		LoseToggleObject.SetActive(false);
		Panel.OnPanelShow();
	}

	public override void OnLose()
	{
		base.OnLose();
		OnToggleObject.SetActive(false);
		LoseToggleObject.SetActive(true);
		Panel.OnPanelHide();
	}
}
