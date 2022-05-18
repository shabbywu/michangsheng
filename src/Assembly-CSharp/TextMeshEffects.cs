using System;
using UnityEngine;

// Token: 0x02000785 RID: 1925
[RequireComponent(typeof(TextMesh))]
public class TextMeshEffects : MonoBehaviour
{
	// Token: 0x0600312C RID: 12588 RVA: 0x000240E4 File Offset: 0x000222E4
	private void Awake()
	{
		this.myTransform = base.transform;
		this.thisComponent = base.GetComponent<TextMesh>();
	}

	// Token: 0x0600312D RID: 12589 RVA: 0x001875A4 File Offset: 0x001857A4
	public void RefreshTextOutline(bool adjustTextSize, bool hasWhiteSpaces, bool increaseFont = true)
	{
		if (!this.neMenjajFont)
		{
			if (LanguageManager.chosenLanguage != "_en" && LanguageManager.chosenLanguage != "_us")
			{
				Font font;
				if (LanguageManager.chosenLanguage == "_th")
				{
					font = (Font)Resources.Load("Angsana New Bold");
				}
				else
				{
					font = (Font)Resources.Load("ARIBLK");
				}
				this.thisComponent.font = font;
				this.thisComponent.GetComponent<Renderer>().sharedMaterial = font.material;
			}
			else
			{
				Font font2 = (Font)Resources.Load("SOUPOFJUSTICE");
				this.thisComponent.font = font2;
				this.thisComponent.GetComponent<Renderer>().sharedMaterial = font2.material;
			}
		}
		if (adjustTextSize)
		{
			this.thisComponent.AdjustFontSize(true, hasWhiteSpaces, increaseFont);
		}
		if (this.myTransform.childCount == 8)
		{
			for (int i = 0; i < 8; i++)
			{
				if (!this.neMenjajFont)
				{
					if (LanguageManager.chosenLanguage != "_en" && LanguageManager.chosenLanguage != "_us")
					{
						Font font3;
						if (LanguageManager.chosenLanguage == "_th")
						{
							font3 = (Font)Resources.Load("Angsana New Bold");
						}
						else
						{
							font3 = (Font)Resources.Load("ARIBLK");
						}
						this.myTransform.GetChild(i).GetComponent<TextMesh>().font = font3;
						this.myTransform.GetChild(i).GetComponent<TextMesh>().GetComponent<Renderer>().sharedMaterial = font3.material;
					}
					else
					{
						Font font4 = (Font)Resources.Load("SOUPOFJUSTICE");
						this.myTransform.GetChild(i).GetComponent<TextMesh>().font = font4;
						this.myTransform.GetChild(i).GetComponent<TextMesh>().GetComponent<Renderer>().sharedMaterial = font4.material;
					}
				}
				this.myTransform.GetChild(i).GetComponent<TextMesh>().text = this.thisComponent.text;
				if (adjustTextSize)
				{
					this.myTransform.GetChild(i).GetComponent<TextMesh>().characterSize = this.thisComponent.characterSize;
				}
			}
		}
	}

	// Token: 0x04002D45 RID: 11589
	private TextMesh thisComponent;

	// Token: 0x04002D46 RID: 11590
	private Transform myTransform;

	// Token: 0x04002D47 RID: 11591
	public static string chosenLanguage = "_en";

	// Token: 0x04002D48 RID: 11592
	public bool neMenjajFont;
}
