using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : BaseController
{
    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Monster;
    }
}
