using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace FourDoor.GameLogic
{
    public abstract class Transition : MonoBehaviour
    {
        public abstract Task Begin();
        public abstract Task End();
    }
}