using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeShopController : MonoBehaviour
{
    public ResourceType type1;
    public ResourceType type2;
    [Tooltip("For wood log 0  for stone 1 for mud block 2 for cement block 4 and for iron rod 6")]
    public int Type1Int;
    [Tooltip("For wood log 0  for stone 1 for mud block 2 for cement block 4 and for iron rod 6")]
    public int Type2Int;
    public GameObject[] effects;
    public GameObject pnl;
    public GameObject btn;
    public int amount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowPnl()
    {
        pnl.SetActive(true);
    }
    
}
