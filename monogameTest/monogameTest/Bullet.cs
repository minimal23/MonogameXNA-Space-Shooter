using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace monogameTest
{
	public class Bullet
	{
		public Rectangle boundingBox;
		public Texture2D texture;
		public Vector2 point, position;
		public bool isVisible;
		public float speed;

		public Bullet(Texture2D newTexture)
		{
			speed = 10;
			texture = newTexture;
			isVisible = false;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(texture, position, Color.White);
		}
	}
}

