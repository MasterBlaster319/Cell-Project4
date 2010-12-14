using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Cell_Project4
{
    class GameObject
    {
        public Texture2D sprite;
        public Vector2 position;
        public float rotation;
        public Vector2 center;
        public Vector2 velocity;
        public bool alive;


        // Create a GamneObject object and associate a Texture with it.  Hooray!
        public GameObject(Texture2D LoadedTexture)
        {
            rotation = 0.0f;
            position = Vector2.Zero;
            sprite = LoadedTexture;
            center = new Vector2(sprite.Width / 2, sprite.Height / 2);
            velocity = Vector2.Zero;
            alive = false;
        }
    }
}
