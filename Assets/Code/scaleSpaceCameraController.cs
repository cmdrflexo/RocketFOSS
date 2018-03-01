using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RFOSSCore
{
    //This is used to move the scale system camera at the appropriate speed to create the false system
    public class scaleSpaceCameraController : MonoBehaviour
    {
        public float scale;
        public Transform mainCamera;

        private void Update()
        {
            this.transform.position = mainCamera.position / scale;
            this.transform.rotation = mainCamera.rotation;
        }
    }
}
