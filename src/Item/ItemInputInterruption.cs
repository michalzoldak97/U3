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

            if (!itemMaster.IsSelectedOnInventory)
                return;

            SceneManager.sceneLoaded += (Scene s, LoadSceneMode l) => inputProvider.CallEventInputInterrupted();
        }

        protected virtual void OnDisable()
        {
            if (!itemMaster.IsSelectedOnInventory)
                return;

            SceneManager.sceneLoaded -= (Scene s, LoadSceneMode l) => inputProvider.CallEventInputInterrupted();
        }
    }
}
