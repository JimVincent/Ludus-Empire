using UnityEngine;
using System.Collections;

public class S_FX_SelfDestuct : MonoBehaviour {

	public float destructTime;
	public float fadeRate;

	private float currentAlpha = 1;
	private float timer;
	private LineRenderer bulletPath;
	private Color startCol = new Color(1,1,1,1);
	private Color endCol = new Color(1,1,1,1);

	// Use this for initialization
	void Start () {
		bulletPath = gameObject.GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		currentAlpha -= Time.deltaTime * fadeRate;
		startCol = new Color(1,1,1,currentAlpha);
		endCol = new Color(1,1,1,currentAlpha);
		bulletPath.SetColors(startCol,endCol);

		if(timer >= destructTime){
			Destroy(gameObject);
		}
	}
}
