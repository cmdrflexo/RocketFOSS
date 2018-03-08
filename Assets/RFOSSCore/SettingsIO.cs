using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Text;

namespace RFOSSCore
{
    public class SettingsIO
    {
        #region
        //Used to tag audio sources to indicate which setting they should obey.
        [System.Serializable]
        public enum volumeTypes
        {
            sfx,
            music,
            ui,
            voice,
        }

        public class Volumes
        {
            public float sfx = 0.5f;
            public float music = 0.5f;
            public float ui = 0.5f;
            public float voice = 0.5f;
        }

        public bool settingsOK = false;

        public Volumes Volume = new Volumes();
        public string SettingsXML;
        #endregion

        //Read the Settings.xml file.
        public void ReadSettings()
        {
            if (File.Exists("Settings.xml"))
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load("Settings.xml");

                Volumes loadVolumes = new Volumes();
                string s, m, u, v;
                s = xDoc.SelectSingleNode("Settings/Volume/sfx").InnerText;
                m = xDoc.SelectSingleNode("Settings/Volume/music").InnerText;
                u = xDoc.SelectSingleNode("Settings/Volume/ui").InnerText;
                v = xDoc.SelectSingleNode("Settings/Volume/voice").InnerText;

                //Try loading the XML file
                try
                {
                    loadVolumes.sfx = Convert.ToSingle(s);
                    loadVolumes.music = Convert.ToSingle(m);
                    loadVolumes.ui = Convert.ToSingle(u);
                    loadVolumes.voice = Convert.ToSingle(v);
                }
                catch
                {
                    //If the converter fails on any node, rewrite the whole settings file and retry
                    WriteDefaultSettings();
                    return;
                }
                finally
                {
                    //If successful, commit the loaded variables into the global variables
                    Volume = loadVolumes;
                }
            }
            else
            {
                WriteDefaultSettings();
            }
        }

        public void WriteSettings(float s, float m, float u, float v)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("Settings.xml");

            xDoc.SelectSingleNode("Settings/Volume/sfx").InnerText = s.ToString();
            xDoc.SelectSingleNode("Settings/Volume/music").InnerText = m.ToString();
            xDoc.SelectSingleNode("Settings/Volume/ui").InnerText = u.ToString();
            xDoc.SelectSingleNode("Settings/Volume/voice").InnerText = v.ToString();

            xDoc.Save(SettingsXML);
            ReadSettings();
        }

        public void WriteDefaultSettings()
        {
            //Write default settings
            XmlTextWriter xWriter = new XmlTextWriter("Settings.xml", Encoding.UTF8);
            xWriter.Formatting = Formatting.Indented;
            xWriter.WriteStartElement("Settings");

            xWriter.WriteStartElement("Volume");
            xWriter.WriteElementString("sfx", "0.5");
            xWriter.WriteElementString("music", "0.5");
            xWriter.WriteElementString("ui", "0.5");
            xWriter.WriteElementString("voice", "0.5");
            xWriter.WriteEndElement();//Volume
            xWriter.WriteEndElement();//Settings

            xWriter.Close();
        }
    }
}