using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDAG : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BundleInfo A = new BundleInfo("Bundle A");
        BundleInfo B = new BundleInfo("Bundle B", A);
        BundleInfo C = new BundleInfo("Bundle C", B);
        BundleInfo D = new BundleInfo("Bundle D", C);
        BundleInfo E = new BundleInfo("Bundle E", D);

        BundleInfo[] unSorted = new[] { A, C, B, E, D};
        var Sorted = Sort(unSorted, item => item.Dependencies);

        foreach (var item in Sorted)
        {
            Debug.Log($"bundle name {item.ToString()}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private static void Visit<T>(T bundle, List<T> sorted, Dictionary<T, bool> isVisited, Func<T, IEnumerable<T>> getDependencies)
    {
        bool isFind;
        var alreadyVisited = isVisited.TryGetValue(bundle, out isFind);

        if (alreadyVisited)
        {
            if (isFind)
            {
                Debug.LogError($"error circle find {bundle.ToString()}");
            }
        }
        else
        {
            isVisited[bundle] = true;
            var dependencies = getDependencies(bundle);
            if (dependencies != null)
            {
                foreach (var dependency in dependencies)
                {
                    Visit<T>(dependency, sorted, isVisited, getDependencies);
                }
            }
            isVisited[bundle] = false;
            sorted.Add(bundle);
        }
    }

    private static List<T> Sort<T>(IEnumerable<T> bundles, Func<T, IEnumerable<T>> getDependencies)
    {
        List<T> sorted = new List<T>();
        Dictionary<T, bool> alreadyVisited = new Dictionary<T, bool>();

        foreach (var bundle in bundles)
        {
            Visit<T>(bundle, sorted, alreadyVisited, getDependencies);
        }

        return sorted;
    }
}

public class BundleInfo
{
    public string Name { get; private set; }

    public BundleInfo[] Dependencies { get; set; }

    public BundleInfo(string name, params BundleInfo[] dependencies)
    {
        Name = name;
        Dependencies = dependencies;
    }

    public void SetDependencies(params BundleInfo[] bundleInfos)
    {
        Dependencies = bundleInfos;
    }

    public override string ToString()
    {
        return Name;
    }
}
