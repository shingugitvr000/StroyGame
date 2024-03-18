using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatsData", menuName = "ScriptableObjects/StatsTableObject")]
public class StatsTableObject : ScriptableObject
{
    public Stats statsData; // Stats 클래스를 상속하는 Stats 스크립터블 오브젝트

}
