using System.Collections.Generic;
using Mogre;
using Microsoft.Kinect;
using PatientDesktopClient.Readers;

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

        private IReadOnlyDictionary<JointType, Joint> joints;
        private Dictionary<string, JointPair> mapping;

        public MainCharacter(SceneManager mgr)
        {
            createMapping();
            createEntity(mgr);
            initializeSkeleton();
            BodyDataReader.Instance.BodyDataRead += bodyDataRead;
        }

        public void Update()
        {
            if (joints == null)
                return;

            foreach (Bone b in skeleton.GetBoneIterator())
            {
                if (mapping.ContainsKey(b.Name))
                {
                    Vector3 current = getBoneLocalDirectionVector(b);
                    Vector3 target = getTargetLocalDirectionVector(b);
                    b.Rotate(current.GetRotationTo(target));
                }
            }
        }

        private void createMapping()
        {
            mapping = new Dictionary<string, JointPair>();
            mapping.Add("upper_arm.L", new JointPair(JointType.ShoulderRight, JointType.ElbowRight));
            mapping.Add("forearm.L", new JointPair(JointType.ElbowRight, JointType.WristRight));
        }

        private void createEntity(SceneManager mgr)
        {
            entity = mgr.CreateEntity(ENTITY_NAME, MESH_NAME);
            sceneNode = mgr.RootSceneNode.CreateChildSceneNode(NODE_NAME);
            sceneNode.AttachObject(entity);
            //sceneNode.SetPosition(0, 50, 0);
        }

        private void initializeSkeleton()
        {
            skeleton = entity.Skeleton;
            foreach (Bone b in skeleton.GetBoneIterator())
            {
                b.SetManuallyControlled(true);
                b.InheritOrientation = true;
                b.InheritScale = true;
            }
        }

        private void bodyDataRead(object source, BodyDataReadEventArgs e)
        {
            joints = e.Joints;
        }

        // TODO this method assumes that bone always has a child!
        private Vector3 getBoneLocalDirectionVector(Bone b)
        {
            Bone child = (Bone) b.GetChild(0);
            Vector3 childWorldPos = child.ConvertLocalToWorldPosition(Vector3.ZERO);
            Vector3 currentDirection = b.ConvertWorldToLocalPosition(childWorldPos);
            currentDirection.Normalise();
            return currentDirection;
        }

        private Vector3 getTargetLocalDirectionVector(Bone b)
        {
            JointPair pair = mapping[b.Name];
            Vector3 worldDirection = getWorldDirectionVector(pair.Start, pair.End);
            return translateWorldToLocalDirectionVector(b, worldDirection);
        }

        private Vector3 translateWorldToLocalDirectionVector(Bone b, Vector3 v)
        {
            Vector3 translation = b.ConvertLocalToWorldPosition(Vector3.ZERO);
            translation = v + translation;
            Vector3 targetDirectionLocal = b.ConvertWorldToLocalPosition(translation);
            targetDirectionLocal.Normalise();
            return targetDirectionLocal;
        }

        private Vector3 getWorldDirectionVector(JointType startJoint, JointType endJoint)
        {
            return getWorldDirectionVector(convertJointToVector(startJoint), convertJointToVector(endJoint));
        }

        private Vector3 getWorldDirectionVector(Vector3 start, Vector3 end)
        {
            Vector3 dir = end - start;
            dir.Normalise();
            return dir;
        }

        private Vector3 convertJointToVector(JointType joint)
        {
            if (joints == null)
                return Vector3.ZERO;
            return new Vector3(joints[joint].Position.X, joints[joint].Position.Y, -joints[joint].Position.Z);
        }

    }
}
