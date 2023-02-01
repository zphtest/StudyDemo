using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DAG : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var moduleA = new Item("Module A");
        var moduleC = new Item("Module C", moduleA);
        var moduleB = new Item("Module B", moduleC);
        var moduleE = new Item("Module E", moduleB);
        var moduleD = new Item("Module D", moduleE);

        //moduleB.SetDependencies(moduleD);

        var unsorted = new[] { moduleA, moduleB, moduleC, moduleE, moduleD };
        var sorted = MySort<Item>(unsorted, x => x.Dependencies);
        foreach (var item in sorted)
        {
            Debug.LogWarning($"Item Name:{item.ToString()}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void Visit<T>(T Item, Func<T, IEnumerable<T>> getDependencies, List<T> sorted, Dictionary<T, bool> visited)
    {
        bool inProcess;
        var alreadVisited = visited.TryGetValue(Item, out inProcess);

        if (alreadVisited)
        {
            if (inProcess)
            {
                Debug.LogError($"Cyclic dependency found {Item.ToString()}");
            }
        }
        else
        {
            visited[Item] = true;

            var dependencies = getDependencies(Item);
            if (dependencies != null)
            {
                foreach (var denpendency in dependencies)
                {
                    Visit<T>(denpendency, getDependencies, sorted, visited);
                }
            }

            visited[Item] = false;
            sorted.Add(Item);
        }
    }


    public static IList<T> MySort<T>(IEnumerable<T> source, Func<T, IEnumerable<T>> getDependencies)
    {
        var sorted = new List<T>();
        var visited = new Dictionary<T, bool>();

        foreach (var item in source)
        {
            Visit<T>(item, getDependencies, sorted, visited);
        }

        return sorted;
    }

    
}

public class Item
{
    public string Name { get; private set; }

    public Item[] Dependencies { get; set; }

    public Item(string name, params Item[] dependencies)
    {
        Name = name;
        Dependencies = dependencies;
    }

    public void SetDependencies(params Item[] dependencies)
    {
        Dependencies = dependencies;
    }

    public override string ToString()
    {
        return Name;
    }
}
