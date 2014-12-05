#pragma strict
var zombie:GameObject;
var blood:GameObject;
var bloodWound:GameObject;

function Start () {
}

function Update () {
//Just check if we click on the zombie
//if the thing we left clicked on happens to be a zombie, it dies
if(Input.GetMouseButtonDown(0)){
var hit:RaycastHit;
if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),hit,100)){
if(hit.transform.tag=="Zombie"){
var bloody:GameObject = Instantiate(blood,hit.point,Quaternion.LookRotation(hit.normal));
bloody.transform.parent=hit.transform;
hit.transform.root.gameObject.GetComponent(Animation_S).die();
var bloodyWound:GameObject = Instantiate(bloodWound,bloody.transform.position,bloody.transform.rotation);
bloodyWound.transform.parent=bloody.transform;
bloodyWound.transform.Translate(Vector3.forward*0.08);
bloodyWound.transform.rotation.eulerAngles = Vector3(180, 0, 0);
}
}
}
if(Input.GetMouseButtonDown(1)){
if(zombie.GetComponent(NavMeshAgent).enabled==true){
var hit2:RaycastHit;
if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),hit2,100)){
zombie.GetComponent(NavMeshAgent).destination=hit2.point;
}
}
else{
zombie.GetComponent(Animator).SetBool("isWalking", true); 
}
}
}

function OnGUI(){
//its where we control the scene and zombie
//clicking restart button reload current scene
//clicking on the enable rootmotion button disables NavAgent (because it interferes with root motion)
//we enable root motion 
//set speed and acceleration of navagent to 0 so it doesn't interfere  with root motion
//then we disable it to make sure 100% that it doesn't interfer it's over kill but it works
//the disable rootmotion button just does the opposite and we reset the nav agent to all the default values
if (GUI.Button (Rect (0,0,400,30), "Restart")) {
Application.LoadLevel(Application.loadedLevel);
}
if (GUI.Button (Rect (0,60,400,30), "Enable RootMotion (Disables NavAgent)")) {
zombie.GetComponent(Animator).applyRootMotion=true;
zombie.GetComponent(NavMeshAgent).enabled=false;
zombie.GetComponent(NavMeshAgent).speed=0;
zombie.GetComponent(NavMeshAgent).acceleration=0;
}
if (GUI.Button (Rect (0,120,400,30), "Disable RootMotion")) {
zombie.GetComponent(Animator).applyRootMotion=false;
zombie.GetComponent(NavMeshAgent).enabled=true;
zombie.GetComponent(NavMeshAgent).speed=0.35;
zombie.GetComponent(NavMeshAgent).acceleration=1;
}
}