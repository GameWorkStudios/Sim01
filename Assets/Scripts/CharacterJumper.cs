using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJumper : MonoBehaviour
{

    private Transform tr;
    private Vector3 targetPosition;

    [SerializeField] private float randomPositionRadius;      
    [SerializeField] private float minDistanceOfNextPosition;
    [SerializeField] private LayerMask groundLayer;


    [SerializeField] private float oneTimeJumpDistance = 1f;
    [SerializeField] private float jumpHeight = 1f;
    [SerializeField] private float timeMultiplier = 1f;

    public bool findRandomPositions = false;

    private int steps;
    private Vector3 temp;
    private int count = 0;

    private float elapsedTime = 0;

    List<CurvePoints> curvePoints;

    struct CurvePoints{
        public Vector3 startPos;
        public Vector3 midPos;
        public Vector3 endPos;
    }


    private void Start() {  
        this.tr = transform;
        targetPosition = FindRandomPosition();
        StartJumpSolution();
    }

    private void Update() {

        if(findRandomPositions){                
            if(Vector3.Distance(targetPosition, this.tr.position) > 0.1f){                
                if(curvePoints.Count>0){
                    if(count < curvePoints.Count){
                        CurvePoints cp = curvePoints[count];
                        ApplyJumpMoveSolution(cp);                    
                    }else{
                        InitializeRoute();
                    }                    
                }
            }else{
                InitializeRoute();
            }
        }
    }

    private void InitializeRoute(){
        count = 0;
        targetPosition = FindRandomPosition();
        StartJumpSolution();
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

    private Vector3 YvalueCorrection(Vector3 correctionNeedVector){
        RaycastHit hit;
        if(Physics.Raycast(new Vector3(correctionNeedVector.x, correctionNeedVector.y + 50f, correctionNeedVector.z),Vector3.down, out hit, 100f, groundLayer)){
            correctionNeedVector.y = hit.point.y + (GetComponent<Collider>().bounds.size.y * 0.5f);
        }
        return correctionNeedVector;
    }

    private List<CurvePoints> StartJumpSolution(){
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
                Vector3 lerp = Vector3.Lerp(transform.position, targetPosition, oneTimeJumpDistance * i / steps);         
                lerp = YvalueCorrection(lerp);
                points.Add(lerp);
            }
            
            //Calculate Curve Control Points
            foreach(Vector3 position in points){
                GameObject tt = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                tt.transform.position = position;
                if(temp != null){
                    Vector3 midOfPoints = (temp + position) * 0.5f;
                    midOfPoints.y += + jumpHeight; // Note : düzeltilecek!
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

    private Vector3 FindRandomPosition(){
        float xPos = Random.Range(this.tr.position.x - randomPositionRadius, this.tr.position.x + randomPositionRadius);
        float zPos = Random.Range(this.tr.position.z - randomPositionRadius, this.tr.position.z + randomPositionRadius);
        Vector3 tempPosition = new Vector3(xPos, 50f, zPos);
        tempPosition = YvalueCorrection(tempPosition);
        if(Vector3.Distance(tempPosition, targetPosition) < this.minDistanceOfNextPosition){
            tempPosition = FindRandomPosition();
        }  
        /*
        RaycastHit hit;
        if(Physics.Raycast(tempPosition, Vector3.down,out hit,100f,groundLayer)){
            tempPosition.y = hit.point.y;
            if(Vector3.Distance(tempPosition, targetPosition) < this.minDistanceOfNextPosition){
                tempPosition = FindRandomPosition();
            }   
        }   
        */
        this.targetPosition = tempPosition;
        return tempPosition;
    }


}


