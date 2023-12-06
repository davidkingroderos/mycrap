using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

namespace MyCrap
{
    public class Camera
    {
        public Vector3 Front { get; set; }
        public Vector3 Up { get; set; }
        public Vector3 Right { get; set; }
        public float Yaw { get; set; }
    }
}
