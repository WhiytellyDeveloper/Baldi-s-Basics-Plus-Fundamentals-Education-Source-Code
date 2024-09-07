using System;
using System.Collections.Generic;
using UnityEngine;

namespace bbpfer.CustomContent.Structures
{
    public class EletricSwingDoor : MonoBehaviour, IButtonReceiver
    {
        private void Start() {
            swingDoor.Lock(false);
            for (int i = 0; i < swingDoor.doors.Length; i++)          
                MaterialModifier.ChangeOverlay(swingDoor.doors[i], offMat);
            
        }

        public void ButtonPressed(bool vaule)
        {

        }


        public SwingDoor swingDoor;
        public Material offMat;
    }
}
