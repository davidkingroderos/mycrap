﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCrap
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (Window window = new(800, 600))
            {
                window.Run();
            }
        }
    }
}
