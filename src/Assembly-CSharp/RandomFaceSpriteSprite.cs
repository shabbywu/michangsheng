using UnityEngine;

public class RandomFaceSpriteSprite : MonoBehaviour
{
	public UITexture body;

	public UITexture eye;

	public UITexture eyebrow;

	public UITexture face;

	public UITexture Facefold;

	public UITexture hair;

	public UITexture hair2;

	public UITexture mouth;

	public UITexture mustache;

	public UITexture nose;

	public UITexture ornament;

	private void Start()
	{
	}

	public void setFace(int avatarID)
	{
		JSONObject jSONObject = jsonData.instance.AvatarRandomJsonData[string.Concat(avatarID)];
		body.mainTexture = (Texture)(object)jsonData.instance.body.getPartByID((int)jSONObject["body"].n).texture;
		PartIsNull(body);
		eye.mainTexture = (Texture)(object)jsonData.instance.eye.getPartByID((int)jSONObject["eye"].n).texture;
		PartIsNull(eye);
		hair.mainTexture = (Texture)(object)jsonData.instance.hair.getPartByID((int)jSONObject["hair"].n).texture;
		PartIsNull(hair);
		mouth.mainTexture = (Texture)(object)jsonData.instance.mouth.getPartByID((int)jSONObject["mouth"].n).texture;
		PartIsNull(mouth);
		eyebrow.mainTexture = (Texture)(object)jsonData.instance.eyebrow.getPartByID((int)jSONObject["eyebrow"].n).texture;
		PartIsNull(eyebrow);
		nose.mainTexture = (Texture)(object)jsonData.instance.nose.getPartByID((int)jSONObject["nose"].n).texture;
		PartIsNull(nose);
		eyebrow.mainTexture = (Texture)(object)jsonData.instance.eyebrow.getPartByID((int)jSONObject["eyebrow"].n).texture;
		PartIsNull(eyebrow);
		hair2.mainTexture = (Texture)(object)jsonData.instance.hair2.getPartByID((int)jSONObject["hair"].n).texture;
		PartIsNull(hair2);
	}

	public void PartIsNull(UITexture Part)
	{
		if ((Object)(object)Part.mainTexture == (Object)null)
		{
			((Component)Part).gameObject.SetActive(false);
		}
	}

	private void Update()
	{
	}
}
