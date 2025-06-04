using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectDMGBuff", menuName = "Scriptable Objects/Perks/ObjectDMGBuff")]
public class ObjectDMGBuff : PerkEffect
{
    public float amount;

    public override void Apply(GameObject player)
    {
        PhotonView playerPV = player.GetComponent<PhotonView>();
        playerPV.RPC("SetObjectDMGMul", RpcTarget.All, amount);
    }

    public override IEnumerator Duration(GameObject player, GameObject perkTextInstance)
    {
        Debug.Log(perkName + " started countdown");
        yield return new WaitForSeconds(perkDuration);
        Debug.Log(perkName + " ran out of duration");

        PhotonView playerPV = player.GetComponent<PhotonView>();
        playerPV.RPC("SetObjectDMGMul", RpcTarget.All, 1f);

        Destroy(perkTextInstance);
    }
}

