using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RFOSSCore;

namespace RFOSSCore
{
    public class AudioVolume : MonoBehaviour
    {

        public SettingsIO.volumeTypes Type;
        private AudioSource source;
        private float InitialVolume;

        private void Start()
        {
            if (!source)
            {
                if (GetComponent<AudioSource>())
                {
                    source = this.GetComponent<AudioSource>();
                    InitialVolume = source.volume;
                }
                else
                {
                    print("No audio source attached to GameObject");
                }
            }
        }

        private void Update()
        {
            if (source)
            {
                switch (Type)
                {
                    case SettingsIO.volumeTypes.sfx:
                        source.volume = InitialVolume * GameManager.Settings.Volume.sfx;
                        break;
                    case SettingsIO.volumeTypes.music:
                        source.volume = InitialVolume * GameManager.Settings.Volume.music;
                        break;
                    case SettingsIO.volumeTypes.ui:
                        source.volume = InitialVolume * GameManager.Settings.Volume.ui;
                        break;
                    case SettingsIO.volumeTypes.voice:
                        source.volume = InitialVolume * GameManager.Settings.Volume.voice;
                        break;
                }
            }
        }
    }
}
