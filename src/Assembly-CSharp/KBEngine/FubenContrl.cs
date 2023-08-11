namespace KBEngine;

public class FubenContrl
{
	public Avatar entity;

	public MapIndexInfo this[string name]
	{
		get
		{
			if (!entity.FuBen.HasField(name))
			{
				entity.FuBen.AddField(name, new JSONObject(JSONObject.Type.OBJECT));
			}
			return new MapIndexInfo(this)
			{
				SenceName = name
			};
		}
	}

	public FubenContrl(Entity avater)
	{
		entity = (Avatar)avater;
	}

	public void outFuBen(bool ToLast = true)
	{
		if (ToLast)
		{
			Tools.instance.loadMapScenes(Tools.instance.getPlayer().lastFuBenScence);
		}
		entity.lastFuBenScence = "";
		entity.NowFuBen = "";
	}

	public bool isInFuBen()
	{
		return entity.NowFuBen != "";
	}

	public void CreatRandomFuBen()
	{
	}
}
