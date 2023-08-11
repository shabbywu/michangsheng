using GUIPackage;

public class selectStaticSkill : prepareSelect
{
	public Skill_UIST obj;

	private void Start()
	{
	}

	public override void addNowPage()
	{
		obj.nowIndex++;
		nowIndex = obj.nowIndex;
		if (nowIndex >= maxPage)
		{
			nowIndex = 0;
			obj.nowIndex = 0;
		}
	}

	public override void reduceIndex()
	{
		obj.nowIndex--;
		nowIndex--;
		if (nowIndex < 0)
		{
			nowIndex = maxPage - 1;
			obj.nowIndex = maxPage - 1;
		}
	}

	public override void resetObj()
	{
		setPageTetx();
	}

	public override void SetFirstPage()
	{
		obj.nowIndex = 0;
		base.SetFirstPage();
	}

	private void Update()
	{
	}
}
