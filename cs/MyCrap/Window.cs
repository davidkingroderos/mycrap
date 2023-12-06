using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace MyCrap
{
    public class Window : GameWindow
    {
        private float[] cube = {
        -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,
        -0.5f,  0.5f,  0.5f,  0.0f,  1.0f,
         0.5f,  0.5f,  0.5f,  1.0f,  1.0f,
        -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,
         0.5f, -0.5f,  0.5f,  1.0f,  0.0f,
         0.5f,  0.5f,  0.5f,  1.0f,  1.0f,

        -0.5f, -0.5f, -0.5f,  0.0f,  0.0f,
        -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,
         0.5f,  0.5f, -0.5f,  1.0f,  1.0f,
        -0.5f, -0.5f, -0.5f,  0.0f,  0.0f,
         0.5f, -0.5f, -0.5f,  1.0f,  0.0f,
         0.5f,  0.5f, -0.5f,  1.0f,  1.0f,

        -0.5f, -0.5f, -0.5f,  0.0f,  0.0f,
        -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,
        -0.5f,  0.5f,  0.5f,  1.0f,  1.0f,
        -0.5f, -0.5f, -0.5f,  0.0f,  0.0f,
        -0.5f, -0.5f,  0.5f,  1.0f,  0.0f,
        -0.5f,  0.5f,  0.5f,  1.0f,  1.0f,

         0.5f, -0.5f, -0.5f,  0.0f,  0.0f,
         0.5f,  0.5f, -0.5f,  0.0f,  1.0f,
         0.5f,  0.5f,  0.5f,  1.0f,  1.0f,
         0.5f, -0.5f, -0.5f,  0.0f,  0.0f,
         0.5f, -0.5f,  0.5f,  1.0f,  0.0f,
         0.5f,  0.5f,  0.5f,  1.0f,  1.0f,

        -0.5f,  0.5f,  0.5f,  0.0f,  0.0f,
        -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,
         0.5f,  0.5f, -0.5f,  1.0f,  1.0f,
        -0.5f,  0.5f,  0.5f,  0.0f,  0.0f,
         0.5f,  0.5f,  0.5f,  1.0f,  0.0f,
         0.5f,  0.5f, -0.5f,  1.0f,  1.0f,

        -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,
        -0.5f, -0.5f, -0.5f,  0.0f,  1.0f,
         0.5f, -0.5f, -0.5f,  1.0f,  1.0f,
        -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,
         0.5f, -0.5f,  0.5f,  1.0f,  0.0f,
         0.5f, -0.5f, -0.5f,  1.0f,  1.0f,
    };

        private int vao;
        private int vbo;
        private Shader shader;

        public int ScreenWidth { get; private set; }
        public int ScreenHeight { get; private set; }

        public Window(int width, int height) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            this.CenterWindow(new Vector2i(width, height));
            ScreenWidth = width;
            ScreenHeight = height;
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, e.Width, e.Height);
            this.ScreenWidth = e.Width;
            this.ScreenHeight = e.Height;
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            vao = GL.GenVertexArray();
            vbo = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, cube.Length * sizeof(float), cube, BufferUsageHint.StaticDraw);

            GL.BindVertexArray(vao);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);

            shader = new("../../../Shaders/vert.shader", "../../../Shaders/frag.shader");
        }

        protected override void OnUnload()
        {
            base.OnUnload();

            GL.DeleteVertexArray(vao);
            GL.DeleteBuffer(vbo);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.ClearColor(0.0f, 1.0f, 1.0f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            shader.Use();
            GL.BindVertexArray(vao);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);

            Context.SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
        }
    }
}
