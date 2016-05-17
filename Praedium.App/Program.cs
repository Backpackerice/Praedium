using Malison.Core;
using Malison.SFML;
using Praedium.Core.Levels;
using Praedium.Engine;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praedium.App
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var width = 80u;
            var height = 48u;
            var videoMode = new VideoMode(width * 16, height * 16);
            var sheetTexture = new Texture(@"Resources/cp437_16x16.png");
            var sheet = new GlyphSheet(sheetTexture, 16, 16);
            //var terminal = new Terminal((int)width, (int)height, Encoding.GetEncoding(437));
            var window = new RenderWindow(videoMode, "Praedium");
            window.SetFramerateLimit(60);

            Game g = new Game(sheet, window);

            g.LoadLevel(new FarmLevel());

            g.Run();
        }
    }
}
