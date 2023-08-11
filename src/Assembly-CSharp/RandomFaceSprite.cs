using UnityEngine;
using UnityEngine.UI;

public class RandomFaceSprite : MonoBehaviour
{
	public Image body;

	public Image eye;

	public Image eyebrow;

	public Image face;

	public Image Facefold;

	public Image hair;

	public Image hair2;

	public Image mouth;

	public Image mustache;

	public Image nose;

	public Image ornament;

	private void Start()
	{
	}

	public void setFace(int avatarID)
	{
		JSONObject jSONObject = jsonData.instance.AvatarRandomJsonData[string.Concat(avatarID)];
		body.sprite = jsonData.instance.body.getPartByID((int)jSONObject["body"].n);
		PartIsNull(body);
		eye.sprite = jsonData.instance.eye.getPartByID((int)jSONObject["eye"].n);
		PartIsNull(eye);
		face.sprite = jsonData.instance.face.getPartByID((int)jSONObject["face"].n);
		PartIsNull(face);
		Facefold.sprite = jsonData.instance.Facefold.getPartByID((int)jSONObject["Facefold"].n);
		PartIsNull(Facefold);
		hair.sprite = jsonData.instance.hair.getPartByID((int)jSONObject["hair"].n);
		PartIsNull(hair);
		mouth.sprite = jsonData.instance.mouth.getPartByID((int)jSONObject["mouth"].n);
		PartIsNull(mouth);
		eyebrow.sprite = jsonData.instance.eyebrow.getPartByID((int)jSONObject["eyebrow"].n);
		PartIsNull(eyebrow);
		nose.sprite = jsonData.instance.nose.getPartByID((int)jSONObject["nose"].n);
		PartIsNull(nose);
		ornament.sprite = jsonData.instance.ornament.getPartByID((int)jSONObject["ornament"].n);
		PartIsNull(ornament);
		eyebrow.sprite = jsonData.instance.eyebrow.getPartByID((int)jSONObject["eyebrow"].n);
		PartIsNull(eyebrow);
		hair2.sprite = jsonData.instance.hair2.getPartByID((int)jSONObject["hair"].n);
		PartIsNull(hair2);
	}

	public void PartIsNull(Image Part)
	{
		if ((Object)(object)Part.sprite == (Object)null)
		{
			((Component)Part).gameObject.SetActive(false);
		}
	}

	private void Update()
	{
	}
}
