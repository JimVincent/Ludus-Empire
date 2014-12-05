#pragma strict

var particleSys:ParticleSystem;

function Start () {
particleSys =  transform.GetComponent("ParticleSystem");;
}

function Update () {
if(particleSys.startSpeed>0){
	particleSys.startSpeed=particleSys.startSpeed-(Time.deltaTime/5);
	//particleSys.emissionRate=particleSys.emissionRate-(Time.deltaTime);//
}
else{
	particleSys.enableEmission=false;
}
}