using System;
using System.Collections.Generic;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000FEE RID: 4078
	public abstract class SpaceBase : Entity
	{
		// Token: 0x06006080 RID: 24704 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onModelIDChanged(uint oldValue)
		{
		}

		// Token: 0x06006081 RID: 24705 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onModelScaleChanged(byte oldValue)
		{
		}

		// Token: 0x06006082 RID: 24706 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onNameChanged(string oldValue)
		{
		}

		// Token: 0x06006083 RID: 24707 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onUidChanged(uint oldValue)
		{
		}

		// Token: 0x06006084 RID: 24708 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onUtypeChanged(uint oldValue)
		{
		}

		// Token: 0x06006085 RID: 24709 RVA: 0x00042FF7 File Offset: 0x000411F7
		public SpaceBase()
		{
		}

		// Token: 0x06006086 RID: 24710 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onComponentsEnterworld()
		{
		}

		// Token: 0x06006087 RID: 24711 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onComponentsLeaveworld()
		{
		}

		// Token: 0x06006088 RID: 24712 RVA: 0x00043012 File Offset: 0x00041212
		public override void onGetBase()
		{
			this.baseEntityCall = new EntityBaseEntityCall_SpaceBase(this.id, this.className);
		}

		// Token: 0x06006089 RID: 24713 RVA: 0x0004302B File Offset: 0x0004122B
		public override void onGetCell()
		{
			this.cellEntityCall = new EntityCellEntityCall_SpaceBase(this.id, this.className);
		}

		// Token: 0x0600608A RID: 24714 RVA: 0x00043044 File Offset: 0x00041244
		public override void onLoseCell()
		{
			this.cellEntityCall = null;
		}

		// Token: 0x0600608B RID: 24715 RVA: 0x0004304D File Offset: 0x0004124D
		public override EntityCall getBaseEntityCall()
		{
			return this.baseEntityCall;
		}

		// Token: 0x0600608C RID: 24716 RVA: 0x00043055 File Offset: 0x00041255
		public override EntityCall getCellEntityCall()
		{
			return this.cellEntityCall;
		}

		// Token: 0x0600608D RID: 24717 RVA: 0x000042DD File Offset: 0x000024DD
		public override void attachComponents()
		{
		}

		// Token: 0x0600608E RID: 24718 RVA: 0x000042DD File Offset: 0x000024DD
		public override void detachComponents()
		{
		}

		// Token: 0x0600608F RID: 24719 RVA: 0x00269850 File Offset: 0x00267A50
		public override void onRemoteMethodCall(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["Space"];
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

		// Token: 0x06006090 RID: 24720 RVA: 0x002698D0 File Offset: 0x00267AD0
		public override void onUpdatePropertys(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["Space"];
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

		// Token: 0x06006091 RID: 24721 RVA: 0x00269BA4 File Offset: 0x00267DA4
		public override void callPropertysSetMethods()
		{
			Dictionary<ushort, Property> idpropertys = EntityDef.moduledefs["Space"].idpropertys;
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

		// Token: 0x04005BC4 RID: 23492
		public EntityBaseEntityCall_SpaceBase baseEntityCall;

		// Token: 0x04005BC5 RID: 23493
		public EntityCellEntityCall_SpaceBase cellEntityCall;

		// Token: 0x04005BC6 RID: 23494
		public uint modelID;

		// Token: 0x04005BC7 RID: 23495
		public byte modelScale = 30;

		// Token: 0x04005BC8 RID: 23496
		public string name = "";

		// Token: 0x04005BC9 RID: 23497
		public uint uid;

		// Token: 0x04005BCA RID: 23498
		public uint utype;
	}
}
