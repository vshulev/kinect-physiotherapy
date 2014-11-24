
function Bone(type) {

    /* 
     * Starting joint for bone.
     * Value is of type WindowsPreview.Kinect.JointType.
     */
    var startJoint = JointBoneMapper.getBoneStartJoint(type);

    /* 
     * Starting joint for bone.
     * Value is of type WindowsPreview.Kinect.JointType.
     */
    var endJoint = JointBoneMapper.getBoneEndJoint(type);

    /* Value is of type BoneType */
    var boneType = type;

    /*
     * Latest direction vector for bone.
     * Value is of type THREE.Vector3
     */
    var directionVector = null;

    /* Returns the bone type. */
    this.getBoneType = function () {
        return boneType;
    }

    /* Returns the direction vector. */
    this.getDirectionVector = function () {
        return directionVector;
    }

    /* Recalculates the direction vector based on the latest joint data. */
    this.recalculateDirectionVector = function (jointData) {
        var sj = jointData[startJoint].position;
        var ej = jointData[endJoint].position;
        directionVector = new THREE.Vector3()
            .subVectors(
                new THREE.Vector3(sj.x, sj.y, -sj.z),
                new THREE.Vector3(ej.x, ej.y, -ej.z)
                )
            .normalize();
    }

}