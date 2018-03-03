using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Text;
using System.Linq;

namespace RFOSSCore
{
    public class SaveIO
    {
        public class saveData
        {
            public string name;
            public double timecode;
            public int credits;
            public int xpMaterials;
            public int xpPropulsion;
            public int xpLifeSupport;
            public int xpElectronics;
            public int xpAstronomy;

            public saveData (string n, double t, int c, int m, int p, int l, int e, int a)
            {
                name = n;
                timecode = t;
                credits = c;
                xpMaterials = m;
                xpPropulsion = p;
                xpLifeSupport = l;
                xpElectronics = e;
                xpAstronomy = a;
            }
        }

        public saveData CurrentSaveData;

        public Dictionary<string, string> SavesList()
        {
            XmlDocument xDoc = new XmlDocument();

            Dictionary<string, string> foundSaves = new Dictionary<string, string>();
            string[] dirList = new string[0];

            //Make sure the saves directory exists. If it does not, create it.
            if (!Directory.Exists("Saves"))
            {
                Debug.Log("Saves directory not found, creating..");
                Directory.CreateDirectory("Saves");
            }

            //At this point, the directory should exist
            dirList = Directory.GetDirectories("Saves/");

            if (dirList.Length > 0)
            {
                foreach(string d in dirList)
                {
                    Debug.Log("Found " + d);
                    string path = d + "/save.xml";
                    string name;
                    if(File.Exists(path))
                    {
                        xDoc.Load(path);
                        name = xDoc.SelectSingleNode("Save/Name").InnerText;
                        foundSaves.Add(name, path);
                    }
                    else
                    {
                        Debug.Log("No valid save file found in " + d + ", skipping!");
                    }
                }
            }
            else
            {
                Debug.Log("No saves found!");
            }

            if(foundSaves.Count > 0)
            {
                return foundSaves;
            }
            else
            {
                return new Dictionary<string, string>();
            }
        }

        public void CreateNewSave(string name)
        {
            Directory.CreateDirectory("Saves/" + name);
            XmlTextWriter xWriter = new XmlTextWriter("Saves/" + name + "/save.xml", Encoding.UTF8);
            xWriter.Formatting = Formatting.Indented;

            xWriter.WriteStartElement("Save");
            xWriter.WriteElementString("Name", name);
            xWriter.WriteElementString("Timecode", "0");
            xWriter.WriteStartElement("Experience");
            xWriter.WriteElementString("Materials", "0");
            xWriter.WriteElementString("Propulsion", "0");
            xWriter.WriteElementString("LifeSupport", "0");
            xWriter.WriteElementString("Electronics", "0");
            xWriter.WriteElementString("Astronomy", "0");
            xWriter.WriteEndElement();//Experience
            xWriter.WriteElementString("Credits", "10000");
            xWriter.WriteEndElement();//Save
            xWriter.Close();
        }

        public void QuickSave(saveData data)
        {
            throw new System.NotImplementedException();
        }

        public void LoadSave(string name)
        {
            XmlDocument xDoc = new XmlDocument();
            string path = "Saves/" + name;

            string saveName;
            double saveTC;
            int saveCredits;
            int saveXpMaterials;
            int saveXpPropulsion;
            int saveXpLifeSupport;
            int saveXpElectronics;
            int saveXpAstronomy;

            if(Directory.Exists("Saves/" + name))
            {
                List<System.IO.FileInfo> sortedFiles = new DirectoryInfo(path).GetFiles().OrderByDescending(x => x.LastWriteTime).ToList();
                if(sortedFiles.Count > 0)
                {
                    xDoc.Load("Saves/" + name + "/" + sortedFiles[0].Name);

                    saveName = xDoc.SelectSingleNode("Save/Name").InnerText;
                    saveTC = Convert.ToDouble(xDoc.SelectSingleNode("Save/Timecode").InnerText);
                    saveCredits = Convert.ToInt32(xDoc.SelectSingleNode("Save/Credits").InnerText);
                    saveXpMaterials = Convert.ToInt32(xDoc.SelectSingleNode("Save/Experience/Materials").InnerText);
                    saveXpPropulsion = Convert.ToInt32(xDoc.SelectSingleNode("Save/Experience/Propulsion").InnerText);
                    saveXpLifeSupport = Convert.ToInt32(xDoc.SelectSingleNode("Save/Experience/LifeSupport").InnerText);
                    saveXpElectronics = Convert.ToInt32(xDoc.SelectSingleNode("Save/Experience/Electronics").InnerText);
                    saveXpAstronomy = Convert.ToInt32(xDoc.SelectSingleNode("Save/Experience/Astronomy").InnerText);

                    CurrentSaveData = new saveData(saveName, saveTC, saveCredits, saveXpMaterials, saveXpPropulsion, saveXpLifeSupport, saveXpElectronics, saveXpAstronomy);
                }
            }
            else
            {
                Debug.LogError("u bad hacker");
            }
        }

        public void DeleteSave(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}
