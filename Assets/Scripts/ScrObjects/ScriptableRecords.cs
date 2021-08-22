using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Records List",menuName ="New Records List")]
public class ScriptableRecords : ScriptableObject
{
    public struct Record{
        public string date;
        public int score;
    }

    public List<Record> records=new List<Record>();
    // DateTime.Now.ToString("MM/dd/yyyy HH:mm"),

}
