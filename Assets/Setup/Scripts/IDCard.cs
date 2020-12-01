using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Trisibo;
[CreateAssetMenu(fileName = "New ID", menuName = "IDCard", order = 50)]
[System.Serializable]
public class IDCard : ScriptableObject
{
    
    public Cluster cluster;
    [SerializeField] public string trio;
    public ChallengeHaptique haptiqueChal;
    public ChallengeInput inputChal;
    public int indexEnum;
  [SerializeField]  public SceneField microGameScene;
    
}


public enum Cluster { Theodore, Aurelien, Thibault};
public enum TrioTheodore { Brigantin, SpanishInquisition, TrapioWare, LeRafiot };
public enum TrioAurelien { ACommeAkuma, RadioRTL, DragonsPépères};
public enum TrioThibault { LLL, Soupe, Fleebos, SAS};


public enum ChallengeHaptique { A1, A2, A3, A4, A5, A6, A7,A8,A9,A10 }
public enum ChallengeInput { I1,I2,I3,I4,I5,I6,I7,I8,I9,I10}