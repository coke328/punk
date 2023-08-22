using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xmlManager : MonoBehaviour
{
    public TextAsset text;
    void Start()
    {
        LoadXml();
    }

    void LoadXml()
    {
        Debug.Log(text);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(text.text);
        XmlNodeList dialogNodes = xmlDoc.SelectNodes("ScenarioRoot/Group/Episode/Place/Dialog");

        foreach (XmlNode node in dialogNodes)
        {
            Debug.Log(node.InnerText);
            Debug.Log("Name :: " + node.SelectSingleNode("Teller").InnerText);
            Debug.Log("Level :: " + node.SelectSingleNode("Title").InnerText);
            Debug.Log("Exp :: " + node.SelectSingleNode("Content").InnerText);
        }
    }
}
