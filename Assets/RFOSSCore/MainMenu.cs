using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RFOSSCore
{
    public class MainMenu
    {
        public enum menuStates
        {
            loading,
            main,
            start,
            settings,
            credits,
            continuegame,
            newgame,
            confirmclose
        }

        public menuStates State;

        public void setMenuState(menuStates s)
        {
            State = s;
        }
    }
}