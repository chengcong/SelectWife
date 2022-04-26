using System;

using Bridge;
using System;
using System.IO;
using static Retyped.dom;
using Console = System.Console;
using Microsoft.Xna.Framework;

namespace SelectWife.Web
{
    public class App
    {
        private static GameMain game;

        public static void Main()
        {
            var canvas = new HTMLCanvasElement();
            canvas.width = 800;
            canvas.height = 480;
            canvas.id = "monogamecanvas";
            document.body.appendChild(canvas);

            document.body.appendChild(new HTMLBRElement());

            var can = new HTMLCanvasElement();
            can.width = 800;
            can.height = 480;
            document.body.appendChild(can);

            game = new GameMain();
            game.Run();
        }
    }
}
