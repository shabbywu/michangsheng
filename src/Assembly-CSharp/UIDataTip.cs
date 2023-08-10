using UnityEngine;

public class UIDataTip : MonoBehaviour
{
	public static UIDataTip Inst;

	private Transform mainLeftPos;

	private Transform exLeftPos;

	private Transform mainRightPos;

	private Transform exRightPos;

	private void Awake()
	{
		Inst = this;
		mainLeftPos = ((Component)this).transform.Find("MainPanelLeftPos");
		exLeftPos = ((Component)this).transform.Find("ExPanelLeftPos");
		mainRightPos = ((Component)this).transform.Find("MainPanelRightPos");
		exRightPos = ((Component)this).transform.Find("ExPanelRightPos");
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
