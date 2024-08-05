using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class GameSceneData : MonoBehaviour
{
    /*
        Objects: 
            1. Goblin:
     
     */
    [Header("Monsters")]
    [SerializeField] GameObject goblinPerfab;

    public List<Goblin> goblinsList;
    private List<GoblinManager> goblins = new List<GoblinManager>();
    private void Awake()
    {
        // khởi tạo danh sách goblins
        goblinsList = PlayerPrefsExtra.GetList<Goblin>("goblinsList", new List<Goblin>());

        LoadMonsters();
    }

    public void SaveMonsters(List<GameObject> monsters, string name)
    {
        if (name == "Goblin")
        {
            goblinsList.Clear();
            foreach (var goblin in monsters)
            {
                Goblin data = new Goblin();
            }
        }
    }

    public void LoadMonsters()
    {
        foreach(Goblin goblin in goblinsList)
        {
            CreateGoblin(goblin);
        }
    }

    public void CreateGoblin(Goblin data)
    {
        GameObject goblinObject = Instantiate(goblinPerfab, data.POS, Quaternion.identity);
        GoblinManager goblin = goblinObject.GetComponent<GoblinManager>();
        goblin.id = data.id;
        goblin.POS = data.POS;
        goblin.health = data.health;
        goblins.Add(goblin);
    }

    public void RemoveGoblin(GoblinManager goblin)
    {
        goblins.Remove(goblin);
        goblinsList.Remove(goblinsList.Find(g => g.id == goblin.id));
    }

}
