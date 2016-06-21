using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace monogameTest
{
	public class Player
	{
		public Rectangle boundingBox;
		public Texture2D texture, bulletTexture;
		public Vector2 position, hpPos;
		public int speed, bulletDelay, health;
		public List<Bullet> bulletList;
		public SoundManager sm = new SoundManager ();
		public SpriteFont hpFont;

		public Player()
		{
			bulletList = new List<Bullet>();
			texture = null;
			position = new Vector2(210, 500);
			speed = 5;
			bulletDelay = 7;
			health = 100;
			hpPos = new Vector2 (0, 10);
		}

		public void LoadContent (ContentManager Content)
		{
			texture = Content.Load<Texture2D>("ShipR.png");
			bulletTexture = Content.Load<Texture2D>("playerbullet.png");
			sm.LoadContent (Content);
			hpFont = Content.Load<SpriteFont> ("Decker");
		}

		public void Draw (SpriteBatch spriteBatch)
		{
			foreach (Bullet b in bulletList)
				b.Draw(spriteBatch);
			spriteBatch.Draw (texture, position, Color.White);
			spriteBatch.DrawString (hpFont, "HP : " + health, hpPos, Color.White);
		}

		public void Update(GameTime gameTime)
		{
			boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

			KeyboardState keyState = Keyboard.GetState();

			if (keyState.IsKeyDown(Keys.Z))
				Shoot();
			UpdateBullet();

			if (keyState.IsKeyDown(Keys.Up))
				position.Y -= speed;
			if (keyState.IsKeyDown(Keys.Down))
				position.Y += speed;
			if (keyState.IsKeyDown(Keys.Left))
				position.X -= speed;
			if (keyState.IsKeyDown(Keys.Right))
				position.X += speed;

			if (position.Y < 0)
				position.Y = 0;
			if (position.Y > 640 - texture.Height)
				position.Y = 640 - texture.Height;
			if (position.X < 0)
				position.X = 0;
			if (position.X > 480 - texture.Width)
				position.X = 480 - texture.Width;
		}

		public void Shoot()
		{
			if (bulletDelay >= 0)
				bulletDelay--;
			if (bulletDelay == 0)
			{
				sm.pShootSound.Play ();
				Bullet newBullet = new Bullet(bulletTexture);
				newBullet.position = new Vector2(position.X + texture.Width / 2 - newBullet.texture.Width / 2, position.Y + texture.Height / 4);
				newBullet.isVisible = true;
				if (bulletList.Count < 50)
					bulletList.Add(newBullet);
			}
			if (bulletDelay < 0)
				bulletDelay = 7;
		}

		public void UpdateBullet()
		{
			foreach (Bullet b in bulletList)
			{
				b.boundingBox = new Rectangle((int)b.position.X, (int)b.position.Y, b.texture.Width, b.texture.Height);
				b.position.Y -= b.speed;
				if (b.position.Y <= 0)
					b.isVisible = false;
			}

			for (int i = 0; i < bulletList.Count; i++)
			{
				if (!bulletList[i].isVisible)
				{
					bulletList.RemoveAt(i);
					i--;
				}
			}
		}
	}
}

