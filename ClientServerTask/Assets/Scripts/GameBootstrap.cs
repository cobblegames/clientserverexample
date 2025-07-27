using UnityEngine;
using Unity.NetCode;

[UnityEngine.Scripting.Preserve]
public class GameBootstrap : ClientServerBootstrap
{
    public override bool Initialize(string defaultWorldName)
    {
        AutoConnectPort = 7979;
        Debug.Log("GameBootstrap Initialize called");
        return base.Initialize(defaultWorldName);
    }
}
