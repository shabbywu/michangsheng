using UnityEngine;

[RequireComponent(typeof(GUITexture))]
public class GUIT_Button : MonoBehaviour
{
	public Color labelColor;

	public Texture t_on;

	public Texture t_off;

	public Texture t_on_over;

	public Texture t_off_over;

	public GameObject callbackObject;

	public string callback;

	private bool over;

	public bool on;

	private void Awake()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).GetComponentInChildren<GUIText>().material.color = labelColor;
		UpdateImage();
	}

	private void Update()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		Rect screenRect = ((GUIElement)((Component)this).GetComponent<GUITexture>()).GetScreenRect();
		if (((Rect)(ref screenRect)).Contains(Input.mousePosition))
		{
			if (!over)
			{
				OnOver();
			}
			if (Input.GetMouseButtonDown(0))
			{
				OnClick();
			}
		}
		else if (over)
		{
			OnOut();
		}
	}

	private void OnClick()
	{
		on = !on;
		callbackObject.SendMessage(callback);
		UpdateImage();
	}

	private void OnOver()
	{
		over = true;
		UpdateImage();
	}

	private void OnOut()
	{
		over = false;
		UpdateImage();
	}

	private void UpdateImage()
	{
		if (over)
		{
			((Component)this).GetComponent<GUITexture>().texture = (on ? t_on_over : t_off_over);
		}
		else
		{
			((Component)this).GetComponent<GUITexture>().texture = (on ? t_on : t_off);
		}
	}

	public void UpdateState(bool b)
	{
		on = b;
		UpdateImage();
	}
}
