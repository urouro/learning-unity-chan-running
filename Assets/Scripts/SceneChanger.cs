using UnityEngine;
using System.Collections;

public class SceneChanger : MonoBehaviour
{
	public string nextScheneName;

	void Start ()
	{

	}

	void Update ()
	{
		if (Input.GetButtonDown("Submit")) {
			Application.LoadLevel(nextScheneName);
		}
	}
}
