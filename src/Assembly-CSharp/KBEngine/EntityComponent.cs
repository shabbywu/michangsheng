namespace KBEngine;

public class EntityComponent
{
	public ushort entityComponentPropertyID;

	public ushort componentType;

	public int ownerID;

	public Entity owner;

	public virtual void onAttached(Entity ownerEntity)
	{
	}

	public virtual void onDetached(Entity ownerEntity)
	{
	}

	public virtual void onEnterworld()
	{
	}

	public virtual void onLeaveworld()
	{
	}

	public virtual ScriptModule getScriptModule()
	{
		return null;
	}

	public virtual void onRemoteMethodCall(ushort methodUtype, MemoryStream stream)
	{
	}

	public virtual void onUpdatePropertys(ushort propUtype, MemoryStream stream, int maxCount)
	{
	}

	public virtual void callPropertysSetMethods()
	{
	}

	public virtual void createFromStream(MemoryStream stream)
	{
		componentType = (ushort)stream.readInt32();
		ownerID = stream.readInt32();
		stream.readUint16();
		ushort num = stream.readUint16();
		if (num > 0)
		{
			onUpdatePropertys(0, stream, num);
		}
	}
}
