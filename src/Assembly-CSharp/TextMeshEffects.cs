using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class TextMeshEffects : MonoBehaviour
{
	private TextMesh thisComponent;

	private Transform myTransform;

	public static string chosenLanguage = "_en";

	public bool neMenjajFont;

	private void Awake()
	{
		myTransform = ((Component)this).transform;
		thisComponent = ((Component)this).GetComponent<TextMesh>();
	}

	public void RefreshTextOutline(bool adjustTextSize, bool hasWhiteSpaces, bool increaseFont = true)
	{
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Expected O, but got Unknown
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Expected O, but got Unknown
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Expected O, but got Unknown
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Expected O, but got Unknown
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Expected O, but got Unknown
		if (!neMenjajFont)
		{
			if (LanguageManager.chosenLanguage != "_en" && LanguageManager.chosenLanguage != "_us")
			{
				Font val = ((!(LanguageManager.chosenLanguage == "_th")) ? ((Font)Resources.Load("ARIBLK")) : ((Font)Resources.Load("Angsana New Bold")));
				thisComponent.font = val;
				((Component)thisComponent).GetComponent<Renderer>().sharedMaterial = val.material;
			}
			else
			{
				Font val2 = (Font)Resources.Load("SOUPOFJUSTICE");
				thisComponent.font = val2;
				((Component)thisComponent).GetComponent<Renderer>().sharedMaterial = val2.material;
			}
		}
		if (adjustTextSize)
		{
			thisComponent.AdjustFontSize(rowSplit: true, hasWhiteSpaces, increaseFont);
		}
		if (myTransform.childCount != 8)
		{
			return;
		}
		for (int i = 0; i < 8; i++)
		{
			if (!neMenjajFont)
			{
				if (LanguageManager.chosenLanguage != "_en" && LanguageManager.chosenLanguage != "_us")
				{
					Font val3 = ((!(LanguageManager.chosenLanguage == "_th")) ? ((Font)Resources.Load("ARIBLK")) : ((Font)Resources.Load("Angsana New Bold")));
					((Component)myTransform.GetChild(i)).GetComponent<TextMesh>().font = val3;
					((Component)((Component)myTransform.GetChild(i)).GetComponent<TextMesh>()).GetComponent<Renderer>().sharedMaterial = val3.material;
				}
				else
				{
					Font val4 = (Font)Resources.Load("SOUPOFJUSTICE");
					((Component)myTransform.GetChild(i)).GetComponent<TextMesh>().font = val4;
					((Component)((Component)myTransform.GetChild(i)).GetComponent<TextMesh>()).GetComponent<Renderer>().sharedMaterial = val4.material;
				}
			}
			((Component)myTransform.GetChild(i)).GetComponent<TextMesh>().text = thisComponent.text;
			if (adjustTextSize)
			{
				((Component)myTransform.GetChild(i)).GetComponent<TextMesh>().characterSize = thisComponent.characterSize;
			}
		}
	}
}
