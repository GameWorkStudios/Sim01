using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class MoverState : State
{

    struct CurvePoints{
        public Vector3 startPos;
        public Vector3 midPos;
        public Vector3 endPos;
    }

    #region CurveSettings
    private float elapsedTime = 0;
    private float timeMultiplier = 1f;
    private int steps;
    private int count = 0;
    private Vector3 temp;
    private List<CurvePoints> curvePoints;
    #endregion CurveSettings

    #region MovenmentSettings
    protected Transform tr;
    protected Vector3 targetPosition;    
    private bool targetPositionSetted = false;
    private LayerMask groundLayerMask;
    private float oneTimeJumpDistance;
    private float jumpHeight;
    private float objectVertialLength;
    #endregion MovenmentSettings
    #region Setters    

    protected event Action onDestinationReached;

    protected LayerMask GroundLayerMask{
        set{
            this.groundLayerMask = value;
        }
    }

    protected float OneTimeJumpDistance{
        set{
            this.oneTimeJumpDistance = value <= 0 ? 1f : value;
        }
    }

    protected float JumpHeight{
        set{
            this.jumpHeight = value <= 0 ? 0.5f : value;
        }
    }

    protected float ObjectVertialLength{
        set{
            this.objectVertialLength = value <= 0 ? 1 : value;
        }
    }
    #endregion Setters

    public override void UpdateState(StateMachine machine)
    {
        if(!targetPositionSetted){
            return;
        }
        if(Vector3.Distance(targetPosition, this.tr.position) > 0.1f){
            this.tr.LookAt(new Vector3(targetPosition.x, this.tr.position.y, targetPosition.z));
            if(curvePoints.Count > 0){
                if(count < curvePoints.Count){
                    CurvePoints cp = curvePoints[count];
                    ApplyJumpMoveSolution(cp);
                }else{
                    this.ReCalculateRoutes();
                }
            }else{
                this.ReCalculateRoutes();
            }
        }else{
            this.ReCalculateRoutes();
        }
    }

    private void ReCalculateRoutes(){
        count = 0;
        targetPositionSetted = false;
        this.onDestinationReached?.Invoke();
    }

    private void ApplyJumpMoveSolution(CurvePoints cp){
        elapsedTime += timeMultiplier * Time.deltaTime;
        Vector3 l1 = Vector3.Lerp(cp.startPos, cp.midPos, elapsedTime);
        Vector3 l2 = Vector3.Lerp(cp.midPos, cp.endPos,elapsedTime);
        Vector3 curveLerp = Vector3.Lerp(l1, l2, elapsedTime); 
        this.tr.position = curveLerp;
        if(Vector3.Distance(this.tr.position, cp.endPos) < 0.1f){   
            this.tr.position = cp.endPos;  
            count++;          
            elapsedTime = 0;
        }        
    }

    protected Vector3 YvalueCorrection(Vector3 correctionNeedVector){
        RaycastHit hit;
        if(Physics.Raycast(new Vector3(correctionNeedVector.x, correctionNeedVector.y + 50f, correctionNeedVector.z),Vector3.down, out hit, 100f, groundLayerMask)){
            correctionNeedVector.y = hit.point.y + (/*GetComponent<Collider>().bounds.size.y*/objectVertialLength * 0.5f);
        }
        return correctionNeedVector;
    }

    private List<CurvePoints> CalculateJumpSolution(){
        if(curvePoints != null && curvePoints.Count>0){
            curvePoints.Clear();
        }
        curvePoints = new List<CurvePoints>();
        //Detection unit distance and step count
        float unitDistance = Vector3.Distance(this.tr.position, targetPosition) / oneTimeJumpDistance;
        steps = Mathf.RoundToInt(unitDistance);
        if(steps > 1f){ // if the steps bigger than one
            //Find unit Distance positions and add the list.
            List<Vector3> points = new List<Vector3>();
            for (int i = 0; i<=steps; i++){
                Vector3 lerp = Vector3.Lerp(this.tr.position, targetPosition, oneTimeJumpDistance * i / steps);         
                lerp = YvalueCorrection(lerp);
                points.Add(lerp);
            }
            
            //Calculate Curve Control Points
            foreach(Vector3 position in points){                
                //FOR DEBUG!
                //GameObject tt = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                //tt.transform.position = position;
                if(temp != null){
                    Vector3 midOfPoints = (temp + position) * 0.5f;
                    midOfPoints.y += jumpHeight; // Note : düzeltilecek!
                    CurvePoints tempCurveStruct = new CurvePoints(){
                        startPos = temp,
                        midPos = midOfPoints,
                        endPos = position
                    };
                    curvePoints.Add(tempCurveStruct);
                }
                temp = position;
            }
            curvePoints.RemoveAt(0);
        }else{
            // find one jump solution
            Vector3 midOfPoints = (this.tr.position + targetPosition) * 0.5f;
            midOfPoints.y += jumpHeight; // Note : düzeltilecek!
            CurvePoints tempCurvePoints = new CurvePoints(){
                startPos = this.tr.position,
                midPos = midOfPoints,
                endPos = targetPosition
            };
            curvePoints.Add(tempCurvePoints);            
        }        
        return curvePoints;
    }

    protected void StartMoveOperation(){
        StopMoveOperation();
        CalculateJumpSolution();
        this.targetPositionSetted = true;
    }

    protected void StopMoveOperation(){
        this.targetPositionSetted = false;
        this.elapsedTime = 0;
        this.count = 0;
    }


}

