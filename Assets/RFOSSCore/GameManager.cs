using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RFOSSCore
{
    //Core class for storing data relevant to the entire play session
    public static class GameManager
    {
        #region GameState, paused/playing states and enum
        public enum e_gameStates
        {
            paused,
            playing
        }

        public static e_gameStates GameState = e_gameStates.playing;

        public static void toggleGameState ()
        {
            if (GameState == e_gameStates.paused)
            {
                GameState = e_gameStates.playing;
            }
            else if (GameState != e_gameStates.paused)
            {
                GameState = e_gameStates.paused;
            }
        }
        #endregion

        #region GameTime and time set method
        private static float gameTime;

        public static float GameTime
        {
            get { return gameTime; }
            set { gameTime = value; }
        }
        #endregion
    }
}
