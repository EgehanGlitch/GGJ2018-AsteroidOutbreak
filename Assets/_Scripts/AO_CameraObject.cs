using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AO_CameraObject : MonoBehaviour {

	[SerializeField] public Transform _followTarget;
    [SerializeField][Range(0.1f, 2f)] private float _followSpeed;

	Vector3 lerpPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
	     lerpPosition = Vector3.Lerp(lerpPosition, _followTarget.position, Time.deltaTime * _followSpeed);
		 transform.position = lerpPosition;
	}
}
