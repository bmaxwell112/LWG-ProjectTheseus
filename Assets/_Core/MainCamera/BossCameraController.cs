using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theseus.Core {
    public class BossCameraController : MonoBehaviour {
        bool moveInProgress;
        public static Vector3 CAMPOS;
        [SerializeField] float MinX, MaxX, MinY, MaxY;
        [SerializeField] GameObject player;

        void Update () {

            CAMPOS = new Vector3 (player.transform.position.x, player.transform.position.y, -10);
            CalcConstraints ();
            transform.position = CAMPOS;
        }

        void CalcConstraints () {
            if (CAMPOS.x > MaxX) {
                CAMPOS.x = MaxX;
            }

            if (CAMPOS.y > MaxY) {
                CAMPOS.y = MaxY;
            }

            if (CAMPOS.x < MinX) {
                CAMPOS.x = MinX;
            }

            if (CAMPOS.y < MinY) {
                CAMPOS.y = MinY;
            }
        }

    }
}