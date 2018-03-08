using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace RFOSSCore
{
    //Core class for storing data relevant to the entire play session
    public static class GameManager
    {
        public static SettingsIO Settings = new SettingsIO();
        public static SaveIO Save = new SaveIO();

        #region Timecode Handling

        public static double timecode
        {
            get { return Save.CurrentSaveData.timecode; }
            set { Save.CurrentSaveData.timecode = value; }
        }
        
        public static void UpdateTimecode (float delta)
        {
            timecode += delta;
        }
        #endregion

        public static void ChangeScene(string scene)
        {
            SceneManager.LoadScene(scene);
        }
    }
}
