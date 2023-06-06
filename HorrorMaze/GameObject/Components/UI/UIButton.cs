

using HorrorMaze.Managers;

namespace HorrorMaze
{
   
    public delegate void ClickEvent();

    /// <summary>
    /// a UI button that can call event by being clicked
    /// Niels
    /// </summary>
    public class UIButton : Component
    {

        public event ClickEvent OnClick, OnHover;
        private Texture2D _texture;
        public Vector2 scale = new Vector2(0.25f,0.25f);
        private Vector2 origin;
        private Vector2 buttonSize;
        bool hover;
        Color color = Color.White;

        UIButton()
        {
            SetTexture("button");
        }

        public void SetTexture(string textureName)
        {
            _texture = GameWorld.Instance.Content.Load<Texture2D>(textureName);
            origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
            buttonSize = origin * scale;
        }

        public void Awake()
        {
            InputManager.buttons.Add(this);
        }

        public void CheckMouse(bool press)
        {
            if(transform.Position.X + buttonSize.X > Mouse.GetState().X &&
                transform.Position.X - buttonSize.X < Mouse.GetState().X &&
                transform.Position.Y + buttonSize.Y > Mouse.GetState().Y &&
                transform.Position.Y - buttonSize.Y < Mouse.GetState().Y)
            {
                if (!hover)
                {
                    hover = true;
                    color = Color.Gray;
                }
                if(OnHover != null)
                    OnHover.Invoke();
                if(press)
                {
                    if (OnClick != null)
                        OnClick.Invoke();
                }
            }
            else if(hover)
            {
                hover = false;
                color = Color.White;
            }
        }

        public void DrawUI(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, transform.Position,null, color,0,origin,scale,SpriteEffects.None,0.9f);
        }
    }
}
