using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace spotifycmd
{
    static class Program
    {
        const int WM_APPCOMMAND = 0x0319;

        const long SPOTIFY_NEXT = 720896;
        const long SPOTIFY_PLAYPAUSE = 917504;
        const long SPOTIFY_MUTE = 524288;
        const long SPOTIFY_VOLUP = 655360;
        const long SPOTIFY_VOLDOWN = 589824;
        const long SPOTIFY_STOP = 851968;
        const long SPOTIFY_PREV = 786432;

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length < 1) return;

            IntPtr hWnd = FindWindow("SpotifyMainWindow", null);

            if (hWnd.ToInt32() != 0)
            {
                long cmd = 0;
                switch (args[0])
                {
                    case "next":
                        cmd = SPOTIFY_NEXT;
                        break;
                }

                IntPtr param = new IntPtr(cmd);

                SendMessage(hWnd, WM_APPCOMMAND, IntPtr.Zero, param);
            }

        }
    }
}
