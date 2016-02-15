using UnityEngine;

namespace com.terranovita.botretreat
{
    public interface IBotDependant
    {
        void UpdateBot(Bot bot);
        void Destroy();
        void SetVariableOffset(float offset);
        GameObject getGameObject();
    }
}