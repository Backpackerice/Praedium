﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Malison.Core;
using Malison.WinForms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Praedium.Core;

namespace Praedium.App
{
    public partial class AppForm : TerminalForm
    {
        [DllImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PeekMessage(out NativeMessage message, IntPtr window, uint filterMin, uint filterMax, uint remove);

        NativeMessage message;
        Game _game;
        KeyInfo _lastKeyInfo;


        public AppForm()
        {
            InitializeComponent();
            _game = new Game(Terminal);

            Application.Idle += Application_Idle;
            TerminalControl.KeyDown += TerminalControl_KeyDown;
            TerminalControl.KeyUp += TerminalControl_KeyUp;
        }
        
        void TerminalControl_KeyUp(object sender, KeyEventArgs e)
        {
            KeyInfo info;

            if (PrepareKeyInfo(e, out info))
            {
                info.Down = false;
                _lastKeyInfo = info;
            }
        }

        void TerminalControl_KeyDown(object sender, KeyEventArgs e)
        {
            KeyInfo info;

            if (PrepareKeyInfo(e, out info))
            {
                info.Down = true;
                _lastKeyInfo = info;
            }
        }

        void Application_Idle(object sender, EventArgs e)
        {
            while (!PeekMessage(out message, IntPtr.Zero, 0, 0, 0))
            {
                _game.ApplyKeyData(_lastKeyInfo);
                _game.Tick();
            }
        }

        public bool PrepareKeyInfo(KeyEventArgs args, out KeyInfo info)
        {
            bool recognized = true;

            info = new KeyInfo();

            switch (args.KeyCode)
            {
                case Keys.Up: info.Key = Key.Up; break;
                case Keys.Down: info.Key = Key.Down; break;
                case Keys.Left: info.Key = Key.Left; break;
                case Keys.Right: info.Key = Key.Right; break;

                case Keys.A: info.Key = Key.A; break;
                case Keys.B: info.Key = Key.B; break;
                case Keys.C: info.Key = Key.C; break;
                case Keys.D: info.Key = Key.D; break;
                case Keys.E: info.Key = Key.E; break;
                case Keys.F: info.Key = Key.F; break;
                case Keys.G: info.Key = Key.G; break;
                case Keys.H: info.Key = Key.H; break;
                case Keys.I: info.Key = Key.I; break;
                case Keys.J: info.Key = Key.J; break;
                case Keys.K: info.Key = Key.K; break;
                case Keys.L: info.Key = Key.L; break;
                case Keys.M: info.Key = Key.M; break;
                case Keys.N: info.Key = Key.N; break;
                case Keys.O: info.Key = Key.O; break;
                case Keys.P: info.Key = Key.P; break;
                case Keys.Q: info.Key = Key.Q; break;
                case Keys.R: info.Key = Key.R; break;
                case Keys.S: info.Key = Key.S; break;
                case Keys.T: info.Key = Key.T; break;
                case Keys.U: info.Key = Key.U; break;
                case Keys.V: info.Key = Key.V; break;
                case Keys.W: info.Key = Key.W; break;
                case Keys.X: info.Key = Key.X; break;
                case Keys.Y: info.Key = Key.Y; break;
                case Keys.Z: info.Key = Key.Z; break;

                case Keys.D0: info.Key = Key.D0; break;
                case Keys.D1: info.Key = Key.D1; break;
                case Keys.D2: info.Key = Key.D2; break;
                case Keys.D3: info.Key = Key.D3; break;
                case Keys.D4: info.Key = Key.D4; break;
                case Keys.D5: info.Key = Key.D5; break;
                case Keys.D6: info.Key = Key.D6; break;
                case Keys.D7: info.Key = Key.D7; break;
                case Keys.D8: info.Key = Key.D8; break;
                case Keys.D9: info.Key = Key.D9; break;

                case Keys.Enter: info.Key = Key.Enter; break;
                case Keys.Tab: info.Key = Key.Tab; break;
                case Keys.Back: info.Key = Key.Backspace; break;
                case Keys.Delete: info.Key = Key.Delete; break;
                case Keys.Escape: info.Key = Key.Escape; break;

                case Keys.OemSemicolon: info.Key = Key.Semicolon; break;
                case Keys.Oemcomma: info.Key = Key.Comma; break;
                case Keys.OemPeriod: info.Key = Key.Period; break;
                case Keys.OemQuestion: info.Key = Key.Slash; break;

                case Keys.F1: info.Key = Key.F1; break;
                case Keys.F2: info.Key = Key.F2; break;
                case Keys.F3: info.Key = Key.F3; break;
                case Keys.F4: info.Key = Key.F4; break;
                case Keys.F5: info.Key = Key.F5; break;
                case Keys.F6: info.Key = Key.F6; break;
                case Keys.F7: info.Key = Key.F7; break;
                case Keys.F8: info.Key = Key.F8; break;
                case Keys.F9: info.Key = Key.F9; break;
                case Keys.F10: info.Key = Key.F10; break;
                case Keys.F11: info.Key = Key.F11; break;
                case Keys.F12: info.Key = Key.F12; break;

                default: 
                    recognized = false;
                    break;
            }

            info.Shift = args.Shift;
            info.Control = args.Control;
            info.Alt = args.Alt;

            return recognized;
        }
    }
}