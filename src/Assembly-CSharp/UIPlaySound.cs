using UnityEngine;
using YSGame;

[AddComponentMenu("NGUI/Interaction/Play Sound")]
public class UIPlaySound : MonoBehaviour
{
	public enum Trigger
	{
		OnClick,
		OnMouseOver,
		OnMouseOut,
		OnPress,
		OnRelease,
		Custom
	}

	public AudioClip audioClip;

	public Trigger trigger;

	private bool mIsOver;

	[Range(0f, 1f)]
	public float volume = 1f;

	[Range(0f, 2f)]
	public float pitch = 1f;

	private bool canPlay
	{
		get
		{
			if (!((Behaviour)this).enabled)
			{
				return false;
			}
			UIButton component = ((Component)this).GetComponent<UIButton>();
			if (!((Object)(object)component == (Object)null))
			{
				return component.isEnabled;
			}
			return true;
		}
	}

	private void OnHover(bool isOver)
	{
		if (trigger == Trigger.OnMouseOver)
		{
			if (mIsOver == isOver)
			{
				return;
			}
			mIsOver = isOver;
		}
		if (canPlay && ((isOver && trigger == Trigger.OnMouseOver) || (!isOver && trigger == Trigger.OnMouseOut)))
		{
			Play();
		}
	}

	private void OnPress(bool isPressed)
	{
		if (trigger == Trigger.OnPress)
		{
			if (mIsOver == isPressed)
			{
				return;
			}
			mIsOver = isPressed;
		}
		if (canPlay && ((isPressed && trigger == Trigger.OnPress) || (!isPressed && trigger == Trigger.OnRelease)))
		{
			Play();
		}
	}

	private void OnClick()
	{
		if (canPlay && trigger == Trigger.OnClick)
		{
			Play();
		}
	}

	private void OnSelect(bool isSelected)
	{
		if (canPlay && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
		{
			OnHover(isSelected);
		}
	}

	public void Play()
	{
		MusicMag.instance.PlayEffectMusic(audioClip);
	}
}
