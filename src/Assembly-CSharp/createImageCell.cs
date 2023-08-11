using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class createImageCell : MonoBehaviour
{
	public int SkinIndex;

	public string SkinType;

	[SerializeField]
	private Image image1;

	[SerializeField]
	private Image image2;

	[SerializeField]
	private Image image3;

	public Toggle toggle;

	private void Start()
	{
	}

	public void resetAvatarFace()
	{
		CreateAvatarMag component = GameObject.Find("CreatAvatar").GetComponent<CreateAvatarMag>();
		List<int> suijiList = jsonData.instance.getSuijiList(SkinType, "AvatarSex" + CreateAvatarMag.inst.faceUI.faceDatabase.ListType);
		jsonData.instance.AvatarRandomJsonData["1"].SetField(SkinType, suijiList[SkinIndex]);
		component.player.randomAvatar(1);
	}

	public void SetImage(Sprite sprite1 = null, Sprite sprite2 = null, Sprite sprite3 = null)
	{
		if ((Object)(object)sprite1 == (Object)null)
		{
			((Behaviour)image1).enabled = false;
		}
		else
		{
			image1.sprite = sprite1;
		}
		if ((Object)(object)sprite2 == (Object)null)
		{
			((Behaviour)image2).enabled = false;
		}
		else
		{
			image2.sprite = sprite2;
		}
		if ((Object)(object)sprite3 == (Object)null)
		{
			((Behaviour)image3).enabled = false;
		}
		else
		{
			image3.sprite = sprite3;
		}
	}

	public void SetColor(Color color)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		((Graphic)image1).color = color;
	}

	public Color GetColor()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		return ((Graphic)image1).color;
	}
}
