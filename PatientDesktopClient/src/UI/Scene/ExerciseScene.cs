using System;
using Mogre;

namespace PatientDesktopClient.UI.Scene
{
    class ExerciseScene
    {
        private static readonly string PLUGINS_CFG = "cfg/plugins.cfg";
        private static readonly string RESOURCES_CFG = "cfg/resources.cfg";

        private Root root;
        private RenderWindow renderWindow;
        private SceneManager sceneManager;
        private Camera mainCam;
        private Viewport viewport;

        public ExerciseScene()
        {
            root = new Root(PLUGINS_CFG);
            if (!root.ShowConfigDialog())
            {
                return;
            }
            //root.Initialise(false);
            renderWindow = root.Initialise(true, "Kinect Physiotherapy"); //root.CreateRenderWindow("Kinect Physiotherapy", 320, 240, false);
            sceneManager = root.CreateSceneManager(SceneType.ST_INTERIOR);
            mainCam = sceneManager.CreateCamera("MainCam");
            viewport = renderWindow.AddViewport(mainCam);

            root.FrameRenderingQueued += new FrameListener.FrameRenderingQueuedHandler(onFrameRenderingQueued);
        }

        public void Start() 
        {
            root.StartRendering();
        }

        private bool onFrameRenderingQueued(FrameEvent evt)
        {
            return true;
        }
          
    }
}
