﻿using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCrap
{
    public class Program
    {
        [Obsolete]
        static void Main(string[] args)
        {
            try
            {
                //using Window window = new(800, 600, "David OpenTK");
                using Window window = new(GameWindowSettings.Default, NativeWindowSettings.Default);

                window.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
