using UnityEngine;

public class CreateFaceButton : MonoBehaviour
{
	private void Start()
	{
		((Component)this).GetComponent<UIButton>().onClick.Add(new EventDelegate(resteChoice));
	}

	public void resteChoice()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		string text = ((Component)((Component)this).transform.Find("Label")).GetComponent<UILabel>().text;
		Create_face component = ((Component)((Component)this).transform.parent.parent).GetComponent<Create_face>();
		AvatarFaceDatabase faceDatabase = component.faceDatabase;
		foreach (Transform item in ((Component)component.ItemGrid).transform)
		{
			Object.Destroy((Object)(object)((Component)item).gameObject);
		}
		faceInfoList faceInfo = faceDatabase.getFaceInfo(text);
		if (faceInfo != null)
		{
			int num = 0;
			foreach (faceInfoDataBaseList face in faceInfo.faceList)
			{
				GameObject val = Object.Instantiate<GameObject>(component.faceChoicePrefab);
				val.transform.parent = ((Component)component.ItemGrid).transform;
				val.transform.localScale = Vector3.one;
				((Component)val.transform.Find("Label")).GetComponent<UILabel>().text = face.Name;
				((Component)val.transform.Find("LabelLight")).GetComponent<UILabel>().text = face.Name;
				if (num == 0)
				{
					((Component)val.transform).GetComponent<UIToggle>().value = true;
					((Component)val.transform).GetComponent<createChoiceBtn>().resteChoice();
					((Component)val.transform).GetComponent<createChoiceBtn>().SetHasColorSelect();
				}
				num++;
			}
		}
		else
		{
			UIPopTip.Inst.Pop("找不到类型" + text);
		}
		component.ItemGrid.repositionNow = true;
	}
}
