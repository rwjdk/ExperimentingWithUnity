using Shared;

namespace Managers
{
    public class DamageTextManager : Singleton<DamageTextManager>
    {
        public ObjectPooler Pooler { get; set; }

        private void Start()
        {
            Pooler = GetComponent<ObjectPooler>();
        }
    }
}