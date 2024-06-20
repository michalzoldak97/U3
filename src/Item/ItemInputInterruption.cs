using UnityEngine;
using UnityEngine.SceneManagement;

namespace U3.Item
{
    public class ItemInputInterruption : MonoBehaviour
    {
        private ItemMaster itemMaster;
        protected IItemInputProvider inputProvider;

        private void SetInit()
        {
            if (itemMaster == null)
                itemMaster = GetComponent<ItemMaster>();

            inputProvider ??= GetComponent<IItemInputProvider>();
        }

        protected virtual void OnEnable()
        {
            SetInit();

            SceneManager.sceneLoaded += OnSceneLoadInterruption;
        }

        protected virtual void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoadInterruption;
        }

        protected void CallItemInterruption()
        {
            if (!itemMaster.IsSelectedOnInventory)
                return;

            inputProvider.CallEventInputInterrupted();
        }

        private void OnSceneLoadInterruption(Scene s, LoadSceneMode l)
        {
            CallItemInterruption();
        }
    }
}
