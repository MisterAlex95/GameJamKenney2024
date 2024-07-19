namespace Helpers
{
    public class DontDestroyOnLoad : UnityEngine.MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}