using UnityEngine;
using System.Collections;

public class growth : MonoBehaviour {

	void Update()
	{
		transform.localScale += new Vector3 (0, 0, 0.05F);
	}
}
