using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class XmlManager : MonoBehaviour
{
    XmlDocument XmlDoc;
    bool IsObject;
    private void Start()
    {
        XmlDoc = new XmlDocument();
    }
    public void LoadXml(string XmlName)
    {
        if (XmlName != null)
        {
            XmlDoc.Load(XmlName);
            Debug.Log("XmlName: " + XmlDoc.Name + " is succesfully Load!");
            StartSetting();
        }
        else
        {
            Debug.Log("Xml Load Fail!");
        }
    }
    private void StartSetting() //���� �ʱ�ȭ
    {
        XmlNode isEnabledNode = XmlDoc.SelectSingleNode("ScenarioRoot/ObjectType");
        IsObject = Convert.ToBoolean(isEnabledNode.InnerText);
    }
    public bool Return_Object_Type()
    {
        return IsObject;
    }
    public Dialog_inform Return_Dialog(int ID)
    {
        XmlNodeList DialogNodes = XmlDoc.SelectNodes("//Dialog");
        foreach (XmlNode node in DialogNodes)
        {
            if (ID == int.Parse(node.Attributes["ID"].Value)) //ID��ġ�ϸ� Ŭ������ ������ ��ȯ
            {
                string model = node.Attributes["Model"].Value;
                string position = node.Attributes["Position"].Value;
                string teller = node.SelectSingleNode("Teller").InnerText;
                string content = node.SelectSingleNode("Content").InnerText;
                return new Dialog_inform(ID, model, position, teller, content);
            }
        }
        return null; //ID ������ null��ȯ
    }
}

public class Dialog_inform //�ѹ��� ��ȭ�� �ʿ��� ������ Ŭ������ �����ϱ����� ���
{
    public int ID;
    public String Model;
    public String Position;
    public String Teller;
    public String Content;
    public Dialog_inform(int ID, string Model, string Position, string Teller, string Content)
    {
        this.ID = ID;
        this.Model = Model;
        this.Position = Position;
        this.Teller = Teller;
        this.Content = Content;
    }
}
