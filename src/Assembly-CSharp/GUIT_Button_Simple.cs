using UnityEngine;

[RequireComponent(typeof(GUITexture))]
public class GUIT_Button_Simple : MonoBehaviour
{
	public Color labelColor;

	public Texture text;

	public Texture text_over;

	public GameObject callbackObject;

	public string callback;

	private bool over;

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
		callbackObject.SendMessage(callback);
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
			((Component)this).GetComponent<GUITexture>().texture = text_over;
		}
		else
		{
			((Component)this).GetComponent<GUITexture>().texture = text;
		}
	}
}
