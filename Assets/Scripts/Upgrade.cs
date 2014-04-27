using UnityEngine;
using System.Collections;

public class Upgrade
{
    public delegate void UpgradeMethod();

    string name;
    public string Name
    {
        get { return name; }
    }

    UpgradeMethod toInvoke;

    public Upgrade(string name, UpgradeMethod toInvoke)
    {
        this.name = name;
        this.toInvoke = toInvoke;
    }

    public void Invoke()
    {
        toInvoke();
    }
}
