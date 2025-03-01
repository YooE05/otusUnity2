using UnityEngine;

namespace SampleGame
{
    public sealed class MenuViewInitializer : ViewInitializer
    {
        protected override void ActionsWithUIInstance(GameObject screenInstance)
        {
            DiContainer.Inject(screenInstance.GetComponent<MenuScreen>());
            screenInstance.SetActive(true);
        }
    }
}