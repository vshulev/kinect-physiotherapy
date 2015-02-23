using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Mogre;

namespace MogreWpfTest
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : System.Windows.Controls.Page
    {
        private static readonly string PLUGINS_CFG = "cfg/plugins.cfg";
        private static readonly string RESOURCES_CFG = "cfg/resources.cfg";
        public static readonly string[] SUPPORTED_RENDER_SYSTEMS = new string[]
        {
            "Direct3D9 Rendering Subsystem",
            "Direct3D9Ex Rendering Subsystem",
            "Direct3D11 Rendering Subsystem",
        };

        private IntPtr hWnd;
        private Root root;
        private RenderWindow renderWindow;
        private SceneManager sceneManager;
        private Camera mainCam;
        private Viewport viewport;
        private Entity entity;
        private RenderTexture renderTarget;
        private D3DImage d3dSource;

        public MainMenu()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainMenuLoaded);
        }

        public void MainMenuLoaded(object sender, RoutedEventArgs e)
        {
            d3dSource = new D3DImage();
            d3dImage.Source = d3dSource;
            acquireHwnd();
            mogreInit();
            attach();
        }

        private void acquireHwnd()
        {
            hWnd = IntPtr.Zero;
            foreach (PresentationSource source in PresentationSource.CurrentSources)
            {
                var hwndSource = (source as HwndSource);
                if (hwndSource != null)
                {
                    hWnd = hwndSource.Handle;
                    break;
                }
            }

            if (hWnd == IntPtr.Zero)
            {
                throw new Exception("Failed to get hWnd from PresentationSource.CurrentSources.");
            }
        }

        private void mogreInit()
        {
            root = new Root(PLUGINS_CFG);
            loadResources();
            setRenderSystem();
            setRenderConfig();
            root.Initialise(false);
            createRenderWindow();
            sceneManager = root.CreateSceneManager(SceneType.ST_INTERIOR);
            setupCam();
            setupViewport();
            createScene();
            createRenderTarget();
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

        private void setRenderSystem()
        {
            bool foundit = false;
            foreach (RenderSystem rs in root.GetAvailableRenderers())
            {
                if (rs == null) continue;

                root.RenderSystem = rs;
                string rname = root.RenderSystem.Name;
                if (SUPPORTED_RENDER_SYSTEMS.Contains(rname))
                {
                    foundit = true;
                    break;
                }
            }

            if (!foundit)
            {
                throw new Exception("Failed to find a compatible render system.  See MogreImage.SupportedRenderSystems for a list of supported renderers.");
            }
        }

        private void setRenderConfig()
        {
            root.RenderSystem.SetConfigOption("Full Screen", "No");
            root.RenderSystem.SetConfigOption("Video Mode", "640 x 480 @ 32-bit colour");
        }

        private void createRenderWindow()
        {
            var misc = new NameValuePairList();
            misc["externalWindowHandle"] = hWnd.ToString();
            renderWindow = root.CreateRenderWindow("OgreImageSource Windows", 0, 0, false, misc);
            renderWindow.IsAutoUpdated = false;
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
            viewport.BackgroundColour = new ColourValue(0.6784314f, 0.8470588f, 0.9019608f);
            mainCam.AspectRatio = ((float)viewport.ActualWidth) / viewport.ActualHeight;
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

            // create character entity
            entity = sceneManager.CreateEntity("Character", "character_male.mesh");
            SceneNode sceneNode = sceneManager.RootSceneNode.CreateChildSceneNode("CharacterNode");
            sceneNode.AttachObject(entity);

            // initialize entity skeleton
            foreach (Bone b in entity.Skeleton.GetBoneIterator())
            {
                b.SetManuallyControlled(true);
                b.InheritOrientation = true;
                b.InheritScale = true;
            }
        }

        private void createRenderTarget()
        {
            TexturePtr texture = TextureManager.Singleton.CreateManual(
                "RttTexture",
                ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME,
                TextureType.TEX_TYPE_2D,
                (uint)viewport.ActualWidth,
                (uint)viewport.ActualHeight,
                0,
                Mogre.PixelFormat.PF_A8R8G8B8,
                (int)TextureUsage.TU_RENDERTARGET);

            renderTarget = texture.GetBuffer().GetRenderTarget();
            renderTarget.AddViewport(mainCam);
            renderTarget.GetViewport(0).SetClearEveryFrame(true);
        }

        private void attach()
        {
            d3dSource.Lock();
            try
            {
                IntPtr surface;
                renderTarget.GetCustomAttribute("DDBACKBUFFER", out surface);
                d3dSource.SetBackBuffer(D3DResourceType.IDirect3DSurface9, surface);
            }
            finally
            {
                d3dSource.Unlock();
            }
            CompositionTarget.Rendering += render;
        }

        private void render(object sender, EventArgs e)
        {
            entity.Skeleton.GetBone("upper_arm.L").Roll(0.1f);

            d3dSource.Lock();
            root.RenderOneFrame();
            renderTarget.Update();
            d3dSource.AddDirtyRect(new Int32Rect(0, 0, (int) d3dSource.Width, (int) d3dSource.Height));
            d3dSource.Unlock();
        }

    }
}
