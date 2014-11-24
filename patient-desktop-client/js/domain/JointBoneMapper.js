
function JointBoneMapper() { }

/*
 * returns the WindowsPreview.Kinect.JointType for
 * the start joint of the bone specified by BoneType
 */
JointBoneMapper.getBoneStartJoint = function (type) {
    if(type == BoneType.spineBaseSpineMid)
        return WindowsPreview.Kinect.JointType.spineBase;
    if(type == BoneType.spineMidSpineShoulder)
        return WindowsPreview.Kinect.JointType.spineMid;
    if(type == BoneType.spineShoulderNeck)
        return WindowsPreview.Kinect.JointType.spineShoulder;
    if(type == BoneType.neckHead)
        return WindowsPreview.Kinect.JointType.neck;
    if(type == BoneType.spineShoulderShoulderRight)
        return WindowsPreview.Kinect.JointType.spineShoulder;
    if(type == BoneType.shoulderRightElbowRight)
        return WindowsPreview.Kinect.JointType.shoulderRight;
    if(type == BoneType.elbowRightWristRight)
        return WindowsPreview.Kinect.JointType.elbowRight;
    if(type == BoneType.wristRightThumbRight)
        return WindowsPreview.Kinect.JointType.wristRight;
    if(type == BoneType.wristRightHandRight)
        return WindowsPreview.Kinect.JointType.wristRight;
    if(type == BoneType.handRightHandTipRight)
        return WindowsPreview.Kinect.JointType.handRight;
    if(type == BoneType.spineShoulderShoulderLeft)
        return WindowsPreview.Kinect.JointType.spineShoulder;
    if(type == BoneType.shoulderLeftElbowLeft)
        return WindowsPreview.Kinect.JointType.shoulderLeft;
    if(type == BoneType.elbowLeftWristLeft)
        return WindowsPreview.Kinect.JointType.elbowLeft;
    if(type == BoneType.wristLeftThumbLeft)
        return WindowsPreview.Kinect.JointType.wristLeft;
    if(type == BoneType.wristLeftHandLeft)
        return WindowsPreview.Kinect.JointType.wristLeft;
    if(type == BoneType.handLeftHandTipLeft)
        return WindowsPreview.Kinect.JointType.handLeft;
    if(type == BoneType.spineBaseHipRight)
        return WindowsPreview.Kinect.JointType.spineBase;
    if(type == BoneType.hipRightKneeRight)
        return WindowsPreview.Kinect.JointType.hipRight;
    if(type == BoneType.kneeRightAnkleRight)
        return WindowsPreview.Kinect.JointType.kneeRight;
    if(type == BoneType.ankleRightFootRight)
        return WindowsPreview.Kinect.JointType.ankleRight;
    if(type == BoneType.spineBaseHipLeft)
        return WindowsPreview.Kinect.JointType.spineBase;
    if(type == BoneType.hipLeftKneeLeft)
        return WindowsPreview.Kinect.JointType.hipLeft;
    if(type == BoneType.kneeLeftAnkleLeft)
        return WindowsPreview.Kinect.JointType.kneeLeft;
    if(type == BoneType.ankleLeftFootLeft)
        return WindowsPreview.Kinect.JointType.ankleLeft;

    return -1;
}

/*
 * returns the WindowsPreview.Kinect.JointType for
 * the end joint of the bone specified by BoneType
 */
JointBoneMapper.getBoneEndJoint = function (type) {
    if (type == BoneType.spineBaseSpineMid)
        return WindowsPreview.Kinect.JointType.spineMid;
    if (type == BoneType.spineMidSpineShoulder)
        return WindowsPreview.Kinect.JointType.spineShoulder;
    if (type == BoneType.spineShoulderNeck)
        return WindowsPreview.Kinect.JointType.neck;
    if (type == BoneType.neckHead)
        return WindowsPreview.Kinect.JointType.head;
    if (type == BoneType.spineShoulderShoulderRight)
        return WindowsPreview.Kinect.JointType.shoulderRight;
    if (type == BoneType.shoulderRightElbowRight)
        return WindowsPreview.Kinect.JointType.elbowRight;
    if (type == BoneType.elbowRightWristRight)
        return WindowsPreview.Kinect.JointType.wristRight;
    if (type == BoneType.wristRightThumbRight)
        return WindowsPreview.Kinect.JointType.thumbRight;
    if (type == BoneType.wristRightHandRight)
        return WindowsPreview.Kinect.JointType.handRight;
    if (type == BoneType.handRightHandTipRight)
        return WindowsPreview.Kinect.JointType.handTipRight;
    if (type == BoneType.spineShoulderShoulderLeft)
        return WindowsPreview.Kinect.JointType.shoulderLeft;
    if (type == BoneType.shoulderLeftElbowLeft)
        return WindowsPreview.Kinect.JointType.elbowLeft;
    if (type == BoneType.elbowLeftWristLeft)
        return WindowsPreview.Kinect.JointType.wristLeft;
    if (type == BoneType.wristLeftThumbLeft)
        return WindowsPreview.Kinect.JointType.thumbLeft;
    if (type == BoneType.wristLeftHandLeft)
        return WindowsPreview.Kinect.JointType.handLeft;
    if (type == BoneType.handLeftHandTipLeft)
        return WindowsPreview.Kinect.JointType.handTipLeft;
    if (type == BoneType.spineBaseHipRight)
        return WindowsPreview.Kinect.JointType.hipRight;
    if (type == BoneType.hipRightKneeRight)
        return WindowsPreview.Kinect.JointType.kneeRight;
    if (type == BoneType.kneeRightAnkleRight)
        return WindowsPreview.Kinect.JointType.ankleRight;
    if (type == BoneType.ankleRightFootRight)
        return WindowsPreview.Kinect.JointType.footRight;
    if (type == BoneType.spineBaseHipLeft)
        return WindowsPreview.Kinect.JointType.hipLeft;
    if (type == BoneType.hipLeftKneeLeft)
        return WindowsPreview.Kinect.JointType.kneeLeft;
    if (type == BoneType.kneeLeftAnkleLeft)
        return WindowsPreview.Kinect.JointType.ankleLeft;
    if (type == BoneType.ankleLeftFootLeft)
        return WindowsPreview.Kinect.JointType.footLeft;

    return -1;
}

