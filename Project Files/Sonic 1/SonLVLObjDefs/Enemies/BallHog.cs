using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.Enemies
{
	class BallHog : ObjectDefinition
	{
		private Sprite[] sprites = new Sprite[2];
		private PropertySpec[] properties = new PropertySpec[2];

		public override void Init(ObjectData data)
		{
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				case '6':
				default:
					sprites[0] = new Sprite(LevelData.GetSpriteSheet("SBZ/Objects.gif").GetSection(1, 170, 22, 37), -11, -17);
					break;
				case '7':
					sprites[0] = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(76, 292, 22, 37), -11, -17);
					break;
			}
			
			sprites[1] = new Sprite(sprites[0], true, false);
			
			properties[0] = new PropertySpec("Bomb Time", typeof(int), "Extended",
				"How long thrown bombs will last, in seconds.", null,
				(obj) => obj.PropertyValue & 0x7F,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x7F) | (byte)((int)value) & 0x7F));

			properties[1] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which way the Ball Hog will be facing.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 0x80 }
				},
				(obj) => obj.PropertyValue & 0x80,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x80) | (byte)((int)value)));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 6; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return subtype + "";
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[0];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[obj.PropertyValue >> 7];
		}
	}
}