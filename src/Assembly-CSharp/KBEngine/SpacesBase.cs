using System;
using System.Collections.Generic;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000C65 RID: 3173
	public abstract class SpacesBase : Entity
	{
		// Token: 0x06005655 RID: 22101 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onModelIDChanged(uint oldValue)
		{
		}

		// Token: 0x06005656 RID: 22102 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onModelScaleChanged(byte oldValue)
		{
		}

		// Token: 0x06005657 RID: 22103 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onNameChanged(string oldValue)
		{
		}

		// Token: 0x06005658 RID: 22104 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onUidChanged(uint oldValue)
		{
		}

		// Token: 0x06005659 RID: 22105 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onUtypeChanged(uint oldValue)
		{
		}

		// Token: 0x0600565A RID: 22106 RVA: 0x0023D962 File Offset: 0x0023BB62
		public SpacesBase()
		{
		}

		// Token: 0x0600565B RID: 22107 RVA: 0x00004095 File Offset: 0x00002295
		public override void onComponentsEnterworld()
		{
		}

		// Token: 0x0600565C RID: 22108 RVA: 0x00004095 File Offset: 0x00002295
		public override void onComponentsLeaveworld()
		{
		}

		// Token: 0x0600565D RID: 22109 RVA: 0x0023D97D File Offset: 0x0023BB7D
		public override void onGetBase()
		{
			this.baseEntityCall = new EntityBaseEntityCall_SpacesBase(this.id, this.className);
		}

		// Token: 0x0600565E RID: 22110 RVA: 0x0023D996 File Offset: 0x0023BB96
		public override void onGetCell()
		{
			this.cellEntityCall = new EntityCellEntityCall_SpacesBase(this.id, this.className);
		}

		// Token: 0x0600565F RID: 22111 RVA: 0x0023D9AF File Offset: 0x0023BBAF
		public override void onLoseCell()
		{
			this.cellEntityCall = null;
		}

		// Token: 0x06005660 RID: 22112 RVA: 0x0023D9B8 File Offset: 0x0023BBB8
		public override EntityCall getBaseEntityCall()
		{
			return this.baseEntityCall;
		}

		// Token: 0x06005661 RID: 22113 RVA: 0x0023D9C0 File Offset: 0x0023BBC0
		public override EntityCall getCellEntityCall()
		{
			return this.cellEntityCall;
		}

		// Token: 0x06005662 RID: 22114 RVA: 0x00004095 File Offset: 0x00002295
		public override void attachComponents()
		{
		}

		// Token: 0x06005663 RID: 22115 RVA: 0x00004095 File Offset: 0x00002295
		public override void detachComponents()
		{
		}

		// Token: 0x06005664 RID: 22116 RVA: 0x0023D9C8 File Offset: 0x0023BBC8
		public override void onRemoteMethodCall(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["Spaces"];
			ushort num;
			if (scriptModule.usePropertyDescrAlias)
			{
				num = (ushort)stream.readUint8();
			}
			else
			{
				num = stream.readUint16();
			}
			ushort key;
			if (scriptModule.useMethodDescrAlias)
			{
				key = (ushort)stream.readUint8();
			}
			else
			{
				key = stream.readUint16();
			}
			if (num == 0)
			{
				Method method = scriptModule.idmethods[key];
				ushort methodUtype = method.methodUtype;
				return;
			}
			ushort properUtype = scriptModule.idpropertys[num].properUtype;
		}

		// Token: 0x06005665 RID: 22117 RVA: 0x0023DA48 File Offset: 0x0023BC48
		public override void onUpdatePropertys(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["Spaces"];
			Dictionary<ushort, Property> idpropertys = scriptModule.idpropertys;
			while (stream.length() > 0U)
			{
				ushort num;
				ushort key;
				if (scriptModule.usePropertyDescrAlias)
				{
					num = (ushort)stream.readUint8();
					key = (ushort)stream.readUint8();
				}
				else
				{
					num = stream.readUint16();
					key = stream.readUint16();
				}
				if (num != 0)
				{
					ushort properUtype = idpropertys[num].properUtype;
					return;
				}
				Property property = idpropertys[key];
				ushort properUtype2 = property.properUtype;
				switch (properUtype2)
				{
				case 40000:
				{
					Vector3 position = this.position;
					this.position = stream.readVector3();
					if (property.isBase())
					{
						if (this.inited)
						{
							this.onPositionChanged(position);
						}
					}
					else if (this.inWorld)
					{
						this.onPositionChanged(position);
					}
					break;
				}
				case 40001:
				{
					Vector3 direction = this.direction;
					this.direction = stream.readVector3();
					if (property.isBase())
					{
						if (this.inited)
						{
							this.onDirectionChanged(direction);
						}
					}
					else if (this.inWorld)
					{
						this.onDirectionChanged(direction);
					}
					break;
				}
				case 40002:
					stream.readUint32();
					break;
				default:
					switch (properUtype2)
					{
					case 41003:
					{
						string oldValue = this.name;
						this.name = stream.readUnicode();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onNameChanged(oldValue);
							}
						}
						else if (this.inWorld)
						{
							this.onNameChanged(oldValue);
						}
						break;
					}
					case 41004:
					{
						uint oldValue2 = this.uid;
						this.uid = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onUidChanged(oldValue2);
							}
						}
						else if (this.inWorld)
						{
							this.onUidChanged(oldValue2);
						}
						break;
					}
					case 41005:
					{
						uint oldValue3 = this.utype;
						this.utype = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onUtypeChanged(oldValue3);
							}
						}
						else if (this.inWorld)
						{
							this.onUtypeChanged(oldValue3);
						}
						break;
					}
					case 41006:
					{
						uint oldValue4 = this.modelID;
						this.modelID = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onModelIDChanged(oldValue4);
							}
						}
						else if (this.inWorld)
						{
							this.onModelIDChanged(oldValue4);
						}
						break;
					}
					case 41007:
					{
						byte oldValue5 = this.modelScale;
						this.modelScale = stream.readUint8();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onModelScaleChanged(oldValue5);
							}
						}
						else if (this.inWorld)
						{
							this.onModelScaleChanged(oldValue5);
						}
						break;
					}
					}
					break;
				}
			}
		}

		// Token: 0x06005666 RID: 22118 RVA: 0x0023DD1C File Offset: 0x0023BF1C
		public override void callPropertysSetMethods()
		{
			Dictionary<ushort, Property> idpropertys = EntityDef.moduledefs["Spaces"].idpropertys;
			Vector3 direction = this.direction;
			Property property = idpropertys[2];
			if (property.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onDirectionChanged(direction);
				}
			}
			else if (this.inWorld && (!property.isOwnerOnly() || base.isPlayer()))
			{
				this.onDirectionChanged(direction);
			}
			uint oldValue = this.modelID;
			Property property2 = idpropertys[4];
			if (property2.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onModelIDChanged(oldValue);
				}
			}
			else if (this.inWorld && (!property2.isOwnerOnly() || base.isPlayer()))
			{
				this.onModelIDChanged(oldValue);
			}
			byte oldValue2 = this.modelScale;
			Property property3 = idpropertys[5];
			if (property3.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onModelScaleChanged(oldValue2);
				}
			}
			else if (this.inWorld && (!property3.isOwnerOnly() || base.isPlayer()))
			{
				this.onModelScaleChanged(oldValue2);
			}
			string oldValue3 = this.name;
			Property property4 = idpropertys[6];
			if (property4.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onNameChanged(oldValue3);
				}
			}
			else if (this.inWorld && (!property4.isOwnerOnly() || base.isPlayer()))
			{
				this.onNameChanged(oldValue3);
			}
			Vector3 position = this.position;
			Property property5 = idpropertys[1];
			if (property5.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onPositionChanged(position);
				}
			}
			else if (this.inWorld && (!property5.isOwnerOnly() || base.isPlayer()))
			{
				this.onPositionChanged(position);
			}
			uint oldValue4 = this.uid;
			Property property6 = idpropertys[7];
			if (property6.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onUidChanged(oldValue4);
				}
			}
			else if (this.inWorld && (!property6.isOwnerOnly() || base.isPlayer()))
			{
				this.onUidChanged(oldValue4);
			}
			uint oldValue5 = this.utype;
			Property property7 = idpropertys[8];
			if (property7.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onUtypeChanged(oldValue5);
					return;
				}
			}
			else if (this.inWorld && (!property7.isOwnerOnly() || base.isPlayer()))
			{
				this.onUtypeChanged(oldValue5);
			}
		}

		// Token: 0x04005118 RID: 20760
		public EntityBaseEntityCall_SpacesBase baseEntityCall;

		// Token: 0x04005119 RID: 20761
		public EntityCellEntityCall_SpacesBase cellEntityCall;

		// Token: 0x0400511A RID: 20762
		public uint modelID;

		// Token: 0x0400511B RID: 20763
		public byte modelScale = 30;

		// Token: 0x0400511C RID: 20764
		public string name = "";

		// Token: 0x0400511D RID: 20765
		public uint uid;

		// Token: 0x0400511E RID: 20766
		public uint utype;
	}
}
