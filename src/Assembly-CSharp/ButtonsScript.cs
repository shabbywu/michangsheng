using UnityEngine;

public class ButtonsScript : MonoBehaviour
{
	private string clickedOn = "";

	private Material save;

	private Vector3 scaleSave;

	public GameObject test;

	private void AnimateButton(GameObject btn, int Onoff)
	{
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Expected O, but got Unknown
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Expected O, but got Unknown
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		if (((Object)btn).name != "PlayerName")
		{
			if (Onoff == 1)
			{
				scaleSave = btn.transform.localScale;
				btn.transform.localScale = scaleSave * 0.8f;
				save = new Material(btn.GetComponent<Renderer>().sharedMaterial);
				Material val = new Material(btn.GetComponent<Renderer>().sharedMaterial);
				val.color = new Color(save.color.r - 0.2f, save.color.g - 0.2f, save.color.b - 0.2f, save.color.a);
				btn.GetComponent<Renderer>().sharedMaterial = val;
			}
			else
			{
				btn.transform.localScale = scaleSave;
				btn.GetComponent<Renderer>().sharedMaterial = save;
			}
		}
	}

	private string RaycastFunct(Vector3 v)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		RaycastHit val = default(RaycastHit);
		if (Physics.Raycast(GameObject.Find("Main Camera").GetComponent<Camera>().ScreenPointToRay(v + Vector3.forward * 10f), ref val, 500f))
		{
			return ((Object)((RaycastHit)(ref val)).transform).name;
		}
		return "";
	}

	private void Start()
	{
	}

	private void Update()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		if (Input.GetMouseButtonDown(0))
		{
			clickedOn = RaycastFunct(Input.mousePosition);
			if (clickedOn != "")
			{
				AnimateButton(GameObject.Find(clickedOn), 1);
			}
		}
		if (Input.GetMouseButtonUp(0))
		{
			if (clickedOn != "")
			{
				AnimateButton(GameObject.Find(clickedOn), 2);
			}
			string text = RaycastFunct(Input.mousePosition);
			if (text == clickedOn && text != "")
			{
				Debug.Log((object)"Boza je kralj!!!!");
			}
		}
	}
}
