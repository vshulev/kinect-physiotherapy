﻿using System;
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

        private MainCharacter model;

        public ExerciseScene()
        {
            root = new Root(PLUGINS_CFG);
            if (!root.ShowConfigDialog())
                return;
            //root.Initialise(false);
            renderWindow = root.Initialise(true, "Kinect Physiotherapy"); //root.CreateRenderWindow("Kinect Physiotherapy", 320, 240, false);
            sceneManager = root.CreateSceneManager(SceneType.ST_INTERIOR);
            setupCam();
            setupViewport();
            loadResources();
            createScene();
            root.FrameRenderingQueued += new FrameListener.FrameRenderingQueuedHandler(onFrameRenderingQueued);
        }

        public void Start()
        {
            root.StartRendering();
        }

        private void setupCam()
        {
            mainCam = sceneManager.CreateCamera("MainCam");
            mainCam.Position = new Vector3(0, 15, 40);
            mainCam.LookAt(new Vector3(0, 10, 0));
            mainCam.NearClipDistance = 5;
        }

        private void setupViewport()
        {
            viewport = renderWindow.AddViewport(mainCam);
            viewport.BackgroundColour = ColourValue.Black;
            mainCam.AspectRatio = ((float) viewport.ActualWidth) / viewport.ActualHeight;
        }

        private void loadResources()
        {
            var cf = new ConfigFile();
            cf.Load(RESOURCES_CFG, "\t:=", true);

            var seci = cf.GetSectionIterator();
            while (seci.MoveNext())
            {
                foreach (var pair in seci.Current)
                {
                    ResourceGroupManager.Singleton.AddResourceLocation(
                        pair.Value, pair.Key, seci.CurrentKey);
                }
            }
            ResourceGroupManager.Singleton.InitialiseAllResourceGroups();
        }

        private void createScene()
        {
            // let there be light
            Light directionalLight = sceneManager.CreateLight("directionalLight");
            directionalLight.Type = Light.LightTypes.LT_DIRECTIONAL;
            directionalLight.DiffuseColour = ColourValue.White;
            directionalLight.SpecularColour = ColourValue.White;
            directionalLight.Position = Vector3.ZERO;
            directionalLight.Direction = new Vector3(0.577350f, -0.577350f, -0.577350f);

            // create axes
            Entity entity = sceneManager.CreateEntity("Axes", "axes.mesh");
            SceneNode sceneNode = sceneManager.RootSceneNode.CreateChildSceneNode("AxesNode");
            sceneNode.AttachObject(entity);

            // create character
            model = new MainCharacter(sceneManager);
        }

        private bool onFrameRenderingQueued(FrameEvent evt)
        {
            model.Update();
            return true;
        }
          
    }
}