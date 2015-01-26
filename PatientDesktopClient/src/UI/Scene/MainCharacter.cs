using Mogre;

namespace PatientDesktopClient.UI.Scene
{
    class MainCharacter
    {

        private static readonly string MESH_NAME = "character_male.mesh";
        private static readonly string ENTITY_NAME = "MainChar";
        private static readonly string NODE_NAME = ENTITY_NAME + "Node";

        private Entity entity;
        private SceneNode sceneNode;
        private Skeleton skeleton;

        public MainCharacter(SceneManager mgr)
        {
            entity = mgr.CreateEntity(ENTITY_NAME, MESH_NAME);
            sceneNode = mgr.RootSceneNode.CreateChildSceneNode(NODE_NAME);
            sceneNode.AttachObject(entity);
        }

        public void Update()
        {

        }

    }
}
