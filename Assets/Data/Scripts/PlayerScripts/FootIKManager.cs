using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FootIKManager : MonoBehaviour
{
    #region Variables
    private Vector3 rightFootPos,leftFootPos,rightFootIK,leftFootIK;
    private Quaternion rightFootIKRotation, leftFootIKRotation;
    private float lastPelvisPosY, lastRightFootPosY,lastLeftFootPosY;

    [Header("Feet Grounder")]
    public bool enableFeetIK = true;
    [Range(0,2)][SerializeField] private float heightFromGroundRaycast = 1.14f;
    [Range(0,2)][SerializeField] private float raycastDownDistance = 1.5f;
    [SerializeField] private LayerMask enviromentLayer;
    [SerializeField] private float pelvisOffset = 0f;
    [Range(0,1)][SerializeField] private float pelvisUpAndDownSpeed = 0.28f;
    [Range(0,1)][SerializeField] private float feetToIKPositionSpeed = 0.5f;

    public string leftFootAnimVariableName = "LeftFootCurve";
    public string rightFootAnimVariableName = "RightFootCurve";
    public bool useProIKFeature =false;
    public bool showSolverDebug =false;
    private Animator anim;
    #endregion
   
   #region Initializations
    private void Awake() {
        anim = GetComponent<Animator>();
    }

    #endregion

    private void FixedUpdate() {
        if(!enableFeetIK){return;}

        AdjustFeetTarget(ref rightFootPos, HumanBodyBones.RightFoot);
        AdjustFeetTarget(ref leftFootPos, HumanBodyBones.LeftFoot);

        //to raycast to the ground and find positions.
        FeetPositionSolver(rightFootPos, ref rightFootIK,
        ref rightFootIKRotation);
        FeetPositionSolver(leftFootPos, ref leftFootIK,
        ref leftFootIKRotation);
    }
    private void OnAnimatorIK(int layerIndex) {
        if(!enableFeetIK){return;}

        MovePelvisHeight();

        anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
        anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
        
        if(useProIKFeature)
        {
            anim.SetIKRotationWeight(AvatarIKGoal.RightFoot,
            anim.GetFloat(rightFootAnimVariableName));
            anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot,
            anim.GetFloat(leftFootAnimVariableName));
        }
        MoveFeetToIKPoint(AvatarIKGoal.RightFoot, rightFootIK, rightFootIKRotation, ref lastRightFootPosY);
        MoveFeetToIKPoint(AvatarIKGoal.LeftFoot, leftFootIK, leftFootIKRotation, ref lastLeftFootPosY);
    }
    
    #region  Feet Grounding Methods

    private void MoveFeetToIKPoint(AvatarIKGoal foot, Vector3 positionIKHolder,Quaternion rotationIKHolder, ref float lastFootPositionY){

        Vector3 targetIKPosition = anim.GetIKPosition(foot);


        if(targetIKPosition != Vector3.zero)
        {
            targetIKPosition = transform.InverseTransformPoint(targetIKPosition);
            positionIKHolder = transform.InverseTransformPoint(positionIKHolder);

            float yVariable = Mathf.Lerp(lastFootPositionY, positionIKHolder.y, feetToIKPositionSpeed);

            targetIKPosition.y += yVariable;
            lastFootPositionY = yVariable;

            targetIKPosition = transform.TransformPoint(targetIKPosition);
            anim.SetIKRotation(foot, rotationIKHolder);
        }

        anim.SetIKPosition(foot, targetIKPosition);
    }
    private void MovePelvisHeight(){
        if(rightFootIK == Vector3.zero || leftFootIK == Vector3.zero || lastPelvisPosY == 0)
        {
            lastPelvisPosY = anim.bodyPosition.y;
            return;
        }

        float lOffsetPosition = leftFootIK.y - transform.position.y;
        float rOffsetPosition = rightFootIK.y - transform.position.y;

        float totalOffset = (lOffsetPosition < rOffsetPosition)? lOffsetPosition : rOffsetPosition;

        Vector3 newPelvisPos = anim.bodyPosition + Vector3.up * totalOffset;
        newPelvisPos.y = Mathf.Lerp(lastPelvisPosY, newPelvisPos.y, pelvisUpAndDownSpeed);
        anim.bodyPosition = newPelvisPos;
        lastPelvisPosY = anim.bodyPosition.y;
    }
    private void FeetPositionSolver(Vector3 fromSkyPosition, 
    ref Vector3 feetIKPositions, ref Quaternion feetIKRotations){
        //raycast handeling section
        RaycastHit feetOutHit;
        if(showSolverDebug){
            Debug.DrawLine(fromSkyPosition,fromSkyPosition + Vector3.down * (raycastDownDistance + heightFromGroundRaycast));
        }
        if(Physics.Raycast(fromSkyPosition,Vector3.down, out feetOutHit,raycastDownDistance+heightFromGroundRaycast,enviromentLayer)){
            // find out feet position from sky position
            feetIKPositions = fromSkyPosition;
            feetIKPositions.y = feetOutHit.point.y + pelvisOffset;
            feetIKRotations = Quaternion.FromToRotation(Vector3.up, 
            feetOutHit.normal) * transform.rotation;
            return;
        }

        feetIKPositions = Vector3.zero; // it didnt work
    }
    private void AdjustFeetTarget(ref Vector3 feetPositions, 
    HumanBodyBones foot){
        feetPositions = anim.GetBoneTransform(foot).position;
        feetPositions.y = transform.position.y + heightFromGroundRaycast;
    }
    #endregion
}
