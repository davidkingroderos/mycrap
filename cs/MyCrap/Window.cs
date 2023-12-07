//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Runtime.ConstrainedExecution;
//using System.Text;
//using System.Threading.Tasks;
//using OpenTK.Graphics.OpenGL4;
//using OpenTK.Mathematics;
//using OpenTK.Windowing.Common;
//using OpenTK.Windowing.Desktop;
//using OpenTK.Windowing.GraphicsLibraryFramework;

//namespace MyCrap
//{
//    public class Window : GameWindow
//    {
//        private float[] cube = {
//        -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,
//        -0.5f,  0.5f,  0.5f,  0.0f,  1.0f,
//         0.5f,  0.5f,  0.5f,  1.0f,  1.0f,
//        -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,
//         0.5f, -0.5f,  0.5f,  1.0f,  0.0f,
//         0.5f,  0.5f,  0.5f,  1.0f,  1.0f,

//        -0.5f, -0.5f, -0.5f,  0.0f,  0.0f,
//        -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,
//         0.5f,  0.5f, -0.5f,  1.0f,  1.0f,
//        -0.5f, -0.5f, -0.5f,  0.0f,  0.0f,
//         0.5f, -0.5f, -0.5f,  1.0f,  0.0f,
//         0.5f,  0.5f, -0.5f,  1.0f,  1.0f,

//        -0.5f, -0.5f, -0.5f,  0.0f,  0.0f,
//        -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,
//        -0.5f,  0.5f,  0.5f,  1.0f,  1.0f,
//        -0.5f, -0.5f, -0.5f,  0.0f,  0.0f,
//        -0.5f, -0.5f,  0.5f,  1.0f,  0.0f,
//        -0.5f,  0.5f,  0.5f,  1.0f,  1.0f,

//         0.5f, -0.5f, -0.5f,  0.0f,  0.0f,
//         0.5f,  0.5f, -0.5f,  0.0f,  1.0f,
//         0.5f,  0.5f,  0.5f,  1.0f,  1.0f,
//         0.5f, -0.5f, -0.5f,  0.0f,  0.0f,
//         0.5f, -0.5f,  0.5f,  1.0f,  0.0f,
//         0.5f,  0.5f,  0.5f,  1.0f,  1.0f,

//        -0.5f,  0.5f,  0.5f,  0.0f,  0.0f,
//        -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,
//         0.5f,  0.5f, -0.5f,  1.0f,  1.0f,
//        -0.5f,  0.5f,  0.5f,  0.0f,  0.0f,
//         0.5f,  0.5f,  0.5f,  1.0f,  0.0f,
//         0.5f,  0.5f, -0.5f,  1.0f,  1.0f,

//        -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,
//        -0.5f, -0.5f, -0.5f,  0.0f,  1.0f,
//         0.5f, -0.5f, -0.5f,  1.0f,  1.0f,
//        -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,
//         0.5f, -0.5f,  0.5f,  1.0f,  0.0f,
//         0.5f, -0.5f, -0.5f,  1.0f,  1.0f,
//        };

//        private int vao;
//        private int vbo;
//        private Shader? shader;
//        private Texture? texture;
//        private Camera? camera;
//        private bool firstMove = true;
//        private Vector2 lastPos;
//        private double time;

//        public int ScreenWidth { get; private set; }
//        public int ScreenHeight { get; private set; }

//        [Obsolete]
//        public Window(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings()
//        {
//            Size = (width, height),
//            Title = title
//        })
//        {
//            this.CenterWindow(new Vector2i(width, height));
//            ScreenWidth = width;
//            ScreenHeight = height;
//        }

//        protected override void OnResize(ResizeEventArgs e)
//        {
//            base.OnResize(e);

//            GL.Viewport(0, 0, e.Width, e.Height);
//            this.ScreenWidth = e.Width;
//            this.ScreenHeight = e.Height;

//            camera!.AspectRatio = Size.X / (float)Size.Y;
//        }

//        protected override void OnLoad()
//        {
//            base.OnLoad();

//            GL.ClearColor(0.0f, 1.0f, 1.0f, 1.0f);
//            GL.Enable(EnableCap.DepthTest);

//            shader = new("../../../Shaders/vert.shader", "../../../Shaders/frag.shader");

//            vao = GL.GenVertexArray();
//            vbo = GL.GenBuffer();

//            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
//            GL.BufferData(BufferTarget.ArrayBuffer, cube.Length * sizeof(float), cube, BufferUsageHint.StaticDraw);

//            GL.BindVertexArray(vao);

//            var vertexLocation = shader.GetAttribLocation("aPos");
//            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
//            GL.EnableVertexAttribArray(vertexLocation);

//            var texCoordLocation = shader.GetAttribLocation("aTexCoord");
//            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
//            GL.EnableVertexAttribArray(texCoordLocation);

//            texture = Texture.LoadFromFile("../../../Resources/sidegrass.png");
//            texture.Use(TextureUnit.Texture0);

//            shader.SetInt("ourTexture", 0);

//            camera = new Camera(Vector3.UnitZ * 3, Size.X / (float)Size.Y);

//            CursorState = CursorState.Grabbed;

//            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
//            GL.BindVertexArray(0);
//        }

//        protected override void OnUnload()
//        {
//            base.OnUnload();

//            GL.DeleteVertexArray(vao);
//            GL.DeleteBuffer(vbo);
//        }

//        protected override void OnRenderFrame(FrameEventArgs args)
//        {
//            base.OnRenderFrame(args);

//            time += 4.0 * args.Time;

//            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

//            shader?.Use();
//            texture?.Use(TextureUnit.Texture0);
//            GL.BindVertexArray(vao);

//            //var model = Matrix4.Identity * Matrix4.CreateRotationX((float)MathHelper.RadiansToDegrees(time));
//            //shader?.SetMatrix4("model", model);
//            shader?.SetMatrix4("view", camera!.GetViewMatrix());
//            shader?.SetMatrix4("projection", camera!.GetProjectionMatrix());

//            GL.DrawArrays(PrimitiveType.Triangles, 0, 24);

//            Context.SwapBuffers();
//        }

//        protected override void OnUpdateFrame(FrameEventArgs args)
//        {
//            base.OnUpdateFrame(args);

//            if (!IsFocused) return;

//            var input = KeyboardState;

//            if (input.IsKeyDown(Keys.Escape))
//            {
//                Close();
//            }

//            const float cameraSpeed = 1.5f;
//            const float sensitivity = 0.2f;

//            if (input.IsKeyDown(Keys.W))
//            {
//                camera!.Position += camera!.Front * cameraSpeed * (float)args.Time;
//            }
//            if (input.IsKeyDown(Keys.S))
//            {
//                camera!.Position -= camera.Front * cameraSpeed * (float)args.Time;
//            }
//            if (input.IsKeyDown(Keys.A))
//            {
//                camera!.Position += camera.Right * cameraSpeed * (float)args.Time;
//            }
//            if (input.IsKeyDown(Keys.D))
//            {
//                camera!.Position -= camera.Right * cameraSpeed * (float)args.Time;
//            }
//            if (input.IsKeyDown(Keys.Space))
//            {
//                camera!.Position += camera.Up * cameraSpeed * (float)args.Time;
//            }
//            if (input.IsKeyDown(Keys.LeftShift))
//            {
//                camera!.Position -= camera.Up * cameraSpeed * (float)args.Time;
//            }

//            var mouse = MouseState;

//            if (firstMove)
//            {
//                lastPos = new Vector2(mouse.X, mouse.Y);
//                firstMove = false;
//            }
//            else
//            {
//                var deltaX = mouse.X - lastPos.X;
//                var deltaY = mouse.Y - lastPos.Y;
//                lastPos = new Vector2(mouse.X, mouse.Y);

//                camera!.Yaw += deltaX * sensitivity;
//                camera!.Pitch -= deltaY * sensitivity;
//            }
//        }

//        protected override void OnMouseWheel(MouseWheelEventArgs e)
//        {
//            base.OnMouseWheel(e);

//            camera!.Fov -= e.OffsetY;
//        }
//    }
//}
using System;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;

namespace MyCrap
{
    // We now have a rotating rectangle but how can we make the view move based on the users input?
    // In this tutorial we will take a look at how you could implement a camera class
    // and start responding to user input.
    // You can move to the camera class to see a lot of the new code added.
    // Otherwise you can move to Load to see how the camera is initialized.

    // In reality, we can't move the camera but we actually move the rectangle.
    // This will explained more in depth in the web version, however it pretty much gives us the same result
    // as if the view itself was moved.
    public class Window : GameWindow
    {
        private readonly float[] _vertices =
        {
            // Position         Texture coordinates
             0.5f,  0.5f, 0.0f, 1.0f, 1.0f, // top right
             0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f  // top left
        };

        private readonly uint[] _indices =
        {
            0, 1, 3,
            1, 2, 3
        };

        private int _elementBufferObject;

        private int _vertexBufferObject;

        private int _vertexArrayObject;

        private Shader _shader;

        private Texture _texture;

        private Texture _texture2;

        // The view and projection matrices have been removed as we don't need them here anymore.
        // They can now be found in the new camera class.

        // We need an instance of the new camera class so it can manage the view and projection matrix code.
        // We also need a boolean set to true to detect whether or not the mouse has been moved for the first time.
        // Finally, we add the last position of the mouse so we can calculate the mouse offset easily.
        private Camera _camera;

        private bool _firstMove = true;

        private Vector2 _lastPos;

        private double _time;

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            GL.Enable(EnableCap.DepthTest);

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            _elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

            _shader = new Shader("../../../Shaders/vert.shader", "../../../Shaders/frag.shader");
            _shader.Use();

            var vertexLocation = _shader.GetAttribLocation("aPos");
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

            var texCoordLocation = _shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            _texture = Texture.LoadFromFile("../../../Resources/sidegrass.png");
            _texture.Use(TextureUnit.Texture0);

            _shader.SetInt("ourTexture", 0);

            // We initialize the camera so that it is 3 units back from where the rectangle is.
            // We also give it the proper aspect ratio.
            _camera = new Camera(Vector3.UnitZ * 3, Size.X / (float)Size.Y);

            // We make the mouse cursor invisible and captured so we can have proper FPS-camera movement.
            CursorState = CursorState.Grabbed;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            _time += 4.0 * e.Time;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.BindVertexArray(_vertexArrayObject);

            _texture.Use(TextureUnit.Texture0);
            _shader.Use();

            //var model = Matrix4.Identity * Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(_time));
            //_shader.SetMatrix4("model", model);
            _shader.SetMatrix4("view", _camera.GetViewMatrix());
            _shader.SetMatrix4("projection", _camera.GetProjectionMatrix());

            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (!IsFocused) // Check to see if the window is focused
            {
                return;
            }

            var input = KeyboardState;

            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            const float cameraSpeed = 1.5f;
            const float sensitivity = 0.2f;

            if (input.IsKeyDown(Keys.W))
            {
                _camera.Position += _camera.Front * cameraSpeed * (float)e.Time; // Forward
            }

            if (input.IsKeyDown(Keys.S))
            {
                _camera.Position -= _camera.Front * cameraSpeed * (float)e.Time; // Backwards
            }
            if (input.IsKeyDown(Keys.A))
            {
                _camera.Position -= _camera.Right * cameraSpeed * (float)e.Time; // Left
            }
            if (input.IsKeyDown(Keys.D))
            {
                _camera.Position += _camera.Right * cameraSpeed * (float)e.Time; // Right
            }
            if (input.IsKeyDown(Keys.Space))
            {
                _camera.Position += _camera.Up * cameraSpeed * (float)e.Time; // Up
            }
            if (input.IsKeyDown(Keys.LeftShift))
            {
                _camera.Position -= _camera.Up * cameraSpeed * (float)e.Time; // Down
            }

            // Get the mouse state
            var mouse = MouseState;

            if (_firstMove) // This bool variable is initially set to true.
            {
                _lastPos = new Vector2(mouse.X, mouse.Y);
                _firstMove = false;
            }
            else
            {
                // Calculate the offset of the mouse position
                var deltaX = mouse.X - _lastPos.X;
                var deltaY = mouse.Y - _lastPos.Y;
                _lastPos = new Vector2(mouse.X, mouse.Y);

                // Apply the camera pitch and yaw (we clamp the pitch in the camera class)
                _camera.Yaw -= deltaX * sensitivity;
                _camera.Pitch -= deltaY * sensitivity; // Reversed since y-coordinates range from bottom to top
            }
        }

        // In the mouse wheel function, we manage all the zooming of the camera.
        // This is simply done by changing the FOV of the camera.
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            _camera.Fov -= e.OffsetY;
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Size.X, Size.Y);
            // We need to update the aspect ratio once the window has been resized.
            _camera.AspectRatio = Size.X / (float)Size.Y;
        }
    }
}
