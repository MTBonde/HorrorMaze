using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze.Managers
{
    public static class InputManager
    {

        public static List<UIButton> buttons = new List<UIButton>();
        static bool mouseDown;

        public static void Update()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                if (Mouse.GetState().LeftButton.HasFlag(ButtonState.Pressed) && !mouseDown)
                {
                    mouseDown = true;
                    buttons[i].CheckMouse(true);
                }
                else if (Mouse.GetState().LeftButton.HasFlag(ButtonState.Released) && mouseDown)
                {
                    mouseDown = false;
                    buttons[i].CheckMouse(false);
                }
                else
                {
                    buttons[i].CheckMouse(false);
                }
            }
        }

        public static void Reset()
        {
            buttons.Clear();
        }
    }
}
