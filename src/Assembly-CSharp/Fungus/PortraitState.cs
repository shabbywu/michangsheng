using UnityEngine;
using UnityEngine.UI;

namespace Fungus;

public class PortraitState
{
	public bool onScreen;

	public bool dimmed;

	public DisplayType display;

	public Sprite portrait;

	public RectTransform position;

	public FacingDirection facing;

	public Image portraitImage;
}
