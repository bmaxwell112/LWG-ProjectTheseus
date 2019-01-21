using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Theseus.Core {
  public class IdleTimer : MonoBehaviour {

    public float secondsToWait = 10;
    private float elapsed;

    [Serializable]
    public class IdleEvent : UnityEvent<GameObject> { }

    public IdleEvent onIdle;

    void Start () { }

    void Update () {
      elapsed += Mathf.Clamp (Time.deltaTime, 0f, 1f / 30f);
      if (Input.anyKey || Input.touchCount > 0 || InputCapture.hThrow != 0 || InputCapture.vThrow != 0) {
        elapsed = 0;
      }
      if (elapsed > secondsToWait) {
        onIdle.Invoke (gameObject);
        enabled = false;
      }
    }
  }
}