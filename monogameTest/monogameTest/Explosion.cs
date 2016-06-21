using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace monogameTest
{
	public class Explosion
	{
		public Texture2D texture;
		public Vector2 position, origin;
		public float timer, interval;
		public int currentFrame, spriteWidth, spriteHeight;
		public Rectangle sourceRect;
		public bool isVisible;

		public Explosion (Texture2D newTexture, Vector2 newPos)
		{
			position = newPos;
			texture = newTexture;
			timer = 0f;
			interval = 0f;
			currentFrame = 1;
			spriteWidth = 128;
			spriteHeight = 128;
			isVisible = true;
		}

		public void LoadContent (ContentManager Content)
		{
		}

		public void Update (GameTime gameTime)
		{
			timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
			if (timer > interval) 
			{
				currentFrame++;
				timer = 0f;
			}
			if (currentFrame == 17) 
			{
				isVisible = false;
				currentFrame = 0;
			}
			sourceRect = new Rectangle (currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);
			origin = new Vector2 (sourceRect.Width / 2, sourceRect.Height / 2);
		}

		public void Draw (SpriteBatch spriteBatch)
		{
			if (isVisible)
				spriteBatch.Draw (texture, position, sourceRect, Color.White, 0f, origin, 0.6f, SpriteEffects.None, 0);
		}
	}
}

