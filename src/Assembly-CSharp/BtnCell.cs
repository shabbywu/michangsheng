using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YSGame;

public class BtnCell : LunDaoBtnBase
{
	public bool isStopBlack;

	public bool isStopSuoFang;

	public bool isStopMusic;

	public bool IsIn;

	[SerializeField]
	private List<Image> targetImageList;

	[SerializeField]
	private List<Text> targetTextList;

	public AudioClip audioClip;

	public UnityEvent mouseUp;

	public UnityEvent mouseDown;

	public UnityEvent mouseEnter;

	public UnityEvent mouseOut;

	private void Awake()
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Expected O, but got Unknown
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Expected O, but got Unknown
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Expected O, but got Unknown
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)((Component)this).GetComponent<Image>() == (Object)null)
		{
			((Graphic)((Component)this).gameObject.AddComponent<Image>()).color = new Color(1f, 1f, 1f, 0f);
		}
		AddMouseUpCallBack(new UnityAction(MouseUp));
		AddMouseEnterCallBack(new UnityAction(MouseEnter));
		AddMouseExitCallBack(new UnityAction(MouseOut));
		AddMouseDownCallBack(new UnityAction(MouseDown));
	}

	private void MouseUp()
	{
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Expected O, but got Unknown
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		if (!isStopSuoFang)
		{
			foreach (Image targetImage in targetImageList)
			{
				((Component)targetImage).transform.localScale = Vector3.one;
			}
			foreach (Text targetText in targetTextList)
			{
				((Component)targetText).transform.localScale = Vector3.one;
			}
			if (!isStopMusic)
			{
				if ((Object)(object)audioClip != (Object)null)
				{
					MusicMag.instance.PlayEffectMusic(audioClip);
				}
				else
				{
					MusicMag.instance.PlayEffectMusic(1);
				}
			}
		}
		if (IsIn)
		{
			mouseUp.AddListener(new UnityAction(MouseOut));
			mouseUp.Invoke();
		}
	}

	private void MouseDown()
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		if (!isStopSuoFang)
		{
			foreach (Image targetImage in targetImageList)
			{
				((Component)targetImage).transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
			}
			foreach (Text targetText in targetTextList)
			{
				((Component)targetText).transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
			}
		}
		if (IsIn)
		{
			mouseDown.Invoke();
		}
	}

	private void MouseEnter()
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		IsIn = true;
		if (!isStopBlack)
		{
			foreach (Image targetImage in targetImageList)
			{
				((Graphic)targetImage).color = new Color(0.5019608f, 0.5019608f, 0.5019608f);
			}
			foreach (Text targetText in targetTextList)
			{
				((Graphic)targetText).color = new Color(0.5019608f, 0.5019608f, 0.5019608f);
			}
		}
		mouseEnter.Invoke();
		MusicMag.instance.PlayEffectMusic(3);
	}

	private void MouseOut()
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		IsIn = false;
		if (!isStopBlack)
		{
			foreach (Image targetImage in targetImageList)
			{
				((Graphic)targetImage).color = new Color(1f, 1f, 1f);
			}
			foreach (Text targetText in targetTextList)
			{
				((Graphic)targetText).color = new Color(1f, 1f, 1f);
			}
		}
		mouseOut.Invoke();
	}
}
