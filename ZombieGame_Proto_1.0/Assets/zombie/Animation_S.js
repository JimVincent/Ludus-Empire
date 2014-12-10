#pragma strict
private var myComponents:Component[];
private var isDead:boolean=false;

function Start () {
//sets all animation variables to false, so that it starts from idle
gameObject.GetComponent(Animator).SetBool("isDead", false); 
gameObject.GetComponent(Animator).SetBool("isWalking", false); 
gameObject.GetComponent(Animator).SetBool("isAttacking", false); 
isDead=false;
myComponents=gameObject.GetComponentsInChildren(Rigidbody);
for (var rigidB : Rigidbody in myComponents) {
		rigidB.isKinematic = true;
	}
}

function attack(){
//sets the attack animation variable to true
//then after 0.4 sec (the time it takes for the animation to end) then sets it to false.
gameObject.GetComponent(Animator).SetBool("isAttacking", true);
yield WaitForSeconds(0.5);
gameObject.GetComponent(Animator).SetBool("isAttacking", false);  
}

function Update () {
if(Input.GetKeyDown(KeyCode.F)){
//goes into attack method
attack();
}
if(isDead==true){
gameObject.GetComponent(NavMeshAgent).enabled=false;
//if the zombie is dead and it enters into animation ragdoll (which is empty) it ragdollizes
if (gameObject.GetComponent(Animator).GetCurrentAnimatorStateInfo(0).IsName("Base.Ragdoll"))
{
gameObject.GetComponent(Animator).enabled=false;//
for (var rigidB : Rigidbody in myComponents) {
	rigidB.isKinematic = false;
}
}
}else{

if(gameObject.GetComponent(NavMeshAgent).hasPath==true){
gameObject.GetComponent(Animator).SetBool("isWalking", true); 
}
else{
if(gameObject.GetComponent(NavMeshAgent).enabled==true){
gameObject.GetComponent(Animator).SetBool("isWalking", false); 
}
}

}
}


function die(){
if(isDead==false){
var i:int=Random.Range(0,12);
Debug.Log(i+1);
gameObject.GetComponent(NavMeshAgent).enabled=false;
gameObject.GetComponent(Animator).SetBool("isDead", true);
gameObject.GetComponent(Animator).SetInteger("DeathAnim",i);
gameObject.GetComponent(Animator).SetBool("isWalking", false); 
gameObject.GetComponent(Animator).SetBool("isAttacking", false); 
isDead=true;
}
}