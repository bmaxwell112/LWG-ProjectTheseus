using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theseus.Character {
	public class CustomNavMesh : MonoBehaviour {

		[SerializeField] int yOffset, yMax;
		[SerializeField] int[] xOffset, xMax;
		public LayerMask canNotMovePast;
		public static Vector2Int[] directions = new Vector2Int[] {
			new Vector2Int (0, 1),
				new Vector2Int (1, 1),
				new Vector2Int (1, 0),
				new Vector2Int (1, -1),
				new Vector2Int (0, -1),
				new Vector2Int (-1, -1),
				new Vector2Int (-1, 0),
				new Vector2Int (-1, 1)
		};

		public List<Vector2Int> points = new List<Vector2Int> ();

		void Start () {
			SetPoints ();
		}

		void OnDrawGizmos () {
			DrawPoints ();
		}
		private void DrawPoints () {
			foreach (Vector2Int point in points) {
				Gizmos.DrawWireCube ((Vector2) point, Vector3.one * 0.25f);
			}
		}

		private void SetPoints () {
			Vector2Int roomPos = new Vector2Int (Mathf.RoundToInt (transform.position.x), Mathf.RoundToInt (transform.position.y));
			int yPos = yOffset + roomPos.y;
			for (int i = 0; i < yMax; i++) {
				for (int j = 0; j < xMax[i]; j++) {
					int xPos = xOffset[i] + roomPos.x + j;
					if (i % 2 != 0) {
						xPos = xOffset[i] + roomPos.x + j;
					}
					points.Add (new Vector2Int (xPos, yPos));
				}
				yPos -= 1;
			}
		}
		public void CheckAllDirections () {
			List<Vector2Int> badPoints = new List<Vector2Int> ();
			Collider2D[] colls = GetComponentsInChildren<Collider2D> ();
			foreach (Vector2Int point in points) {
				bool removed = false;
				foreach (Collider2D coll in colls) {
					if (coll.gameObject.layer == canNotMovePast) {
						if (coll.bounds.Contains ((Vector2) point)) {
							badPoints.Add (point);
							removed = true;
							break;
						}
					}
				}
				if (!removed) {
					for (int i = 0; i < directions.Length; i++) {
						if (Physics2D.Raycast ((Vector2) point, directions[i], 0.15f, canNotMovePast)) {
							badPoints.Add (point);
							break;
						}
					}
				}

			}
			foreach (Vector2Int point in badPoints) {
				points.Remove (point);
			}
		}
	}

}