using System.Collections.Generic;
using UnityEngine;

public class InstOnAxis : MonoBehaviour {

	[SerializeField] private int _count = 1;
	[SerializeField] private int _offset = 1;
	[SerializeField] private GameObject _obj;
	[SerializeField] private Axis _axis;
	[SerializeField] private Vector3Int offsetFromAxis = new Vector3Int(0, 0, 0);
	private List<GameObject> temp = new List<GameObject>();
	
	enum Axis
	{
		x,
		y,
		z
	}

	void Start() {
		CreateObj();
	}

	public void CreateObj() {
		int x = 0;
		int y = 0;
		int z = 0;

		if (_axis == Axis.x) {
			x = _offset;
			y = offsetFromAxis.y;
			z = offsetFromAxis.z;
		} else if (_axis == Axis.y) {
			y = _offset;
			x = offsetFromAxis.x;
			z = offsetFromAxis.z;
		} else if (_axis == Axis.z) {
			z = _offset;
			x = offsetFromAxis.x;
			y = offsetFromAxis.y;
		}

		for (int i = 0; i < _count; i++) {
			GameObject tempObj = Instantiate(_obj, new Vector3(x * i, y * i, z * i), _obj.transform.rotation);
			temp.Add(tempObj);
		}
	}

	public void DestroyObj() {
		for (int i = 0; i < temp.Count; i++) {
			if (temp[i] != null) DestroyImmediate(temp[i]);
			temp.Remove(temp[i]);
			i--;
		}
	}
}
