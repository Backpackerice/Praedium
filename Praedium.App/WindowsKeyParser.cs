using System;
using Praedium.Engine.UI;
using SFML.Window;

namespace Praedium.App
{
    public class WindowsKeyParser : IKeyParser
    {
        public KeyEventArgs ReceivedEventArgs
        {
            get;
            private set;
        }

        public WindowsKeyParser(KeyEventArgs args)
        {
            ReceivedEventArgs = args;
        }

        public bool Parse(out KeyInfo info)
        {
            bool recognized = true;

            info = new KeyInfo();

            switch (ReceivedEventArgs.Code)
            {
                case SFML.Window.Keyboard.Key.Up: info.Key = Key.Up; break;
                case SFML.Window.Keyboard.Key.Down: info.Key = Key.Down; break;
                case SFML.Window.Keyboard.Key.Left: info.Key = Key.Left; break;
                case SFML.Window.Keyboard.Key.Right: info.Key = Key.Right; break;

                case SFML.Window.Keyboard.Key.A: info.Key = Key.A; break;
                case SFML.Window.Keyboard.Key.B: info.Key = Key.B; break;
                case SFML.Window.Keyboard.Key.C: info.Key = Key.C; break;
                case SFML.Window.Keyboard.Key.D: info.Key = Key.D; break;
                case SFML.Window.Keyboard.Key.E: info.Key = Key.E; break;
                case SFML.Window.Keyboard.Key.F: info.Key = Key.F; break;
                case SFML.Window.Keyboard.Key.G: info.Key = Key.G; break;
                case SFML.Window.Keyboard.Key.H: info.Key = Key.H; break;
                case SFML.Window.Keyboard.Key.I: info.Key = Key.I; break;
                case SFML.Window.Keyboard.Key.J: info.Key = Key.J; break;
                case SFML.Window.Keyboard.Key.K: info.Key = Key.K; break;
                case SFML.Window.Keyboard.Key.L: info.Key = Key.L; break;
                case SFML.Window.Keyboard.Key.M: info.Key = Key.M; break;
                case SFML.Window.Keyboard.Key.N: info.Key = Key.N; break;
                case SFML.Window.Keyboard.Key.O: info.Key = Key.O; break;
                case SFML.Window.Keyboard.Key.P: info.Key = Key.P; break;
                case SFML.Window.Keyboard.Key.Q: info.Key = Key.Q; break;
                case SFML.Window.Keyboard.Key.R: info.Key = Key.R; break;
                case SFML.Window.Keyboard.Key.S: info.Key = Key.S; break;
                case SFML.Window.Keyboard.Key.T: info.Key = Key.T; break;
                case SFML.Window.Keyboard.Key.U: info.Key = Key.U; break;
                case SFML.Window.Keyboard.Key.V: info.Key = Key.V; break;
                case SFML.Window.Keyboard.Key.W: info.Key = Key.W; break;
                case SFML.Window.Keyboard.Key.X: info.Key = Key.X; break;
                case SFML.Window.Keyboard.Key.Y: info.Key = Key.Y; break;
                case SFML.Window.Keyboard.Key.Z: info.Key = Key.Z; break;

                case SFML.Window.Keyboard.Key.Num0: info.Key = Key.D0; break;
                case SFML.Window.Keyboard.Key.Num1: info.Key = Key.D1; break;
                case SFML.Window.Keyboard.Key.Num2: info.Key = Key.D2; break;
                case SFML.Window.Keyboard.Key.Num3: info.Key = Key.D3; break;
                case SFML.Window.Keyboard.Key.Num4: info.Key = Key.D4; break;
                case SFML.Window.Keyboard.Key.Num5: info.Key = Key.D5; break;
                case SFML.Window.Keyboard.Key.Num6: info.Key = Key.D6; break;
                case SFML.Window.Keyboard.Key.Num7: info.Key = Key.D7; break;
                case SFML.Window.Keyboard.Key.Num8: info.Key = Key.D8; break;
                case SFML.Window.Keyboard.Key.Num9: info.Key = Key.D9; break;

                //case SFML.Window.Keyboard.Key.Enter: info.Key = Key.Enter; break;
                case SFML.Window.Keyboard.Key.Tab: info.Key = Key.Tab; break;
                case SFML.Window.Keyboard.Key.Back: info.Key = Key.Backspace; break;
                case SFML.Window.Keyboard.Key.Delete: info.Key = Key.Delete; break;
                case SFML.Window.Keyboard.Key.Escape: info.Key = Key.Escape; break;

                case SFML.Window.Keyboard.Key.SemiColon: info.Key = Key.Semicolon; break;
                case SFML.Window.Keyboard.Key.Comma: info.Key = Key.Comma; break;
                case SFML.Window.Keyboard.Key.Period: info.Key = Key.Period; break;
                //case SFML.Window.Keyboard.Key.Question: info.Key = Key.Slash; break;

                case SFML.Window.Keyboard.Key.F1: info.Key = Key.F1; break;
                case SFML.Window.Keyboard.Key.F2: info.Key = Key.F2; break;
                case SFML.Window.Keyboard.Key.F3: info.Key = Key.F3; break;
                case SFML.Window.Keyboard.Key.F4: info.Key = Key.F4; break;
                case SFML.Window.Keyboard.Key.F5: info.Key = Key.F5; break;
                case SFML.Window.Keyboard.Key.F6: info.Key = Key.F6; break;
                case SFML.Window.Keyboard.Key.F7: info.Key = Key.F7; break;
                case SFML.Window.Keyboard.Key.F8: info.Key = Key.F8; break;
                case SFML.Window.Keyboard.Key.F9: info.Key = Key.F9; break;
                case SFML.Window.Keyboard.Key.F10: info.Key = Key.F10; break;
                case SFML.Window.Keyboard.Key.F11: info.Key = Key.F11; break;
                case SFML.Window.Keyboard.Key.F12: info.Key = Key.F12; break;

                default: 
                    recognized = false;
                    break;
            }

            return recognized;
        }
    }
}
