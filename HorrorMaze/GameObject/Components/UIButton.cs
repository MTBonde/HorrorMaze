using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
   
    public delegate void ClickEvent();

    public class UIButton : Component
    {

        public Vector2 size;
        public event ClickEvent OnClick, OnHover;


        public void Update()
        {
            if(transform.Position.X + size.X / 2 > Mouse.GetState().X &&
                transform.Position.X - size.X / 2 < Mouse.GetState().X &&
                transform.Position.Y + size.Y / 2 > Mouse.GetState().Y &&
                transform.Position.Y - size.Y / 2 < Mouse.GetState().Y)
            {
                if(OnHover != null)
                    OnHover.Invoke();
                if(Mouse.GetState().LeftButton.HasFlag(ButtonState.Pressed))
                {
                    if (OnClick != null)
                        OnClick.Invoke();
                }
            }
        }
    }
}
