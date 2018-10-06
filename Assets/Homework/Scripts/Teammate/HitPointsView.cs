using System.Collections.Generic;
using UnityEngine;

namespace Homework
{
	public sealed class HitPointsView : MonoBehaviour
	{
		public GameObject hitPointEffect;

		private TeammateModel model;
		private Queue<GameObject> hitPoints = new Queue<GameObject>();
		
		private void Awake() {
			model = GetComponentInParent<TeammateModel>();	// получаем ссылку на модель текущего тиммэйта
			TeammateController.OnHitPointApplied += ApplyHitPoint;
			TeammateController.OnHitPointDeleted += DeleteHitPoint;
		}

		private void OnDestroy() {
			TeammateController.OnHitPointApplied -= ApplyHitPoint;
			TeammateController.OnHitPointDeleted -= DeleteHitPoint;
		}

		private void ApplyHitPoint(Vector3 hitPoint, TeammateModel teammate) {
			if (teammate != model) return;	// сравниваем тиммэйта, который пришел из параметров, и текущего
			GameObject point = Instantiate(hitPointEffect, hitPoint, Quaternion.identity);
			hitPoints.Enqueue(point);
		}

		private void DeleteHitPoint(TeammateModel teammate) {
			if (teammate != model) return;  // сравниваем тиммэйта, который пришел из параметров, и текущего
			if (hitPoints.Count <= 1) return;   // чтобы исчезала пройденная точка, а не предстоящая
			GameObject point = hitPoints.Dequeue();
			Destroy(point);
		}
	}
}