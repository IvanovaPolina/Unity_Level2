using UnityEngine;

public sealed class Destroyer : MonoBehaviour {

	private void OnTriggerEnter(Collider other) {
		Destroy(other.gameObject);
		Debug.Log("Destroyed: " + other.name);
	}
}
