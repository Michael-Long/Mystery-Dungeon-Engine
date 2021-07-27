using Assets.Logic.Animation;
using Assets.ObjectTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameEngine.Entities
{
    public class Item : Entity
    {
        public override IEnumerator DoAction()
        {
            throw new System.NotImplementedException();
        }

        public override EntityType GetEntityType()
        {
            return EntityType.Item;
        }

        public override LegacyAnimationController GetAnimationController()
        {
            throw new System.NotImplementedException();
        }
    }
}