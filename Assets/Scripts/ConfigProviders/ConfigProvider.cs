using UnityEngine;

namespace GunPrototype.ConfigProviders
{

    public abstract class ConfigProvider<T> : MonoBehaviour
    {
        public abstract T GetConfig();
    }
}