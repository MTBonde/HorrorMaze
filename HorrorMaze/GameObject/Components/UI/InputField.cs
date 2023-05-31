

namespace HorrorMaze
{
    public class InputField : Component
    {

        public string input = "";
        private Keys[] lastPressedKeys;
        int dots;
        float time, timer = 0.5f;

        public void Update()
        {
            KeyboardState keyState = Keyboard.GetState();

            Keys[] pressedKeys = keyState.GetPressedKeys();

            // Exceptions to handle special inputs
            if (input.Length > 0)
            {
                if (keyState.IsKeyDown(Keys.Back) && !lastPressedKeys.Contains(Keys.Back)) // Delete a character
                    input = input.Remove(input.Length - 1, 1);
                else if (keyState.IsKeyDown(Keys.Space) && !lastPressedKeys.Contains(Keys.Space)) // Create a space in the string
                    input += ' ';
            }

            for (int i = 0; i < pressedKeys.Length; ++i) // Check every pressed key
            {
                if (lastPressedKeys.Contains(pressedKeys[i])) // Only handle first keypress and ignore keys that are held down
                    continue;
                if (input.Length == 20)//set max input length
                    return;

                string str = KeyToStringChar(pressedKeys[i]); // Convert pressed key to a string
                if (str != null)
                    input += str;
            }

            lastPressedKeys = pressedKeys;
            if (!string.IsNullOrWhiteSpace(input))
                gameObject.GetComponent<TextRenderer>().SetText(input);
            else
            {
                time += Globals.DeltaTime;
                if (time > timer)
                {
                    time = 0;
                    if (dots == 3)
                    {
                        gameObject.GetComponent<TextRenderer>().SetText("Enter name...");
                        dots = 0;
                        return;
                    }
                    else
                    {
                        string theDots = "";
                        for (int i = 0; i < dots; i++)
                        {
                            theDots += ".";
                        }
                        gameObject.GetComponent<TextRenderer>().SetText("Enter name" + theDots);
                    }
                    dots++;
                }
            }
        }

        private static string KeyToStringChar(Keys key) => key switch // A C#9 logical pattern
        {
            // Numbers
            >= Keys.D0 and <= Keys.D9 => key.ToString().TrimStart('D'), // Numbers are given as D1, D2 etc. So just remove the D

            // Letters
            >= Keys.A and <= Keys.Z => key.ToString(), // Can be converted to string as is

            // Special characters ÆØÅ
            // These need to be added to fonts to be displayed correctly. The character codes to add to fonts are commented below
            Keys.OemCloseBrackets => "Å", // &#197;
            Keys.OemTilde => "Æ", // &#198;
            Keys.OemQuotes => "Ø", // &#216;

            // All other keys ignored
            _ => null
        };
    }
}
