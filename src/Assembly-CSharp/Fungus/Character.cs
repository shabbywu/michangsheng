using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus;

[ExecuteInEditMode]
public class Character : MonoBehaviour, ILocalizable
{
	[Tooltip("Character name as displayed in Say Dialog.")]
	[SerializeField]
	protected string nameText;

	[Tooltip("Color to display the character name in Say Dialog.")]
	[SerializeField]
	protected Color nameColor = Color.white;

	[Tooltip("Sound effect to play when this character is speaking.")]
	[SerializeField]
	protected AudioClip soundEffect;

	[Tooltip("List of portrait images that can be displayed for this character.")]
	[SerializeField]
	protected List<Sprite> portraits;

	[Tooltip("Direction that portrait sprites face.")]
	[SerializeField]
	protected FacingDirection portraitsFace;

	[Tooltip("Sets the active Say dialog with a reference to a Say Dialog object in the scene. This Say Dialog will be used whenever the character speaks.")]
	[SerializeField]
	protected SayDialog setSayDialog;

	[FormerlySerializedAs("notes")]
	[TextArea(5, 10)]
	[SerializeField]
	protected string description;

	protected PortraitState portaitState = new PortraitState();

	protected static List<Character> activeCharacters = new List<Character>();

	public static List<Character> ActiveCharacters => activeCharacters;

	public virtual string NameText => nameText;

	public virtual Color NameColor => nameColor;

	public virtual AudioClip SoundEffect => soundEffect;

	public virtual List<Sprite> Portraits => portraits;

	public virtual FacingDirection PortraitsFace => portraitsFace;

	public virtual Sprite ProfileSprite { get; set; }

	public virtual PortraitState State => portaitState;

	public virtual SayDialog SetSayDialog => setSayDialog;

	protected virtual void OnEnable()
	{
		if (!activeCharacters.Contains(this))
		{
			activeCharacters.Add(this);
		}
	}

	protected virtual void OnDisable()
	{
		activeCharacters.Remove(this);
	}

	public string GetObjectName()
	{
		return ((Object)((Component)this).gameObject).name;
	}

	public virtual bool NameStartsWith(string matchString)
	{
		if (!((Object)this).name.StartsWith(matchString, ignoreCase: true, CultureInfo.CurrentCulture))
		{
			return nameText.StartsWith(matchString, ignoreCase: true, CultureInfo.CurrentCulture);
		}
		return true;
	}

	public virtual Sprite GetPortrait(string portraitString)
	{
		if (string.IsNullOrEmpty(portraitString))
		{
			return null;
		}
		for (int i = 0; i < portraits.Count; i++)
		{
			if ((Object)(object)portraits[i] != (Object)null && string.Compare(((Object)portraits[i]).name, portraitString, ignoreCase: true) == 0)
			{
				return portraits[i];
			}
		}
		return null;
	}

	public virtual string GetStandardText()
	{
		return nameText;
	}

	public virtual void SetStandardText(string standardText)
	{
		nameText = standardText;
	}

	public virtual string GetDescription()
	{
		return description;
	}

	public virtual string GetStringId()
	{
		return "CHARACTER." + nameText;
	}
}
