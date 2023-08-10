using UnityEngine;
using UnityEngine.UI;

namespace EpicToonFX;

public class ETFXButtonScript : MonoBehaviour
{
	public GameObject Button;

	private Text MyButtonText;

	private string projectileParticleName;

	private ETFXFireProjectile effectScript;

	private ETFXProjectileScript projectileScript;

	public float buttonsX;

	public float buttonsY;

	public float buttonsSizeX;

	public float buttonsSizeY;

	public float buttonsDistance;

	private void Start()
	{
		effectScript = GameObject.Find("ETFXFireProjectile").GetComponent<ETFXFireProjectile>();
		getProjectileNames();
		MyButtonText = ((Component)Button.transform.Find("Text")).GetComponent<Text>();
		MyButtonText.text = projectileParticleName;
	}

	private void Update()
	{
		MyButtonText.text = projectileParticleName;
	}

	public void getProjectileNames()
	{
		projectileScript = effectScript.projectiles[effectScript.currentProjectile].GetComponent<ETFXProjectileScript>();
		projectileParticleName = ((Object)projectileScript.projectileParticle).name;
	}

	public bool overButton()
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		Rect val = default(Rect);
		((Rect)(ref val))._002Ector(buttonsX, buttonsY, buttonsSizeX, buttonsSizeY);
		Rect val2 = default(Rect);
		((Rect)(ref val2))._002Ector(buttonsX + buttonsDistance, buttonsY, buttonsSizeX, buttonsSizeY);
		if (((Rect)(ref val)).Contains(new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y)) || ((Rect)(ref val2)).Contains(new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y)))
		{
			return true;
		}
		return false;
	}
}
