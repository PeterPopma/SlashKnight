using UnityEngine;
using System.Collections;

public class DeleteAfterDelay : MonoBehaviour
{
	public float delay = 6.0f;

	void Update()
	{
		delay -= Time.deltaTime;
		if (delay < 0f)
			GameObject.Destroy(this.gameObject);
	}
}
