using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKeyManager : MonsterManager
{
    private static MonsterKeyManager instance = null;
    public static MonsterKeyManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<MonsterKeyManager>();
            return instance;
        }
    }
    public List<ItemData> KeyData;
    public List<Color> KeyColor;
    private void Start()
    {
        base.Start();
        KeyColor.Add(new Color(255, 0, 0));
        KeyColor.Add(new Color(0, 255, 0));
        KeyColor.Add(new Color(255, 255, 255));
    }
    public override GameObject GetUnAtiveObject()
    {
        GameObject result = null;
        for (int i = 0; i < ObjectCount; i++)
        {
            if (Objects[i].gameObject.activeSelf == false)
                result =  Objects[i].gameObject;
        }
        result.gameObject.SetActive(true);
        int index = Random.Range(0, 2);
        if (index > 3)
            index = 0;
        result.GetComponent<SpriteRenderer>().color = KeyColor[index];
        result.GetComponent<Item>().itemData = KeyData[index];
        return result;
    }
}
